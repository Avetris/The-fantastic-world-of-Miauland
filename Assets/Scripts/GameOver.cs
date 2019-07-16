using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	// SCRIPT FOR ADJUST RESOLUTION VARIABLES
	float originalWidth = 1280;  // define here the original resolution
	float originalHeight = 720; // you used to create the GUI contents 
	private Vector3 scale;
	
	// AUDIOS SOURCE
	private AudioSource buttonSound;
		
	// CUSTOM GUISKINS
	public GUISkin playButtonSkin, backButtonSkin, soundsOnButtonSkin, soundsOffButtonSkin, puntuationTextAreaSkin;
	GUISkin soundsButtonSkin;

	string puntuation;
	string scene;

	int time,timeAux;
	// Use this for initialization
	void Start () {
		scene = "";
		puntuation = PlayerPrefs.GetInt ("Puntuation").ToString ();
		buttonSound = gameObject.GetComponent<AudioSource>();
		if (PlayerPrefs.GetString ("Sound") == "off") {
			soundsButtonSkin = soundsOffButtonSkin;
		}else{
			soundsButtonSkin = soundsOnButtonSkin;
		}
		InvokeRepeating ("AddTime", 0.5f, 0.5f);
		
	}

	void AddTime(){
		time++;
	}
	
	// Update is called once per frame
	void Update () {
		if(scene!=""){
			if((timeAux+2)==time){
				Application.LoadLevel (scene);
			}
		}
		KeyBoard ();
	}

	void KeyBoard(){
		if((Input.GetKeyDown(KeyCode.R)) || (Input.GetKeyDown(KeyCode.Space))){
			if(PlayerPrefs.GetString("Sound")!="off"){
				buttonSound.Play();
			}
			PlayerPrefs.SetInt ("Puntuation",0);
			changeScene ();	
			scene="LevelScene";
		}else if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
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
						
		if(GUI.Button(new Rect(380,280,540,360), "", playButtonSkin.button)){
			if(PlayerPrefs.GetString("Sound")!="off"){
				buttonSound.Play();
			}
			PlayerPrefs.SetInt ("Puntuation",0);
			changeScene ();	
			scene="LevelScene";
		}
		if(GUI.Button(new Rect(30,600,200,90), "", soundsButtonSkin.button)){
			if(PlayerPrefs.GetString ("Sound")=="off"){
				PlayerPrefs.SetString("Sound","on");
				soundsButtonSkin = soundsOnButtonSkin;
			}else{
				PlayerPrefs.SetString("Sound","off");
				soundsButtonSkin = soundsOffButtonSkin;
			}
		}
		
		if(GUI.Button(new Rect(1040,600,180,120), "", backButtonSkin.button)){
			if(PlayerPrefs.GetString("Sound")!="off"){
				buttonSound.Play();
			}
			PlayerPrefs.SetInt ("Puntuation",0);
			changeScene ();	
			scene="MainMenuScene";
		}

		
		GUI.contentColor = Color.black;
		GUI.backgroundColor = Color.clear;
		GUI.Label(new Rect(550, 200, 180, 80), puntuation, puntuationTextAreaSkin.textArea);
		//...
		// restore matrix before returning
		GUI.matrix = svMat; // restore matrix
		
	}
	void changeScene(){
		timeAux = time;
	}
}
