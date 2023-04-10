using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient{

    public class RC_SingleBuffer : MonoBehaviour
    {
        private SwarmManager swarmManager;

        private GameObject GO_ControllerRight;
        private float raycastDistance;
        private LayerMask LayerMask_Interaction;
        private LayerMask LayerMask_ROSBot;
        private LayerMask LayerMask_MatSafe;
        private GameObject Prefab_TargetWaypoint;
        private GameObject Prefab_WaypointPath;

        private Dictionary<string, GameObject> dictTargetWaypointGO;
        private Dictionary<string, GameObject> dictWaypointPathGO;

        private string TargetWaypointNamePrefix = "Waypoint";
        private string WaypointPathNamePrefix = "Path";
        private float waypointPathMinDistance = 0.1f;

        void Start(){
            // Get SwarmManager
            swarmManager = GetComponent<SwarmManager>();
            
            // Get variables from SwarmManager
            GO_ControllerRight = swarmManager.GO_ControllerRight;
            raycastDistance = swarmManager.raycastDistance;
            LayerMask_Interaction = swarmManager.LayerMask_Interaction;
            LayerMask_ROSBot = swarmManager.LayerMask_ROSBot;
            LayerMask_MatSafe = swarmManager.LayerMask_MatSafe;
            Prefab_TargetWaypoint = swarmManager.Prefab_TargetWaypoint;
            Prefab_WaypointPath = swarmManager.Prefab_WaypointPath;

            dictTargetWaypointGO = new Dictionary<string, GameObject>();
            dictWaypointPathGO = new Dictionary<string, GameObject>();
        }

        void Update(){
            // Find physics raycast result
            Ray ray = new Ray(GO_ControllerRight.transform.position, GO_ControllerRight.transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, raycastDistance, LayerMask_Interaction)){
                // Define helpful variables
                GameObject hitGO = hit.transform.gameObject;
                int hitLayer = hitGO.layer;

                bool isROSBot = swarmManager.layerIsInLayerMask(hitLayer, LayerMask_ROSBot);
                bool isMatSafe = swarmManager.layerIsInLayerMask(hitLayer, LayerMask_MatSafe);

                bool clicked_A = OVRInput.GetDown(OVRInput.Button.One,OVRInput.Controller.RTouch) || swarmManager.click_A;
                swarmManager.click_A = false;

                // Handle selecting target waypoints
                bool updateWaypoint = isMatSafe && clicked_A && swarmManager.robotControlName=="Single Buffer";
                if(updateWaypoint){
                    if(swarmManager.selectTarget != null){
                        string ROSBotID = swarmManager.selectTarget.GetComponent<ROSBot>().ID;
                        Vector3 waypointGlobalSpace = hit.point;
                        Vector3 waypointWorldSpace = swarmManager.getWaypointWorldSpace(waypointGlobalSpace);
                        
                        // Update targetWaypointGO
                        GameObject targetWaypointGO;
                        if(dictTargetWaypointGO.ContainsKey(ROSBotID)){
                            targetWaypointGO = dictTargetWaypointGO[ROSBotID];
                        }else{
                            targetWaypointGO = Instantiate(Prefab_TargetWaypoint, swarmManager.GO_World.transform);
                            targetWaypointGO.transform.localPosition = waypointWorldSpace;
                            targetWaypointGO.transform.name = TargetWaypointNamePrefix + ROSBotID;
                            dictTargetWaypointGO[ROSBotID] = targetWaypointGO;
                        }
                        targetWaypointGO.transform.localPosition = waypointWorldSpace;
                    }
                }
            }

            // Handle play button press
            if(swarmManager.pressedPlay){
                swarmManager.pressedPlay = false;
                playBuffer();
            }

            // Update waypointPathGO
            foreach(string ROSBotID in dictTargetWaypointGO.Keys){
                Vector3 waypointWorldSpace = dictTargetWaypointGO[ROSBotID].transform.localPosition;
                Vector3 ROSBotPos = swarmManager.swarmInterface.getPosition(ROSBotID);
                Vector3 posDiff = waypointWorldSpace - ROSBotPos;
                if(posDiff.magnitude >= waypointPathMinDistance){
                    GameObject waypointPathGO;
                    if(dictWaypointPathGO.ContainsKey(ROSBotID)){
                        waypointPathGO = dictWaypointPathGO[ROSBotID];
                    }else{
                        waypointPathGO = Instantiate(Prefab_WaypointPath, swarmManager.GO_World.transform);
                        waypointPathGO.transform.name = WaypointPathNamePrefix + ROSBotID;
                        dictWaypointPathGO[ROSBotID] = waypointPathGO;
                    }
                    waypointPathGO.transform.localPosition = ROSBotPos;
                    waypointPathGO.transform.localRotation = Quaternion.FromToRotation(Vector3.forward, posDiff.normalized);
                    waypointPathGO.transform.localScale = new Vector3(1, 1, posDiff.magnitude);
                }else{
                    // Delete path
                    if(dictWaypointPathGO.ContainsKey(ROSBotID)){
                        Destroy(dictWaypointPathGO[ROSBotID]);
                        dictWaypointPathGO.Remove(ROSBotID);
                    }
                }
            }
        }

        private void playBuffer(){
            Dictionary<string,GameObject>.KeyCollection keys = dictTargetWaypointGO.Keys;
            foreach(string ROSBotID in keys){
                Vector3 waypointWorldSpace = dictTargetWaypointGO[ROSBotID].transform.localPosition;
                swarmManager.dictTargetWaypoint[ROSBotID] = waypointWorldSpace;
            }
        }

        void OnDestroy() {
            foreach(string ROSBotID in dictTargetWaypointGO.Keys){
                Destroy(dictTargetWaypointGO[ROSBotID]);
            }
            foreach(string ROSBotID in dictWaypointPathGO.Keys){
                Destroy(dictWaypointPathGO[ROSBotID]);
            }
        }
    }

}