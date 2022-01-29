using System;
using UnityEngine;
public class TestCarController : MonoBehaviour{
    #region WheelMeshes
        [Header("Wheel Mesh")]
            public Transform frontLeftWheelMesh, frontRightWheelMesh, rearLeftWheelMesh, rearRightWheelMesh;
    #endregion
    #region WheelColliders
        [Header("Wheel Collider")]
            public WheelCollider wheelFL, wheelFR, wheelRL, wheelRR;
    #endregion
    #region Vehicle Parameters
        [Header("Vehicle Parameters")]
            public float maxTorque = 500f;
            public float brakeTorque = 1000f;
            [Tooltip("Maximum turn angle in degrees")]
            public float maxFrontWheelTurnAngle = 30f, maxRearWheelTurnAngle = 5f; // differenciate @ low and high speed
            [Tooltip("Center of Mass")]            
            [SerializeField] private Vector3 centerOfMass;
            public Rigidbody rigidbody;
            private float torquePower = 0f;
            private float steerAngle = 30f;
    #endregion
    private void Awake() {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    void Update(){
        Vector3 temp = frontLeftWheelMesh.localEulerAngles;
        Vector3 temp1 = frontRightWheelMesh.localEulerAngles;
        Vector3 temp2 = rearLeftWheelMesh.localEulerAngles;
        Vector3 temp3 = rearRightWheelMesh.localEulerAngles;
        temp.y = wheelFL.steerAngle - (frontLeftWheelMesh.localEulerAngles.z);
        frontLeftWheelMesh.localEulerAngles = temp;

        temp1.y = wheelFR.steerAngle - (frontRightWheelMesh.localEulerAngles.z);
        frontRightWheelMesh.localEulerAngles = temp1;

        temp2.y = wheelRL.steerAngle - (rearLeftWheelMesh.localEulerAngles.z);
        rearLeftWheelMesh.localEulerAngles = temp2;

        temp3.y = wheelRR.steerAngle - (rearRightWheelMesh.localEulerAngles.z);
        rearRightWheelMesh.localEulerAngles = temp3;
        frontLeftWheelMesh.Rotate(wheelFL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        frontRightWheelMesh.Rotate(wheelFR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        rearLeftWheelMesh.Rotate(wheelRL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        rearRightWheelMesh.Rotate(wheelRR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
    }
    void FixedUpdate(){
        int accelerate = 0, brake = 0, steer = 0, handbrake = 0;
        if(Input.GetKey(KeyCode.UpArrow)) accelerate = 1;
        if(Input.GetKey(KeyCode.LeftArrow)) steer = -1;
        if(Input.GetKey(KeyCode.RightArrow)) steer = 1;            
        if(Input.GetKey(KeyCode.DownArrow)) brake = 1;
        if(Input.GetKey(KeyCode.Space)) handbrake = 1;
        // CONTROLS - FORWARD & RearWARD
        if (Input.GetKey(KeyCode.Space)) {
            // BRAKE
            torquePower = 0f;
            wheelRL.brakeTorque = brakeTorque;
            wheelRR.brakeTorque = brakeTorque;
        }else {
            // SPEED
            torquePower = maxTorque * Mathf.Clamp(Input.GetAxis("Vertical"), -1, 1);
            wheelRL.brakeTorque = 0f;
            wheelRR.brakeTorque = 0f;
        }
        // Apply torque
        wheelRR.motorTorque = torquePower;
        wheelRL.motorTorque = torquePower;
        // apply steering
        wheelFL.steerAngle = wheelFR.steerAngle = Mathf.Clamp(steer, -1, 1) * maxFrontWheelTurnAngle;
        if(rigidbody.velocity.magnitude > 50) wheelRR.steerAngle = wheelRL.steerAngle = Mathf.Clamp(steer, -1, 1) * maxRearWheelTurnAngle;
    }
}