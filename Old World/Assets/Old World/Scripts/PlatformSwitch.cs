using UnityEngine;
using System.Collections;

public class PlatformSwitch : TriggeredByLight
{
    public GameObject platform;
    private PlatformMovement script;

	// Use this for initialization
	void Start ()
    {
        script = platform.GetComponent<PlatformMovement>();
	}

    protected override void HitByLightStay()
    {
        script.Move();
    }
}
