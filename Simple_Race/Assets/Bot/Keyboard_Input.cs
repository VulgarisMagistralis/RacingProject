using UnityEngine;

namespace Simple_Race.Race_Systems{
    public class Keyboard_Input : Base_Input{
        public override Input_Data GenerateInput(){
            return new Input_Data{
                Accelerate = Input.GetButton("Accelerate"),
                Brake = Input.GetButton("Brake"),
                TurnInput = Input.GetAxis("Horizontal")
            };
        }

    }


}