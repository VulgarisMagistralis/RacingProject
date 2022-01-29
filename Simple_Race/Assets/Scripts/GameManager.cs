using Unity.MLAgents;
using UnityEngine;
namespace UnityStandardAssets.Vehicles.Car{
	public class GameManager : MonoBehaviour{
		public GameObject needle;
		public CarController car;
		private float desired_pos, speed;
		private float start_pos = 220f, end_pos = -45f;
		public ClusterManager[] trainingClusters;
		public static GameManager instance{get; private set;}
		public InputController input_controller{get; private set;}  
		int trackNumber;     
		private void Awake(){
			Application.targetFrameRate = 60;
			instance = this;
			input_controller = GetComponentInChildren<InputController>();
		    trackNumber = (int)Academy.Instance.EnvironmentParameters.GetWithDefault("track_no", trackNumber);
			trainingClusters = GameObject.FindObjectsOfType<ClusterManager>();
		}
		public void CheckAcademy(){
			
		}
		private void FixedUpdate(){
			/*speed = car.CurrentSpeed;
			Update_Needle();*/
		}
		private void Update_Needle(){
			desired_pos = start_pos - end_pos;
			float temp = speed / 180;
			needle.transform.eulerAngles = new Vector3(0, 0, start_pos - temp * desired_pos);
		}
	}
}