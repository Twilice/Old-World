using UnityEngine;
using System.Collections;
/*public class TriggeredByLight : MonoBehaviour
{
 TODO : ska ligga annan stans sen
	virtual public HitByLightEnter()
	{

	}

	virtual public HitByLightExit()
	{

	}

	virtual public HitByLightStay()
	{

	}
}*/

public class EmitLightRayCast : MonoBehaviour {

    private GameObject lastHitObject = null;
    // Use this for initialization
    void Start () {
	
	}
 
	
	// Update is called once per frame
	void Update () {
        if (FirstPersonViewToggle.FirstPerson)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 500))
            {
				/*      
				TODO TODO TODO
				
						   	   foreacH( getComponents<TriggeredByLight>)
						   {
						   HitByLightEnter() etc . etc.
				   }

					
						   GameObject hitObject = hit.transform.gameObject;

						   if(lastHitObject.Equals(hitObject) == false)
						   {
							   hitObject.SendMessage("HitByLightEnter"); - byt
							   lastHitObject.SendMessage("HitByLightExit");
							   lastHitObject = hitObject;
						   }
						   hitObject.SendMessage("HitByLightStay");
						   //TODO: kan skicka en data, typ struct med vinkel + ljusfärg
				  */
			}
			else
            {
            //    lastHitObject.SendMessage("HitByLightExit");
            //    lastHitObject = null;
            }
        }
    }
}
