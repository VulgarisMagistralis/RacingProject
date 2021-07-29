using UnityEngine;
using System.Collections.Generic;
namespace UnityStandardAssets.Vehicles.Car{

public class Player : MonoBehaviour{
	public enum Control_Type{Human, Bot, Mobile};
	[SerializeField] Control_Type driver;
	public float best_lap_time{get; private set;} = Mathf.Infinity;
	public float last_lap_time{get; private set;} = 0;
	public float current_lap_time{get; private set;} = 0;
	public int current_lap_count{get; private set;} = 0;
	private float time_stamp;
	private int last_check_point = 0;
	private Transform check_point_parent;
	private int check_point_count;
	private int check_point_layer;
	private Vehicle vehicle_controller;
	public Track_Way_Points way_points;
	public List<Transform> points = new List<Transform>();
	public Transform current_waypoint;
	[Range(0,10)] public int distance_offset;
	[Range(0,5)] public float steer_force;

	private void Awake() {
		way_points = GameObject.FindGameObjectWithTag("PATH").GetComponent<Track_Way_Points>();
		points = way_points.points;
		current_waypoint = points[points.Count-1];
		check_point_parent = GameObject.Find("Check_Points").transform;
		check_point_count = check_point_parent.childCount;
		check_point_layer = LayerMask.NameToLayer("Check_Point");
		vehicle_controller = GetComponent<Vehicle>();
		driver = Control_Type.Human;
	}
	private void Start_Lap(){
		Debug.Log("Start Lap!");
		current_lap_count++;
		last_check_point = 1;
		time_stamp = Time.time;
	}
	private void End_Lap(){
		last_lap_time = Time.time - time_stamp;
		best_lap_time = Mathf.Min(last_lap_time, best_lap_time);
		Debug.Log("Lap Time: "+ last_lap_time);
	}
	private void OnTriggerEnter(Collider collider){
		if(collider.gameObject.layer != check_point_layer) return;
		//passed checkpoint 1 ?
		if(collider.gameObject.name == "1"){
			Debug.Log("passed");
			// and passed all of them as well , then lap ended
			if(last_check_point == check_point_count) End_Lap();
			if(current_lap_count == 0 || last_check_point == check_point_count) Start_Lap(); //first lap or new lap
			return;
		}
		//ordering of checkpoints
		if(collider.gameObject.name == (last_check_point+1).ToString()) last_check_point++;
	} 
    void Update(){
		current_lap_time = time_stamp > 0 ? Time.time - time_stamp : 0;
    }
	private void FixedUpdate() {
		Waypoints_Delta();
		switch(driver){
			case Control_Type.Human: human_driver();
				break;
			case Control_Type.Bot: bot_driver();
				break;
			case Control_Type.Mobile:
				break;
		}
	}
	private void human_driver(){
		//vehicle_controller.Steer = GameManager.instance.input_controller.steer_input;
		//vehicle_controller.Throttle = GameManager.instance.input_controller.throttle_input;
	}
	private void bot_driver(){
		vehicle_controller.Throttle = 0.2f;
		vehicle_controller.Steer = bot_steer();
	}
	private float bot_steer(){
		Vector3 relative = transform.InverseTransformPoint(current_waypoint.transform.position);
		relative /= relative.magnitude;
		return (relative.x / relative.magnitude) * steer_force;
	}
	private void Waypoints_Delta(){
		Vector3 vehicle_position = gameObject.transform.position;
		float distance = Mathf.Infinity;
		for(int i = 0; i < points.Count; ++i){
			Vector3 difference = points[i].transform.position - vehicle_position;
			float current_distance = difference.magnitude;
			if(current_distance < distance){
				current_waypoint = points[i + distance_offset];
				distance = current_distance;
			}
		}
	}
	private void OnDrawGizmos() {
		if(current_waypoint == null) return;
		Gizmos.DrawWireSphere(current_waypoint.position, 3);
	}
}
}