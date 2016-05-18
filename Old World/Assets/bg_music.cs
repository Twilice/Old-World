using UnityEngine;
using System.Collections;

public class bg_music : MonoBehaviour {
	sfx Sfx;

	// Use this for initialization
	void Start () {
		Sfx = GetComponent<sfx>();
		Sfx.Play(0);
	}
}
