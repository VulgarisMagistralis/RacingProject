using UnityEngine;
public class testController : MonoBehaviour{
    [SerializeField] WheelCollider FLWheelCollider;
    [SerializeField] WheelCollider FRWheelCollider;
    [SerializeField] WheelCollider RLWheelCollider;
    [SerializeField] WheelCollider RRWheelCollider;
    [SerializeField] Transform FLWheelMesh;
    [SerializeField] Transform FRWheelMesh;
    [SerializeField] Transform RLWheelMesh;
    [SerializeField] Transform RRWheelMesh;
    public float acceleration = 500f;
    public float brakingForce = 300f;
    public float turnAngle = 45f;
    private float currentAcceleration = 0f;
    private float currentBrakeFroce = 0f;
    private float currentTurnAngle = 0f;
    private void FixedUpdate() {
        //Acceleration
        currentAcceleration = acceleration * Input.GetAxis("Vertical");
        //Braking
        if(Input.GetKey(KeyCode.Space)) currentBrakeFroce = brakingForce;
        else currentBrakeFroce = 0f;
        //AWD Control
        FRWheelCollider.motorTorque = currentAcceleration;
        FLWheelCollider.motorTorque = currentAcceleration;
        RRWheelCollider.motorTorque = currentAcceleration;
        RLWheelCollider.motorTorque = currentAcceleration;
        //Braking
        FRWheelCollider.brakeTorque = currentBrakeFroce;
        FLWheelCollider.brakeTorque = currentBrakeFroce;
        RRWheelCollider.brakeTorque = currentBrakeFroce;
        RLWheelCollider.brakeTorque = currentBrakeFroce;
        //Turning
        currentTurnAngle = turnAngle * Input.GetAxis("Horizontal");
        FRWheelCollider.steerAngle = currentTurnAngle;
        FLWheelCollider.steerAngle = currentTurnAngle;
        UpdateWheel(FLWheelCollider, FLWheelMesh);
        UpdateWheel(FRWheelCollider, FRWheelMesh);
        UpdateWheel(RLWheelCollider, RLWheelMesh);
        UpdateWheel(RRWheelCollider,RRWheelMesh); 
    }
    private void UpdateWheel(WheelCollider wheelCollider, Transform wheelTransform){
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}