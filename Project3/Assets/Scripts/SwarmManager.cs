using System.Collections;
using System.Collections.Generic;
using RosSharp.RosBridgeClient;
using UnityEngine;
using UnityEngine.UI;

namespace RosSharp.RosBridgeClient{

    public class SwarmManager : MonoBehaviour
    {
        [Header("GameObjects - OVR")]
        public GameObject GO_OVRPlayerController;
        public GameObject GO_OVRCameraRig;
        public GameObject GO_TrackingSpace;
        public GameObject GO_CenterEyeAnchor;
        public GameObject GO_ControllerLeft;
        public GameObject GO_ControllerRight;

        [Header("GameObjects - OVR")]
        public GameObject GO_Marker;
        public GameObject GO_World;

        [Header("Prefabs")]
        public GameObject Prefab_ROSBot;
        public GameObject Prefab_SelectMarker;
        public GameObject Prefab_HighlightMarker;
        public GameObject Prefab_TargetWaypoint;

        [Header("Layers")]
        public LayerMask LayerMask_Interaction;
        public LayerMask LayerMask_ROSBot;
        public LayerMask LayerMask_MatSafe;

        [Header("ROS Parameters")]
        public string topicSubscribed = "/rosbot01/pose";
        public string topicPublished = "/rosbot01/targetWaypoint";
        public bool enableROS = true;

        [Header("Buttons")]
        public GameObject[] GO_RC_Buttons;
        public Color Color_Button_Unselected;
        public Color Color_Button_Selected;

        [Header("Misc")]
        public Material MAT_line;
        public Text Text_DebugData;
        public string ROSBotNamePrefix = "Rosbot";
        public string TargetWaypointNamePrefix = "Waypoint";
        public float ROSBotFloorYOffset = 0;

        [Header("Unity UI Input")]
        public bool click_Trigger = false;
        public bool click_A = false;
        public bool hold_A = false;

        private Vector3 targetWaypoint;
        private CustomSubscriber sub;
        private CustomPublisher pub;

        // World transform variables
        private bool editingWorld = false;
        private Vector3 saveContLeftPos;
        private Vector3 saveContRightPos;
        private Vector3 saveWorldPos;
        private Quaternion saveWorldRot;
        private Vector3 saveWorldScale;

        // Interaction
        private LineRenderer lineRenderer;
        private int lineColorNum = 0;
        private GameObject highlightTarget = null;
        private GameObject highlightMarker;
        public GameObject selectTarget = null;
        private GameObject selectMarker;
        public float raycastDistance = 100;

        // Robots
        private SwarmInterface swarmInterface;
        private Dictionary<string, GameObject> dictROSBotGO;
        public Dictionary<string, Vector3> dictTargetWaypoint;
        public string robotControlName = "";
        public float MultipleBufferWaypointMinGap = 0.5f;
        public float MultipleBufferPlayDelay = 1;
        public bool pressedPlay = false;

        private MonoBehaviour robotControl = null;

        // Start is called before the first frame update
        void Start(){
            targetWaypoint = new Vector3(0,0,0);

            dictROSBotGO = new Dictionary<string, GameObject>();
            dictTargetWaypoint = new Dictionary<string, Vector3>();

            if(enableROS){
                swarmInterface = GetComponent<SwarmROSBridge>();
            }else{
                swarmInterface = GetComponent<SwarmSimulator>();
            }

            lineRenderer = GO_ControllerRight.AddComponent<LineRenderer>();
            lineRenderer.SetPosition(0, new Vector3(0,0,0));
            lineRenderer.useWorldSpace = false;
            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;
            lineRenderer.material = MAT_line;

            // Update button colors
            SetRobotControl("");
        }

        // Update is called once per frame
        void Update()
        {
            updateROSBots();
            updateDebugData();
            updateInteraction();
            updateWorldTransform();
        }

        public void CycleLineColor(){
            lineColorNum++;
            if(lineColorNum == 1){
                MAT_line.color = Color.red;
            }else if(lineColorNum == 2){
                MAT_line.color = Color.green;
            }else if(lineColorNum == 3){
                lineColorNum = 0;
                MAT_line.color = Color.blue;
            }
            lineRenderer.material = MAT_line;
        }

        private void updateROSBots(){
            // Parse through ROSBots using swarmInterface
            foreach(string ROSBotID in swarmInterface.getROSBotIDs()){
                // Handle position/rotation
                Vector3 position = swarmInterface.getPosition(ROSBotID);
                Quaternion rotation = swarmInterface.getRotation(ROSBotID);

                // Check for unset position and skip
                if(position == Vector3.positiveInfinity) continue;

                GameObject ROSBot;
                if(!dictROSBotGO.ContainsKey(ROSBotID)){
                    // ROSBot doesn't exist, create it!
                    ROSBot = Instantiate(Prefab_ROSBot, GO_World.transform);
                    ROSBot.transform.localPosition = position;
                    ROSBot.transform.localRotation = rotation;
                    ROSBot.transform.name = ROSBotNamePrefix + ROSBotID;
                    ROSBot.GetComponent<ROSBot>().ID = ROSBotID;
                    dictROSBotGO[ROSBotID] = ROSBot;
                }else{
                    ROSBot = dictROSBotGO[ROSBotID];
                }

                Vector3 flooredPos = position;
                flooredPos.y = ROSBotFloorYOffset;
                ROSBot.transform.localPosition = flooredPos;
                ROSBot.transform.localRotation = rotation;

                // Handle target waypoints
                Vector3 targetWaypoint;
                if(!dictTargetWaypoint.ContainsKey(ROSBotID)){
                    // Set default target waypoint to starting position
                    targetWaypoint = position;
                    dictTargetWaypoint[ROSBotID] = targetWaypoint;
                }else{
                    targetWaypoint = dictTargetWaypoint[ROSBotID];
                }
                swarmInterface.setTargetWaypoint(ROSBotID, targetWaypoint);
            }
            //Debug.Log("ROSBot03 target waypoint = ("+dictTargetWaypoint["03"].x+","+dictTargetWaypoint["03"].y+","+dictTargetWaypoint["03"].z+").");
        }

        private void updateDebugData(){
            // Update Debug Data
            Vector3 OVR_PC_Pos = GO_OVRPlayerController.transform.position;
            Vector3 OVR_CR_Pos = GO_OVRCameraRig.transform.position;
            Vector3 OVR_TS_Pos = GO_TrackingSpace.transform.position;
            Vector3 OVR_CEA_Pos = GO_CenterEyeAnchor.transform.position;

            string debugData = "";
            debugData += "OVR_PC: (" + OVR_PC_Pos.x + ", " + OVR_PC_Pos.y + ", " + OVR_PC_Pos.z + ")\n";
            debugData += "OVR_CR: (" + OVR_CR_Pos.x + ", " + OVR_CR_Pos.y + ", " + OVR_CR_Pos.z + ")\n";
            debugData += "OVR_TS: (" + OVR_TS_Pos.x + ", " + OVR_TS_Pos.y + ", " + OVR_TS_Pos.z + ")\n";
            debugData += "OVR_CEA: (" + OVR_CEA_Pos.x + ", " + OVR_CEA_Pos.y + ", " + OVR_CEA_Pos.z + ")\n";
            Text_DebugData.text = debugData;
        }

        private void updateInteraction(){
            // Find physics raycast result
            Ray ray = new Ray(GO_ControllerRight.transform.position, GO_ControllerRight.transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, raycastDistance, LayerMask_Interaction)){
                lineRenderer.SetPosition(1, new Vector3(0,0,hit.distance));

                // Define helpful variables
                GameObject hitGO = hit.transform.gameObject;
                int hitLayer = hitGO.layer;

                bool isROSBot = layerIsInLayerMask(hitLayer, LayerMask_ROSBot);
                Button button = hit.transform.GetComponent<Button>();
                bool isButton = button != null;
                bool isMatSafe = layerIsInLayerMask(hitLayer, LayerMask_MatSafe);

                bool clicked_Trigger = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger,OVRInput.Controller.RTouch) || click_Trigger;
                click_Trigger = false;

                // Handle buttons
                if(clicked_Trigger && isButton){
                    button.onClick.Invoke();
                }

                // Handle selecting ROSBots
                if(clicked_Trigger){
                    // Handle new selection, updating a selection, removing a previous selection
                    if(isROSBot){
                        if(selectTarget == null){
                            // Handle new selection
                            selectTarget = hitGO;
                            selectMarker = Instantiate(Prefab_SelectMarker, hitGO.transform);
                        }else{
                            if(selectTarget != hitGO){
                                // Update a selection
                                selectTarget = hitGO;
                                selectMarker.transform.parent = hitGO.transform;
                                selectMarker.transform.localPosition = Vector3.zero;
                            }else{
                                // Remove a previous selection
                                selectTarget = null;
                                Destroy(selectMarker);
                            }
                        }
                    }
                    // Handle clicking on floor to remove a previous selection
                    if(isMatSafe && selectTarget != null){
                        selectTarget = null;
                        Destroy(selectMarker);
                    }
                }

                // Handle highlighting ROSBots
                if(isROSBot){
                    // Handle new highlight, updating a highlight
                    if(highlightTarget == null){
                        highlightTarget = hitGO;
                        highlightMarker = Instantiate(Prefab_HighlightMarker, hitGO.transform);
                    }else{
                        if(highlightTarget != hitGO){
                            highlightTarget = hitGO;
                            highlightMarker.transform.parent = hitGO.transform;
                            highlightMarker.transform.localPosition = Vector3.zero;
                        }
                    }
                }else{
                    // Handle removing a highlight
                    if(highlightTarget != null){
                        highlightTarget = null;
                        Destroy(highlightMarker);
                    }
                }

            }else{
                lineRenderer.SetPosition(1, new Vector3(0,0,0));

                // Handle removing highlight
                if(highlightTarget != null){
                    highlightTarget = null;
                    Destroy(highlightMarker);
                }
            }
        }

        private void updateWorldTransform(){
            // Update world transform
            bool getLHandTrigger = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
            bool getRHandTrigger = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);
            if(getLHandTrigger && getRHandTrigger){
                if(!editingWorld){
                    // Start editing the world
                    editingWorld = true;
                    saveContLeftPos = GO_ControllerLeft.transform.position;
                    saveContRightPos = GO_ControllerRight.transform.position;
                    saveWorldPos = GO_World.transform.position;
                    saveWorldRot = GO_World.transform.rotation;
                    saveWorldScale = GO_World.transform.localScale;
                }else{
                    // Continue editing the world
                    Vector3 oldHandDiff = saveContRightPos - saveContLeftPos;
                    Vector3 oldHandDiffXZ = new Vector3(oldHandDiff.x, 0, oldHandDiff.z);
                    Vector3 newHandDiff = GO_ControllerRight.transform.position - GO_ControllerLeft.transform.position;
                    Vector3 newHandDiffXZ = new Vector3(newHandDiff.x, 0, newHandDiff.z);

                    float scaleRatio = newHandDiffXZ.magnitude / oldHandDiffXZ.magnitude;
                    Quaternion newRotation = Quaternion.FromToRotation(oldHandDiffXZ, newHandDiffXZ);

                    Vector3 oldContCenterPos = (saveContLeftPos + saveContRightPos)/2;
                    Vector3 newContCenterPos = (GO_ControllerLeft.transform.position + GO_ControllerRight.transform.position)/2;
                    Vector3 oldWorldOffset = saveWorldPos - oldContCenterPos;

                    GO_World.transform.position = newContCenterPos + (newRotation * oldWorldOffset * scaleRatio);
                    GO_World.transform.rotation = saveWorldRot * newRotation;
                    GO_World.transform.localScale = saveWorldScale.x * scaleRatio * Vector3.one;
                }
            }else{
                if(editingWorld){
                    // Stop editing the world
                    editingWorld = false;
                }else{
                    // Continue not editing the world
                }
            }
        }
        
        public void SetRobotControl(string targetNameRC){
            string prevRCName = robotControlName;
            robotControlName = targetNameRC;

            // Handle old waypoints
            bool keepWaypoints = false;
            keepWaypoints |= prevRCName==targetNameRC;
            keepWaypoints |= (prevRCName=="Instant Click" && targetNameRC=="Instant Hold");
            keepWaypoints |= (prevRCName=="Instant Hold" && targetNameRC=="Instant Click");

            if(!keepWaypoints){
                dictTargetWaypoint.Clear();
            }

            // Update button colors
            foreach(GameObject GO_RC_Button in GO_RC_Buttons){
                string buttonNameRC = GO_RC_Button.GetComponentInChildren<Text>().text;
                //Debug.Log("Comparing "+buttonNameRC+" and "+targetNameRC+".");
                if(buttonNameRC == targetNameRC){
                    GO_RC_Button.GetComponent<Image>().color = Color_Button_Selected;
                }else{
                    GO_RC_Button.GetComponent<Image>().color = Color_Button_Unselected;
                }
            }

            // Delete previous robotControl
            if(robotControl != null){
                Destroy(robotControl);
            }

            // Create new robotControl
            if(targetNameRC == "Instant Click" || targetNameRC == "Instant Hold"){
                robotControl = (MonoBehaviour) gameObject.AddComponent<RC_Instant>();
            }

        }

        public bool layerIsInLayerMask(int layer, LayerMask layerMask){
            return ((layerMask >> layer) & 1) == 1;
        }
    
        public Vector3 getWaypointWorldSpace(Vector3 waypointGlobalSpace){
            Vector3 worldPos = GO_World.transform.position;
            Quaternion worldRot = GO_World.transform.rotation;
            float worldScale = GO_World.transform.localScale.x;
            Vector3 waypointWorldSpace =  Quaternion.Inverse(worldRot) * (1/worldScale * new Vector3(waypointGlobalSpace.x - worldPos.x, 0, waypointGlobalSpace.z - worldPos.z));
            return waypointWorldSpace;
        }

        public void PlayBuffer(){
            pressedPlay = true;
        }
    }
}