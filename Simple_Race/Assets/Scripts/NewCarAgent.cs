using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityStandardAssets.Vehicles.Car;
namespace Simple_Race{
    public class NewCarAgent : Agent{
/*        static float largeValue = 999999f;
        public NewCarController carController;
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private List<Checkpoint> checkpoints;
        public int targetCheckpointIndex = 0, lapCount = 1, masksToCollide; //masks that sensor rays hit
        public float lapStartTime, currentLapTime, fastestLapTime = largeValue, currentAvgSpeed = 0f, fastestAvgSpeed = 0f, lapDistance = 0f, offroadStartTime;
        private Vector3 lastPos;
        private bool onTrack; // indicates on or off track; using to end episode
        private void Start(){
            transform.localPosition = spawnTransform.localPosition;
            onTrack = true;
            lastPos = transform.localPosition;
            masksToCollide = LayerMask.GetMask("Track", "Ground");
            Debug.Assert(LoadedCheckpoints(),"Checkpoints not loaded");
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
            lapDistance += Vector3.Distance(transform.localPosition,lastPos);
            lastPos = transform.localPosition;
        }
        public override void OnEpisodeBegin(){ //reset parameters for each episode
            transform.localPosition = spawnTransform.localPosition;
            onTrack = true;
            lastPos = transform.localPosition;
            transform.rotation = new Quaternion(0f, -1f, 0f, 1f);
            lapCount = 1;
            currentLapTime = 0;
            currentAvgSpeed = 0;
            targetCheckpointIndex = 0;
            carController.ResetCar();       
        }
        public override void CollectObservations(VectorSensor sensor){
            sensor.AddObservation(Vector3.Dot(transform.forward, checkpoints[targetCheckpointIndex].transform.forward));
          //  sensor.AddObservation(checkpoints[targetCheckpointIndex].transform);
           // sensor.AddObservation(checkpoints[targetCheckpointIndex].transform.forward);
        }
        
        public override void OnActionReceived(ActionBuffers actions){ //translate ai input to Move() parameters
            if(actions.ContinuousActions[1] < 0) carController.Move(actions.ContinuousActions[0], 0, actions.ContinuousActions[1], 0);
            else carController.Move(actions.ContinuousActions[0], actions.ContinuousActions[1], 0, 0);
        }
        private void OnTriggerEnter(Collider trigger) {
            if(trigger.CompareTag("Checkpoint"))
                if(targetCheckpointIndex == trigger.GetComponent<Checkpoint>().checkpointIndex){
                    AddReward(1f);     
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
            //addreward @ less steering variation
            targetCheckpointIndex = 0;
            lapDistance = 0;
            lapCount++;
        }
        public override void Heuristic(in ActionBuffers actionsOut){
            int moveHorizontal = 0, moveVertical = 0;
            if(Input.GetKey(KeyCode.UpArrow)) moveHorizontal = 1;
            if(Input.GetKey(KeyCode.DownArrow)) moveHorizontal = -1;
            if(Input.GetKey(KeyCode.RightArrow)) moveVertical = 1;
            if(Input.GetKey(KeyCode.LeftArrow)) moveVertical = -1;
            ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
            continuousActions[1] = moveHorizontal;
            continuousActions[0] = moveVertical;
        }
        private bool LongTimeOffroad(){
            if(Time.time - offroadStartTime > 6) return true;
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
        }*/
    }
}