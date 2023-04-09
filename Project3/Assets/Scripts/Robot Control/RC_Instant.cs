using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient{

    public class RC_Instant : MonoBehaviour
    {
        private SwarmManager swarmManager;

        private GameObject GO_ControllerRight;
        private float raycastDistance;
        private LayerMask LayerMask_Interaction;
        private LayerMask LayerMask_ROSBot;
        private LayerMask LayerMask_MatSafe;
        private GameObject Prefab_TargetWaypoint;

        private Dictionary<string, GameObject> dictTargetWaypointGO;

        private string TargetWaypointNamePrefix = "Waypoint";

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

            dictTargetWaypointGO = new Dictionary<string, GameObject>();
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
                bool holding_A = OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch) || swarmManager.hold_A;

                // Handle selecting target waypoints
                bool updateWaypoint = false;
                updateWaypoint |= isMatSafe && clicked_A && swarmManager.robotControlName=="Instant Click";
                updateWaypoint |= isMatSafe && holding_A && swarmManager.robotControlName=="Instant Hold";
                if(updateWaypoint){
                    if(swarmManager.selectTarget != null){
                        string ROSBotID = swarmManager.selectTarget.GetComponent<ROSBot>().ID;
                        Vector3 waypointGlobalSpace = hit.point;
                        Vector3 waypointWorldSpace = swarmManager.getWaypointWorldSpace(waypointGlobalSpace);
                        
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

                        swarmManager.dictTargetWaypoint[ROSBotID] = waypointWorldSpace;
                    }
                }
            }
        }

        void OnDestroy() {
            Dictionary<string, GameObject>.KeyCollection keys = dictTargetWaypointGO.Keys;
            foreach(string ROSBotID in keys){
                GameObject targetWaypointGO = dictTargetWaypointGO[ROSBotID];
                dictTargetWaypointGO.Remove(ROSBotID);
                Destroy(targetWaypointGO);
            }
        }
    }
}
