using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public enum Player{
	none,
	Player1,
	Player2
}
[System.Serializable]
public class PlayerControls{
	public Player player=Player.none;
	public string moveLeft="left";
	public string moveRight="right";
	public string jump="space";
	public string attack="g";
}
public class CharacterScript : MonoBehaviour {
	public int hp=100;
	public float jumpForce=5f;
	public float forwardSpeed=400f;
	public float backwardSpeed=300f;
	public Player player=Player.none;
	//Way character is facing
	public bool isRight=true;
	public Text health;
	public Animator anim;
	public bool attacking = false;
	public AudioSource sound;
	GameObject target;
	bool onGround; 
	Rigidbody2D rBody;
	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D> ();
		GameObject[] players=GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject g in players) {
			if (g != gameObject) {
				target = g;
			}
		}
		Physics2D.IgnoreCollision (transform.FindChild ("Right").GetComponent<CircleCollider2D> (), GetComponent<BoxCollider2D> ());
		Physics2D.IgnoreCollision (transform.FindChild ("Left").GetComponent<CircleCollider2D> (), GetComponent<BoxCollider2D> ());
		Physics2D.IgnoreCollision (transform.FindChild ("Body").GetComponent<BoxCollider2D> (), GetComponent<BoxCollider2D> ());
		sound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		//health.text = "HP: " + hp;
		if (onGround) {
			//rBody.velocity = Vector2.zero;
			if ((!Input.GetKey (ManagerSettings.getControls (player).moveLeft) && !Input.GetKey (ManagerSettings.getControls (player).moveRight)) || Input.GetKey (ManagerSettings.getControls (player).attack)) {
				anim.SetBool ("WalkF", false);
				anim.SetBool ("WalkB", false);
				anim.SetBool ("Neutral", true);
				rBody.velocity = Vector2.zero;
				if (Input.GetKey (ManagerSettings.getControls (player).attack)) {
					anim.SetBool ("Punching", true);
					attacking = true;
				}
			}
			if (Input.GetKeyUp (ManagerSettings.getControls (player).attack)) {
				anim.SetBool ("Punching", false);
				attacking = false;
			} else if (Input.GetKeyDown (ManagerSettings.getControls (player).moveLeft)) {
				moveLeft ();
			} else if (Input.GetKeyDown (ManagerSettings.getControls (player).moveRight)) {
				moveRight ();

			}
			if (Input.GetKeyDown (ManagerSettings.getControls (player).jump)&&!anim.GetBool("Punching")) {
				anim.SetTrigger ("Jump");	
				jump ();

			}



		} else {
			anim.SetBool ("Land", false);
			if (Input.GetKeyDown (ManagerSettings.getControls (player).attack)) {
				anim.SetBool ("Punching", true);
				attacking = true;
			}
			if (Input.GetKeyUp (ManagerSettings.getControls (player).attack)) {
				anim.SetBool ("Punching", false);
				attacking = false;
			}
		}
		if (!isTargetInFront ()) {
			flip ();
		}
			

	}
	void moveLeft(){
		if (isRight) {
			anim.SetBool("WalkB", true);
			rBody.velocity = new Vector2 (-backwardSpeed*Time.deltaTime, 0);
		} else {
			anim.SetBool("WalkF", true);

			rBody.velocity = new Vector2 (-forwardSpeed*Time.deltaTime, 0);
		}
		anim.SetBool("Neutral", false);
	}
	void moveRight(){
		if (!isRight) {
			anim.SetBool("WalkB", true);
			rBody.velocity = new Vector2 (backwardSpeed*Time.deltaTime, 0);
		} else {
			//print ("going right");
			anim.SetBool("WalkF", true);

			rBody.velocity = new Vector2 (forwardSpeed*Time.deltaTime, 0);
		}
		anim.SetBool("Neutral", false);
	}
	void jump(){
		rBody.velocity+=new Vector2(0,jumpForce);
		onGround = false;
	}
	void OnCollisionEnter2D(Collision2D coll){
		//print (coll.gameObject.tag);
		if (coll.gameObject.tag == "Ground") {
			anim.SetBool ("Land", true);
			onGround = true;
		}
		if (coll.gameObject.tag == "Player") {
			if (coll.gameObject.GetComponent<CharacterScript> ().attacking) {
				sound.Play();
				hp -= 4;
			}
		}
	}
	bool isTargetInFront(){
		if (isRight) {
			return target.transform.position.x > transform.position.x;
		} 
		return target.transform.position.x < transform.position.x;
	}
	void setTarget(GameObject ob){
		
	}
	void flip(){
		isRight = !isRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
