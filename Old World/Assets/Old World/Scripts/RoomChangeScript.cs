using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RoomChangeScript : MonoBehaviour
{
	private int counter = 0;
	private Scene[] rooms;
	
	void Awake ()
	{
		rooms = SceneManager.GetAllScenes();
		Debug.Log(rooms[counter].ToString());
		Debug.Log(rooms.Length);
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			counter++;
			if (counter > rooms.Length)
			{
				counter = 0;
			}
			Debug.Log(counter);
			Debug.Log(rooms[counter].ToString());
			
		}

		if (Input.GetKeyDown(KeyCode.Return))
		{
			SceneManager.LoadScene(rooms[counter].ToString());
		}
	}
}
