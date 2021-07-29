using UnityEngine;
using System.Collections.Generic;

public class Wheel : MonoBehaviour{
	public bool steer;
	public bool invert_steer;
	public bool power;
	public float steer_angle{get;set;}
	public float torque{get;set;}
	public float slip;
	private WheelCollider wheel_collider;
	private Transform wheel_transform;
	private Transform rim_transform;
	private MeshRenderer[] wheel;
    void Start(){
        wheel_collider = GetComponentInChildren<WheelCollider>();
		wheel = GetComponentsInChildren<MeshRenderer>();
		wheel_transform = wheel[0].GetComponent<Transform>();
		rim_transform = wheel[1].GetComponent<Transform>();
    }
	private void FixedUpdate() {
		Wheel_Friction();
		Wheel_Animation();
		Vehicle_Movement();
	}
	private void Wheel_Animation(){
		wheel_collider.GetWorldPose(out Vector3 position, out Quaternion rotation);
		wheel_transform.position = position;
		rim_transform.position = position;
		wheel_transform.rotation = rotation;
		rim_transform.rotation = rotation;
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
