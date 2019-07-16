using UnityEngine;
using System.Collections;

public class BackgroundCicleScript : MonoBehaviour {
	
	// SCRIPT FOR ADJUST RESOLUTION VARIABLES	
	float originalWidth = 1280;  // define here the original resolution
	float originalHeight = 720; // you used to create the GUI contents 
	private Vector3 scale;

	public float y;
	float yAux;
	// Use this for initialization
	void Start () {
		scale.x = Screen.width/originalWidth; 
		scale.y = Screen.height/originalHeight;
		if(y!=0){
			y=Screen.height-scale.y;
		}
	}
	
	void OnGUI(){
		scale.x = Screen.width/originalWidth; // calculate hor scale
		scale.y = Screen.height/originalHeight; // calculate vert scale
		scale.z = 1;
		Matrix4x4 svMat = GUI.matrix; // save current matrix
		// substitute matrix - only scale is altered from standard
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		// draw your GUI controls here:
		//...
		
		guiTexture.pixelInset = new Rect (0.5f, y, 1, 1);

		GUI.matrix = svMat;
		
	}
	// Update is called once per frame
	void Update () {
		y = y - Time.deltaTime*1700;
		if(y<=-Screen.height){
			y=Screen.height-scale.y;
		}
	}
}
