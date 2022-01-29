using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityStandardAssets.Vehicles.Car;
using System.Timers;
/*
-Checkpoint Collider En/Disbale is late
-Function to end lap
-Find a way to detect off road
*/
namespace Simple_Race{
    public class Car_Agent : Agent{
        private Timer idleTimer;
        public Transform[] raycasts;
        private Vector3 lastPos, currPos;
        public CarController carController;
        private Checkpoint previousCheckpoint;
        private RayPerceptionSensorComponent3D sensorRays;
        [SerializeField] public Transform spawnTransform;
        [SerializeField] public List<Checkpoint> checkpoints;
        static float largeValue = 999999f;
        public int targetCheckpointIndex, lapCount; //masks that sensor rays hit
        public float lapStartTime = 0f, currentLapTime = 0f, fastestLapTime = largeValue,
            currentAvgSpeed = 0f, fastestAvgSpeed = 0f, lapDistance = 0f, 
            steeringVariation = 0f, steeringVariationLowest = largeValue;
        private void Start(){
            PrepareTimer();
            Debug.Assert(LoadedCheckpoints(),"Checkpoints not loaded");
            checkpoints.Sort((c1,c2) => c1.checkpointIndex.CompareTo(c2.checkpointIndex));//ordered checkpoint list
            sensorRays = this.GetComponent<RayPerceptionSensorComponent3D>();
        }
        private void FixedUpdate(){
            Debug.Log(" Rewards "+ GetCumulativeReward());
            if(this.GetCumulativeReward() < -50) TerminateEpisode(0);
            if(Mathf.Abs(spawnTransform.localPosition.y - transform.localPosition.y) > 3) TerminateEpisode(-30); //need a better solution
            lapDistance += (int)Mathf.Abs(Vector3.Distance(currPos, transform.localPosition));
            currPos = transform.localPosition;
        }
        public override void OnEpisodeBegin(){
            HardReset();
            SoftReset();
            EnableCheckpointColliders();
        }
        public override void CollectObservations(VectorSensor sensor){
            var checkpointDirection = (checkpoints[targetCheckpointIndex].transform.localPosition - carController.transform.localPosition).normalized;
            sensor.AddObservation(Vector3.Dot(carController.m_Rigidbody.velocity.normalized, checkpointDirection));

            sensor.AddObservation((checkpoints[targetCheckpointIndex].transform.localPosition - transform.localPosition).normalized);
            sensor.AddObservation(carController.CurrentSpeed);
            sensor.AddObservation(this.transform.forward);
            sensor.AddObservation(Vector3.Distance(this.transform.localPosition, checkpoints[targetCheckpointIndex].transform.localPosition));            
        }
        public override void OnActionReceived(ActionBuffers actions){ //translate ai input to Move() parameters
            steeringVariation += Mathf.Abs(actions.ContinuousActions[1]);
            if(actions.ContinuousActions[0] > 0) AddReward(4f);
            if(actions.ContinuousActions[2] > 0) AddReward(-10);
            //Debug.Log("input " +actions.ContinuousActions[1]+" "+ actions.ContinuousActions[0] +" "+actions.ContinuousActions[2]+" "+actions.ContinuousActions[3]);
            carController.Move(actions.ContinuousActions[1], actions.ContinuousActions[0], -actions.ContinuousActions[2], actions.ContinuousActions[3]);
        }
        private void OnTriggerEnter(Collider trigger){
            if(!trigger.CompareTag("Checkpoint")) return; //IDC except checkpoints
            if(targetCheckpointIndex != trigger.GetComponent<Checkpoint>().checkpointIndex) TerminateEpisode(-100); //wrong checkpoint = punishment
            ToggleCheckpointCollider(trigger);
            AddReward(20f + 20 * targetCheckpointIndex); // reward for passing correct checkpoint
            if(targetCheckpointIndex == 1) SoftReset(); // Reset Lap parameters
            targetCheckpointIndex = (targetCheckpointIndex + 1) % checkpoints.Capacity; //cycling checkpoints
        }
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
        private void PrepareTimer(){
            idleTimer = new Timer(); // @ Interval/1000 seconds mark check if vehicle stuck then toggle bool
            idleTimer.Elapsed += new ElapsedEventHandler(VehicleStuck);
            idleTimer.Interval = 3000;
            idleTimer.AutoReset = true;
            idleTimer.Enabled = true;
            idleTimer.Start();
        }
        private void TerminateEpisode(int finalReward){
            //Debug.Log(finalReward);
            //currentLapTime = Time.time - lapStartTime;
            //currentAvgSpeed = lapDistance / currentLapTime;
            //EvaluateEpisodePerformance();
            AddReward(finalReward);
            EndEpisode();
        }
        private void HardReset(){
            lapCount = 1;
            targetCheckpointIndex = 0;
            lastPos = spawnTransform.localPosition;
            carController.ResetCar(spawnTransform);
        }
        private void SoftReset(){
            lapDistance = 0;
            currentLapTime = 0;
            currentAvgSpeed = 0;
            lapStartTime = Time.time;
            lapCount++;
        }
        private void EvaluateEpisodePerformance(){
            if(IsFastestAvgSpeed()) AddReward(4f);
            if(IsFastestLap()) AddReward(6f);
            if(SteeringDerivationLess()) AddReward(8f);
        }
        private void ToggleCheckpointCollider(Collider trigger){
            if(previousCheckpoint != null) previousCheckpoint.EnableCollider();
            trigger.GetComponent<Checkpoint>().DisableCollider();
            previousCheckpoint = trigger.GetComponent<Checkpoint>();
        }
        private void VehicleStuck(object source, ElapsedEventArgs e){// @ every 3 seconds
            if(Vector3.Distance(lastPos, currPos) < 10) TerminateEpisode(-100);
            lastPos = currPos;
        }
        private bool SteeringDerivationLess(){
            if(steeringVariation < steeringVariationLowest) return false;
            steeringVariationLowest = steeringVariation; return true;
        }
        private bool LoadedCheckpoints(){
            checkpoints = GameObject.FindObjectsOfType<Checkpoint>().ToList<Checkpoint>();
            if(checkpoints == null) return false;
            return true;
        }
        private bool IsFastestLap(){
            if(fastestLapTime < currentLapTime) return false;
            fastestLapTime = currentLapTime; return true;
        }
        private bool IsFastestAvgSpeed(){
            if(fastestAvgSpeed > currentAvgSpeed) return false;
            fastestAvgSpeed = currentAvgSpeed; return true;
        }
        private void EnableCheckpointColliders(){
            foreach(Checkpoint checkpoint in checkpoints) checkpoint.EnableCollider();}
    }
}