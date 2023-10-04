using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient{

    public class RC_MultipleBuffer : MonoBehaviour
    {
        private SwarmManager swarmManager;

        private GameObject GO_ControllerRight;
        private float raycastDistance;
        private LayerMask LayerMask_Interaction;
        private LayerMask LayerMask_ROSBot;
        private LayerMask LayerMask_MatSafe;
        private GameObject Prefab_TargetWaypoint;
        private GameObject Prefab_WaypointPath;
        private float MultipleBufferWaypointMinGap;
        private float MultipleBufferWaypointNextGap;

        private Dictionary<string, GameObject> dictTargetWaypointGO;
        private Dictionary<string, List<Vector3>> dictBufferedWaypoints;
        private Dictionary<string, List<GameObject>> dictWaypointPathGOs;
        private Dictionary<string, GameObject> dictCurrentPathGO;

        private string TargetWaypointNamePrefix = "Waypoint";
        private string WaypointPathNamePrefix = "Path";
        private bool playingMultipleBuffer = false;
        private float lastPlayTime = 0;
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
            MultipleBufferWaypointMinGap = swarmManager.MultipleBufferWaypointMinGap;
            MultipleBufferWaypointNextGap = swarmManager.MultipleBufferWaypointNextGap;

            dictTargetWaypointGO = new Dictionary<string, GameObject>();
            dictBufferedWaypoints = new Dictionary<string, List<Vector3>>();
            dictWaypointPathGOs = new Dictionary<string, List<GameObject>>();
            dictCurrentPathGO = new Dictionary<string, GameObject>();
        }

        void Update(){
            // Stop new interaction if currently playing buffer
            if(!playingMultipleBuffer){
                // Find physics raycast result
                Ray ray = new Ray(GO_ControllerRight.transform.position, GO_ControllerRight.transform.forward);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, raycastDistance, LayerMask_Interaction)){
                    // Define helpful variables
                    GameObject hitGO = hit.transform.gameObject;
                    int hitLayer = hitGO.layer;

                    bool isROSBot = swarmManager.layerIsInLayerMask(hitLayer, LayerMask_ROSBot);
                    bool isMatSafe = swarmManager.layerIsInLayerMask(hitLayer, LayerMask_MatSafe);

                    bool holding_A = OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch) || swarmManager.hold_A;

                    // Handle selecting target waypoints
                    bool updateWaypoint = isMatSafe && holding_A && swarmManager.robotControlName=="Multiple Buffer";
                    if(updateWaypoint){
                        if(swarmManager.selectTarget != null){
                            string ROSBotID = swarmManager.selectTarget.GetComponent<ROSBot>().ID;
                            Vector3 waypointGlobalSpace = hit.point;
                            Vector3 waypointWorldSpace = swarmManager.getWaypointWorldSpace(waypointGlobalSpace);

                            // Add new point
                            if(!dictBufferedWaypoints.ContainsKey(ROSBotID)){
                                dictBufferedWaypoints[ROSBotID] = new List<Vector3>();
                            }
                            int countWaypoint = dictBufferedWaypoints[ROSBotID].Count;
                            bool addedWaypoint = false;
                            if(countWaypoint < 1){
                                dictBufferedWaypoints[ROSBotID].Add(waypointWorldSpace);
                                addedWaypoint = true;
                            }else{
                                Vector3 lastPoint = dictBufferedWaypoints[ROSBotID][countWaypoint-1];
                                float distance = Vector3.Distance(lastPoint, waypointWorldSpace);
                                if(distance >= MultipleBufferWaypointMinGap){
                                    dictBufferedWaypoints[ROSBotID].Add(waypointWorldSpace);
                                    //Debug.Log("Added point. Total points = " + dictBufferedWaypoints[ROSBotID].Count);
                                    addedWaypoint = true;
                                }
                            }
                            // Update targetWaypointGO
                            if(addedWaypoint){
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
                            // Add new waypointPathGO
                            if(addedWaypoint){
                                if(!dictWaypointPathGOs.ContainsKey(ROSBotID)){
                                    dictWaypointPathGOs[ROSBotID] = new List<GameObject>();
                                }
                                int countPaths = dictWaypointPathGOs[ROSBotID].Count;
                                Vector3 fromPoint;
                                if(countPaths < 1){
                                    fromPoint = swarmManager.swarmInterface.getPosition(ROSBotID);
                                }else{
                                    int numWaypoints = dictBufferedWaypoints[ROSBotID].Count;
                                    fromPoint = dictBufferedWaypoints[ROSBotID][numWaypoints-2];
                                }
                                Vector3 posDiff = waypointWorldSpace - fromPoint;
                                GameObject waypointPathGO = Instantiate(Prefab_WaypointPath, swarmManager.GO_World.transform);
                                waypointPathGO.transform.name = WaypointPathNamePrefix + ROSBotID + "_" + countPaths;
                                waypointPathGO.transform.localPosition = fromPoint;
                                waypointPathGO.transform.localRotation = Quaternion.FromToRotation(Vector3.forward, posDiff.normalized);
                                waypointPathGO.transform.localScale = new Vector3(1, 1, posDiff.magnitude);
                                
                                dictWaypointPathGOs[ROSBotID].Add(waypointPathGO);
                            }
                        }
                    }
                }
            }

            // Handle play button press
            if(swarmManager.pressedPlay){
                swarmManager.pressedPlay = false;
                playingMultipleBuffer = true;
            }

            // Handle playing buffer
            if(playingMultipleBuffer){
                if(dictBufferedWaypoints.Keys.Count == 0){
                    // Finished playing
                    playingMultipleBuffer = false;
                }else{
                    // Still playing, iterate through each ROSBot
                    List<string> ROSBotIDsToRemove = new List<string>();
                    foreach(string ROSBotID in dictBufferedWaypoints.Keys){
                        if(dictBufferedWaypoints[ROSBotID].Count == 0){
                            // No more buffered points, remove list...
                            ROSBotIDsToRemove.Add(ROSBotID);
                        }else{
                            // Buffered points remaining
                            Vector3 waypointWorldSpace = dictBufferedWaypoints[ROSBotID][0];
                            Vector3 ROSBotPos = swarmManager.swarmInterface.getPosition(ROSBotID);
                            Vector3 posDiff = waypointWorldSpace - ROSBotPos;
                            // Check distance to current waypoint
                            if(posDiff.magnitude > MultipleBufferWaypointNextGap){
                                // Far away, it is the current goal

                                // Update targetWaypoint
                                swarmManager.dictTargetWaypoint[ROSBotID] = waypointWorldSpace;

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

                            }else{
                                // Very close, move on to next point
                                
                                // Remove current target
                                dictBufferedWaypoints[ROSBotID].RemoveAt(0);
                            }

                            // Delete necessary paths
                            if(dictWaypointPathGOs[ROSBotID].Count >= dictBufferedWaypoints[ROSBotID].Count){
                                Destroy(dictWaypointPathGOs[ROSBotID][0]);
                                dictWaypointPathGOs[ROSBotID].RemoveAt(0);
                            }

                        }
                    }
                    foreach(string ROSBotIDToRemove in ROSBotIDsToRemove){
                        dictBufferedWaypoints.Remove(ROSBotIDToRemove);
                    }
                }
            }

            // Handle currentPathGO
            foreach(string ROSBotID in swarmManager.dictTargetWaypoint.Keys){
                Vector3 ROSBotPos = swarmManager.swarmInterface.getPosition(ROSBotID);
                Vector3 waypointWorldSpace = swarmManager.dictTargetWaypoint[ROSBotID];

                Vector3 posDiff = waypointWorldSpace - ROSBotPos;
                GameObject currentPathGO;
                if(dictCurrentPathGO.ContainsKey(ROSBotID)){
                    currentPathGO = dictCurrentPathGO[ROSBotID];
                }else{
                    currentPathGO = Instantiate(Prefab_WaypointPath, swarmManager.GO_World.transform);
                    currentPathGO.transform.name = WaypointPathNamePrefix + ROSBotID;
                    dictCurrentPathGO[ROSBotID] = currentPathGO;
                }
                currentPathGO.transform.localPosition = ROSBotPos;
                currentPathGO.transform.localRotation = Quaternion.FromToRotation(Vector3.forward, posDiff.normalized);
                currentPathGO.transform.localScale = new Vector3(1, 1, posDiff.magnitude);
            }

        }

        void OnDestroy() {
            // Destroy targetWaypointGO
            foreach(string ROSBotID in dictTargetWaypointGO.Keys){
                Destroy(dictTargetWaypointGO[ROSBotID]);
            }
            // Destory waypointPathGOs
            foreach(string ROSBotID in dictWaypointPathGOs.Keys){
                for(int i=0; i<dictWaypointPathGOs[ROSBotID].Count; i++){
                    Destroy(dictWaypointPathGOs[ROSBotID][i]);
                }
            }
            // Destroy currentPathGO
            foreach(string ROSBotID in dictCurrentPathGO.Keys){
                Destroy(dictCurrentPathGO[ROSBotID]);
            }
        }
    }
}