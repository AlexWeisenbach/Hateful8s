using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthScript : MonoBehaviour 
{
	public CharacterScript character;
	public Image healthBar;
	public float maxSize = 1.75f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 scale = healthBar.rectTransform.localScale;
		scale.x = maxSize * (character.hp / 100f);
		healthBar.rectTransform.localScale = scale;
	}
}
