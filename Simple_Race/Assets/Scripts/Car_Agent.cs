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
*/
namespace Simple_Race{
    public class Car_Agent : Agent{
        private Timer idleTimer;
        private Vector3 lastPos, currPos;
        public CarController carController;
        private Checkpoint previousCheckpoint;
        [SerializeField] public Transform spawnTransform;
        [SerializeField] public List<Checkpoint> checkpoints;
        static float largeValue = 999999f;
        public int targetCheckpointIndex = 0, lapCount = 1, masksToCollide; //masks that sensor rays hit
        public float lapStartTime = 0f, currentLapTime = 0f, fastestLapTime = largeValue,
            currentAvgSpeed = 0f, fastestAvgSpeed = 0f, lapDistance = 0f, 
            steeringVariation = 0f, steeringVariationLowest = largeValue;
        private void Start(){
            PrepareTimer();
            ResetParameters();   
            masksToCollide = LayerMask.GetMask("Track", "Ground");
            Debug.Assert(LoadedCheckpoints(),"Checkpoints not loaded");
            checkpoints.Sort((c1,c2) => c1.checkpointIndex.CompareTo(c2.checkpointIndex));//ordered checkpoint list
        }
        private void FixedUpdate(){
            if(this.GetCumulativeReward() < -5) TerminateEpisode(-20);
            RaycastHit downward, upward; // if hits (top or bottom) a ground layer end episode
            if((Physics.Raycast(transform.localPosition, transform.TransformDirection(Vector3.up), out upward, 10f, masksToCollide) && upward.collider.gameObject.tag == "Ground") ||
               (Physics.Raycast(transform.localPosition, transform.TransformDirection(Vector3.down), out downward, 10f, masksToCollide) && downward.collider.gameObject.tag == "Ground")) 
               TerminateEpisode(-30);
            lapDistance += (int)Mathf.Abs(Vector3.Distance(currPos, transform.localPosition));
            currPos = transform.localPosition;
        }
        public override void OnEpisodeBegin(){
            ResetParameters();
        }
        public override void CollectObservations(VectorSensor sensor){ // redo demos without commentsparameters
            sensor.AddObservation(checkpoints[targetCheckpointIndex].transform.localPosition);
            sensor.AddObservation(Vector3.Dot(transform.forward, checkpoints[targetCheckpointIndex].transform.forward));
            if(targetCheckpointIndex == checkpoints.Capacity - 1) return;
            sensor.AddObservation(checkpoints[targetCheckpointIndex + 1].transform.localPosition);
            sensor.AddObservation(Vector3.Dot(transform.forward, checkpoints[targetCheckpointIndex + 1].transform.forward));
        }
        public override void OnActionReceived(ActionBuffers actions){ //translate ai input to Move() parameters
            steeringVariation += Mathf.Abs(actions.ContinuousActions[1]);
            carController.Move(actions.ContinuousActions[1], actions.ContinuousActions[0], -actions.ContinuousActions[2], 0);
        }
        private void OnTriggerEnter(Collider trigger) {
            if(!trigger.CompareTag("Checkpoint")) return; //IDC except checkpoints
            if(targetCheckpointIndex != trigger.GetComponent<Checkpoint>().checkpointIndex) return; //IDC except expected checkpoint
            if(previousCheckpoint != null) ToggleCheckpointCollider(trigger); // not the best way to enable/disable checkpoint colliders
            AddReward(20f); // reward for passing correct checkpoint
            if(targetCheckpointIndex >= 6) TerminateEpisode(30); //check and increment iteration
            targetCheckpointIndex++;
        }
        public override void Heuristic(in ActionBuffers actionsOut){
            int accelerate = 0, brake = 0, steer = 0, handbrake = 0;
            if(Input.GetKey(KeyCode.UpArrow)) accelerate = 1;
            if(Input.GetKey(KeyCode.LeftArrow)) steer = -1;
            if(Input.GetKey(KeyCode.RightArrow)) steer = 1;
            if(Input.GetKey(KeyCode.Space)) handbrake = 1;            
            if(Input.GetKey(KeyCode.DownArrow)) brake = 1;
            ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
            continuousActions[0] = accelerate;
            continuousActions[1] = steer;
            continuousActions[2] = brake;
            continuousActions[3] = handbrake;
        }
        private void PrepareTimer(){
            idleTimer = new Timer(); // @ 3 second mark check if vehicle stuck then toggle bool
            idleTimer.Elapsed += new ElapsedEventHandler(VehicleStuck);
            idleTimer.Interval = 3000;
            idleTimer.AutoReset = true;
            idleTimer.Enabled = true;
            idleTimer.Start();
        }
        private void ResetParameters(){
            lapCount = 1;
            lapDistance = 0;
            currentLapTime = 0;
            currentAvgSpeed = 0;
            lapStartTime = Time.time;
            targetCheckpointIndex = 0;
            lastPos = spawnTransform.localPosition;
            carController.ResetCar(spawnTransform);
        }
        private void TerminateEpisode(int finalReward){ // run evaulated before ending episode ++
            Debug.Log("so far " + targetCheckpointIndex);
            currentLapTime = Time.time - lapStartTime;
            currentAvgSpeed = lapDistance / currentLapTime;
            EvaluateEpisodePerformance();
            AddReward(finalReward);
            EndEpisode();
        }
        private void EvaluateEpisodePerformance(){
            if(IsFastestAvgSpeed()) AddReward(4f);
            if(IsFastestLap()) AddReward(6f);
            if(SteeringDerivationLess()) AddReward(8f);
        }
        private void ToggleCheckpointCollider(Collider trigger){
            previousCheckpoint.EnableCollider();
            trigger.GetComponent<Checkpoint>().DisableCollider();
            previousCheckpoint = trigger.GetComponent<Checkpoint>();
        }
        private void VehicleStuck(object source, ElapsedEventArgs e){// @ every 3 seconds
            if(Vector3.Distance(lastPos, currPos) < 3) AddReward(-5f);
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
    }
}