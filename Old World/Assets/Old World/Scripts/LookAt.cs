using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{
    public Transform target;

    void Awake()
    {
        if (target == null)
            Debug.LogError("LookAt (" + transform.name + ") is not looking at anything, did you forget to set target in scene?");
    }

    void Update()
    {
        transform.LookAt(target);
    }
}