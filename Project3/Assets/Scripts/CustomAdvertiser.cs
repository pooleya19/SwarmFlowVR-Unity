using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient{

    public class CustomAdvertiser{
        public static string CustomAdvertise(System.Type type, string TopicName, ref RosConnector connector){
            RosSocket rosSocket = connector.RosSocket;
            string publisherID = null;
            if(type == typeof(MessageTypes.Actionlib.GoalID)){
                publisherID = rosSocket.Advertise<MessageTypes.Actionlib.GoalID>(TopicName);
            }else if(type == typeof(MessageTypes.Actionlib.GoalStatus)){
                publisherID = rosSocket.Advertise<MessageTypes.Actionlib.GoalStatus>(TopicName);
            }else if(type == typeof(MessageTypes.Actionlib.GoalStatusArray)){
                publisherID = rosSocket.Advertise<MessageTypes.Actionlib.GoalStatusArray>(TopicName);
            }else if(type == typeof(MessageTypes.ActionlibTutorials.FibonacciAction)){
                publisherID = rosSocket.Advertise<MessageTypes.ActionlibTutorials.FibonacciAction>(TopicName);
            }else if(type == typeof(MessageTypes.ActionlibTutorials.FibonacciActionFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.ActionlibTutorials.FibonacciActionFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.ActionlibTutorials.FibonacciActionGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.ActionlibTutorials.FibonacciActionGoal>(TopicName);
            }else if(type == typeof(MessageTypes.ActionlibTutorials.FibonacciActionResult)){
                publisherID = rosSocket.Advertise<MessageTypes.ActionlibTutorials.FibonacciActionResult>(TopicName);
            }else if(type == typeof(MessageTypes.ActionlibTutorials.FibonacciFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.ActionlibTutorials.FibonacciFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.ActionlibTutorials.FibonacciGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.ActionlibTutorials.FibonacciGoal>(TopicName);
            }else if(type == typeof(MessageTypes.ActionlibTutorials.FibonacciResult)){
                publisherID = rosSocket.Advertise<MessageTypes.ActionlibTutorials.FibonacciResult>(TopicName);
            }else if(type == typeof(MessageTypes.FileServer.GetBinaryFileRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.FileServer.GetBinaryFileRequest>(TopicName);
            }else if(type == typeof(MessageTypes.FileServer.GetBinaryFileResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.FileServer.GetBinaryFileResponse>(TopicName);
            }else if(type == typeof(MessageTypes.FileServer.SaveBinaryFileRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.FileServer.SaveBinaryFileRequest>(TopicName);
            }else if(type == typeof(MessageTypes.FileServer.SaveBinaryFileResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.FileServer.SaveBinaryFileResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.Accel)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.Accel>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.AccelStamped)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.AccelStamped>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.AccelWithCovariance)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.AccelWithCovariance>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.AccelWithCovarianceStamped)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.AccelWithCovarianceStamped>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.Inertia)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.Inertia>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.InertiaStamped)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.InertiaStamped>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.Point)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.Point>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.Point32)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.Point32>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.PointStamped)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.PointStamped>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.Polygon)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.Polygon>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.PolygonStamped)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.PolygonStamped>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.Pose)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.Pose>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.Pose2D)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.Pose2D>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.PoseArray)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.PoseArray>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.PoseStamped)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.PoseStamped>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.PoseWithCovariance)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.PoseWithCovariance>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.PoseWithCovarianceStamped)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.PoseWithCovarianceStamped>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.Quaternion)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.Quaternion>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.QuaternionStamped)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.QuaternionStamped>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.Transform)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.Transform>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.TransformStamped)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.TransformStamped>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.Twist)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.Twist>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.TwistStamped)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.TwistStamped>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.TwistWithCovariance)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.TwistWithCovariance>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.TwistWithCovarianceStamped)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.TwistWithCovarianceStamped>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.Vector3)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.Vector3>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.Vector3Stamped)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.Vector3Stamped>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.Wrench)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.Wrench>(TopicName);
            }else if(type == typeof(MessageTypes.Geometry.WrenchStamped)){
                publisherID = rosSocket.Advertise<MessageTypes.Geometry.WrenchStamped>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ExecuteTrajectoryAction)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ExecuteTrajectoryAction>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ExecuteTrajectoryActionFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ExecuteTrajectoryActionFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ExecuteTrajectoryActionGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ExecuteTrajectoryActionGoal>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ExecuteTrajectoryActionResult)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ExecuteTrajectoryActionResult>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ExecuteTrajectoryFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ExecuteTrajectoryFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ExecuteTrajectoryGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ExecuteTrajectoryGoal>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ExecuteTrajectoryResult)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ExecuteTrajectoryResult>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MoveGroupAction)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MoveGroupAction>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MoveGroupActionFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MoveGroupActionFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MoveGroupActionGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MoveGroupActionGoal>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MoveGroupActionResult)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MoveGroupActionResult>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MoveGroupFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MoveGroupFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MoveGroupGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MoveGroupGoal>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MoveGroupResult)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MoveGroupResult>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MoveGroupSequenceAction)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MoveGroupSequenceAction>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MoveGroupSequenceActionFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MoveGroupSequenceActionFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MoveGroupSequenceActionGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MoveGroupSequenceActionGoal>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MoveGroupSequenceActionResult)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MoveGroupSequenceActionResult>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MoveGroupSequenceFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MoveGroupSequenceFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MoveGroupSequenceGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MoveGroupSequenceGoal>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MoveGroupSequenceResult)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MoveGroupSequenceResult>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PickupAction)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PickupAction>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PickupActionFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PickupActionFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PickupActionGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PickupActionGoal>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PickupActionResult)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PickupActionResult>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PickupFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PickupFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PickupGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PickupGoal>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PickupResult)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PickupResult>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PlaceAction)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PlaceAction>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PlaceActionFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PlaceActionFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PlaceActionGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PlaceActionGoal>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PlaceActionResult)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PlaceActionResult>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PlaceFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PlaceFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PlaceGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PlaceGoal>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PlaceResult)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PlaceResult>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.AllowedCollisionEntry)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.AllowedCollisionEntry>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.AllowedCollisionMatrix)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.AllowedCollisionMatrix>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.AttachedCollisionObject)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.AttachedCollisionObject>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.BoundingVolume)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.BoundingVolume>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.CartesianPoint)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.CartesianPoint>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.CartesianTrajectory)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.CartesianTrajectory>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.CartesianTrajectoryPoint)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.CartesianTrajectoryPoint>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.CollisionObject)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.CollisionObject>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ConstraintEvalResult)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ConstraintEvalResult>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.Constraints)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.Constraints>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ContactInformation)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ContactInformation>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.CostSource)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.CostSource>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.DisplayRobotState)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.DisplayRobotState>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.DisplayTrajectory)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.DisplayTrajectory>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GenericTrajectory)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GenericTrajectory>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.Grasp)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.Grasp>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GripperTranslation)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GripperTranslation>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.JointConstraint)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.JointConstraint>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.JointLimits)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.JointLimits>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.KinematicSolverInfo)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.KinematicSolverInfo>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.LinkPadding)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.LinkPadding>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.LinkScale)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.LinkScale>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MotionPlanDetailedResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MotionPlanDetailedResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MotionPlanRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MotionPlanRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MotionPlanResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MotionPlanResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MotionSequenceItem)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MotionSequenceItem>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MotionSequenceRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MotionSequenceRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MotionSequenceResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MotionSequenceResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.MoveItErrorCodes)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.MoveItErrorCodes>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ObjectColor)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ObjectColor>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.OrientationConstraint)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.OrientationConstraint>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.OrientedBoundingBox)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.OrientedBoundingBox>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PlaceLocation)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PlaceLocation>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PlannerInterfaceDescription)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PlannerInterfaceDescription>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PlannerParams)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PlannerParams>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PlanningOptions)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PlanningOptions>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PlanningScene)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PlanningScene>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PlanningSceneComponents)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PlanningSceneComponents>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PlanningSceneWorld)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PlanningSceneWorld>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PositionConstraint)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PositionConstraint>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.PositionIKRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.PositionIKRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.RobotState)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.RobotState>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.RobotTrajectory)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.RobotTrajectory>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.TrajectoryConstraints)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.TrajectoryConstraints>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.VisibilityConstraint)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.VisibilityConstraint>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.WorkspaceParameters)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.WorkspaceParameters>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ApplyPlanningSceneRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ApplyPlanningSceneRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ApplyPlanningSceneResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ApplyPlanningSceneResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ChangeControlDimensionsRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ChangeControlDimensionsRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ChangeControlDimensionsResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ChangeControlDimensionsResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ChangeDriftDimensionsRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ChangeDriftDimensionsRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ChangeDriftDimensionsResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ChangeDriftDimensionsResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.CheckIfRobotStateExistsInWarehouseRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.CheckIfRobotStateExistsInWarehouseRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.CheckIfRobotStateExistsInWarehouseResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.CheckIfRobotStateExistsInWarehouseResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.DeleteRobotStateFromWarehouseRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.DeleteRobotStateFromWarehouseRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.DeleteRobotStateFromWarehouseResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.DeleteRobotStateFromWarehouseResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ExecuteKnownTrajectoryRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ExecuteKnownTrajectoryRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ExecuteKnownTrajectoryResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ExecuteKnownTrajectoryResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetCartesianPathRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetCartesianPathRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetCartesianPathResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetCartesianPathResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetMotionPlanRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetMotionPlanRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetMotionPlanResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetMotionPlanResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetMotionSequenceRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetMotionSequenceRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetMotionSequenceResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetMotionSequenceResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetPlannerParamsRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetPlannerParamsRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetPlannerParamsResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetPlannerParamsResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetPlanningSceneRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetPlanningSceneRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetPlanningSceneResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetPlanningSceneResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetPositionFKRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetPositionFKRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetPositionFKResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetPositionFKResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetPositionIKRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetPositionIKRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetPositionIKResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetPositionIKResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetRobotStateFromWarehouseRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetRobotStateFromWarehouseRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetRobotStateFromWarehouseResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetRobotStateFromWarehouseResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetStateValidityRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetStateValidityRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GetStateValidityResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GetStateValidityResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GraspPlanningRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GraspPlanningRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.GraspPlanningResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.GraspPlanningResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ListRobotStatesInWarehouseRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ListRobotStatesInWarehouseRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.ListRobotStatesInWarehouseResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.ListRobotStatesInWarehouseResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.LoadMapRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.LoadMapRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.LoadMapResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.LoadMapResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.QueryPlannerInterfacesRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.QueryPlannerInterfacesRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.QueryPlannerInterfacesResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.QueryPlannerInterfacesResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.RenameRobotStateInWarehouseRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.RenameRobotStateInWarehouseRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.RenameRobotStateInWarehouseResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.RenameRobotStateInWarehouseResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.SaveMapRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.SaveMapRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.SaveMapResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.SaveMapResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.SaveRobotStateToWarehouseRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.SaveRobotStateToWarehouseRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.SaveRobotStateToWarehouseResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.SaveRobotStateToWarehouseResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.SetPlannerParamsRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.SetPlannerParamsRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.SetPlannerParamsResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.SetPlannerParamsResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.UpdatePointcloudOctomapRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.UpdatePointcloudOctomapRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Moveit.UpdatePointcloudOctomapResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Moveit.UpdatePointcloudOctomapResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.GetMapAction)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.GetMapAction>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.GetMapActionFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.GetMapActionFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.GetMapActionGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.GetMapActionGoal>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.GetMapActionResult)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.GetMapActionResult>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.GetMapFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.GetMapFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.GetMapGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.GetMapGoal>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.GetMapResult)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.GetMapResult>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.GridCells)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.GridCells>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.MapMetaData)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.MapMetaData>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.OccupancyGrid)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.OccupancyGrid>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.Odometry)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.Odometry>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.Path)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.Path>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.GetMapRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.GetMapRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.GetMapResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.GetMapResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.GetPlanRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.GetPlanRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.GetPlanResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.GetPlanResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.SetMapRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.SetMapRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Nav.SetMapResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Nav.SetMapResponse>(TopicName);
            }else if(type == typeof(MessageTypes.ObjectRecognition.ObjectRecognitionAction)){
                publisherID = rosSocket.Advertise<MessageTypes.ObjectRecognition.ObjectRecognitionAction>(TopicName);
            }else if(type == typeof(MessageTypes.ObjectRecognition.ObjectRecognitionActionFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.ObjectRecognition.ObjectRecognitionActionFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.ObjectRecognition.ObjectRecognitionActionGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.ObjectRecognition.ObjectRecognitionActionGoal>(TopicName);
            }else if(type == typeof(MessageTypes.ObjectRecognition.ObjectRecognitionActionResult)){
                publisherID = rosSocket.Advertise<MessageTypes.ObjectRecognition.ObjectRecognitionActionResult>(TopicName);
            }else if(type == typeof(MessageTypes.ObjectRecognition.ObjectRecognitionFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.ObjectRecognition.ObjectRecognitionFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.ObjectRecognition.ObjectRecognitionGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.ObjectRecognition.ObjectRecognitionGoal>(TopicName);
            }else if(type == typeof(MessageTypes.ObjectRecognition.ObjectRecognitionResult)){
                publisherID = rosSocket.Advertise<MessageTypes.ObjectRecognition.ObjectRecognitionResult>(TopicName);
            }else if(type == typeof(MessageTypes.ObjectRecognition.ObjectInformation)){
                publisherID = rosSocket.Advertise<MessageTypes.ObjectRecognition.ObjectInformation>(TopicName);
            }else if(type == typeof(MessageTypes.ObjectRecognition.ObjectType)){
                publisherID = rosSocket.Advertise<MessageTypes.ObjectRecognition.ObjectType>(TopicName);
            }else if(type == typeof(MessageTypes.ObjectRecognition.RecognizedObject)){
                publisherID = rosSocket.Advertise<MessageTypes.ObjectRecognition.RecognizedObject>(TopicName);
            }else if(type == typeof(MessageTypes.ObjectRecognition.RecognizedObjectArray)){
                publisherID = rosSocket.Advertise<MessageTypes.ObjectRecognition.RecognizedObjectArray>(TopicName);
            }else if(type == typeof(MessageTypes.ObjectRecognition.Table)){
                publisherID = rosSocket.Advertise<MessageTypes.ObjectRecognition.Table>(TopicName);
            }else if(type == typeof(MessageTypes.ObjectRecognition.TableArray)){
                publisherID = rosSocket.Advertise<MessageTypes.ObjectRecognition.TableArray>(TopicName);
            }else if(type == typeof(MessageTypes.ObjectRecognition.GetObjectInformationRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.ObjectRecognition.GetObjectInformationRequest>(TopicName);
            }else if(type == typeof(MessageTypes.ObjectRecognition.GetObjectInformationResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.ObjectRecognition.GetObjectInformationResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Octomap.Octomap)){
                publisherID = rosSocket.Advertise<MessageTypes.Octomap.Octomap>(TopicName);
            }else if(type == typeof(MessageTypes.Octomap.OctomapWithPose)){
                publisherID = rosSocket.Advertise<MessageTypes.Octomap.OctomapWithPose>(TopicName);
            }else if(type == typeof(MessageTypes.Octomap.BoundingBoxQueryRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Octomap.BoundingBoxQueryRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Octomap.BoundingBoxQueryResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Octomap.BoundingBoxQueryResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Octomap.GetOctomapRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Octomap.GetOctomapRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Octomap.GetOctomapResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Octomap.GetOctomapResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.TypeDef)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.TypeDef>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.DeleteParamRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.DeleteParamRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.DeleteParamResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.DeleteParamResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.GetActionServersRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.GetActionServersRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.GetActionServersResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.GetActionServersResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.GetParamNamesRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.GetParamNamesRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.GetParamNamesResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.GetParamNamesResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.GetParamRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.GetParamRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.GetParamResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.GetParamResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.GetTimeRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.GetTimeRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.GetTimeResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.GetTimeResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.HasParamRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.HasParamRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.HasParamResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.HasParamResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.MessageDetailsRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.MessageDetailsRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.MessageDetailsResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.MessageDetailsResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.NodeDetailsRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.NodeDetailsRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.NodeDetailsResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.NodeDetailsResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.NodesRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.NodesRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.NodesResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.NodesResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.PublishersRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.PublishersRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.PublishersResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.PublishersResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.SearchParamRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.SearchParamRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.SearchParamResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.SearchParamResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.ServiceHostRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.ServiceHostRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.ServiceHostResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.ServiceHostResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.ServiceNodeRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.ServiceNodeRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.ServiceNodeResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.ServiceNodeResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.ServiceProvidersRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.ServiceProvidersRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.ServiceProvidersResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.ServiceProvidersResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.ServiceRequestDetailsRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.ServiceRequestDetailsRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.ServiceRequestDetailsResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.ServiceRequestDetailsResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.ServiceResponseDetailsRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.ServiceResponseDetailsRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.ServiceResponseDetailsResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.ServiceResponseDetailsResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.ServicesForTypeRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.ServicesForTypeRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.ServicesForTypeResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.ServicesForTypeResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.ServicesRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.ServicesRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.ServicesResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.ServicesResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.ServiceTypeRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.ServiceTypeRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.ServiceTypeResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.ServiceTypeResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.SetParamRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.SetParamRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.SetParamResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.SetParamResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.SubscribersRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.SubscribersRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.SubscribersResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.SubscribersResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.TopicsForTypeRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.TopicsForTypeRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.TopicsForTypeResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.TopicsForTypeResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.TopicsRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.TopicsRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.TopicsResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.TopicsResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.TopicTypeRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.TopicTypeRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Rosapi.TopicTypeResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Rosapi.TopicTypeResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.BatteryState)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.BatteryState>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.CameraInfo)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.CameraInfo>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.ChannelFloat32)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.ChannelFloat32>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.CompressedImage)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.CompressedImage>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.FluidPressure)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.FluidPressure>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.Illuminance)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.Illuminance>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.Image)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.Image>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.Imu)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.Imu>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.JointState)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.JointState>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.Joy)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.Joy>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.JoyFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.JoyFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.JoyFeedbackArray)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.JoyFeedbackArray>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.LaserEcho)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.LaserEcho>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.LaserScan)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.LaserScan>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.MagneticField)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.MagneticField>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.MultiDOFJointState)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.MultiDOFJointState>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.MultiEchoLaserScan)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.MultiEchoLaserScan>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.NavSatFix)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.NavSatFix>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.NavSatStatus)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.NavSatStatus>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.PointCloud)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.PointCloud>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.PointCloud2)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.PointCloud2>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.PointField)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.PointField>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.Range)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.Range>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.RegionOfInterest)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.RegionOfInterest>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.RelativeHumidity)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.RelativeHumidity>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.Temperature)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.Temperature>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.TimeReference)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.TimeReference>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.SetCameraInfoRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.SetCameraInfoRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Sensor.SetCameraInfoResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Sensor.SetCameraInfoResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Shape.Mesh)){
                publisherID = rosSocket.Advertise<MessageTypes.Shape.Mesh>(TopicName);
            }else if(type == typeof(MessageTypes.Shape.MeshTriangle)){
                publisherID = rosSocket.Advertise<MessageTypes.Shape.MeshTriangle>(TopicName);
            }else if(type == typeof(MessageTypes.Shape.Plane)){
                publisherID = rosSocket.Advertise<MessageTypes.Shape.Plane>(TopicName);
            }else if(type == typeof(MessageTypes.Shape.SolidPrimitive)){
                publisherID = rosSocket.Advertise<MessageTypes.Shape.SolidPrimitive>(TopicName);
            }else if(type == typeof(MessageTypes.Std.Bool)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.Bool>(TopicName);
            }else if(type == typeof(MessageTypes.Std.Byte)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.Byte>(TopicName);
            }else if(type == typeof(MessageTypes.Std.ByteMultiArray)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.ByteMultiArray>(TopicName);
            }else if(type == typeof(MessageTypes.Std.Char)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.Char>(TopicName);
            }else if(type == typeof(MessageTypes.Std.ColorRGBA)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.ColorRGBA>(TopicName);
            }else if(type == typeof(MessageTypes.Std.Empty)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.Empty>(TopicName);
            }else if(type == typeof(MessageTypes.Std.Float32)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.Float32>(TopicName);
            }else if(type == typeof(MessageTypes.Std.Float32MultiArray)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.Float32MultiArray>(TopicName);
            }else if(type == typeof(MessageTypes.Std.Float64)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.Float64>(TopicName);
            }else if(type == typeof(MessageTypes.Std.Float64MultiArray)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.Float64MultiArray>(TopicName);
            }else if(type == typeof(MessageTypes.Std.Header)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.Header>(TopicName);
            }else if(type == typeof(MessageTypes.Std.Int16)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.Int16>(TopicName);
            }else if(type == typeof(MessageTypes.Std.Int16MultiArray)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.Int16MultiArray>(TopicName);
            }else if(type == typeof(MessageTypes.Std.Int32)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.Int32>(TopicName);
            }else if(type == typeof(MessageTypes.Std.Int32MultiArray)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.Int32MultiArray>(TopicName);
            }else if(type == typeof(MessageTypes.Std.Int64)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.Int64>(TopicName);
            }else if(type == typeof(MessageTypes.Std.Int64MultiArray)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.Int64MultiArray>(TopicName);
            }else if(type == typeof(MessageTypes.Std.Int8)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.Int8>(TopicName);
            }else if(type == typeof(MessageTypes.Std.Int8MultiArray)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.Int8MultiArray>(TopicName);
            }else if(type == typeof(MessageTypes.Std.MultiArrayDimension)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.MultiArrayDimension>(TopicName);
            }else if(type == typeof(MessageTypes.Std.MultiArrayLayout)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.MultiArrayLayout>(TopicName);
            }else if(type == typeof(MessageTypes.Std.String)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.String>(TopicName);
            }else if(type == typeof(MessageTypes.Std.UInt16)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.UInt16>(TopicName);
            }else if(type == typeof(MessageTypes.Std.UInt16MultiArray)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.UInt16MultiArray>(TopicName);
            }else if(type == typeof(MessageTypes.Std.UInt32)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.UInt32>(TopicName);
            }else if(type == typeof(MessageTypes.Std.UInt32MultiArray)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.UInt32MultiArray>(TopicName);
            }else if(type == typeof(MessageTypes.Std.UInt64)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.UInt64>(TopicName);
            }else if(type == typeof(MessageTypes.Std.UInt64MultiArray)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.UInt64MultiArray>(TopicName);
            }else if(type == typeof(MessageTypes.Std.UInt8)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.UInt8>(TopicName);
            }else if(type == typeof(MessageTypes.Std.UInt8MultiArray)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.UInt8MultiArray>(TopicName);
            }else if(type == typeof(MessageTypes.Std.EmptyRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.EmptyRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Std.EmptyResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.EmptyResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Std.SetBoolRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.SetBoolRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Std.SetBoolResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.SetBoolResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Std.TriggerRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.TriggerRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Std.TriggerResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Std.TriggerResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Tf2.LookupTransformAction)){
                publisherID = rosSocket.Advertise<MessageTypes.Tf2.LookupTransformAction>(TopicName);
            }else if(type == typeof(MessageTypes.Tf2.LookupTransformActionFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.Tf2.LookupTransformActionFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.Tf2.LookupTransformActionGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.Tf2.LookupTransformActionGoal>(TopicName);
            }else if(type == typeof(MessageTypes.Tf2.LookupTransformActionResult)){
                publisherID = rosSocket.Advertise<MessageTypes.Tf2.LookupTransformActionResult>(TopicName);
            }else if(type == typeof(MessageTypes.Tf2.LookupTransformFeedback)){
                publisherID = rosSocket.Advertise<MessageTypes.Tf2.LookupTransformFeedback>(TopicName);
            }else if(type == typeof(MessageTypes.Tf2.LookupTransformGoal)){
                publisherID = rosSocket.Advertise<MessageTypes.Tf2.LookupTransformGoal>(TopicName);
            }else if(type == typeof(MessageTypes.Tf2.LookupTransformResult)){
                publisherID = rosSocket.Advertise<MessageTypes.Tf2.LookupTransformResult>(TopicName);
            }else if(type == typeof(MessageTypes.Tf2.TF2Error)){
                publisherID = rosSocket.Advertise<MessageTypes.Tf2.TF2Error>(TopicName);
            }else if(type == typeof(MessageTypes.Tf2.TFMessage)){
                publisherID = rosSocket.Advertise<MessageTypes.Tf2.TFMessage>(TopicName);
            }else if(type == typeof(MessageTypes.Tf2.FrameGraphRequest)){
                publisherID = rosSocket.Advertise<MessageTypes.Tf2.FrameGraphRequest>(TopicName);
            }else if(type == typeof(MessageTypes.Tf2.FrameGraphResponse)){
                publisherID = rosSocket.Advertise<MessageTypes.Tf2.FrameGraphResponse>(TopicName);
            }else if(type == typeof(MessageTypes.Trajectory.JointTrajectory)){
                publisherID = rosSocket.Advertise<MessageTypes.Trajectory.JointTrajectory>(TopicName);
            }else if(type == typeof(MessageTypes.Trajectory.JointTrajectoryPoint)){
                publisherID = rosSocket.Advertise<MessageTypes.Trajectory.JointTrajectoryPoint>(TopicName);
            }else if(type == typeof(MessageTypes.Trajectory.MultiDOFJointTrajectory)){
                publisherID = rosSocket.Advertise<MessageTypes.Trajectory.MultiDOFJointTrajectory>(TopicName);
            }else if(type == typeof(MessageTypes.Trajectory.MultiDOFJointTrajectoryPoint)){
                publisherID = rosSocket.Advertise<MessageTypes.Trajectory.MultiDOFJointTrajectoryPoint>(TopicName);
            }
            return publisherID;
        }
    }

}