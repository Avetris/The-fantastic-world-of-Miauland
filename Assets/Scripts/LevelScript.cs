using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {
	// SCRIPT FOR ADJUST RESOLUTION VARIABLES
	float originalWidth = 1280;  // define here the original resolution
	float originalHeight = 720; // you used to create the GUI contents 
	private Vector3 scale;

	int level;
	public GameObject tutorial1, tutorial2,lasr, part1_1,part1_2,part1_3,part1_4,part1_5,part1_6,part2_1,part2_2,part2_3,part2_4,part2_5,part2_6,part2_7,part2_8,part3_1,part3_2,part3_3,part3_4,part3_5,part3_6,part3_7,part3_8, part3_9;
	GameObject[] list = new GameObject[10];

	public GUISkin puntuationTextSkin;

	int time, timeAux;
	int puntuation;
	int extra;
	
	bool stand;
	bool laser, laser2;
	bool ball;
	bool kill;

	Animator anim;

	AudioSource buttonSound;
	public AudioClip lvl1,lvl2,lvl3,bonus;

	float velocity, velocityAux;
	Sprite spr;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		velocity = 5;		 
		kill = false;
		stand = false;
		laser = false;
		buttonSound = gameObject.GetComponent<AudioSource> ();
		level = PlayerPrefs.GetInt ("Level");
		if(level==1){
			buttonSound.clip = lvl1;	
			list[0]=part1_1;
			list[1]=part1_2;
			list[2]=part1_3;
			list[3]=part1_4;
			list[4]=part1_5;
			list[5]=part1_6;			
			Instantiate (part1_1, new Vector3 (0, 20,0.64333f), Quaternion.identity);
			Instantiate (tutorial1, new Vector3 (0, 10,0.64333f), Quaternion.identity);
		}else if(level==2){			
			list[0]=part2_1;
			list[1]=part2_2;
			list[2]=part2_3;
			list[3]=part2_4;
			list[4]=part2_5;
			list[5]=part2_6;
			list[6]=part2_7;
			list[7]=part2_8;
			Instantiate (part2_1, new Vector3 (0, 20,0.64333f), Quaternion.identity);
			Instantiate (tutorial2, new Vector3 (0, 10,0.64333f), Quaternion.identity);
			buttonSound.clip = lvl2;
		}else if(level==3){			
			list[0]=part3_1;
			list[1]=part3_2;
			list[2]=part3_3;
			list[3]=part3_4;
			list[4]=part3_5;
			list[5]=part3_6;
			list[6]=part3_7;
			list[7]=part3_8;
			list[8]=part3_9;
			if(PlayerPrefs.GetInt("Puntuation")==0){
				Instantiate (part3_1, new Vector3 (0, 15,0.64333f), Quaternion.identity);
			}
			buttonSound.clip = lvl3;
		}
		if(PlayerPrefs.GetString ("Sound")!="off"){
			buttonSound.Play();
		}

		if(PlayerPrefs.GetInt("Time")>=0){
			time=PlayerPrefs.GetInt("Time");

		}else{
			time=0;
		}
		if(PlayerPrefs.GetInt("Puntuation")>=0){
			puntuation=PlayerPrefs.GetInt("Puntuation");
		}else{
			puntuation=0;
		}
		ball = false;
		InvokeRepeating ("changeVelocity", 1f, 10f);
		InvokeRepeating ("CreateLaser", 5f, 3f);
		InvokeRepeating ("Create", 5f, 7f);
		InvokeRepeating ("Time", 1f, 1f);
		InvokeRepeating ("Puntuation", 1f, 1f);
						
	}
	
	// Update is called once per frame
	void Update () {
		KeyBoard ();
		Laser ();
		changeVelocityBall ();
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
		
		GUI.contentColor = Color.red;
		GUI.backgroundColor = Color.clear;
		GUI.Label (new Rect (50, 50, 1000, 100), puntuation.ToString(),puntuationTextSkin.textArea);
	}

	public bool getStand(){
		bool ret = false;
		if(stand || kill){
			ret = true;
		}
		return ret;
	}

	void CreateLaser(){
		int x = Random.Range(1, 3);
		if(x==2){			
			Instantiate (lasr, new Vector3 (Random.Range(-8,9), 20,0.64333f), Quaternion.identity);
		}
	}

	void Create(){
		if(!stand){
			if(PlayerPrefs.GetInt ("Level")==1){
				Instantiate (list [Random.Range(0,6)], new Vector3 (0, 12,0.64333f), Quaternion.identity);
			}else if(PlayerPrefs.GetInt ("Level")==2){
				Instantiate (list [Random.Range(0,8)], new Vector3 (0, 12,0.64333f), Quaternion.identity);
			}else if(PlayerPrefs.GetInt ("Level")==3){
				Instantiate (list [Random.Range(1,9)], new Vector3 (0, 12,0.64333f), Quaternion.identity);
			}
		}
	}
	void Time(){
		if(!stand){
			time++;
		}
	}
	
	void Puntuation(){
		if(!stand && !laser2){
			puntuation=puntuation+10;
		}
	}
	void End(){
		if(PlayerPrefs.GetInt ("Level")==1){
			if(PlayerPrefs.GetInt("Puntuation1")<puntuation){
				PlayerPrefs.SetInt("Puntuation1",puntuation);
			}
		}else if(PlayerPrefs.GetInt ("Level")==2){
			if(PlayerPrefs.GetInt("Puntuation2")<puntuation){
				PlayerPrefs.SetInt("Puntuation2",puntuation);
			}
		}else if(PlayerPrefs.GetInt ("Level")==3){
			if(PlayerPrefs.GetInt("Puntuation3")<puntuation){
				PlayerPrefs.SetInt("Puntuation3",puntuation);
			}
		}
		Application.LoadLevel ("GameOverScene");
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Wall") {
			if(!laser){				
				if(!laser2){
					kill=true;
					anim.SetBool ("Kill",true);
					PlayerPrefs.SetInt("Puntuation",puntuation);
				}
			}
		}else if(other.tag == "JumpWall"){
			if(!laser){
				if(!laser2){
					PlayerPrefs.SetInt("Time",time);
					PlayerPrefs.SetInt("Puntuation",puntuation);
					Application.LoadLevel("FallDownScene");
				}
			}
		}else if(other.tag == "HoleWall"){
			if(!laser){
				if(!ball){					
					if(!laser2){
						kill=true;
						anim.SetBool ("Kill",true);
						PlayerPrefs.SetInt("Puntuation",puntuation);
					}
				}
			}
		}else if(other.tag == "Laser"){
			if(!laser){
				if(!laser2){
					laser=true;
					laser2=true;
					changeMusic();
					puntuation=puntuation+50;
					DestroyObject(other);
					anim.SetBool("Laser",true);
					timeAux=time;
				}
			}
		}
	}

	void KeyBoard(){
			if(!kill){
				if(!stand && !laser){
					if((Input.GetKeyDown(KeyCode.E))||(Input.GetKeyDown(KeyCode.Space))){
						if(level>1){
							if(ball){
								ball = false;
								anim.SetBool("Bola",false);
							}else{
								ball = true;
								anim.SetBool("Bola",true);
							}
						}
					}else if((Input.GetAxis("Horizontal") > 0.3f && Input.GetAxis("Horizontal") != 0) || (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))){
						if(transform.position.x<8.8f){
							transform.Translate(Vector2.right/10);
							if(Input.GetKeyUp(KeyCode.D) && !Input.GetKeyUp(KeyCode.A)){
								transform.Translate (-Vector2.right/getVelocity());
							}
						}
					}else if((Input.GetAxis("Horizontal") < -0.3f && Input.GetAxis("Horizontal") != 0) || (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))){
						if(transform.position.x>-7.6f){
							transform.Translate(-Vector2.right/10);
							if(!Input.GetKeyUp(KeyCode.D) && Input.GetKeyUp(KeyCode.A)){
							transform.Translate (Vector2.right/getVelocity());
							}
						}
					}else if(Input.GetKeyDown(KeyCode.P)){
						anim.SetBool("Stand",true);
						stand=true;
					}else if(Input.GetKeyDown(KeyCode.Escape)){
						PlayerPrefs.SetInt("Puntuacion",puntuation);
						if(PlayerPrefs.GetInt ("Level")==1){
							
							if(PlayerPrefs.GetInt("Puntuation1")<puntuation){
								PlayerPrefs.SetInt("Puntuation1",puntuation);
							}
						}else if(PlayerPrefs.GetInt ("Level")==2){
							if(PlayerPrefs.GetInt("Puntuation2")<puntuation){
								PlayerPrefs.SetInt("Puntuation2",puntuation);
							}
						}else if(PlayerPrefs.GetInt ("Level")==3){
							if(PlayerPrefs.GetInt("Puntuation3")<puntuation){
								PlayerPrefs.SetInt("Puntuation3",puntuation);
							}
						}
						Application.LoadLevel("GameOverScene");
					}
				}else{
					if(Input.GetKeyDown(KeyCode.P)){
						if(stand){		
							anim.SetBool("Stand",false);
							stand=false;
						}else if(!stand){		
							anim.SetBool("Stand",true);
							stand=true;
						}
					}else if(Input.GetKeyDown(KeyCode.Escape)){
						PlayerPrefs.SetInt("Puntuacion",puntuation);
						if(PlayerPrefs.GetInt ("Level")==1){
							if(PlayerPrefs.GetInt("Puntuation1")<puntuation){
								PlayerPrefs.SetInt("Puntuation1",puntuation);
							}
						}else if(PlayerPrefs.GetInt ("Level")==2){
							if(PlayerPrefs.GetInt("Puntuation2")<puntuation){
								PlayerPrefs.SetInt("Puntuation2",puntuation);
							}
						}else if(PlayerPrefs.GetInt ("Level")==3){
							if(PlayerPrefs.GetInt("Puntuation3")<puntuation){
								PlayerPrefs.SetInt("Puntuation3",puntuation);
							}
						}
					Application.LoadLevel("GameOverScene");
					}
				}
		}
	}

	void Laser(){
		if(laser){
			if(time>=(timeAux+5)){
				anim.SetBool("Laser",false);
				laser=false;
				changeMusic();
				beNormal();
			}
		}else if(time>=(timeAux+8)){
			laser2=false;
		}
	}

	void LevelSucces(){
		if(puntuation >= 500){
			if(PlayerPrefs.GetInt("Level") == 1){
				PlayerPrefs.SetString("Level2", "available");
			}else if(PlayerPrefs.GetInt("Level") == 2){
				PlayerPrefs.SetString("Level3", "available");
			}
		}
	}

	void changeVelocityBall(){
		if(ball){
			velocityAux=velocity*2;
		}
	}

	void changeVelocity(){
		if (time%5 == 0){
			velocity = velocity + (float)(velocity*0.1);
		}
	}

	public float getVelocity(){
		float vel = velocity;
		if (ball) {
			vel=velocityAux;
		}
		return vel;
	}

	void beGgant(){
		transform.localPosition = new Vector3(0,-0.5f,-2);
		transform.localScale = new Vector2 (2.5f, 2.5f);
	}
	void beNormal(){
		changeMusic ();
		transform.localPosition = new Vector3 (0, -2.39f,0);
		transform.localScale = new Vector2 (1, 1);
	}

	void changeMusic(){
		buttonSound.Stop ();
		if (laser) {
			buttonSound.clip = bonus;
		}else{
			if(level==1){				
				buttonSound.clip = lvl1;
			}else if(level==2){				
				buttonSound.clip = lvl2;
			}else if(level==3){				
				buttonSound.clip = lvl3;
			}
		}
		buttonSound.Play();
	}
}