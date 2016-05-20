using UnityEngine;
using System.Collections;
//Gör så att vi kommer åt FMODUnity-relaterad kod.
using FMODUnity;

//Detta script är tänkt att ersätta StudioEventEmitter. Anledningen är för att ni ska slippa rota in i deras inbyggda
//scripts för mycket. Se info ovanför varje funktion. Det som inte har kommentarer är inget ni behöver använda.

public class EventPlayer : MonoBehaviour
{
    //I denna string som exponeras i Inspector skriver ni in sökvägen för det event eller snapshot ni vill använda t.ex.
    //event:/Ambiance/MittEvent
    public string eventName;

    //Denna bool som är exponerad i Inspector avgör om eventet eller snapshotet ska starta automatiskt när scriptet startas.
    public bool startOnAwake = true;
    private Rigidbody cachedRigidBody;


    [FMODUnity.EventRef]
    FMOD.Studio.EventInstance eventToPlay;
    FMOD.Studio.ParameterInstance paramInstance;
    //FMOD.Studio.CueInstance cueInstance; //TODO what is CueInstance? Is it important?

    void Awake()
    {
        cachedRigidBody = GetComponent<Rigidbody>();
        eventToPlay = FMODUnity.RuntimeManager.CreateInstance(eventName);
        if (startOnAwake)
        {
            eventToPlay.start();
        }
    }

    void Update()
    {
        //Denna rad kod gör så att 3D-ljud fungerar som de ska. Kan vara bra att veta om man vill skriva något liknande själv.
        //Kräver using FMODUnity; högst upp.
        if (eventToPlay != null)
        {
            eventToPlay.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject, cachedRigidBody));
        }

        //Testa funktioner enkelt genom att kommentera bort någon av funktionerna nedan och använd
        //knappen O som i Olof för att testa saker.
       /* if (Input.GetKeyDown(KeyCode.O))
        {
            PlayEvent();
            StopEvent(true);
            ChangeParameter("ExampleName", 1.0f);
            CueTrigger();
        }*/
    }

    //Genom att skapa en referens till det här scriptet i ett annat script på följande sätt:
    //public EventPlayer exampleNamne;
    //så kan ni köra nedanstående funktioner från andra scripts.
    //Fördelen med detta är att ni kan återanvända det här scriptet till så många events ni vill på olika GameObjects
    //utan att behöva ändra något i detta scriptet.


    //Denna funktion startar det event eller snapshot som ni angivit i string eventName.
    //För att köra funktionen från ett annat script skriv:
    //exampleName.PlayEvent();
    public void PlayEvent()
    {
        eventToPlay.start();
    }

    //Stoppar eventet eller snapshoten, antingen direkt(IMMIDIATE) eller med modulation(ALLOWFADEOUT).
    //För att köra funktionen från ett annat script skriv:
    //exampleName.StopEvent(true); eller exampleName.StopEvent(false);
    public void StopEvent(bool instant)
    {
        if (instant == true)
        {
            eventToPlay.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        else
        {
            eventToPlay.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    void OnDestroy()
    {
        StopEvent(true);
    }


    //Denna funktion sätter valfri parameter i eventet till ett valfritt värde.
    //För att köra funktionen från ett annat script skriv:
    //exampleName.ChangeParameter("NamnetPåDinParameter", 1.0f);
    //1.0f är såklart bara ett exempel-värde.
    public void ChangeParameter(string name, float value)
    {
        eventToPlay.getParameter(name, out paramInstance);
        paramInstance.setValue(value);
    }

    //Denna funktion triggar en Cue/Sustain point i ett event eller snapshot.
    //För att köra funktionen från ett annat script skriv:
    //exampleName.CueTrigger();
    //Har ni flera Sustain points i eventet triggas den som är aktiv just då.
   /* public void CueTrigger()
    {
        //eventToPlay.getCue("KeyOff", out cueInstance); //TODO what is CueInstance? Is it important?
        //cueInstance.trigger(); //TODO what is CueInstance? Is it important?
    }*/
}




