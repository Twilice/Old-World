using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

	public int i;
	sfx Sfx;
	// Use this for initialization
	void Start () {
		Sfx = GetComponent<sfx>();
	}


	public void Push()
	{
		switch (i)
		{
			case (0):
				Debug.Log("StartGame");
				Sfx.Play(1);
				break;
			case (1):
				Debug.Log("Quit");
				Sfx.Play(1);
				break;
			case (2):
				Debug.Log("Options");
				Sfx.Play(1);
				break;
		}
		
	}
}
