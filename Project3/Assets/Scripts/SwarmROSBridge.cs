using System.Collections;
using System.Collections.Generic;
using RosSharp.RosBridgeClient;
using UnityEngine;

namespace RosSharp.RosBridgeClient{

    public class SwarmROSBridge : SwarmInterface{

        public string[] ROSBotIDs;
        public string topicROSBotPrefix = "/Rosbot";
        public string topicPoseSuffix = "/pose";
        public string topicWaypointSuffix = "/targetWaypoint";

        private Dictionary<string, Vector3> dictTargetWaypoint;
        private Dictionary<string, Vector3> dictPosition;
        private Dictionary<string, int> dictPositionSeqNum;
    
        private Dictionary<string, Quaternion> dictRotation;
        private Dictionary<string, CustomSubscriber> dictSubscriber;
        private Dictionary<string, CustomPublisher> dictPublisher; 

        public float angleCompensator = 90;

        void Start(){
            // Initialize dictionaries
            dictTargetWaypoint = new Dictionary<string, Vector3>();
            dictPosition = new Dictionary<string, Vector3>();
            dictRotation = new Dictionary<string, Quaternion>();
            dictSubscriber = new Dictionary<string, CustomSubscriber>();
            dictPublisher = new Dictionary<string, CustomPublisher>();
            dictPositionSeqNum = new Dictionary<string, int>();
        }

        void Update(){
            foreach(string ROSBotID in ROSBotIDs){
                // Handle rosbot pose subscriber
                CustomSubscriber sub;
                if(!dictSubscriber.ContainsKey(ROSBotID)){
                    // If subscriber doesn't exist, generate it!
                    string topicSubscribed = topicROSBotPrefix + ROSBotID + topicPoseSuffix;
                    sub = gameObject.AddComponent<CustomSubscriber>();
                    sub.Init<MessageTypes.Geometry.PoseStamped>(topicSubscribed);
                    dictSubscriber[ROSBotID] = sub;
                }else{
                    sub = dictSubscriber[ROSBotID];
                }

                MessageTypes.Geometry.PoseStamped poseMsg = (MessageTypes.Geometry.PoseStamped) sub.receivedValue;
                if(poseMsg != null){
                    int newSeqNum = (int) poseMsg.header.seq;
                    int oldSeqNum;
                    if(!dictPositionSeqNum.ContainsKey(ROSBotID)){
                        oldSeqNum = newSeqNum-1;
                    }else{
                        oldSeqNum = dictPositionSeqNum[ROSBotID];
                    }
                    if(newSeqNum > oldSeqNum){
                        dictPositionSeqNum[ROSBotID] = newSeqNum;

                        MessageTypes.Geometry.Point pos = poseMsg.pose.position;
                        MessageTypes.Geometry.Quaternion rot = poseMsg.pose.orientation;
                        Vector3 position = new Vector3((float)pos.x, (float)pos.z, (float)pos.y);
                        Quaternion rotation = new Quaternion(-(float)rot.x, -(float)rot.z, -(float)rot.y, (float)rot.w);
                        rotation = rotation * Quaternion.AngleAxis(angleCompensator, Vector3.up);
                        Vector3 forwardAxis = rotation * Vector3.forward;
                        forwardAxis.y = 0;
                        rotation = Quaternion.FromToRotation(Vector3.forward, forwardAxis);
                        dictPosition[ROSBotID] = position;
                        dictRotation[ROSBotID] = rotation;
                        dictPositionSeqNum[ROSBotID] = (int) poseMsg.header.seq;
                    }
                }

                // Handle rosbot waypoint publisher
                CustomPublisher pub;
                if(!dictPublisher.ContainsKey(ROSBotID)){
                    string topicPublished = topicROSBotPrefix + ROSBotID + topicWaypointSuffix;
                    pub = gameObject.AddComponent<CustomPublisher>();
                    pub.Init<MessageTypes.Geometry.Vector3>(topicPublished, 10);
                    dictPublisher[ROSBotID] = pub;
                }else{
                    pub = dictPublisher[ROSBotID];
                }

                if(dictTargetWaypoint.ContainsKey(ROSBotID)){
                    Vector3 targetWaypoint = dictTargetWaypoint[ROSBotID];
                    Vector3 formatted = new Vector3(targetWaypoint.x, targetWaypoint.z, 0);
                    pub.message = ROSHelper.getGeometryVector3(formatted);
                }
            }
        }

        public override string[] getROSBotIDs(){
            return ROSBotIDs;
        }

        public override void setTargetWaypoint(string ROSBotID, Vector3 newTargetWaypoint){
            dictTargetWaypoint[ROSBotID] = newTargetWaypoint;
        }

        public override Vector3 getPosition(string ROSBotID){
            if(dictPosition.ContainsKey(ROSBotID)){
                return dictPosition[ROSBotID];
            }else{
                return Vector3.positiveInfinity;
            }
        }
        
        public override Quaternion getRotation(string ROSBotID){
            if(dictPosition.ContainsKey(ROSBotID)){
                return dictRotation[ROSBotID];
            }else{
                return Quaternion.identity;
            }
        }
    }

}