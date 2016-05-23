using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum CameraStatus
{
    ThirdPersonView,
    FirstPersonView,
    InspectView,
};

public enum ZoomStatus
{
    zoomingIn,
    zoomingOut
};


public class StateController
{
    // bara spara skit så övergången ser mer smooth utmellan scener
    public static bool savedPosition = false;
    public static Vector3 playerPos;
    public static Quaternion playerRot;
    public static Vector3 cameraPos;
    public static Quaternion cameraRot;
    public static float cameraDist;

    public static CameraStatus currentView = CameraStatus.ThirdPersonView;
    public static ZoomStatus currentZoom = ZoomStatus.zoomingOut;
    public static bool cursorLocked = true;
    public static bool menuOpen = false;
    public static Rooms currentRoom = Rooms.Room1_1;

    public static FMOD.Studio.EventInstance musicEvent;
    public static FMOD.Studio.ParameterInstance musicParameter;

    public static FMOD.Studio.EventInstance ambientEvent;
    public static FMOD.Studio.ParameterInstance ambientParameter;
    public static FMOD.Studio.EventInstance ambientRoom;
    public static FMOD.Studio.EventInstance ambientCorridor;

    //public static float parameterIncrement = 0.25f;
    public static float musicParamValue = 0;
    public static MoveLensTarget lensScript;

    private static List<string> activeTagsHub;
    private static List<string> activeTagsRoom1_1;
    private static List<string> activeTagsRoom1_2;
    private static List<string> activeTagsRoom1_3;
    private static List<string> activeTagsRoom1_4;
    private static List<string> activeTagsRoom1_5;

    private static float hubParamenter = 1f;
    private static float r1_1Paramenter = 2f;
    private static bool isInCorridor = false;
   // private float r1-2Paramenter = Xf;

    static StateController()
    {
        activeTagsHub = new List<string>();
        activeTagsRoom1_1 = new List<string>();
        activeTagsRoom1_2 = new List<string>();
        activeTagsRoom1_3 = new List<string>();
        activeTagsRoom1_4 = new List<string>();
        activeTagsRoom1_5 = new List<string>();

        musicEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Music/RoomMusic");
        ambientEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/2D/Ambient"); 
      //  ambientCorridor = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/2D/Digital");
      //  ambientRoom = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/2D/Organic");

        musicEvent.getParameter("ChangeMusic", out musicParameter);
        musicParameter.setValue(0);

        ambientEvent.getParameter("Parameter 1", out ambientParameter);
        ambientParameter.setValue(0);

        musicEvent.start();
        ambientEvent.start();
    }
    public static void LoadGame(Rooms newScene)
    {
        TurnOffMusic();
        currentRoom = newScene;
        TurnOnMusic();
     //   ambientRoom.start();

        SceneManager.LoadScene(RoomToString(newScene));
      
    }

    public static void EnterCorridor()
    {
        if (isInCorridor == false)
        {
            isInCorridor = true;
            ambientParameter.setValue(2f);
            //ambientRoom.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            // ambientCorridor.start();
            TurnOffMusic();
        }
    }

    public static void LeaveCorridor()
    {
        if (isInCorridor == true)
        {
            isInCorridor = false;
            ambientParameter.setValue(1f);
            // ambientCorridor.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            // ambientRoom.start();
            TurnOnMusic();
        }
    }
    public static void TurnOffMusic()
    {
        musicParameter.setValue(0);
        /*   if(currentRoom == Rooms.Hub  )
           {
               musicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
           }
           else if (currentRoom == Rooms.Room1_1)
           {
               R1_1Event.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
           }*/
    }
    public static void TurnOnMusic()
    {
        ambientParameter.setValue(1f);
        if (isInCorridor)
        {
            ambientParameter.setValue(2f);
            musicParameter.setValue(0);
        }
        else if (currentRoom == Rooms.Hub)
        {
            musicParameter.setValue(hubParamenter);
        }
        else if (currentRoom == Rooms.Room1_1)
        {
            musicParameter.setValue(r1_1Paramenter);
        }
        /*    if (currentRoom == Rooms.Hub)
            {
                R1_1Event.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }*/

    }
    public static bool loading = false;
    public static IEnumerator LoadScene(Rooms newScene)
    {
        //todo uncomment soon
        if (newScene.Equals(currentRoom) == false)
        {
            if (loading == false)
            {
                LoadFade fade = GameObject.Find("_Camera").GetComponent<LoadFade>();
                loading = true;
               // TurnOffMusic();
                currentRoom = newScene;
                //TurnOnMusic();

                AsyncOperation async = SceneManager.LoadSceneAsync(RoomToString(newScene));
                async.allowSceneActivation = false;

                while(!async.isDone)
                {
                    // [0, 0.9] > [0, 1]
                    float progress = Mathf.Clamp01(async.progress / 0.9f);
                    Debug.Log("Loading progress: " + (progress * 100) + "%");
                

                    // Loading completed
                    if (async.progress == 0.9f)
                    //if(async.isDone)
                    {           //save old stuff
                        Transform player = GameObject.Find("Player").transform;
                        Transform camera = GameObject.Find("MainCamera").transform;
                        playerPos = player.position;
                        playerRot = player.rotation;
                        cameraPos = camera.position;
                        cameraRot = camera.rotation;
                        cameraDist = camera.GetComponent<CameraOrbit>().distance;
                        savedPosition = true;

                        lensScript.PickupLens();

                  
                        break;
                    }

                    yield return null;

                }
               /* while (true)
                {
                    if(fade.FadedToBlack)
                    {
                        break;
                    }
                    yield return null;
                }*/
                loading = false;
                async.allowSceneActivation = true;
            }
        }
    }

    private static string RoomToString(Rooms room)
    {
        switch (room)
        {
            case Rooms.Hub:
                return "Hub";
            case Rooms.Room1_1:
                return "Room1-1";
            case Rooms.Room1_2:
                return "Room1-2";
            case Rooms.Room1_3:
                return "Room1-3";
            case Rooms.Room1_4:
                return "Room1-4";
            case Rooms.Room1_5:
                return "Room1-5";
            default:
                return "Room1-1";
        }
        
    }

    //TODO MEGA IMPORTANT
    // returnerar true ifall rum 1.1 blir aktiverat och byter till 1.2, kolla vilka variabler som settas och gettas
    public static bool roomFullyPowered
    {
        get
        {
         /*   Debug.Log("GET");
            Debug.Log("activehub" + activeHub);
            Debug.Log("activeRoom1_1" + activeRoom1_1);
            Debug.Log("activeRoom1_2" + activeRoom1_2);
            Debug.Log("activeRoom1_3" + activeRoom1_3);
            Debug.Log("activeRoom1_4" + activeRoom1_4);
            Debug.Log("activeRoom1_5" + activeRoom1_5);
            */
            if (currentRoom == Rooms.Hub)
                return activeHub;

            else if (currentRoom == Rooms.Room1_1)
                return activeRoom1_1;

            else if (currentRoom == Rooms.Room1_2)
                return activeRoom1_2;

            else if (currentRoom == Rooms.Room1_3)
                return activeRoom1_3;

            else if (currentRoom == Rooms.Room1_4)
                return activeRoom1_4;

            else if (currentRoom == Rooms.Room1_5)
                return activeRoom1_5;
            return false;
        }
        set
        {
        /*    Debug.Log("SET");
            Debug.Log("currentRoom" + currentRoom);
            Debug.Log("activehub" + activeHub);
            Debug.Log("activeRoom1_1" + activeRoom1_1);
            Debug.Log("activeRoom1_2" + activeRoom1_2);
            Debug.Log("activeRoom1_3" + activeRoom1_3);
            Debug.Log("activeRoom1_4" + activeRoom1_4);
            Debug.Log("activeRoom1_5" + activeRoom1_5);*/
            if (loading) return;
            if (currentRoom == Rooms.Hub)
                activeHub = value;

            else if (currentRoom == Rooms.Room1_1)
            {
                activeRoom1_1 = value;
                if (activeRoom1_1)
                    r1_1Paramenter = 3f;
                if(isInCorridor  == false)
                    musicParameter.setValue(r1_1Paramenter);
                else
                    musicParameter.setValue(0);
            }
            else if (currentRoom == Rooms.Room1_2)
                activeRoom1_2 = value;

            else if (currentRoom == Rooms.Room1_3)
                activeRoom1_3 = value;

            else if (currentRoom == Rooms.Room1_4)
                activeRoom1_4 = value;

            else if (currentRoom == Rooms.Room1_5)
                activeRoom1_5 = value;
        /*    Debug.Log("activehub" + activeHub);
            Debug.Log("activeRoom1_1" + activeRoom1_1);
            Debug.Log("activeRoom1_2" + activeRoom1_2);
            Debug.Log("activeRoom1_3" + activeRoom1_3);
            Debug.Log("activeRoom1_4" + activeRoom1_4);
            Debug.Log("activeRoom1_5" + activeRoom1_5);*/
        }
    }

    private static bool activeHub = false;
    private static bool activeRoom1_1 = false;
    private static bool activeRoom1_2 = false;
    private static bool activeRoom1_3 = false;
    private static bool activeRoom1_4 = false;
    private static bool activeRoom1_5 = false;

    public static void ActivateSegment(string tag)
    {
        if (currentRoom == Rooms.Hub)
            activeTagsHub.Add(tag);

        else if (currentRoom == Rooms.Room1_1)
            activeTagsRoom1_1.Add(tag);

        else if (currentRoom == Rooms.Room1_2)
            activeTagsRoom1_2.Add(tag);

        else if (currentRoom == Rooms.Room1_3)
            activeTagsRoom1_3.Add(tag);

        else if (currentRoom == Rooms.Room1_4)
            activeTagsRoom1_4.Add(tag);

        else if (currentRoom == Rooms.Room1_5)
            activeTagsRoom1_5.Add(tag);

   //     Debug.Log(currentRoom);
    }

    public static bool SegmentActive(string tag)
    {
       // Debug.Log(currentRoom);
        if (currentRoom == Rooms.Hub)
            return activeTagsHub.Contains(tag);

        else if (currentRoom == Rooms.Room1_1)
            return activeTagsRoom1_1.Contains(tag);

        else if (currentRoom == Rooms.Room1_2)
        {
            /*foreach(string s in activeTagsRoom1_2)
            {
                Debug.Log(s);
            }
            Debug.Log("list contains " + tag +" " + activeTagsRoom1_2.Contains(tag));*/
            return activeTagsRoom1_2.Contains(tag);
        }
        else if (currentRoom == Rooms.Room1_3)
            return activeTagsRoom1_3.Contains(tag);

        else if (currentRoom == Rooms.Room1_4)
            return activeTagsRoom1_4.Contains(tag);

        else if (currentRoom == Rooms.Room1_5)
            return activeTagsRoom1_5.Contains(tag);
        
        return false;
    }
}
public enum Rooms
{
    NoRoom,
    Hub,
    Room1_1,
    Room1_2,
    Room1_3,
    Room1_4,
    Room1_5
};