using UnityEngine;
using System.Collections;

public class LevelSelectScript : MonoBehaviour {
	
	// SCRIPT FOR ADJUST RESOLUTION VARIABLES
	float originalWidth = 1280;  // define here the original resolution
	float originalHeight = 720; // you used to create the GUI contents 
	private Vector3 scale;
	
	// AUDIOS SOURCE
	private AudioSource buttonSound;
		
	// CUSTOM GUISKINS
	public GUISkin levelAvailableButtonSkin, levelDisavailableButtonSkin, puntuationTextAreaSkin;
	GUISkin level1ButtonSkin,level2ButtonSkin, level3ButtonSkin;

	string puntuation1, puntuation2, puntuation3;
	string scene;

	int time,timeAux;
	// Use this for initialization
	void Start () {
		puntuation1 = PlayerPrefs.GetInt("Puntuation1").ToString ();
		puntuation2 = PlayerPrefs.GetInt("Puntuation2").ToString ();
		puntuation3 = PlayerPrefs.GetInt("Puntuation3").ToString ();
		PlayerPrefs.SetString("Level2","available");
			PlayerPrefs.SetString("Level3","available");
		scene = "";
		buttonSound = gameObject.GetComponent<AudioSource>();
		level1ButtonSkin = levelAvailableButtonSkin;
		if(PlayerPrefs.GetString("Level2")=="available"){
			level2ButtonSkin=levelAvailableButtonSkin;
		}else{
			level2ButtonSkin=levelDisavailableButtonSkin;
		}
		if(PlayerPrefs.GetString("Level3")=="available"){
			level3ButtonSkin=levelAvailableButtonSkin;
		}else{
			level3ButtonSkin=levelDisavailableButtonSkin;
		}

		InvokeRepeating("AddTime", 0.5f,0.5f);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(scene!=""){
			if((timeAux+2)==time){
				Application.LoadLevel(scene);
			}
		}
		KeyBoard ();
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
		
		if(GUI.Button(new Rect(115,275,170,170), "", level1ButtonSkin.button)){
			if(PlayerPrefs.GetString("Sound")!="off"){
				buttonSound.Play();
			}
			PlayerPrefs.SetInt("Level",1);
			PlayerPrefs.SetInt("Puntuation",0);
			changeScene ();
			scene="LevelScene";
		}
		if(GUI.Button(new Rect(525,275,170,170), "", level2ButtonSkin.button)){
			if(PlayerPrefs.GetString ("Sound")=="off"){
				buttonSound.Play();
			}
			if(PlayerPrefs.GetString("Level2")=="available"){
				PlayerPrefs.SetInt("Level",2);
				PlayerPrefs.SetInt("Puntuation",0);
				changeScene ();
				scene="LevelScene";
			}
		}
		if(GUI.Button(new Rect(950,275,170,170), "", level3ButtonSkin.button)){
			if(PlayerPrefs.GetString("Sound")!="off"){
				buttonSound.Play();
			}
			if(PlayerPrefs.GetString("Level3")=="available"){
				PlayerPrefs.SetInt("Level",3);
				PlayerPrefs.SetInt("Puntuation",0);
				changeScene ();
				scene="LevelScene";
			}
		}
		
		GUI.contentColor = Color.black;
		GUI.backgroundColor = Color.clear;
		GUI.Label(new Rect(115, 500, 1000, 80), puntuation1, puntuationTextAreaSkin.textArea);
		GUI.Label(new Rect(525, 500, 1000, 80), puntuation2, puntuationTextAreaSkin.textArea);
		GUI.Label(new Rect(960, 500, 1000, 80), puntuation3, puntuationTextAreaSkin.textArea);
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

	void KeyBoard(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}	
	}
}
