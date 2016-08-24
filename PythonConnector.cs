using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;

public class PythonConnector : MonoBehaviour {

	private Process p;

	// Use this for initialization
	void Start () {
		connect ();
		//print("hey!" + Application.dataPath);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void connect(){
		// using System.Diagnostics;
		p = new Process();
		p.StartInfo.FileName = "python";
		p.StartInfo.Arguments = "test.py";    
		// Pipe the output to itself - we will catch this later
		p.StartInfo.RedirectStandardError=true;
		p.StartInfo.RedirectStandardOutput=true;
		p.StartInfo.CreateNoWindow = true;

		// Where the script lives
		p.StartInfo.WorkingDirectory = Application.dataPath+"/../../PredictionSample/"; 
		p.StartInfo.UseShellExecute = false;

		// exit event
		p.EnableRaisingEvents = true;
		p.Exited += new EventHandler(processExitEvent);

		p.Start();
		// Read the output - this will show is a single entry in the console - you could get  fancy and make it log for each line - but thats not why we're here
//		UnityEngine.Debug.Log( p.StandardOutput.ReadToEnd() );
//		p.WaitForExit();
//		p.Close ();
	}

	// Handle Exited event and display process information.
	private void processExitEvent(object sender, System.EventArgs e)
	{
		UnityEngine.Debug.Log( p.StandardOutput.ReadToEnd() );
	}
}
