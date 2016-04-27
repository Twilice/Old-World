using UnityEngine;
using System.Collections;

public class PlatformSwitch : MonoBehaviour
{
    public GameObject platform;
    private PlatformMovement script;

    // Use this for initialization
    void Start()
    {
        script = platform.GetComponent<PlatformMovement>();
    }

    void Update()
    {
        script.Move();
    }
}
