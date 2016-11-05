using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class ManagerSettings : MonoBehaviour {
	static Dictionary<Player,PlayerControls> dic;
	public PlayerControls[] controlDefinitions;
	GameObject[] characters;
	// Use this for initialization
	void Start () {
		characters=GameObject.FindGameObjectsWithTag ("Player");
		//Physics2D.IgnoreCollision (characters[0].transform.FindChild("Left").GetComponent<CircleCollider2D>(),characters[1].transform.FindChild("Left").GetComponent<CircleCollider2D>());
		//Physics2D.IgnoreCollision (characters[1].transform.FindChild("Right").GetComponent<CircleCollider2D>(),characters[0].transform.FindChild("Right").GetComponent<CircleCollider2D>());

	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject g in characters) {
			if (g.GetComponent<CharacterScript> ().hp <= 0) {
				SceneManager.LoadScene (g.name+" scene");
				break;
			}
		}
	}
	public static PlayerControls getControls(Player player){
		if (dic.ContainsKey (player)) {
			return dic [player];
		}
		return new PlayerControls ();
	}
	void Awake(){
		dic = new Dictionary<Player,PlayerControls> ();
		foreach (PlayerControls p in controlDefinitions) {
			dic [p.player] = p;
		}
	}

}
