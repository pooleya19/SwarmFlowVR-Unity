using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMarker : MonoBehaviour
{
    public void setMarkerColor(Color color){
        Material temp = GetComponentInChildren<MeshRenderer>().material;
        temp.color = color;
        GetComponentInChildren<MeshRenderer>().material = temp;
    }
}
