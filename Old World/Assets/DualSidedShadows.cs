﻿using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class DualSidedShadows : MonoBehaviour {
    
	void Awake () {
        foreach (Renderer r in transform.GetComponents<Renderer>())
        {
            r.shadowCastingMode = ShadowCastingMode.TwoSided;
        }
    }
	
}
