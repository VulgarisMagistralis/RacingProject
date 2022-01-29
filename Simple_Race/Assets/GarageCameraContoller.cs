using UnityEngine;
using UnityEngine.UI;
public class GarageCameraContoller : MonoBehaviour{
    public Transform carHolder; // platform for the vehicles
    public Button prevButton, nextButton;
    private new Camera camera;
    private float zoomSpeed = 50;
    private void Awake(){camera = GetComponent<Camera>();}
    void LateUpdate(){
        // Controls for Garage Camera : Zoom[WS] / Rotate[QE] / Change[AD] 
        if(Input.GetKeyDown(KeyCode.A)) prevButton.onClick.Invoke();
        if(Input.GetKeyDown(KeyCode.D)) nextButton.onClick.Invoke();
        if(Input.GetKey(KeyCode.Q)) transform.RotateAround (carHolder.position, Vector3.up, 90 * Time.deltaTime);
        if(Input.GetKey(KeyCode.E)) transform.RotateAround (carHolder.position, Vector3.up, -90 * Time.deltaTime);
        if(Input.GetKey(KeyCode.W) && camera.fieldOfView >= 30) camera.fieldOfView = Mathf.MoveTowards(camera.fieldOfView, carHolder.position.y, zoomSpeed * Time.deltaTime);
        if(Input.GetKey(KeyCode.S) && camera.fieldOfView <= 70) camera.fieldOfView = Mathf.MoveTowards(camera.fieldOfView, carHolder.position.y, -zoomSpeed * Time.deltaTime);
    }
}