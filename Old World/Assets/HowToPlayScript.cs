using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HowToPlayScript : MonoBehaviour
{
	public List<GameObject> HTP_Objects;
	
	private bool active = false;

	void Awake()
	{
		for (int i = 0; i < HTP_Objects.Count; i++)
		{
			HTP_Objects[i].SetActive(false);
		}
	}

    void Start()
    {
        if(StateController.GameOn == true && StateController.howToPlayPrompt)
            Activate();
        else if(StateController.GameOn)
        {
            GameObject.Find("Menu").GetComponent<MenuScript>().ResumeGame();
        }
    }

    public void Activate()
    {
        for (int i = 0; i < HTP_Objects.Count; i++)
        {
            HTP_Objects[i].SetActive(true);
        }
        active = true;
    }

    void Update()
    {
        if (StateController.GameOn == false && Input.GetButtonDown("IntroSkip"))
        {
            Deactivate();
        }
    }

    public void Deactivate()
    {
        if(StateController.howToPlayPrompt)
        {
            StateController.howToPlayPrompt = false;
            if (StateController.GameOn) GameObject.Find("Menu").GetComponent<MenuScript>().ResumeGame();
        }

        for (int i = 0; i < HTP_Objects.Count; i++)
        {
            HTP_Objects[i].SetActive(false);
        }
        active = false;
    }
}
