using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CorridorScript : MonoBehaviour {

    public string startScene = "Hub";

	void Start () {
        SceneManager.LoadScene(startScene);

    }
	
}
