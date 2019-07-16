using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	// SCRIPT FOR ADJUST RESOLUTION VARIABLES
	float originalWidth = 1280;  // define here the original resolution
	float originalHeight = 720; // you used to create the GUI contents 
	private Vector3 scale;
	
	// AUDIOS SOURCE
	private AudioSource buttonSound;

	int time, timeAux;
	string scene;
	// CUSTOM GUISKINS
	public GUISkin playButtonSkin, creditsButtonSkin, soundsOnButtonSkin, soundsOffButtonSkin;
	GUISkin soundsButtonSkin;

	// Use this for initialization
	void Start () {

		scene = "";
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
		KeyBoard ();
		
		if(scene!=""){
			if((timeAux+2)==time){
				Application.LoadLevel (scene);
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
		
		if(GUI.Button(new Rect(30,250,600,300), "",playButtonSkin.button)){
			if(PlayerPrefs.GetString("Sound")!="off"){
				buttonSound.Play();
			}
			changeScene ();
			scene="LevelSelectorScene";
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
		
		if(GUI.Button(new Rect(1000,650,200,50), "", creditsButtonSkin.button)){
			if(PlayerPrefs.GetString("Sound")!="off"){
				buttonSound.Play();
			}
			changeScene ();
			scene="CreditsScene";
		}
		//...
		// restore matrix before returning
		GUI.matrix = svMat; // restore matrix
		
	}
	void changeScene(){
		timeAux = time;
	}

	void KeyBoard(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}	
	}
}
