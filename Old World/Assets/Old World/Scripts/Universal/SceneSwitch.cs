using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {

    public Rooms newScene = Rooms.Room1_2;

    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("startload");
            StartCoroutine(StateController.LoadScene(newScene));
            //SceneManager.LoadScene(newScene);
        }
    }
}
