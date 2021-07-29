using System;
using UnityEngine;

namespace Simple_Race.Race_Systems{
    [DefaultExecutionOrder(100)]
    public class Car_Animation : MonoBehaviour{
        public Race_Car_Controller car_controller;
        public float steering_animation_damping = 10f;
        public float max_steering_angle;
        public Wheel fr, fl, rr, rl;
        private float m_smoothed_steering_in;
        [Serializable] public class Wheel{
            public Transform wheel_transform;
            public WheelCollider wheel_collider;
            Quaternion m_steerless_local_rot;
            public void Setup() => m_steerless_local_rot = wheel_transform.localRotation;
            public void StoreDefaultRotation() => m_steerless_local_rot = wheel_transform.localRotation;
            public void SetToDefaultRotation() => wheel_transform.localRotation = m_steerless_local_rot;
        }
        private void Start(){
            fr.Setup();
            fl.Setup();
            rr.Setup();
            rl.Setup();
        }
        private void FixedUpdate() {
            m_smoothed_steering_in = Mathf.MoveTowards(m_smoothed_steering_in, car_controller.Input.TurnInput, steering_animation_damping * Time.deltaTime);
            float rotation_angle = m_smoothed_steering_in * max_steering_angle;
            fl.wheel_collider.steerAngle = rotation_angle;
            fr.wheel_collider.steerAngle = rotation_angle;
            UpdateWheelFromCollider(fl);
            UpdateWheelFromCollider(fr);
            UpdateWheelFromCollider(rr);
            UpdateWheelFromCollider(rl);
        }
        private void LateUpdate() {
            UpdateWheelFromCollider(fl);
            UpdateWheelFromCollider(fr);
            UpdateWheelFromCollider(rr);
            UpdateWheelFromCollider(rl);
        }
        private void UpdateWheelFromCollider(Wheel wheel){
            wheel.wheel_collider.GetWorldPose(out Vector3 position, out Quaternion rotation);
            wheel.wheel_transform.position = position;
            wheel.wheel_transform.rotation = rotation;
        }
    }
}