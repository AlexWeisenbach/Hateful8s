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
	public float forwardSpeed=1000f;
	public float backwardSpeed=4f;
	public Player player=Player.none;
	//Way character is facing
	public bool isRight=true;
	public Text health;

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
		health.text = "HP: " + hp;
		if (onGround) {
			//rBody.velocity = Vector2.zero;
			if (!Input.GetKey (ManagerSettings.getControls(player).moveLeft) && !Input.GetKey (ManagerSettings.getControls(player).moveRight)) {

				rBody.velocity = Vector2.zero;
			}
			else if (Input.GetKeyDown (ManagerSettings.getControls(player).moveLeft)) {
				moveLeft ();
			} else if (Input.GetKeyDown (ManagerSettings.getControls(player).moveRight)) {
				moveRight ();

			}
			if(Input.GetKeyDown(ManagerSettings.getControls(player).jump)){
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
		isRight = !isRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
