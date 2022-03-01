using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class NewUIController : MonoBehaviour{
    private Camera mainCamera;
    private Vector3 initialCameraPosition;
    private float cameraZoomingDuration = 15f, elapsedTime;
    private Vector3 targetCameraPosition = new Vector3(4.68f,3.91f,-9.55f);
    private bool cameraMoveTriggered = false;
    private Label playerName;
    public Button careerButton, newPlayerButton, quickRaceButton, optionsButton, quitButton, optionsBackButton, createButton, carreerBackButton;
    public GroupBox menuGroupBox, optionsGroupBox, careerGroupBox;
    private List<GroupBox> groupBoxes = new List<GroupBox>();
    private void Start() {
        mainCamera = Camera.main;
        initialCameraPosition = mainCamera.transform.position;
        var root = GetComponent<UIDocument>().rootVisualElement; // ui tree root
        //Button elements
        optionsBackButton = root.Q<Button>("OptionsBackButton");
        carreerBackButton = root.Q<Button>("CareerBackButton");
        newPlayerButton = root.Q<Button>("NewPlayerButton");
        quickRaceButton = root.Q<Button>("QuickRaceButton");
        optionsButton = root.Q<Button>("OptionsButton");
        careerButton = root.Q<Button>("CareerButton");
        createButton = root.Q<Button>("CreateButton");
        quitButton = root.Q<Button>("QuitButton"); 
        playerName = root.Q<Label>("PlayerName");       
        //define button clicked behaviour
        quitButton.clicked += QuitButtonPressed;
        careerButton.clicked += CareerButtonPressed;
        optionsButton.clicked += OptionsButtonPressed;
        quickRaceButton.clicked += QuickRaceButtonPressed;
        newPlayerButton.clicked += NewPlayerButtonPressed;
        carreerBackButton.clicked += CareerBackButtonPressed;
        optionsBackButton.clicked += OptionsBackButtonPressed;
        //Group Box elements
        menuGroupBox = root.Q<GroupBox>("MenuGroupBox");
        careerGroupBox = root.Q<GroupBox>("CareerGroupBox");
        optionsGroupBox = root.Q<GroupBox>("OptionsGroupBox");
        groupBoxes.Add(menuGroupBox);
        groupBoxes.Add(careerGroupBox);
        groupBoxes.Add(optionsGroupBox);
        DisableOtherGroupBoxes(menuGroupBox);
        playerName.text = "asdasda"; //testing
    }
    private void FixedUpdate(){ //very stupid
        if(cameraMoveTriggered){
            elapsedTime += Time.deltaTime;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetCameraPosition,elapsedTime/cameraZoomingDuration);
        }else{
            elapsedTime += Time.deltaTime;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, initialCameraPosition,elapsedTime / cameraZoomingDuration);
        }
    }
    private void CareerButtonPressed(){
        //display all profiles
        elapsedTime = 0;
        cameraMoveTriggered = true;
        DisableOtherGroupBoxes(careerGroupBox);
    }
    private void NewPlayerButtonPressed(){
        //popup name writer

    }
    private void QuickRaceButtonPressed(){
        SceneManager.LoadScene("GarageScene");
    }
    private void OptionsBackButtonPressed(){
        DisableOtherGroupBoxes(menuGroupBox);
    }
    private void CareerBackButtonPressed(){
        elapsedTime = 0;
        cameraMoveTriggered = false;
        DisableOtherGroupBoxes(menuGroupBox);
    }
    private void DisableOtherGroupBoxes(GroupBox activeGroupBox){
        activeGroupBox.style.display = DisplayStyle.Flex;
        foreach(GroupBox gb in groupBoxes) if(gb.name != activeGroupBox.name) gb.style.display = DisplayStyle.None;
    }
    private void OptionsButtonPressed(){DisableOtherGroupBoxes(optionsGroupBox);}
    private void QuitButtonPressed(){Application.Quit();}
}