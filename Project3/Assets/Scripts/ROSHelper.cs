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

        public static MessageTypes.Geometry.Twist getEmpty_GeometryTwist(Vector3 linear, Vector3 angular){
            MessageTypes.Geometry.Vector3 newLinear = new MessageTypes.Geometry.Vector3{x = linear.x, y = linear.y, z = linear.z};
            MessageTypes.Geometry.Vector3 newAngular = new MessageTypes.Geometry.Vector3{x = angular.x, y = angular.y, z = angular.z};
            MessageTypes.Geometry.Twist newTwist = new MessageTypes.Geometry.Twist{linear = newLinear, angular = newAngular};
            return newTwist;
        }
    }

}