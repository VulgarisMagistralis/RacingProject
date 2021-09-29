using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityStandardAssets.Vehicles.Car;
using System.Timers;
using System.Net.Sockets;

namespace Simple_Race{
    /*
        ? CHANGE EPISODE TO SESSION FUNC NAMES

        !Sensor for vehicle to understand what is where    
    */
    [System.Serializable]
    public struct Sensor{
        public Transform Transform;
        public float RayDistance;
        public float HitValidationDistance;
    }
    public class Car_Agent_2 : Agent{
        /*
            !Predefined large value for comparative parameters
        */
        static float largeValue = 999999f;
        /*
            !Period for checking vehicles position within <idleRadius>
        */
        private Timer idleTimer;
        /*
            !Position parameters to check if vehicle stayed within <idleRadius> for a period
        */
        private Vector3 lastPosition, currentPosition;
        /*
            !Used for accessing position and speed
        */
        public CarController carController;
        #region Sensory
            [Header("Observation Parameters")]
                [Tooltip("Collidable Layers")]
                public LayerMask Mask;
                [Tooltip("Sensors to detect around the vehicle")]
                public Sensor[] Sensors;
                [Header("Checkpoints")]
                    [Tooltip("Checkpoint for Vehicle to pass through")]
                    public List<Checkpoint> Checkpoints;
                [Tooltip("Checkpoint Layer")]
                public LayerMask ChekpointMask;
                [Tooltip("Would the agent need a custom transform to be able to raycast and hit the track? If not assigned, then the root transform will be used.")]
                public Transform AgentSensorTransform;
        #endregion
        #region Rewards
            [Header("Rewards")]
                [Tooltip("Peanlty for leaving Track")]
                public float LeftTrackPenalty = -10f;
                [Tooltip("Passed the correct Checkpoint")]
                public float PassCheckpointReward = 10f;
                [Tooltip("Reward for getting closer to the target Checkpoint")]
                public float CheckpointProximityReward = 2f;
                [Tooltip("Fast Track time Reward")]
                public float SpeedReward = 10f;
                [Tooltip("Reward for Acceleration")]
                public float AccelerationReward = 3f;
                [Tooltip("Penalty for being stuck")]
                public float StuckPenalty = -20f;
        #endregion
        #region ResetParameters
            [Header("Inference Reset Params")]
                [Tooltip("What is the unique mask that the agent should detect when it falls out of the track?")]
                public LayerMask OutOfBoundsMask;
                [Tooltip("What are the layers we want to detect for the track and the ground?")]
                public LayerMask TrackMask;
                [Tooltip("How far should the ray be when casted? For larger karts - this value should be larger too.")]
                public float GroundCastDistance;
        #endregion
        #region Debugging
            [Header("Debug Option")]
                [Tooltip("Should we visualize the rays that the agent draws?")]
                public bool ShowRaycasts;
        #endregion
        #region Statistics
            [Header("Statistics")]
                [Tooltip("Lap counter for the Session")]
                public float lapCount = 0;
                [Tooltip("System time at the start of the Session")]
                public float lapStartTime = 0f;
                [Tooltip("Lap timer for the current Lap")]
                public float  currentLapTime = 0f;
                [Tooltip("Fastest Lap in the Session")]
                public float fastestLapTime = largeValue;
                [Tooltip("Average Speed of the current Lap")]
                public float currentAvgSpeed = 0f;
                [Tooltip("Fastest average speed of all the Laps in the Session")]
                public float fastestAvgSpeed = 0f;
                [Tooltip("Traveled distance since the start of the Lap")]
                public float lapDistance = 0f;
                [Tooltip("Total amount Steering input")]
                public float steeringVariation = 0f;
                [Tooltip("Least of amount steering for all the Laps in the Session")]
                public float steeringVariationLowest = largeValue;
                [Tooltip("Target Checkpoint Index")]
                public int targetCheckpointIndex;
                [Tooltip("Total amount of Rewards")]
                public float accummulatedReward = 0f;
        #endregion
        [SerializeField] public Transform spawnTransform;
        private bool endEpisode = false;
        public int idleRadius = 5;
        private void Awake(){carController = GetComponent<CarController>();}        
        private void Start(){
            PrepareTimer();
            Debug.Assert(LoadedCheckpoints(),"Checkpoints not loaded");
            Checkpoints.Sort((c1,c2) => c1.checkpointIndex.CompareTo(c2.checkpointIndex));
        }
        private void Update() {
            if(!endEpisode) return;
            endEpisode = false;
            TerminateEpisode(accummulatedReward);
        }
        private void LateUpdate() {
            if(ShowRaycasts) Debug.DrawRay(transform.position, Vector3.down * GroundCastDistance, Color.cyan);
            currentPosition = transform.position;
            if(GetCumulativeReward() < -30) TerminateEpisode(0);
        }
        /*
            ? check dot results
        */
        public override void CollectObservations(VectorSensor sensor){
            endEpisode = false;
            accummulatedReward = 0;
            sensor.AddObservation(carController.VehicleVelocity);
            var direction = (Checkpoints[targetCheckpointIndex].transform.position - carController.transform.position).normalized;
            sensor.AddObservation(Vector3.Dot(carController.VehicleVelocity.normalized, direction));
            sensor.AddObservation(Vector3.Dot(carController.transform.forward, direction));
            if(ShowRaycasts) Debug.DrawLine(AgentSensorTransform.position, Checkpoints[targetCheckpointIndex].transform.position, Color.blue);
            foreach(Sensor currentSensor in Sensors){
                var hit = Physics.Raycast(AgentSensorTransform.position, currentSensor.Transform.forward, out var hitInfo, currentSensor.RayDistance, Mask, QueryTriggerInteraction.Ignore);
                if(ShowRaycasts){
                    Debug.DrawRay(AgentSensorTransform.position, currentSensor.Transform.forward * currentSensor.RayDistance, Color.green);
                    Debug.DrawRay(AgentSensorTransform.position, currentSensor.Transform.forward * currentSensor.HitValidationDistance, Color.red);
                    if(hit && hitInfo.distance < currentSensor.HitValidationDistance) Debug.DrawRay(hitInfo.point, Vector3.up * 3.0f, Color.magenta);
                }
                if(hit && (hitInfo.collider.tag == "Ground") && (hitInfo.distance < currentSensor.HitValidationDistance)){
                    accummulatedReward += LeftTrackPenalty;
                    endEpisode = true;
                }
                sensor.AddObservation(hit ? hitInfo.distance : currentSensor.RayDistance);
            }
            sensor.AddObservation(carController.AccelInput);
            AddReward(AccelerationReward * (carController.AccelInput > 0 ? 1 : 0));
            sensor.AddObservation(carController.BrakeInput);
            AddReward(StuckPenalty * (carController.BrakeInput == 0 ? 0 : 1));
            sensor.AddObservation(carController.HandbrakeInput);
            AddReward(StuckPenalty * (carController.HandbrakeInput == 0 ? 0 : 1));
        }
        /*
            !3 point fields from going to TargetCheckpoint, accelerating without braking and speeding reward
        */
        public override void OnActionReceived(ActionBuffers actions){
            var nextCheckpoint = Checkpoints[(targetCheckpointIndex + 1) % Checkpoints.Count];
            var direction = (nextCheckpoint.transform.position - carController.transform.position).normalized;
            var reward = Vector3.Dot(carController.VehicleVelocity.normalized, direction);
            if(ShowRaycasts) Debug.DrawRay(AgentSensorTransform.position, carController.VehicleVelocity,Color.yellow);
            AddReward(reward * CheckpointProximityReward);
            AddReward(carController.VehicleVelocity.magnitude * SpeedReward);
            //steeringVariation += Mathf.Abs(actions.ContinuousActions[1]);
            carController.Move(actions.ContinuousActions[1], actions.ContinuousActions[0], -actions.ContinuousActions[2], actions.ContinuousActions[3]);
        }
        /*
            !If it is the correct Checkpoint give Reward
            ! Add lap resets
        */
        private void OnTriggerEnter(Collider collider){
            var maskedValue = 1 << collider.gameObject.layer;
            var triggered = maskedValue & ChekpointMask;
            if(targetCheckpointIndex + 1 == Checkpoints.Count) targetCheckpointIndex = 0;
            if(triggered > 0 && targetCheckpointIndex == collider.GetComponent<Checkpoint>().checkpointIndex){
                targetCheckpointIndex = (targetCheckpointIndex + 1) % Checkpoints.Count;            
                AddReward(PassCheckpointReward); 
            }
        }
        /*
            !Function for Player controlling the vehicle
        */
        public override void Heuristic(in ActionBuffers actionsOut){
            int accelerate = 0, brake = 0, steer = 0, handbrake = 0;
            if(Input.GetKey(KeyCode.UpArrow)) accelerate = 1;
            if(Input.GetKey(KeyCode.LeftArrow)) steer = -1;
            if(Input.GetKey(KeyCode.RightArrow)) steer = 1;            
            if(Input.GetKey(KeyCode.DownArrow)) brake = 1;
            if(Input.GetKey(KeyCode.Space)) handbrake = 1;
            ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
            continuousActions[0] = accelerate;
            continuousActions[1] = steer;
            continuousActions[2] = brake;
            continuousActions[3] = handbrake;
        }
        /*
            !Preparing timer for periodic checks for vehicle movement
        */
        private void PrepareTimer(){
            idleTimer = new Timer(); // @ Interval/1000 seconds mark check if vehicle stuck then toggle bool
            idleTimer.Elapsed += new ElapsedEventHandler(VehicleStuck);
            idleTimer.Interval = 3000;
            idleTimer.AutoReset = true;
            idleTimer.Enabled = true;
            idleTimer.Start();
        }
        /*
            !Closing procedure for Episode
        */
        private void TerminateEpisode(float finalReward){
            //Debug.Log(finalReward);
            //currentLapTime = Time.time - lapStartTime;
            //currentAvgSpeed = lapDistance / currentLapTime;
            //EvaluateEpisodePerformance();
            AddReward(accummulatedReward + finalReward);
            EndEpisode();
            OnEpisodeBegin();
        }
        /*
            !Starting a new Episode
        */
        public override void OnEpisodeBegin(){
            HardReset();
            SoftReset();
        }        
        /*
            !Defaulting vehicle parameters for a new run
        */
        private void HardReset(){
            lapCount = 1;
            targetCheckpointIndex = 0;
            lastPosition = spawnTransform.position;
            carController.ResetCar(spawnTransform);
        }
        /*
            !Defaulting lap statistics for a new Lap
        */
        private void SoftReset(){
            lapDistance = 0;
            currentLapTime = 0;
            currentAvgSpeed = 0;
            lapStartTime = Time.time;
            lapCount++;
        }
        /*
            !Evaluating performance of the vehicle for that episode
        */
        private void EvaluateEpisodePerformance(){
            if(IsFastestAvgSpeed()) AddReward(4f);
            if(IsFastestLap()) AddReward(6f);
            if(SteeringDerivationLess()) AddReward(8f);
        }
        /*
            !Called periodicially to see if the vehicle is stuck, end session if so
        */
        private void VehicleStuck(object source, ElapsedEventArgs e){// @ every 3 seconds
            if(Vector3.Distance(lastPosition, currentPosition) < idleRadius) accummulatedReward += StuckPenalty;
            lastPosition = currentPosition;
        }
        /*
            !Check if least steered Lap 
        */
        private bool SteeringDerivationLess(){
            if(steeringVariation < steeringVariationLowest) return false;
            steeringVariationLowest = steeringVariation; return true;
        }
        /*
            !Load checkpoints from track
        */
        private bool LoadedCheckpoints(){
            Checkpoints = GameObject.FindObjectsOfType<Checkpoint>().ToList<Checkpoint>();
            if(Checkpoints == null) return false;
            return true;
        }
        /*
            !Check if it is the fastest Lap
        */        
        private bool IsFastestLap(){
            if(fastestLapTime < currentLapTime) return false;
            fastestLapTime = currentLapTime; return true;
        }
        /*
            !Check if it has highest average speed
        */
        private bool IsFastestAvgSpeed(){
            if(fastestAvgSpeed > currentAvgSpeed) return false;
            fastestAvgSpeed = currentAvgSpeed; return true;
        }
    }
}