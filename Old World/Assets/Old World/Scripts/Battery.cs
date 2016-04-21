using UnityEngine;
using System.Collections;

public class Battery : MonoBehaviour 
{
    private bool charged = false;


	void Start ()
    {
        
	}

    public void setBatteryCharged(bool b)
    {
        charged = b;
    }

	void Update () 
	{

    }
    
    void OnCollisionEnter()
    {
		
    }
}
