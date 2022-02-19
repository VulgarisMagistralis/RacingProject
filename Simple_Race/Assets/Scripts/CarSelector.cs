using UnityEngine;

public class CarSelector : MonoBehaviour{
    private int currentCar;
    public Transform platformCenter;
    private void Start() {
        SelectCar(0);
    }
    private void SelectCar(int carIndex){
        for(int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).gameObject.transform.localPosition = platformCenter.localPosition;
            transform.GetChild(i).gameObject.transform.rotation = platformCenter.rotation;
            transform.GetChild(i).gameObject.SetActive(i == carIndex);
        }
    }
    public void ChangeCar(int indexChange){
        currentCar += indexChange;
        if(currentCar < 0) currentCar = transform.childCount-1;
        else if(currentCar >= transform.childCount) currentCar = 0;
        SelectCar(currentCar);
    }
}
