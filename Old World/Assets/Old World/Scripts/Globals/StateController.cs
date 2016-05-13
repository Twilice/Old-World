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

    [FMODUnity.EventRef]
    public static FMOD.Studio.EventInstance musicEvent;
    public static FMOD.Studio.ParameterInstance musicParameter;
    public static string eventName = "event:/Music/Wing 1.1 (Test 2)";
    public static string parameterName = "progress";
    public static float parameterIncrement = 0.25f;
    public static float musicParamValue = 0;
    public static MoveLensTarget lensScript;

    private static List<string> activeTagsHub;
    private static List<string> activeTagsRoom1_1;
    private static List<string> activeTagsRoom1_2;
    private static List<string> activeTagsRoom1_3;
    private static List<string> activeTagsRoom1_4;
    private static List<string> activeTagsRoom1_5;


    static StateController()
    {
        activeTagsHub = new List<string>();
        activeTagsRoom1_1 = new List<string>();
        activeTagsRoom1_2 = new List<string>();
        activeTagsRoom1_3 = new List<string>();
        activeTagsRoom1_4 = new List<string>();
        activeTagsRoom1_5 = new List<string>();

        musicEvent = FMODUnity.RuntimeManager.CreateInstance(eventName);
        musicEvent.getParameter(parameterName, out musicParameter);

        musicParameter.setValue(musicParamValue);

        musicEvent.start();
    }
    public static bool loading = false;
    public static IEnumerator LoadScene(Rooms newScene)
    {
        //todo uncomment soon
        //if (newScene.Equals(currentRoom) == false)
        //{
        if (loading == false)
        {
            LoadFade fade = GameObject.Find("_Camera").GetComponent<LoadFade>();
            fade.sceneEnding = true;
            loading = true;
         
            AsyncOperation async = SceneManager.LoadSceneAsync(RoomToString(newScene));
            async.allowSceneActivation = false;

            while(!async.isDone)
            {
                // [0, 0.9] > [0, 1]
                float progress = Mathf.Clamp01(async.progress / 0.9f);
                Debug.Log("Loading progress: " + (progress * 100) + "%");

                

                // Loading completed
                if (async.progress == 0.9f)
                {
                    //save old stuff
                    Transform player = GameObject.Find("Player").transform;
                    Transform camera = GameObject.Find("MainCamera").transform;
                    playerPos = player.position;
                    playerRot = player.rotation;
                    cameraPos = camera.position;
                    cameraRot = camera.rotation;
                    cameraDist = camera.GetComponent<CameraOrbit>().distance;
                    savedPosition = true;

                    lensScript.PickupLens();

                    loading = false;
                    async.allowSceneActivation = true;
                }

                yield return null;

            }
            Debug.Log("kan det här hända?");
            async.allowSceneActivation = true;
            yield return null;
       
            }
    //    }
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

    public static bool roomFullyPowered
    {
        get
        {
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
            if (currentRoom == Rooms.Hub)
                activeHub = value;

            else if (currentRoom == Rooms.Room1_1)
                activeRoom1_1 = value;

            else if (currentRoom == Rooms.Room1_2)
                activeRoom1_2 = value;

            else if (currentRoom == Rooms.Room1_3)
                activeRoom1_3 = value;

            else if (currentRoom == Rooms.Room1_4)
                activeRoom1_4 = value;

            else if (currentRoom == Rooms.Room1_5)
                activeRoom1_5 = value;
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
    }

    public static bool SegmentActive(string tag)
    {
        if(currentRoom == Rooms.Hub)
            return activeTagsHub.Contains(tag);

        else if (currentRoom == Rooms.Room1_1)
            return activeTagsRoom1_1.Contains(tag);
        
        else if (currentRoom == Rooms.Room1_2)       
            return activeTagsRoom1_2.Contains(tag);
        
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
    Hub,
    Room1_1,
    Room1_2,
    Room1_3,
    Room1_4,
    Room1_5
};