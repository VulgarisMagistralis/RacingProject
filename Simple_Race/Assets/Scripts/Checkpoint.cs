using System.Text.RegularExpressions;
using System;
using UnityEngine;
namespace Simple_Race{
    public class Checkpoint : MonoBehaviour{
        public int checkpointIndex;
        private void Awake(){
            checkpointIndex = Int32.Parse(Regex.Match(name,@"\d+").Value);
        }
        public void EnableCollider(){

        }
        public void DisableCollider(){

        }
    }
}