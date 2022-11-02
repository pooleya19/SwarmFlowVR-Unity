using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLogger : MonoBehaviour
{

    public OVRInput.Controller controller;
    float lastTimePrint = 0;
    public float printFrequency = 2;
    float printDelay;

    public bool printPositions = false;

    // Start is called before the first frame update
    void Start()
    {
        printDelay = 1/printFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time;
        if(currentTime - lastTimePrint >= printDelay){
            lastTimePrint = currentTime;

            string message = "ControllerData: " + controller.ToString() + ", Val: " + OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger,controller);
            //Debug.Log(message);

            if(printPositions){
                Transform currentTrans = transform;
                for(int i=0; i<7; i++){
                    Debug.Log(currentTrans.name + ": " + currentTrans.position);
                    currentTrans = currentTrans.parent;
                }
            }
        }
    }
}
