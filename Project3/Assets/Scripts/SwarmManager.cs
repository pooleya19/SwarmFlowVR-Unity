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
        public GameObject Prefab_Menu;
        public GameObject Prefab_WaypointPath;

        [Header("Layers")]
        public LayerMask LayerMask_Interaction;
        public LayerMask LayerMask_ROSBot;
        public LayerMask LayerMask_MatSafe;

        [Header("ROS Parameters")]
        public string topicSubscribed = "/rosbot01/pose";
        public string topicPublished = "/rosbot01/targetWaypoint";
        public bool enableROS = true;

        [Header("Buttons")]
        public GameObject[] GO_Buttons;
        public Color Color_Button_Unselected;
        public Color Color_Button_Highlighted;
        public Color Color_Button_Selected;
        private Button buttonSelected = null;
        private string playButtonName = "Play Buffer";

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
        private GameObject GO_menu;
        private Vector3 menuPosition = new Vector3(0.1f,0,0.25f);
        private Quaternion menuRotation = Quaternion.Euler(0,-45,-90);
        private Vector3 menuScale = 0.05f * Vector3.one;
        private LineRenderer lineRenderer;
        private GameObject highlightTarget = null;
        private GameObject highlightMarker;
        public GameObject selectTarget = null;
        private GameObject selectMarker;
        public float raycastDistance = 100;

        // Robots
        public SwarmInterface swarmInterface;
        private Dictionary<string, GameObject> dictROSBotGO;
        public Dictionary<string, Vector3> dictTargetWaypoint;
        public string robotControlName = "";
        public float MultipleBufferWaypointMinGap = 0.5f;
        public float MultipleBufferWaypointNextGap = 0.4f;
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

            // Init menu
            GO_menu = Instantiate(Prefab_Menu);
            updateMenuPosRot();
            GO_menu.transform.localScale = menuScale;
            GO_menu.transform.name = "Menu";
            MenuHandler menuHandler = GO_menu.GetComponent<MenuHandler>();
            GO_Buttons = menuHandler.GO_Buttons;
            menuHandler.outButtonClick = onButtonClick;

            // Update button colors
            SetRobotControl("");
        }

        // Update is called once per frame
        void Update()
        {
            updateMenuPosRot();
            updateROSBots();
            //updateDebugData();
            updateInteraction();
            updateWorldTransform();
        }

        private void updateMenuPosRot(){
            GO_menu.transform.localPosition = GO_ControllerLeft.transform.position + GO_ControllerLeft.transform.rotation * menuPosition;
            GO_menu.transform.localRotation = GO_ControllerLeft.transform.rotation * menuRotation;
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
                Transform hitTransform = hit.collider.transform;
                GameObject hitGO = hitTransform.gameObject;
                int hitLayer = hitGO.layer;

                bool isROSBot = layerIsInLayerMask(hitLayer, LayerMask_ROSBot);
                Button button = hitTransform.GetComponent<Button>();
                bool isButton = button != null;
                bool isMatSafe = layerIsInLayerMask(hitLayer, LayerMask_MatSafe);

                bool clicked_Trigger = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger,OVRInput.Controller.RTouch) || click_Trigger;
                click_Trigger = false;

                // Handle buttons
                if(clicked_Trigger && isButton){
                    button.onClick.Invoke();

                    string buttonText = button.transform.GetComponentInChildren<Text>().text;
                    if(buttonText != playButtonName){
                        buttonSelected = button;
                    }
                }
                // Handle button colors
                foreach(GameObject ButtonGO in GO_Buttons){
                    Button buttonComp = ButtonGO.GetComponent<Button>();
                    if(buttonComp == buttonSelected){
                        buttonComp.GetComponent<Image>().color = Color_Button_Selected;
                    }else{
                        buttonComp.GetComponent<Image>().color = Color_Button_Unselected;
                    }
                }
                if(isButton){
                    button.GetComponent<Image>().color = Color_Button_Highlighted;
                }

                // Handle selecting ROSBots
                if(clicked_Trigger){
                    // Handle new selection, updating a selection, removing a previous selection
                    if(isROSBot){
                        if(selectTarget == null){
                            // Handle new selection
                            selectTarget = hitGO;
                            selectMarker = Instantiate(Prefab_SelectMarker, hitTransform);
                        }else{
                            if(selectTarget != hitGO){
                                // Update a selection
                                selectTarget = hitGO;
                                selectMarker.transform.parent = hitTransform;
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
                        highlightMarker = Instantiate(Prefab_HighlightMarker, hitTransform);
                    }else{
                        if(highlightTarget != hitGO){
                            highlightTarget = hitGO;
                            highlightMarker.transform.parent = hitTransform;
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
        
        public void onButtonClick(string buttonName){
            if(buttonName == playButtonName){
                pressedPlay = true;
            }else{
                SetRobotControl(buttonName);
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

            // Delete previous robotControl
            if(robotControl != null){
                Destroy(robotControl);
            }

            // Create new robotControl
            if(targetNameRC == "Instant Click" || targetNameRC == "Instant Hold"){
                robotControl = (MonoBehaviour) gameObject.AddComponent<RC_Instant>();
            }else if(targetNameRC == "Single Buffer"){
                robotControl = (MonoBehaviour) gameObject.AddComponent<RC_SingleBuffer>();
            }else if(targetNameRC == "Multiple Buffer"){
                robotControl = (MonoBehaviour) gameObject.AddComponent<RC_MultipleBuffer>();
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

    }
}