using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {

    public Rooms newScene = Rooms.Room1_2;

    public void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (StateController.currentRoom.Equals(newScene) == false && StateController.loading == false)
            {
                LoadFade fade = GameObject.Find("_Camera").GetComponent<LoadFade>();
                //fade.FadeToBlack();
                StartCoroutine(StateController.LoadScene(newScene));
            }
        }
    }
}
