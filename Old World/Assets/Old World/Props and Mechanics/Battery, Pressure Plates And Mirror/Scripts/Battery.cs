using UnityEngine;
using System.Collections;

public class Battery : MonoBehaviour
{
    // private bool charged = false;
    [HideInInspector]
    public int amountOfCharge;
    [Range(1, 10)]
    public int maxChargeInSeconds;

    private bool pickedUp = false;
    private bool canBePickedUp = false;
    private Transform player;
    private Collider attachedTo;
    /*    public void setBatteryCharged(bool b)
        {
            charged = b;
        }*/

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        if (Input.GetButtonDown("Action"))
        {
            // drop to ground
            if(pickedUp)
            {
                RaycastHit hit;
				float heightDifference;
                if (Physics.Raycast(transform.position, -transform.up, out hit, 5, ~0, QueryTriggerInteraction.Ignore))
                {
                    heightDifference = hit.distance-0.35f;
                }
                else heightDifference = 4.65f;

                transform.position = new Vector3(transform.position.x, transform.position.y-heightDifference, transform.position.z);
                pickedUp = false;
            }
            // pickup from ground or detach from slot
            else if (canBePickedUp)
            {
                if (pickedUp == false)
                {
                    if (attachedTo != null && attachedTo.GetComponent<BatteryUser>() != null)
                    {
                        attachedTo.GetComponent<BatteryUser>().battery = null;
                    }
                    pickedUp = true;
                }
            }
        }

        // follow player position without parenting
        if(pickedUp)
        {
            transform.
            transform.rotation = player.rotation;
            transform.position = player.position + player.forward*1 + player.up*1;
        }
        if (amountOfCharge >= (maxChargeInSeconds * 60))
        {
            GetComponent<Renderer>().material.color = Color.green;
        }

        else if (amountOfCharge < (maxChargeInSeconds * 60))
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }

        if (amountOfCharge >= (maxChargeInSeconds * 60))
        {
            amountOfCharge = maxChargeInSeconds * 60;
        }

        if (amountOfCharge < 0)
        {
            amountOfCharge = 0;
        }
    }

    public void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
            canBePickedUp = true;

        // if colliding with something that uses battery
        else if (coll.GetComponent<BatteryUser>() != null && coll.GetComponent<BatteryUser>().battery == null)
        {
            attachedTo = coll;
            pickedUp = false;
            coll.GetComponent<BatteryUser>().battery = this;
            transform.position = coll.transform.position + coll.transform.right * 0.5f;
        } 
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
            canBePickedUp = false;

        // prevent it from detaching wrong slot
        else if (coll == attachedTo)
        {
            attachedTo = null;
        }
    }
}
