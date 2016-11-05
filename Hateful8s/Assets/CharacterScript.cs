using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {
	public int hp=100;
	public float jumpForce=5f;
	public float forwardSpeed=1000f;
	public float backwardSpeed=4f;
	//Way character is facing
	public bool isRight=true;

	GameObject target;
	bool onGround; 
	Rigidbody2D rBody;
	// Use this for initialization
	void Start () {
		forwardSpeed *= Time.deltaTime;
		backwardSpeed *= Time.deltaTime;
		rBody = GetComponent<Rigidbody2D> ();
		GameObject[] players=GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject g in players) {
			if (g != gameObject) {
				target = g;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (onGround) {
			//rBody.velocity = Vector2.zero;
			if (!Input.GetKey ("left") && !Input.GetKey ("right")) {

				rBody.velocity = Vector2.zero;
			}
			else if (Input.GetKeyDown ("left")) {
				moveLeft ();
			} else if (Input.GetKeyDown ("right")) {
				moveRight ();

			}
			if(Input.GetKeyDown("space")){
				jump ();
			}



		}
		if (!isTargetInFront ()) {
			flip ();
		}
			

	}
	void moveLeft(){
		if (isRight) {

			rBody.velocity = new Vector2 (-backwardSpeed, 0);

		} else {
			rBody.velocity = new Vector2 (-forwardSpeed, 0);
		}
	}
	void moveRight(){
		if (!isRight) {
			rBody.velocity = new Vector2 (backwardSpeed, 0);
		} else {
			//print ("going right");
			rBody.velocity = new Vector2 (forwardSpeed, 0);
		}
	}
	void jump(){
		rBody.velocity+=new Vector2(0,jumpForce);
		onGround = false;
	}
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Ground") {
			
			onGround = true;
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
		print ("flipped");
		isRight = !isRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
