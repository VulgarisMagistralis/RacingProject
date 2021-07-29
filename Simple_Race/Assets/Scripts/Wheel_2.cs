using UnityEngine;

public class Wheel_2 : MonoBehaviour{
	public bool steer;
	public bool invert_steer;
	public bool power;
	public float steer_angle{get;set;}
	public float torque{get;set;}
	public float slip;
	private WheelCollider wheel_collider;
	//current vehicle has 3 levels of detail per wheel
	private Transform wheel_transform_1, wheel_transform_2, wheel_transform_3;
	private MeshRenderer[] wheel;
    void Start(){
        wheel_collider = GetComponent<WheelCollider>();
		wheel = GetComponentsInChildren<MeshRenderer>();
		wheel_transform_1 = wheel[0].GetComponent<Transform>();
		wheel_transform_2 = wheel[1].GetComponent<Transform>();
		wheel_transform_3 = wheel[2].GetComponent<Transform>();
    }
	private void FixedUpdate() {
		Wheel_Animation();
		Vehicle_Movement();
		Wheel_Friction();
	}
	private void Wheel_Animation(){
		wheel_collider.GetWorldPose(out Vector3 position, out Quaternion rotation);
		wheel_transform_1.position = position;
		wheel_transform_2.position = position;
		wheel_transform_3.position = position;
		wheel_transform_1.rotation = rotation;
		wheel_transform_2.rotation = rotation;
		wheel_transform_3.rotation = rotation;
	}
	private void Vehicle_Movement(){
		if(steer) wheel_collider.steerAngle = steer_angle * (invert_steer ? -1 : 1);
		if(power) wheel_collider.motorTorque = torque;
	}
	private void Wheel_Friction(){
		WheelHit wheel_hit;
		wheel_collider.GetGroundHit(out wheel_hit);
		slip = wheel_hit.sidewaysSlip;
	}
}
