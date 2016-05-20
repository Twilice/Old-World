using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RoomChangeScript : MonoBehaviour
{
	private int counter = 0;
	private int numberOfRooms = 0;
	
	void Awake ()
	{
		numberOfRooms = SceneManager.sceneCountInBuildSettings;
		Debug.Log(counter);
	}
	
	void Update ()
	{

		if (Input.GetKeyDown(KeyCode.Return))
		{
			SceneManager.LoadScene(counter, LoadSceneMode.Single);
			Debug.Log("Loaded room: " + counter);
		}

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			counter++;
			if (counter > numberOfRooms)
			{
				counter = 0;
			}
			Debug.Log(counter);
		}
	}
}
