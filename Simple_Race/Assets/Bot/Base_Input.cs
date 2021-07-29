using UnityEngine;

namespace Simple_Race.Race_Systems{
    public struct Input_Data{
        public bool Accelerate;
        public bool Brake;
        public float TurnInput;
    }
    public interface I_Input{ Input_Data GenerateInput();}
    public abstract class Base_Input : MonoBehaviour, I_Input{ public abstract Input_Data GenerateInput();}
}