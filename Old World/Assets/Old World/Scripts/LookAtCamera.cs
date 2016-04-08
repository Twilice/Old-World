using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour
{
    private Transform target;

    void Awake()
    {
        target = GameObject.Find("MainCamera").transform;
    }

    void Update()
    {
        transform.LookAt(target);
    }
}