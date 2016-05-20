using UnityEngine;
using System.Collections;
using FMODUnity;
using System.Collections.Generic;

public class sfx : MonoBehaviour {

	public List<string> eventName;

	[FMODUnity.EventRef]
	//FMOD.Studio.EventInstance eventToPlay;
	FMOD.Studio.ParameterInstance paramInstance;
	List<FMOD.Studio.EventInstance> eventToPlay = new List<FMOD.Studio.EventInstance>();

	//Denna bool som är exponerad i Inspector avgör om eventet eller snapshotet ska starta automatiskt när scriptet startas.
	public bool startOnAwake = true;
	private Rigidbody cachedRigidBody;

	// Use this for initialization
	void Start () {
		if (eventToPlay.Count != 0)
		{
			for (int i = 0; i < eventName.Count; i++)
			{
				if(cachedRigidBody != null)
                    eventToPlay[i].set3DAttributes(RuntimeUtils.To3DAttributes(gameObject, cachedRigidBody));
			}
		}
	}
	
	void Awake () {
		cachedRigidBody = GetComponent<Rigidbody>();
		for (int i = 0; i < eventName.Count; i++)
		{
			eventToPlay.Add(FMODUnity.RuntimeManager.CreateInstance(eventName[i]));
		}
	}

	public void ChangeParameter(int i, string name, float value)
	{
		eventToPlay[i].getParameter(name, out paramInstance);
	//	paramInstance.setValue(value);
	}

	public void setVolume(int i, float value)
	{
		eventToPlay[i].setVolume(value);
	}

	public void Play(int i)
	{
		eventToPlay[i].start();
	}
}
