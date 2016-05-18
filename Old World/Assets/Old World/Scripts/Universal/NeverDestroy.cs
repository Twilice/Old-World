using UnityEngine;
using System.Collections;

public class NeverDestroy : MonoBehaviour {
    
	void Awake () {
        DontDestroyOnLoad(gameObject);
    }
}
