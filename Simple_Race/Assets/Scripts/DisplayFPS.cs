using UnityEngine;
public class DisplayFPS : MonoBehaviour{
	float deltaTime = 0.0f;
	void Update(){
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	} 
	void OnGUI(){
		int w = Screen.width, h = Screen.height;
		GUIStyle style = new GUIStyle();
		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color (0.0f, 0.0f, 0f, 0.4f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{1:0.}", msec, fps);
		GUI.Label(rect, text, style);
	}
}