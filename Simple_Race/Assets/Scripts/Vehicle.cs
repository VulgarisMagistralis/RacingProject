using UnityEngine;

public class Vehicle : MonoBehaviour{
	public Transform center_of_mass;
	private float motor_torque = 1900f;
	private float radius = 6;
	public float CurrentSpeed = 0.0f;
	private float downforce = 1.0f;
	private Rigidbody rigidbody_vehicle;
	private Wheel_2[] wheels;
	public float Steer{get;set;}
	public float Throttle{get;set;}
	private void Start() {
		wheels = GetComponentsInChildren<Wheel_2>();
		rigidbody_vehicle = GetComponent<Rigidbody>();
		rigidbody_vehicle.centerOfMass = center_of_mass.transform.localPosition;
	}
   private void FixedUpdate(){
	   CurrentSpeed = this.rigidbody_vehicle.velocity.magnitude;
	   foreach(var wheel in wheels){
		   wheel.steer_angle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * Steer;
		   wheel.torque = Throttle * motor_torque;   
	   }
	   add_downforce();
   }
   private void add_downforce(){
	   rigidbody_vehicle.AddForce(-transform.up * rigidbody_vehicle.velocity.magnitude * downforce);
   }
}
