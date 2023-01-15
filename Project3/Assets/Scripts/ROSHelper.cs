using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient{

    public class ROSHelper : MonoBehaviour
    {
        public static MessageTypes.Geometry.PointStamped getEmpty_GeometryPointStamped(float x, float y, float z, string frameID){
            MessageTypes.Std.Header newHeader = new MessageTypes.Std.Header{frame_id = frameID, seq = 1};
            MessageTypes.Geometry.Point newPoint = new MessageTypes.Geometry.Point{x = x, y = y, z = z};
            MessageTypes.Geometry.PointStamped newPointStamped = new MessageTypes.Geometry.PointStamped{header = newHeader, point = newPoint};
            return newPointStamped;
        }

        public static MessageTypes.Geometry.PointStamped getEmpty_GeometryPointStamped(Vector3 point, string frameID){
            return getEmpty_GeometryPointStamped(point.x, point.y, point.z, frameID);
        }
    }

}