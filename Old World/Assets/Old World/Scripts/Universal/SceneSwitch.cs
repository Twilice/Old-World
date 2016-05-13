using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {

    public Rooms newScene = Rooms.Room1_2;

    public void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(StateController.LoadScene(newScene));
            //SceneManager.LoadScene(newScene);
        }
    }
}
