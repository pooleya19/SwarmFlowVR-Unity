using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;


namespace RosSharp.RosBridgeClient{
    
    [RequireComponent(typeof(RosConnector))]
    public class CustomSubscriber : MonoBehaviour
    {
        private RosConnector connector;
        private bool connected;
        private System.Type initialType;
        private string topicName;
        private string subscriberID;
        private delegate void MethodDelegate(Message message);

        public Message receivedValue;
        
        public void Init<T>(string TopicName){
            connector = GetComponent<RosConnector>();
            connected = false;
            topicName = TopicName;
            initialType = typeof(T);

            receivedValue = null;
        }

        void Update(){
            if(connector.RosSocket == null) return;
            if(!connected){
                connected = true;
                try{
                    Type Type_SubscriptionHandlerGeneric = typeof(SubscriptionHandler<>).MakeGenericType(initialType);
                    MethodInfo SubscriptionCallbackGeneric = typeof(CustomSubscriber).GetMethod("ReceiveMessage").MakeGenericMethod(initialType);
                    Delegate what = Delegate.CreateDelegate(Type_SubscriptionHandlerGeneric,this,SubscriptionCallbackGeneric);
                    object ret = typeof(RosSocket).GetMethod("Subscribe").MakeGenericMethod(initialType).Invoke(connector.RosSocket, new object[]{topicName,what,0,1,int.MaxValue,"none"});
                    subscriberID = (string) ret;
                }catch(Exception ex){
                    Debug.LogError("Subscription Error.");
                    Debug.LogError(ex.ToString());
                }
            }
        }

        public void ReceiveMessage<T>(T message) where T : Message{
            if(message.GetType() == initialType){
                receivedValue = message;
            }
        }
    }

}
