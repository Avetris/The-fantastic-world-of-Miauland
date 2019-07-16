using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {
	
	// SCRIPT FOR ADJUST RESOLUTION VARIABLES	
	float originalWidth = 1280;  // define here the original resolution
	float originalHeight = 720; // you used to create the GUI contents 
	private Vector3 scale;

	public Texture fond1, fond2, fond3;

	// Use this for initialization
	void Start () {	
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
		if(PlayerPrefs.GetInt("Level")==1){
			guiTexture.texture = fond1;
		}else if(PlayerPrefs.GetInt("Level")==2){
			guiTexture.texture = fond2;
		}else if(PlayerPrefs.GetInt("Level")==3){
			guiTexture.texture = fond3;
		}
	}
	// Update is called once per frame
	void Update () {

	}
}
