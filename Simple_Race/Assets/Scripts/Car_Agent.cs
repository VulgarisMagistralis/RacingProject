using System.Linq;
using UnityEngine;
using System.Timers;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Collections.Generic;
using UnityStandardAssets.Vehicles.Car;
/*
    -Checkpoint Collider En/Disbale is late
    -When stationary decrease points TIMERS
    

*/
namespace Simple_Race{
    public class Car_Agent : Agent{
        static float largeValue = 999999f;
        public CarController carController;
        private Timer idleTimer;
        [SerializeField] public Transform spawnTransform;
        [SerializeField] public List<Checkpoint> checkpoints;
        private Checkpoint previousCheckpoint;
        public int targetCheckpointIndex = 0, lapCount = 1, masksToCollide; //masks that sensor rays hit
        public float lapStartTime, currentLapTime, fastestLapTime = largeValue, currentAvgSpeed = 0f, fastestAvgSpeed = 0f, lapDistance = 0f;
        private Vector3 lastPos, currPos;
        private bool onTrack;// indicates on or off track; using to end episode
        private bool isStuck = false;
        private void Start(){       
            masksToCollide = LayerMask.GetMask("Track", "Ground");
            Debug.Assert(LoadedCheckpoints(),"Checkpoints not loaded");
            PrepareTimer();
            ResetParameters();
            checkpoints.Sort((c1,c2) => c1.checkpointIndex.CompareTo(c2.checkpointIndex));
        }  
        private void FixedUpdate(){
            RaycastHit hit1;
            if(onTrack){
                if(Physics.Raycast(transform.localPosition, transform.TransformDirection(Vector3.down), out hit1, 10f, masksToCollide))
                    if(hit1.collider.gameObject.tag == "Ground"){
                        onTrack = false;
                        AddReward(-0.8f);
                        EndEpisode();
                    }else AddReward(0.5f);    
            }else{
                onTrack = true;
                EndEpisode();
            }
            currPos = transform.localPosition;
            Debug.Log(this.GetCumulativeReward());
            lapDistance += Vector3.Distance(currPos,lastPos);
        }
        public override void OnEpisodeBegin(){ //reset parameters for each episode
            ResetParameters();
            lapCount = 1;
            currentLapTime = 0;
            currentAvgSpeed = 0;      
            targetCheckpointIndex = 0;
            carController.ResetCar();
        }
        public override void CollectObservations(VectorSensor sensor){
            sensor.AddObservation(Vector3.Dot(transform.forward, checkpoints[targetCheckpointIndex].transform.forward));
        }
        public override void OnActionReceived(ActionBuffers actions){ //translate ai input to Move() parameters
            carController.Move(actions.ContinuousActions[1], actions.ContinuousActions[0],
            -actions.ContinuousActions[2], 0);
        }
        private void OnTriggerEnter(Collider trigger) {
            if(trigger.CompareTag("Checkpoint"))
                if(targetCheckpointIndex == trigger.GetComponent<Checkpoint>().checkpointIndex){
                    if(previousCheckpoint != null) previousCheckpoint.EnableCollider();
                    trigger.GetComponent<Checkpoint>().DisableCollider();
                    previousCheckpoint = trigger.GetComponent<Checkpoint>(); // not the best way to enable/disable checkpoints
                    AddReward(3f);
                    if(targetCheckpointIndex == checkpoints.Capacity - 1) EndLap();
                    else targetCheckpointIndex++;           
                }else AddReward(-0.4f);
        }
        private void EndLap(){ //change checkpoints
            currentLapTime = Time.time - lapStartTime;
            currentAvgSpeed = lapDistance / currentLapTime;
            Debug.Log("Time: " + currentLapTime + "Dist: " + currentAvgSpeed);
            if(IsFastestAvgSpeed()) AddReward(0.4f);
            if(IsFastestLap()) AddReward(0.6f);
            if(SteeringDerivationLess()) AddReward(0.8f);
            targetCheckpointIndex = 0;
            lapDistance = 0;
            lapCount++;
        }
        public override void Heuristic(in ActionBuffers actionsOut){
            int accelerate = 0, brake = 0, steer = 0, handbrake = 0;
            if(Input.GetKey(KeyCode.UpArrow)) accelerate = 1;
            if(Input.GetKey(KeyCode.Space)) handbrake = 1;
            if(Input.GetKey(KeyCode.LeftArrow)) steer = -1;
            if(Input.GetKey(KeyCode.RightArrow)) steer = 1;
            if(Input.GetKey(KeyCode.DownArrow)) brake = 1;
            ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
            continuousActions[0] = accelerate;
            continuousActions[1] = steer;
            continuousActions[2] = brake;
            continuousActions[3] = handbrake;
        }
        private void VehicleStuck(object source, ElapsedEventArgs e){// @ every 3 seconds 
            if(Vector3.Distance(lastPos, currPos) < 2){
                isStuck = true;
                AddReward(-0.2f);
            }else{
                isStuck = false;
                AddReward(0.4f);
            } 
            lastPos = currPos;
        }
        private void ResetParameters(){
            onTrack = true;
            isStuck = false;
            lastPos = spawnTransform.localPosition;
            transform.rotation = new Quaternion(0f, -1f, 0f, 1f);
            transform.localPosition = spawnTransform.localPosition;
        }
        private void PrepareTimer(){
            idleTimer = new Timer(); // @ 3 second mark check if vehicle stuck then toggle bool
            idleTimer.Elapsed += new ElapsedEventHandler(VehicleStuck);
            idleTimer.Interval = 3000;
            idleTimer.AutoReset = true;
            idleTimer.Enabled = true;
            idleTimer.Start();
        }
        private bool SteeringDerivationLess(){
            return false;
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