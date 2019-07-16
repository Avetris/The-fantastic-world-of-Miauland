using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	bool velocityIncrease;
	float velocity;
	public GameObject player;
	bool destroy;
	// Use this for initialization
	void Start () {
		destroy = false;
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Destroy ();
		velocity =player.GetComponent<LevelScript> ().getVelocity ();
		if(!player.GetComponent<LevelScript>().getStand()){
			transform.Translate (-Vector2.up*Time.deltaTime*velocity);
		}
		if(transform.position.y<=-40){
			DestroyImmediate(gameObject);
		}
	}
	void OnTriggerEnter(Collider other){
		if(gameObject.tag=="Laser"){
			if(other.tag=="Wall" || other.tag=="JumpWall" || other.tag=="HoleWall"){
				destroy=true;
			}else if(other.tag =="GameController"){
				destroy=true;
			}
		}
	}

	void OnTriggerStay(Collider other){
		if(gameObject.tag=="Laser"){
			if(other.tag=="Wall" || other.tag=="JumpWall" || other.tag=="HoleWall"){
				destroy=true;
			}else if(other.tag =="GameController"){
				destroy=true;
			}
		}
	} 
	void Destroy(){
		if(destroy){
			DestroyImmediate(gameObject);
		}
	}
}
