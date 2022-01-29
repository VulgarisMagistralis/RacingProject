using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour{

    public void LoadScene(string scneneName){
        SceneManager.LoadScene(scneneName);
    }
    public void QuitGame(){
        Application.Quit();
    }
}

