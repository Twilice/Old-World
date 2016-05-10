using UnityEngine;
using System.Collections;

public class sfx : MonoBehaviour {

	public string eventName;

	[FMODUnity.EventRef]
	FMOD.Studio.EventInstance eventToPlay;
	FMOD.Studio.ParameterInstance paramInstance;

	// Use this for initialization
	void Start () {
	
	}
	
	void Awake () {
		eventToPlay = FMODUnity.RuntimeManager.CreateInstance(eventName);
	}

	public void ChangeParameter(string name, float value)
	{
		eventToPlay.getParameter(name, out paramInstance);
		paramInstance.setValue(value);
	}

	public void Play()
	{

	}
}
