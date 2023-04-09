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
        public float MultipleBufferWaypointMinGap;
        public float MultipleBufferPlayDelay;

        private Dictionary<string, GameObject> dictTargetWaypointGO;
        private Dictionary<string, List<Vector3>> dictBufferedWaypoints;

        private string TargetWaypointNamePrefix = "Waypoint";
        private bool playingMultipleBuffer = false;
        private float lastPlayTime = 0;

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
            MultipleBufferWaypointMinGap = swarmManager.MultipleBufferWaypointMinGap;
            MultipleBufferPlayDelay = swarmManager.MultipleBufferPlayDelay;

            dictTargetWaypointGO = new Dictionary<string, GameObject>();
            dictBufferedWaypoints = new Dictionary<string, List<Vector3>>();
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

                            if(!dictBufferedWaypoints.ContainsKey(ROSBotID)){
                                dictBufferedWaypoints[ROSBotID] = new List<Vector3>();
                            }
                            // Add new point
                            int count = dictBufferedWaypoints[ROSBotID].Count;
                            if(count < 1){
                                dictBufferedWaypoints[ROSBotID].Add(waypointWorldSpace);
                            }else{
                                Vector3 lastPoint = dictBufferedWaypoints[ROSBotID][count-1];
                                float distance = Vector3.Distance(lastPoint, waypointWorldSpace);
                                if(distance >= MultipleBufferWaypointMinGap){
                                    dictBufferedWaypoints[ROSBotID].Add(waypointWorldSpace);
                                    //Debug.Log("Added point. Total points = " + dictBufferedWaypoints[ROSBotID].Count);

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
                Dictionary<string,List<Vector3>>.KeyCollection keys = dictBufferedWaypoints.Keys;
                if(keys.Count == 0){
                    // Finished playing
                    playingMultipleBuffer = false;
                }else{
                    // Still playing
                    float currentTime = Time.time;
                    if(currentTime - lastPlayTime > MultipleBufferPlayDelay){
                        lastPlayTime = currentTime;

                        foreach(string ROSBotID in keys){
                            // Safely get next point
                            Vector3 waypointWorldSpace = dictBufferedWaypoints[ROSBotID][0];
                            dictBufferedWaypoints[ROSBotID].RemoveAt(0);
                            if(dictBufferedWaypoints[ROSBotID].Count == 0){
                                dictBufferedWaypoints.Remove(ROSBotID);
                            }

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
                        }
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