using UnityEngine;
using System.Collections;

public class OneTimeBark : MonoBehaviour {


    bool hasBarked = false;
    public string barkName = "event:/Character/Plopp";
    public float delay = 0;
    FMOD.Studio.EventInstance barkToPlay;
    private GameObject player;
    void Awake()
    {
        player = GameObject.Find("Player");
        barkToPlay = FMODUnity.RuntimeManager.CreateInstance(barkName);
    }

    void OnTriggerEnter()
    {
        if(hasBarked == false)
        {
            hasBarked = true;
            Invoke("PlayBark", delay);
        }
    }

    void Update()
    {
        barkToPlay.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(player));
    }

    void PlayBark()
    {
      
        barkToPlay.start();
    }
    
}
