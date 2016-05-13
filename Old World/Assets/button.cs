using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void push()
	{
		SceneManager.LoadScene("Room1-1");
	}
}
