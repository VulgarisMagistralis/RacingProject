using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track_Way_Points : MonoBehaviour{
	public Color line_colour;
	public List<Transform> points = new List<Transform>();
	private void OnDrawGizmosSelected() {
		Gizmos.color = line_colour;
		points = new List<Transform>();
		Transform[] path = GetComponentsInChildren<Transform>();
		for(int i = 1; i < path.Length; ++i) points.Add(path[i]);
		Vector3 previous_point;
		Vector3 current_point;
		for(int i = 0; i < points.Count; ++i){
			current_point = points[i].position;
			previous_point = (i == 0 ? points[points.Count-1].position : points[i - 1].position);
			Gizmos.DrawLine(previous_point, current_point);
			Gizmos.DrawSphere(current_point, 1);
		}
	}
}
