using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorScript : MonoBehaviour
{
	public List<GeneratorScript> Generators;
<<<<<<< HEAD

	private bool IsPowerered = false;
	
	void Start ()
	{
	}

	void Update ()
=======
    private EmissiveTest[] e;

	private bool IsPowerered = false;
    private float t = 0.0f;
    void Start ()
	{

    }

    void Update ()
>>>>>>> refs/remotes/origin/master
	{
		IsPowerered = true;
		for (int i = 0; i < Generators.Count; i++)
		{
			if (Generators[i].Active == false)
			{
				IsPowerered = false;
			}
		}

		if (IsPowerered == true)
		{
			Activate();
<<<<<<< HEAD
		}
=======
           
            Quaternion fromAngle = transform.rotation;
            if (t < 1.0f)
            {
                t += Time.deltaTime * 0.2f;

                EmissiveTest.colorIntesity = Mathf.Lerp(0f, 2.8f, t);
                
            }
        }


>>>>>>> refs/remotes/origin/master
	}

	void Activate()
	{
		gameObject.GetComponent<MovingPlatformScript>().Activate();
<<<<<<< HEAD
	}
=======
        //EmissiveTest.colorIntesity = 3f;
        //StartCoroutine(LightLight());
        //GameObject.Find("L1").active = false;
	}

   /* IEnumerator LightLight()
    {
        float t = 0.0f;
        Quaternion fromAngle = transform.rotation;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale * 0.2f);

            EmissiveTest.colorIntesity = Mathf.Lerp(0f, 3f, t);

            yield return null;
        }
    }*/
>>>>>>> refs/remotes/origin/master
}
