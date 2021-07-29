using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour{
        private CarController m_Car; // the car controller we want to use
        private void Awake(){
            // get the car controller
            m_Car = GetComponent<CarController>();
        }
        private void FixedUpdate(){
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
        }
    }
}
