using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwarmInterface : MonoBehaviour
{
    public abstract string[] getROSBotIDs();

    public abstract void setTargetWaypoint(string ROSBotID, Vector3 newTargetWaypoint);

    public abstract Vector3 getPosition(string ROSBotID);

    public abstract Quaternion getRotation(string ROSBotID);
}
