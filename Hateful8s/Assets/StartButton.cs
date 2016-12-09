using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}

	public void OnMouseDown()
	{
		SceneManager.LoadScene("Jared's Test Scene");
	}
}
