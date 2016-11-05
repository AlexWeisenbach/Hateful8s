using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ManagerSettings : MonoBehaviour {
	static Dictionary<Player,PlayerControls> dic;
	public PlayerControls[] controlDefinitions;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
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
