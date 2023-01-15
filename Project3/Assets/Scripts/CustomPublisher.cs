using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;

namespace RosSharp.RosBridgeClient{
    
    [RequireComponent(typeof(RosConnector))]
    public class CustomPublisher : MonoBehaviour{

        private RosConnector connector;
        private bool connected;
        private string publisherID;
        public Message message;

        private string topicName;
        private float topicFrequency;
        private float lastPublicationTime;
        private System.Type initialType;

        public void Init<T>(string TopicName, int TopicFreq) where T : Message{
            connector = GetComponent<RosConnector>();
            connected = false;

            topicName = TopicName;
            topicFrequency = TopicFreq;
            lastPublicationTime = Time.time;
            initialType = typeof(T);
        }

        void Update(){
            if(connector.RosSocket == null) return;
            if(!connected){
                connected = true;
                try{
                    object ret = typeof(RosSocket).GetMethod("Advertise").MakeGenericMethod(initialType).Invoke(connector.RosSocket, new object[]{topicName});
                    publisherID = (string) ret;
                    //publisherID = CustomAdvertiser.CustomAdvertise(initialType, topicName, ref connector);
                }catch(Exception ex){
                    Debug.LogError("Advertisement Error.");
                    Debug.LogError(ex.ToString());
                }
            }
            
            if(message != null && Time.time - lastPublicationTime >= 1/topicFrequency){
                lastPublicationTime = Time.time;
                if(initialType != message.GetType()){
                    Debug.LogWarning("Incorrect Topic Message Type for Topic " + topicName + "; Target Message Type: " + initialType.ToString() + "; Requested Message Type: " + message.GetType().ToString());
                }else{
                    try{
                        connector.RosSocket.Publish(publisherID,message);
                    }catch(Exception ex){
                        Debug.LogError("Publishing Error.");
                        Debug.LogError(ex.ToString());
                    }
                }
            }
        }        
    }
}