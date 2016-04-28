using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {

    public string newScene = "Room1-1";

    public void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
            SceneManager.LoadScene(newScene);
    }
}
