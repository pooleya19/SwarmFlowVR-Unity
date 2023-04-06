using System.Collections;
using System.Collections.Generic;
using RosSharp.RosBridgeClient;
using UnityEngine;
using UnityEngine.UI;

namespace RosSharp.RosBridgeClient{

    public class SwarmManager : MonoBehaviour
    {
        [Header("GameObjects")]
        public GameObject GO_OVRPlayerController;
        public GameObject GO_OVRCameraRig;
        public GameObject GO_TrackingSpace;
        public GameObject GO_CenterEyeAnchor;
        public GameObject GO_ControllerLeft;
        public GameObject GO_ControllerRight;
        public GameObject GO_ROSBot;
        public GameObject GO_Marker;
        public GameObject GO_World;

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
        public LayerMask UILayerMask;
        public Text Text_DebugData;

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

        private LineRenderer lineRenderer;
        private int lineColorNum = 0;
        public bool click = false;

        private string robotControlName = "";

        // Start is called before the first frame update
        void Start()
        {
            targetWaypoint = new Vector3(0,0,0);

            if(enableROS){
                sub = gameObject.AddComponent<CustomSubscriber>();
                sub.Init<MessageTypes.Geometry.PoseStamped>(topicSubscribed);
                pub = gameObject.AddComponent<CustomPublisher>();
                pub.Init<MessageTypes.Geometry.Vector3>(topicPublished,10);
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

            // Update line renderers
            Ray ray = new Ray(GO_ControllerRight.transform.position, GO_ControllerRight.transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100, UILayerMask)){
                lineRenderer.SetPosition(1, new Vector3(0,0,hit.distance));
                //Debug.Log("Raycast hit: " + hit.transform.name);
                if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger,OVRInput.Controller.RTouch) || click){
                    click = false;
                    Button button = hit.transform.GetComponent<Button>();
                    if(button){
                        button.onClick.Invoke();
                        Debug.Log("Clicked!");
                    }
                }
            }else{
                lineRenderer.SetPosition(1, new Vector3(0,0,1));
                //Debug.Log("No raycast hit");
            }


            // Update world transform
            bool getLHandTrigger = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
            bool getRHandTrigger = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
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

            // Update marker position
            if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)){
                Vector3 contPos = GO_ControllerRight.transform.position;
                GO_Marker.transform.position = new Vector3(contPos.x, 0, contPos.z);
            }

            // Update target waypoint
            Vector3 markPos = GO_Marker.transform.position;
            targetWaypoint = new Vector3(markPos.x, markPos.z, 0);
            if(enableROS) pub.message = ROSHelper.getGeometryVector3(targetWaypoint);

            // Get recent pose data
            if(enableROS){
                MessageTypes.Geometry.PoseStamped pose_Rosbot01 = (MessageTypes.Geometry.PoseStamped) sub.receivedValue;
                if(pose_Rosbot01 != null){
                    MessageTypes.Geometry.Point pos = pose_Rosbot01.pose.position;
                    MessageTypes.Geometry.Quaternion rot = pose_Rosbot01.pose.orientation;
                    Vector3 position_Rosbot01 = new Vector3((float)pos.x, (float)pos.z, (float)pos.y);
                    Quaternion rotation_Rosbot01 = new Quaternion(-(float)rot.x, -(float)rot.z, -(float)rot.y, (float)rot.w);
                    GO_ROSBot.transform.position = position_Rosbot01;
                    GO_ROSBot.transform.rotation = rotation_Rosbot01;
                }
            }
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

        public void SetRobotControl(string targetNameRC){
            if(targetNameRC != ""){
                robotControlName = targetNameRC;
                Debug.Log("Set robot control name to " + robotControlName + ".");
            }

            // Update button colors
            foreach(GameObject GO_RC_Button in GO_RC_Buttons){
                string buttonNameRC = GO_RC_Button.GetComponentInChildren<Text>().text;
                if(buttonNameRC == targetNameRC){
                    GO_RC_Button.GetComponent<Image>().color = Color_Button_Selected;
                }else{
                    GO_RC_Button.GetComponent<Image>().color = Color_Button_Unselected;
                }

            }

        }
    }

}