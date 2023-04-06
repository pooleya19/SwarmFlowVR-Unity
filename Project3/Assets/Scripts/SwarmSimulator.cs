using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmSimulator : SwarmInterface
{
    [Header("ROSBot IDs")]
    public string[] ROSBotIDs;
    
    [Header("Waypoint Control Parameters")]
    public float closeDistanceThreshold = 0.1f;
    public float closeAngleThreshold = 20;
    public float maxRotationSpeed = 20;
    public float maxLinearSpeed = 1;
    public float anglePGain = 2;
    public float linearPGain = 2;

    private Dictionary<string, Vector3> dictTargetWaypoint;
    private Dictionary<string, Vector3> dictPosition;
    private Dictionary<string, Quaternion> dictRotation;

    void Start(){
        // Initialize dictionaries
        dictTargetWaypoint = new Dictionary<string, Vector3>();
        dictPosition = new Dictionary<string, Vector3>();
        dictRotation = new Dictionary<string, Quaternion>();

        // Initialize simulated rosbots
        int numBots = ROSBotIDs.Length;
        if(numBots > 0){
            float minX = -1;
            float maxX = 1;
            for(int i=0; i<numBots; i++){
                // Calculate starting x-position
                float startX;
                if(numBots == 1){
                    startX = (minX + maxX)/2;
                }else{
                    float diffX = (maxX - minX) / (numBots - 1);
                    startX = minX + i*diffX;
                }

                Vector3 startPos = new Vector3(startX, 0, 0);
                Quaternion startRot = Quaternion.identity;

                string ROSBotID = ROSBotIDs[i];
                dictPosition[ROSBotID] = startPos;
                dictRotation[ROSBotID] = startRot;
            }
        }
    }

    void FixedUpdate(){
        foreach(string ROSBotID in ROSBotIDs){
            if(ROSBotID != "01") continue;
            // Check for valid position and target waypoint
            if(dictPosition.ContainsKey(ROSBotID) && dictTargetWaypoint.ContainsKey(ROSBotID)){
                Vector3 position = dictPosition[ROSBotID];
                position.y = 0;
                Quaternion rotation = dictRotation[ROSBotID];
                Vector3 targetWaypoint = dictTargetWaypoint[ROSBotID];
                targetWaypoint.y = 0;

                Vector3 waypointDiff = targetWaypoint - position;
                if(waypointDiff.magnitude < closeDistanceThreshold){
                    // ROSBot is close to target! Skip
                    continue;
                }

                // Handle rotation first
                Vector3 forward = rotation * Vector3.forward;
                forward.Normalize();
                float angleDiff = Vector3.Angle(forward, waypointDiff) * Mathf.Sign((Vector3.Cross(forward, waypointDiff)).y);
                float angleDiffMag = Mathf.Abs(angleDiff);
                float rotationSpeedMag = Mathf.Min(anglePGain * angleDiffMag, maxRotationSpeed);
                float incrementAngle = rotationSpeedMag * Mathf.Sign(angleDiff) * Time.fixedDeltaTime;

                Quaternion incrementRotation = Quaternion.AngleAxis(incrementAngle, Vector3.up);
                Quaternion newRotation = rotation * incrementRotation;
                dictRotation[ROSBotID] = newRotation;

                if(angleDiffMag <= closeAngleThreshold){
                    float distance = waypointDiff.magnitude;
                    float linearSpeedMag = Mathf.Min(linearPGain * distance, maxLinearSpeed);
                    Vector3 incrementPosition = linearSpeedMag * Time.fixedDeltaTime * forward;
                    Vector3 newPosition = position + incrementPosition;
                    dictPosition[ROSBotID] = newPosition; 

                    //Debug.Log("angleDiff=" + angleDiff + ", distance=" + distance);
                }else{
                    //Debug.Log("angleDiff=" + angleDiff);
                }
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
