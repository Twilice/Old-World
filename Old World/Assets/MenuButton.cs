using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
                SceneManager.LoadScene("Introduction");
                Sfx.Play(1);
				break;
			case (1):
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Sfx.Play(1);
				break;
			case (2):
				Debug.Log("Options");
				Sfx.Play(1);
				break;
		}
		
	}
}
