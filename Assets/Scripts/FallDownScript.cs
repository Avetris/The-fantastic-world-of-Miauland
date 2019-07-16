using UnityEngine;
using System.Collections;

public class FallDownScript : MonoBehaviour {

	// SCRIPT FOR ADJUST RESOLUTION VARIABLES
	float originalWidth = 1280;  // define here the original resolution
	float originalHeight = 720; // you used to create the GUI contents 
	private Vector3 scale;

	// AUDIOS SOURCE
	private AudioSource buttonSound;

	int clicks, maxClicks, percent25, percent50, percent75;
	int time;

	public Sprite perc25, perc50, perc75;

	// Use this for initialization
	void Start () {
		clicks = 0;
		time = 0;
		if(PlayerPrefs.GetInt("Puntuation")>=500){
			maxClicks = Random.Range (35, 42);
		}else{
			maxClicks = Random.Range (25, 35);
		}
		percent25 = maxClicks/4;
		percent50 = maxClicks/2;
		percent75 = percent25*3;
		InvokeRepeating ("Time", 1, 1);
	}

	void change(){
		if(clicks==percent25){			
			GetComponent<SpriteRenderer>().sprite = perc25;
		}else if(clicks==percent50){			
			GetComponent<SpriteRenderer>().sprite = perc50;
		}else if(clicks==percent75){			
			GetComponent<SpriteRenderer>().sprite = perc75;
		}
	}

	// Update is called once per frame
	void Update () {
		KeyBoard ();
		change ();
		if(clicks==maxClicks){
			PlayerPrefs.SetInt("Puntuation",PlayerPrefs.GetInt("Puntuation")+100);
			Application.LoadLevel("LevelScene");
		}else if(time==5){
			Application.LoadLevel("GameOverScene");
		}
	}

	void Time(){
		time++;
	}


	void KeyBoard(){
		if(Input.GetKeyDown(KeyCode.Space)){
			clicks++;
		}else if(Input.GetKeyDown(KeyCode.Escape)){
			Application.LoadLevel("GameOverScene");
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
	}
}
