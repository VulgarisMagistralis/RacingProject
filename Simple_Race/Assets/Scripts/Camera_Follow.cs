using UnityEngine;

public class Camera_Follow : MonoBehaviour{
    public Transform target;
	public float distance = 3.0f;
	public float height = 3.0f;
	public float damping = 10f;
	void LateUpdate () {
			Vector3 wantedPosition;
			wantedPosition = target.TransformPoint(0, height, -distance);
			transform.position = Vector3.Lerp(transform.position, wantedPosition, damping);
			transform.LookAt(target, target.up);
		}
}
