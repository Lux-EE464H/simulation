using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrainerScript : MonoBehaviour {

	private SunBehaviourScript sun;
	private LightBehaviourScript lighting;
	List<string> linesToWrite_blue; // each line is a datapoint for Machine Learning
	List<string> linesToWrite_green; // each line is a datapoint for Machine Learning
	List<string> linesToWrite_red; // each line is a datapoint for Machine Learning

	// Use this for initialization
	void Start () {
		sun = GameObject.Find ("Sun").GetComponent<SunBehaviourScript> ();
		lighting = GameObject.Find ("Lighting").GetComponentInChildren<LightBehaviourScript> ();
		linesToWrite_blue = new List<string> ();
		linesToWrite_green = new List<string> ();
		linesToWrite_red = new List<string> ();
	}
	
	// Update is called once per frame
	void Update () {
		//linesToWrite.Add ("" + lighting.getCurrentLightConfiguration () + "," + sun.getCurrentMinsPastNoon () + "," + sun.getCurrentMeridiem());
		float theTime = sun.getCurrentMinsPastNoon();
		float timeCos = sun.getTimeCosineCoord (theTime);
		float timeSin = sun.getTimeSineCoord (theTime);
		//print (timeCos + "," + timeSin);
		string theMeridiem = sun.getCurrentMeridiem();
		linesToWrite_blue.Add("" + lighting.getCurrentLightConfiguration_Blue() + "," + timeCos + "," + timeSin + "," + theMeridiem);
		linesToWrite_green.Add("" + lighting.getCurrentLightConfiguration_Green() + "," + timeCos + "," + timeSin + "," + theMeridiem);
		linesToWrite_red.Add("" + lighting.getCurrentLightConfiguration_Red() + "," + timeCos + "," + timeSin + "," + theMeridiem);
	}

	void OnApplicationQuit(){
		Debug.Log ("Writing training data...");
		//System.IO.File.WriteAllLines (Application.dataPath + "/../LuxTraining_Blue.txt", linesToWrite_blue.ToArray());
		//System.IO.File.WriteAllLines (Application.dataPath + "/../LuxTraining_Green.txt", linesToWrite_green.ToArray());
		System.IO.File.WriteAllLines (Application.dataPath + "/../LuxTraining_Red.txt", linesToWrite_red.ToArray());
		Debug.Log ("...done");
	}
}
