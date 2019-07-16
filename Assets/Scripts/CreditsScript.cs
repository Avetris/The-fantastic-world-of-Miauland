using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour {
	
	// SCRIPT FOR ADJUST RESOLUTION VARIABLES
	float originalWidth = 1280;  // define here the original resolution
	float originalHeight = 720; // you used to create the GUI contents 
	private Vector3 scale;
	
	// AUDIOS SOURCE
	private AudioSource buttonSound;
	
	// CUSTOM GUISKINS
	public GUISkin backButtonSkin;
	GUISkin soundsButtonSkin;

	string scene;

	int time,timeAux;
	// Use this for initialization
	void Start () {
		scene = "";
		buttonSound = gameObject.GetComponent<AudioSource>();
		InvokeRepeating ("AddTime", 0.5f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if(scene!=""){
			if((timeAux + 2)==time){
				Application.LoadLevel(scene);
			}
		}
	}
	
	void OnGUI(){
		scale.x = Screen.width/originalWidth; // calculate hor scale
		scale.y = Screen.height/originalHeight; // calculate vert scale
		scale.z = 1;
		var svMat = GUI.matrix; // save current matrix
		// substitute matrix - only scale is altered from standard
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		// draw your GUI controls here:
		//...
		
		GUI.contentColor = Color.black;
		
		if(GUI.Button(new Rect(1040,600,180,120), "",backButtonSkin.button)){
			if(PlayerPrefs.GetString("Sound")!="off"){
				buttonSound.Play();
			}
			changeScene ();
			scene="MainMenuScene";
		}
		//...
		// restore matrix before returning
		GUI.matrix = svMat; // restore matrix
		
	}
	void changeScene(){
		timeAux = time;
	}

	void AddTime(){
		time++;
	}
}
