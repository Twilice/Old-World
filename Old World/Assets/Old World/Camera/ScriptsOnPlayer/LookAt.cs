using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{
    [Header("Manual")]
    public Transform target;
    [Header("Automatic")]
    public string search;
    
    void Awake()
    {
        if (target == null)
        {
            if(search.Equals("") == false)
            {
                GameObject go = GameObject.Find(search);
                if(go != null)
                    target = go.transform;
                else
                    Debug.LogError("LookAt (" + transform.name + ") did not find ("+search+").");
            }
            else Debug.LogError("LookAt (" + transform.name + ") is not looking at anything, did you forget to set target or search?");
        }
    }

    void Update()
    {
        transform.LookAt(target);
    }
}