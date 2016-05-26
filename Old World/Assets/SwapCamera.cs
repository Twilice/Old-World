using UnityEngine;
using System.Collections;

public class SwapCamera : MonoBehaviour
{

    private Camera main;
    private Camera flying;
    private GameObject player;
    // Use this for initialization
    void Start()
    {
        main = Camera.main;
        flying = GameObject.Find("FlyingCamera").GetComponent<Camera>();
        flying.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (main.enabled)
            {
                main.enabled = false;
                main.gameObject.GetComponent<CameraOrbit>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                main.gameObject.SetActive(false);
                flying.enabled = true;
                player.GetComponent<PlayerInputHandler>().enabled = false;
            }
            else
            {

                flying.enabled = false;
                main.gameObject.SetActive(true);
                main.gameObject.GetComponent<CameraOrbit>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                main.enabled = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Cursor.visible = !Cursor.visible;
        }

    }
}
