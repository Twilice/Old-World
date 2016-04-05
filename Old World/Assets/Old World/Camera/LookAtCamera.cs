using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        transform.LookAt(target);
    }
}