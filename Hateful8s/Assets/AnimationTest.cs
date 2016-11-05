using UnityEngine;
using System.Collections;

public class AnimationTest : MonoBehaviour {
	public	Animator anim;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("d"))
		{
			anim.SetBool("WalkF", true);
			anim.SetBool("Neutral", false);
		}
		if(Input.GetKeyUp("d"))
		{
			anim.SetBool("WalkF", false);
			anim.SetBool("Neutral", true);
		}
		if(Input.GetKey("a"))
		{
			anim.SetBool("WalkB", true);
			anim.SetBool("Neutral", false);
		}
		if(Input.GetKeyUp("a"))
		{
			anim.SetBool("WalkB", false);
			anim.SetBool("Neutral", true);
		}
		if(Input.GetKeyDown("space"))
		{
			anim.SetBool("Punching", true);
		}
		if(Input.GetKeyUp("space"))
		{
			anim.SetBool("Punching", false);
		}
		if(Input.GetKey("w"))
		{
			anim.SetTrigger("Jump");		
		}
	}
}
