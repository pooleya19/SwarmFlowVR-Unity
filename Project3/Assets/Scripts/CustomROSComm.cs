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

        public GameObject GO_ControllerLeft;
        public GameObject GO_ControllerRight;

        void Start(){
            pub = gameObject.AddComponent<CustomPublisher>();
            pub.Init<MessageTypes.Std.String>("/TestString",2);

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
        }

        void Update(){
            string newData = "Time: " + Time.time.ToString();
            pub.message = new MessageTypes.Std.String{data = newData};

            string frameID = "1";
            
            Vector3 controllerPosLeft = GO_ControllerLeft.transform.position;
            pub_ControllerPosLeft.message = ROSHelper.getEmpty_GeometryPointStamped(controllerPosLeft, frameID);
            
            Vector3 controllerPosRight = GO_ControllerRight.transform.position;
            pub_ControllerPosRight.message = ROSHelper.getEmpty_GeometryPointStamped(controllerPosRight, frameID);
        }
    }
}