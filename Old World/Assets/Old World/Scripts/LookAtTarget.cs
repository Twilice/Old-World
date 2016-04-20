using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class LookAtTarget : MonoBehaviour
{
    private Transform target;

    void Awake()
    {
        target = GameObject.Find("ThirdPersonTarget").transform;
    }

    void Update()
    {
        transform.LookAt(target);
    }
}