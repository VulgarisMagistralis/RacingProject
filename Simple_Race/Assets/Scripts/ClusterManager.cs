using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ClusterManager : MonoBehaviour{
   	public List<Training> trainingEnvironments;
    private void Awake() {
        trainingEnvironments = GetComponentsInChildren<Training>().ToList<Training>();
        trainingEnvironments[1].Disable();
        trainingEnvironments[2].Disable();        
    }
    public void AcademyTracker(){
        //Academy.Instance.EnvironmentParameters.GetWithDefault("track_no",x); // instead check game manager func

    }
    public void SelectEnvironment(int envNo){
        trainingEnvironments[envNo].Enable();
    }
}
