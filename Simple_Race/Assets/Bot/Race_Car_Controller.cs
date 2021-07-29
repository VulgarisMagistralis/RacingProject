using System.Collections.Generic;
using UnityEngine;

namespace Simple_Race.Race_Systems{
    public class Race_Car_Controller : MonoBehaviour{
        [System.Serializable]
        public struct Stats{
            public float TopSpeed, Acceleration, ReverseSpeed, ReverseAcceleration, Braking, Steer;
            [Range(0.2f, 1)] public float AccelerationCurve;
            [Range(0.0f, 1.0f)] public float Grip;
            public float CoastingDrag; // current speed to 0 without input
            public float AddedGravity; // gravity when airborne
        }
        public Input_Data Input {get; private set;}
        public float AirPercent {get; private set;}
        public Rigidbody Rigidbody {get; private set;}
        public float GroundPercent {get; private set;}
        public Race_Car_Controller.Stats baseStats = new Race_Car_Controller.Stats{
            Grip = .95f,
            Steer = 5f,
            Braking = 10f,
            TopSpeed = 10f,
            Acceleration = 5f,
            AddedGravity = 1f,
            CoastingDrag = 4f,
            ReverseSpeed = 5f,
            AccelerationCurve = 4f,
            ReverseAcceleration = 5f
        };
        public List<GameObject> m_VisualWheels; //wheel mesh?
        public Transform CenterOfMass;
        [Range(0.0f, 20.0f)] public float AirborneReorientationCoefficient = 3.0f;
        // DRIFT --------------
        [Range (.01f, 1f)]   public float DriftGrip = .4f; // Grip @ drift
        [Range (0f, 10f)]    public float DriftAdditionalSteer = 5f;
        [Range (1f ,30f)]    public float MinAngleToFinishDrift = 10f;
        [Range (.01f, .99f)] public float MinSpeedPercentToFinishDrift = .5f;
        [Range (1f, 20f)]    public float DriftControl = 10f; //easier to control drift @ higher value
        [Range (0f, 20f)]    public float DriftDampening = 10f; //longer drift @ low value
        // VFX ----------------
        public List<Transform> Nozzles;
        public ParticleSystem DriftSparkVFX;
        //public GameObject JumpVFX, NozzleVFX, DriftTrailPrefab;
        [Range (0f, .2f)]   public float DriftSparkHorizontalOffset =.1f;
        [Range (-.1f, .1f)] public float DriftTrailVerticalOffset;
        [Range (0f, 90f)]   public float DriftSparkRotation = 17f;
        // Suspension -------------
        [Range (0f, 1f)] public float SuspensionHeight = .2f;
        [Range (-1f, 1f)] public float WheelsPositionVerticalOffset = .0f;
        [Range (0.0f, 5000.0f)] public float SuspensionDamp = 500f;
        [Range (10.0f, 100000.0f)] public float SuspensionSpring = 20000f;
        public WheelCollider FR, FL, RR, RL; // Wheels
        public LayerMask GroundLayers = Physics.DefaultRaycastLayers;
        I_Input[] m_Inputs;
        const float k_NullInput = .01f, k_NullSpeed = .01f;
        Vector3 m_VerticalReference = Vector3.up;
        // Drift related params
        float m_CurrentGrip = 1f;
        float m_DriftTurningPower = 0f;
        float m_PreviousGroundPercent = 1f;
        public bool IsDrifting {get; private set;} = false;
        public bool WantsToDrift {get; private set;} = false;
        readonly List<(GameObject trailRoot, WheelCollider wheel, TrailRenderer trail)> m_DriftTrailInstances = new List<(GameObject, WheelCollider, TrailRenderer )>();
        readonly List<(WheelCollider wheel, float horizontalOffset, float rotation, ParticleSystem sparks)> m_DriftSparkInstances = new List<(WheelCollider, float, float, ParticleSystem)>();
        // Race Car related params
        bool m_CanMove = true;
        Race_Car_Controller.Stats m_FinalStats;
        Quaternion m_LastValidRotation;
        Vector3 m_LastValidPosition;
        Vector3 m_LastCollisionNormal;
        bool m_HasCollision;
        bool m_InAir = false;
        public void SetCanMove(bool move) => m_CanMove = move;
        public float GetMaxSpeed() => Mathf.Max(m_FinalStats.TopSpeed, m_FinalStats.ReverseSpeed);
        private void ActivateDriftVFX(bool active){
            foreach(var vfx in m_DriftSparkInstances)
                if(active && vfx.wheel.GetGroundHit(out WheelHit hit))
                    if(!vfx.sparks.isPlaying)
                        vfx.sparks.Play();
                else
                    if(vfx.sparks.isPlaying)
                        vfx.sparks.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            foreach(var trail in m_DriftTrailInstances)
                trail.Item3.emitting = active && trail.wheel.GetGroundHit(out WheelHit hit);
        }
        private void UpdateDriftVFXOrientation(){
            foreach (var vfx in m_DriftSparkInstances){
                vfx.sparks.transform.position = vfx.wheel.transform.position - (vfx.wheel.radius * Vector3.up) + (DriftTrailVerticalOffset * Vector3.up) + (transform.right * vfx.horizontalOffset);
                vfx.sparks.transform.rotation = transform.rotation * Quaternion.Euler(0.0f, 0.0f, vfx.rotation);
            }
            foreach (var trail in m_DriftTrailInstances){
                trail.trailRoot.transform.position = trail.wheel.transform.position - (trail.wheel.radius * Vector3.up) + (DriftTrailVerticalOffset * Vector3.up);
                trail.trailRoot.transform.rotation = transform.rotation;
            }
        }
        void UpdateSuspensionParams(WheelCollider wheel){
            wheel.suspensionDistance = SuspensionHeight;
            wheel.center = new Vector3(0.0f, WheelsPositionVerticalOffset, 0.0f);
            JointSpring spring = wheel.suspensionSpring;
            spring.spring = SuspensionSpring;
            spring.damper = SuspensionDamp;
            wheel.suspensionSpring = spring;
        }
        void Awake(){
            Rigidbody = GetComponent<Rigidbody>();
            if(Rigidbody == null) Debug.Log("Empty");
            m_Inputs = GetComponents<I_Input>();
            UpdateSuspensionParams(FL);
            UpdateSuspensionParams(FR);
            UpdateSuspensionParams(RL);
            UpdateSuspensionParams(RR);
            m_CurrentGrip = baseStats.Grip;
            if (DriftSparkVFX != null){
                AddSparkToWheel(RL, -DriftSparkHorizontalOffset, -DriftSparkRotation);
                AddSparkToWheel(RR, DriftSparkHorizontalOffset, DriftSparkRotation);
            }
          /*  if (DriftTrailPrefab != null){
                AddTrailToWheel(RL);
                AddTrailToWheel(RR);
            }
            if (NozzleVFX != null)            
                foreach (var nozzle in Nozzles)
                    Instantiate(NozzleVFX, nozzle, false);*/
        }
       /* void AddTrailToWheel(WheelCollider wheel){
            GameObject trailRoot = Instantiate(DriftTrailPrefab, gameObject.transform, false);
            TrailRenderer trail = trailRoot.GetComponentInChildren<TrailRenderer>();
            trail.emitting = false;
            m_DriftTrailInstances.Add((trailRoot, wheel, trail));
        }*/
        void AddSparkToWheel(WheelCollider wheel, float horizontalOffset, float rotation){
            GameObject vfx = Instantiate(DriftSparkVFX.gameObject, wheel.transform, false);
            ParticleSystem spark = vfx.GetComponent<ParticleSystem>();
            spark.Stop();
            m_DriftSparkInstances.Add((wheel, horizontalOffset, -rotation, spark));
        }
        void FixedUpdate(){
            UpdateSuspensionParams(FL);
            UpdateSuspensionParams(FR);
            UpdateSuspensionParams(RL);
            UpdateSuspensionParams(RR);
            GatherInputs();
            Rigidbody.centerOfMass = transform.InverseTransformPoint(CenterOfMass.position);
            int groundedCount = 0;
            if (FL.isGrounded && FL.GetGroundHit(out WheelHit hit)) groundedCount++;
            if (FR.isGrounded && FR.GetGroundHit(out hit)) groundedCount++;
            if (RL.isGrounded && RL.GetGroundHit(out hit)) groundedCount++;
            if (RR.isGrounded && RR.GetGroundHit(out hit)) groundedCount++;
            GroundPercent = (float) groundedCount / 4.0f;   // calculate how grounded and airborne we are
            AirPercent = 1 - GroundPercent;
            if (m_CanMove)
                MoveVehicle(Input.Accelerate, Input.Brake, Input.TurnInput);
            GroundAirbourne();
            m_PreviousGroundPercent = GroundPercent;
            UpdateDriftVFXOrientation();
        }
        void GatherInputs(){
            Input = new Input_Data();      // reset input
            WantsToDrift = false;
            for (int i = 0; i < m_Inputs.Length; i++){    // gather nonzero input from our sources
                Input = m_Inputs[i].GenerateInput();
                WantsToDrift = Input.Brake && Vector3.Dot(Rigidbody.velocity, transform.forward) > 0.0f;
            }
        }
        public void Reset(){
            Vector3 euler = transform.rotation.eulerAngles;
            euler.x = euler.z = 0f;
            transform.rotation = Quaternion.Euler(euler);
        }
        public float LocalSpeed(){
            if (m_CanMove){
                float dot = Vector3.Dot(transform.forward, Rigidbody.velocity);
                if (Mathf.Abs(dot) > 0.1f){
                    float speed = Rigidbody.velocity.magnitude;
                    return dot < 0 ? -(speed / m_FinalStats.ReverseSpeed) : (speed / m_FinalStats.TopSpeed);
                }
                return 0f;
            }
            else  return Input.Accelerate ? 1.0f : 0.0f;      // use this value to play car sound when it is waiting the race start countdown.
        }
        void GroundAirbourne(){  // while in the air, fall faster
            if (AirPercent >= 1) Rigidbody.velocity += Physics.gravity * Time.fixedDeltaTime * m_FinalStats.AddedGravity;
        }
        void OnCollisionEnter(Collision collision) => m_HasCollision = true;
        void OnCollisionExit(Collision collision) => m_HasCollision = false;
        void OnCollisionStay(Collision collision){
            m_HasCollision = true;
            m_LastCollisionNormal = Vector3.zero;
            float dot = -1.0f;
            foreach (var contact in collision.contacts)
                if (Vector3.Dot(contact.normal, Vector3.up) > dot)
                    m_LastCollisionNormal = contact.normal;
        }
        void MoveVehicle(bool accelerate, bool brake, float turnInput){
            float accelInput = (accelerate ? 1.0f : 0.0f) - (brake ? 1.0f : 0.0f);
            float accelerationCurveCoeff = 5;             // manual acceleration curve coefficient scalar
            Vector3 localVel = transform.InverseTransformVector(Rigidbody.velocity);
            bool accelDirectionIsFwd = accelInput >= 0;
            bool localVelDirectionIsFwd = localVel.z >= 0;
            float maxSpeed = localVelDirectionIsFwd ? m_FinalStats.TopSpeed : m_FinalStats.ReverseSpeed;
            float accelPower = accelDirectionIsFwd ? m_FinalStats.Acceleration : m_FinalStats.ReverseAcceleration;
            float currentSpeed = Rigidbody.velocity.magnitude;
            float accelRampT = currentSpeed / maxSpeed;
            float multipliedAccelerationCurve = m_FinalStats.AccelerationCurve * accelerationCurveCoeff;
            float accelRamp = Mathf.Lerp(multipliedAccelerationCurve, 1, accelRampT * accelRampT);
            bool isBraking = (localVelDirectionIsFwd && brake) || (!localVelDirectionIsFwd && accelerate);
            float finalAccelPower = isBraking ? m_FinalStats.Braking : accelPower;
            float finalAcceleration = finalAccelPower * accelRamp;
            float turningPower = IsDrifting ? m_DriftTurningPower : turnInput * m_FinalStats.Steer;     // apply inputs to forward/backward
            Quaternion turnAngle = Quaternion.AngleAxis(turningPower, transform.up);
            Vector3 fwd = turnAngle * transform.forward;
            Vector3 movement = fwd * accelInput * finalAcceleration * ((m_HasCollision || GroundPercent > 0.0f) ? 1.0f : 0.0f);
            bool wasOverMaxSpeed = currentSpeed >= maxSpeed;         // forward movement
            if (wasOverMaxSpeed && !isBraking)  movement *= 0.0f; // if over max speed, cannot accelerate faster.
            Vector3 newVelocity = Rigidbody.velocity + movement * Time.fixedDeltaTime;
            newVelocity.y = Rigidbody.velocity.y;
            //  clamp max speed if we are on ground
            if (GroundPercent > 0.0f && !wasOverMaxSpeed)  newVelocity = Vector3.ClampMagnitude(newVelocity, maxSpeed);
            // coasting is when we aren't touching accelerate
 //           if (Mathf.Abs(accelInput) < k_NullInput && GroundPercent > 0.0f) newVelocity = Vector3.MoveTowards(newVelocity, new Vector3(0, Rigidbody.velocity.y, 0), Time.fixedDeltaTime * m_FinalStats.CoastingDrag);
 //           Rigidbody.velocity = newVelocity;
            // Drift
            if (GroundPercent > 0.0f){
                if (m_InAir){
                    m_InAir = false;
               //     Instantiate(JumpVFX, transform.position, Quaternion.identity);
                }
                // manual angular velocity coefficient
                float angularVelocitySteering = 0.4f;
                float angularVelocitySmoothSpeed = 20f;

                // turning is reversed if we're going in reverse and pressing reverse
                if (!localVelDirectionIsFwd && !accelDirectionIsFwd) angularVelocitySteering *= -1.0f;
                var angularVel = Rigidbody.angularVelocity;

                // move the Y angular velocity towards our target
                angularVel.y = Mathf.MoveTowards(angularVel.y, turningPower * angularVelocitySteering, Time.fixedDeltaTime * angularVelocitySmoothSpeed);

                Rigidbody.angularVelocity = angularVel; // apply the angular velocity

                // rotate rigidbody's velocity as well to generate immediate velocity redirection
                // manual velocity steering coefficient
                float velocitySteering = 25f;

                // If the karts lands with a forward not in the velocity direction, we start the drift
                if (GroundPercent >= 0.0f && m_PreviousGroundPercent < 0.1f) {
                    Vector3 flattenVelocity = Vector3.ProjectOnPlane(Rigidbody.velocity, m_VerticalReference).normalized;
                    if (Vector3.Dot(flattenVelocity, transform.forward * Mathf.Sign(accelInput)) < Mathf.Cos(MinAngleToFinishDrift * Mathf.Deg2Rad)) {
                        IsDrifting = true;
                        m_CurrentGrip = DriftGrip;
                        m_DriftTurningPower = 0.0f;
                    }
                }
                // Drift Management
                if (!IsDrifting){
                    if ((WantsToDrift || isBraking) && currentSpeed > maxSpeed * MinSpeedPercentToFinishDrift) {
                        IsDrifting = true;
                        m_DriftTurningPower = turningPower + (Mathf.Sign(turningPower) * DriftAdditionalSteer);
                        m_CurrentGrip = DriftGrip;
                        ActivateDriftVFX(true);
                    }
                }
                if (IsDrifting) {
                    float turnInputAbs = Mathf.Abs(turnInput);
                    if (turnInputAbs < k_NullInput)  m_DriftTurningPower = Mathf.MoveTowards(m_DriftTurningPower, 0.0f, Mathf.Clamp01(DriftDampening * Time.fixedDeltaTime));
                    // Update the turning power based on input
                    float driftMaxSteerValue = m_FinalStats.Steer + DriftAdditionalSteer;
                    m_DriftTurningPower = Mathf.Clamp(m_DriftTurningPower + (turnInput * Mathf.Clamp01(DriftControl * Time.fixedDeltaTime)), -driftMaxSteerValue, driftMaxSteerValue);
                    bool facingVelocity = Vector3.Dot(Rigidbody.velocity.normalized, transform.forward * Mathf.Sign(accelInput)) > Mathf.Cos(MinAngleToFinishDrift * Mathf.Deg2Rad);
                    bool canEndDrift = true;
                    if (isBraking)  canEndDrift = false;
                    else if (!facingVelocity) canEndDrift = false;
                    else if (turnInputAbs >= k_NullInput && currentSpeed > maxSpeed * MinSpeedPercentToFinishDrift)  canEndDrift = false;
                    if (canEndDrift || currentSpeed < k_NullSpeed) { // No Input, and car aligned with speed direction => Stop the drift
                        IsDrifting = false;
                        m_CurrentGrip = m_FinalStats.Grip;
                    }
                }  // rotate our velocity based on current steer value
                Rigidbody.velocity = Quaternion.AngleAxis(turningPower * Mathf.Sign(localVel.z) * velocitySteering * m_CurrentGrip * Time.fixedDeltaTime, transform.up) * Rigidbody.velocity;
            }
            else  m_InAir = true;
            bool validPosition = false;
            if (Physics.Raycast(transform.position + (transform.up * 0.1f), -transform.up, out RaycastHit hit, 3.0f, 1 << 9 | 1 << 10 | 1 << 11)) {// Layer: ground (9) / Environment(10) / Track (11)
                Vector3 lerpVector = (m_HasCollision && m_LastCollisionNormal.y > hit.normal.y) ? m_LastCollisionNormal : hit.normal;
                m_VerticalReference = Vector3.Slerp(m_VerticalReference, lerpVector, Mathf.Clamp01(AirborneReorientationCoefficient * Time.fixedDeltaTime * (GroundPercent > 0.0f ? 10.0f : 1.0f)));    // Blend faster if on ground
            }else {
                Vector3 lerpVector = (m_HasCollision && m_LastCollisionNormal.y > 0.0f) ? m_LastCollisionNormal : Vector3.up;
                m_VerticalReference = Vector3.Slerp(m_VerticalReference, lerpVector, Mathf.Clamp01(AirborneReorientationCoefficient * Time.fixedDeltaTime));
            }
            validPosition = GroundPercent > 0.7f && !m_HasCollision && Vector3.Dot(m_VerticalReference, Vector3.up) > 0.9f;
            // Airborne / Half on ground management
            if (GroundPercent < 0.7f) {
                Rigidbody.angularVelocity = new Vector3(0.0f, Rigidbody.angularVelocity.y * 0.98f, 0.0f);
                Vector3 finalOrientationDirection = Vector3.ProjectOnPlane(transform.forward, m_VerticalReference);
                finalOrientationDirection.Normalize();
                if (finalOrientationDirection.sqrMagnitude > 0.0f)  Rigidbody.MoveRotation(Quaternion.Lerp(Rigidbody.rotation, Quaternion.LookRotation(finalOrientationDirection, m_VerticalReference), Mathf.Clamp01(AirborneReorientationCoefficient * Time.fixedDeltaTime)));
            }else if (validPosition) {
                m_LastValidPosition = transform.position;
                m_LastValidRotation.eulerAngles = new Vector3(0.0f, transform.rotation.y, 0.0f);
            }
            ActivateDriftVFX(IsDrifting && GroundPercent > 0.0f);
        }
    }
}