using UnityEngine;
using UnityEngine.UI;
namespace UnityStandardAssets.Vehicles.Car{
	public class UIController : MonoBehaviour{
		public GameObject race_panel;
		public Text text_current_lap_count;
		public Text text_current_lap_time;
		public Text text_best_lap;
		public Text text_last_lap;
		public Player update_UI;
		private int current_lap_count = -1;
		private float current_lap_time;
		private float last_lap;
		private float best_lap;
		// Update is called once per frame
		void Update(){
			if(update_UI == null) return;
			if(update_UI.current_lap_count != current_lap_count){
				current_lap_count = update_UI.current_lap_count;
				text_current_lap_count.text = $"LAP : {current_lap_count}";
			}
			if(update_UI.current_lap_time != current_lap_time){
				current_lap_time = update_UI.current_lap_time;
				text_current_lap_time.text = $"TIME : {(int)current_lap_time/60}:{(current_lap_time) %60 : 00.000}";
			}
			if(update_UI.last_lap_time != last_lap){
				last_lap = update_UI.last_lap_time;
				text_last_lap.text = $"LAST : {(int)last_lap/60}:{(last_lap) %60 : 00.000}";
			}
			if(update_UI.best_lap_time != best_lap){
				best_lap = update_UI.best_lap_time;
				text_best_lap.text =best_lap < 100000 ? $"BEST : {(int)best_lap/60}:{(best_lap) %60 : 00.000}" : "BEST : N/A";
			}
		}
	}
}