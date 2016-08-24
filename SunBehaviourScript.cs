using UnityEngine;
using System.Collections;

public class SunBehaviourScript : MonoBehaviour {

	private const float MINUTES_IN_24_HOURS = 1440.0f;

	private int updateCount = 0;
	public float simSpeed; // 0.4f
	private float dayLengthCount;

	private float currentTimeInMins; // this represents the number of minutes past noon that we are currently at
	private string meridiem = "PM"; // AM or PM?

	private int daysPassed = 0;

	private Rect timeRect;

	// Use this for pre-initialization
	void Awake () {
		// start simulation at noon
		this.transform.localEulerAngles = new Vector3 (90.0f, 0.0f, 0.0f);
		dayLengthCount = 360.0f/simSpeed;
		print ("Day " + daysPassed);
		timeRect = new Rect (3 * Screen.width / 4, 3 * Screen.height / 4, Screen.width / 8, Screen.height / 8);
	}

	// Use this for initialization
	void Start(){

	}


	// Update is called once per frame
	void Update () {
		// after 900 iterations, we are back to noon
		updateCount += 1;
		this.transform.Rotate (new Vector3 (simSpeed, 0.0f, 0.0f));
		if (updateCount >= dayLengthCount) {
			daysPassed += 1;
			print ("Day " + daysPassed);
			updateCount = 0;
		}
	}

	public float getDayLengthCount(){
		return dayLengthCount;
	}

	void OnGUI(){
		float dayLengthCountToMins = (24 * 60) / dayLengthCount;
		currentTimeInMins = (updateCount * dayLengthCountToMins)%(24*60);
		//print (currentTimeInMins);
		float hour = currentTimeInMins / 60;
		float mins = (hour - Mathf.Floor (hour)) * 60;

		hour = (int) Mathf.Floor(hour);
		mins = (int) Mathf.Floor (mins);

		hour = (hour + 12) % 24;

//		if (hour == 0) {
//			hour = 12;
//		}
//		if (hour > 12) {
//			hour -= 12;
//		}

		if (hour <= 11) {
			meridiem = "AM";
		}
		else {
			meridiem = "PM";
		}

		string h = hour.ToString ("00");
		string m = mins.ToString("00");

		string time = "" + h + ":" + m;
		GUI.Box (timeRect, time + " (" + meridiem + ")");
		//Debug.Log (time + " (" + meridiem + ")");
	}

	public float getCurrentMinsPastNoon(){
		return currentTimeInMins;
	}

	public string getCurrentMeridiem(){
		return meridiem;
	}

	public float getTimeCosineCoord(float minute){
		float val = Mathf.Cos (minute * (2 * Mathf.PI / MINUTES_IN_24_HOURS));
		if (val < 0.0000001f && val > -0.0000001f) {
			val = 0;
		}
		return val;
	}

	public float getTimeSineCoord(float minute){
		float val = Mathf.Sin (minute * (2 * Mathf.PI / MINUTES_IN_24_HOURS));
		if (val < 0.0000001f && val > -0.0000001f) {
			val = 0;
		}
		return val;
	}
}
