using System.Collections;
using System.Collections.Generic;
using RosSharp.RosBridgeClient;
using UnityEngine;

namespace RosSharp.RosBridgeClient{

    public class CustomROSComm : MonoBehaviour{

        private CustomPublisher pub;
        private CustomPublisher cursorPub;
        private CustomPublisher TFStaticPub;

        private CustomPublisher pub_ControllerPosLeft;
        private CustomPublisher pub_ControllerPosRight;

        private CustomPublisher pub_RosbotVel;

        private CustomSubscriber sub_TestFollower;
        public string TestFollowerPoseTopicName;

        public GameObject GO_ControllerLeft;
        public GameObject GO_ControllerRight;

        public GameObject GO_TestFollower;

        void Start(){
            pub = gameObject.AddComponent<CustomPublisher>();
            pub.Init<MessageTypes.Std.String>("/TestString",2);

            sub_TestFollower = gameObject.AddComponent<CustomSubscriber>();
            sub_TestFollower.Init<MessageTypes.Geometry.PoseStamped>(TestFollowerPoseTopicName);

            /*
            pub_ControllerPosLeft = gameObject.AddComponent<CustomPublisher>();
            pub_ControllerPosLeft.Init<MessageTypes.Geometry.PointStamped>("/ControllerPosLeft",20);

            pub_ControllerPosRight = gameObject.AddComponent<CustomPublisher>();
            pub_ControllerPosRight.Init<MessageTypes.Geometry.PointStamped>("/ControllerPosRight",20);

            
            TFStaticPub = gameObject.AddComponent<CustomPublisher>();
            TFStaticPub.Init<MessageTypes.Tf2.TFMessage>("/tf_static",5);
            MessageTypes.Std.Header newHeader = new MessageTypes.Std.Header{frame_id = "0"};
            MessageTypes.Geometry.Vector3 newTranslation = new MessageTypes.Geometry.Vector3{x = 0, y = 0, z = 0};
            MessageTypes.Geometry.Quaternion newRotation = new MessageTypes.Geometry.Quaternion{w = 1, x = 0, y = 0, z = 0};
            MessageTypes.Geometry.Transform newTransform = new MessageTypes.Geometry.Transform{translation = newTranslation, rotation = newRotation};
            MessageTypes.Geometry.TransformStamped rootTrans = new MessageTypes.Geometry.TransformStamped{header = newHeader, transform = newTransform, child_frame_id="1"};
            MessageTypes.Geometry.TransformStamped[] newTransforms = new MessageTypes.Geometry.TransformStamped[1]{rootTrans};
            TFStaticPub.message = new MessageTypes.Tf2.TFMessage{transforms = newTransforms};
            */

            pub_RosbotVel = gameObject.AddComponent<CustomPublisher>();
            pub_RosbotVel.Init<MessageTypes.Geometry.Twist>("/cmd_vel",10);
        }

        void Update(){
            string newData = "Time: " + Time.time.ToString();
            pub.message = new MessageTypes.Std.String{data = newData};

            MessageTypes.Geometry.PoseStamped value_TestFollower = (MessageTypes.Geometry.PoseStamped) sub_TestFollower.receivedValue;
            if(value_TestFollower != null){
                MessageTypes.Geometry.Point position = value_TestFollower.pose.position;
                MessageTypes.Geometry.Quaternion orientation = value_TestFollower.pose.orientation;
                Vector3 position_TestFollower = new Vector3((float)position.x, (float)position.z, (float)position.y);
                Quaternion rotation_TestFollower = new Quaternion((float)orientation.x, (float)orientation.y, (float)orientation.z, (float)orientation.w);
                GO_TestFollower.transform.position = position_TestFollower;
                GO_TestFollower.transform.rotation = rotation_TestFollower;
            }

            /*
            string frameID = "1";
            Vector3 controllerPosLeft = GO_ControllerLeft.transform.position;
            pub_ControllerPosLeft.message = ROSHelper.getEmpty_GeometryPointStamped(controllerPosLeft, frameID);
            
            Vector3 controllerPosRight = GO_ControllerRight.transform.position;
            pub_ControllerPosRight.message = ROSHelper.getEmpty_GeometryPointStamped(controllerPosRight, frameID);
            */

            Vector3 linear = Vector3.zero;
            Vector3 angular = Vector3.zero;

            OVRInput.Controller controllerR = OVRInput.Controller.RTouch;
            OVRInput.Controller controllerL = OVRInput.Controller.LTouch;
            
            Vector2 rightJS = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controllerR);
            Vector2 leftJS = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controllerR);

            bool sendCommands = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger,controllerR);

            if(sendCommands){
                linear = Vector3.one * 1.3f * rightJS.y;
                angular = Vector3.one * -2 * rightJS.x;
            }

            pub_RosbotVel.message = ROSHelper.getEmpty_GeometryTwist(linear,angular);
        }
    }
}