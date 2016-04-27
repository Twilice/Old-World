using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour
{
    float height;
    float brod;
    float maxHeight;
    float lowestHeight;
    float maxLeft;
    float minLeft;
    bool state;
    bool state2;
    bool atTop;
    bool atRight;
    bool atLeftestest;

	// Use this for initialization
	void Awake ()
    {
        height = transform.position.y;
        brod = transform.position.z;
        maxHeight = 24;
        lowestHeight = 21;
        maxLeft = 0;
        minLeft = -4;
        state = false;
        state2 = false;
        atTop = false;
        atLeftestest = false;


    }

    public void Move()
    {
        // Move the platform up and down
        if (atTop == false)
        {
            if (state == false && transform.position.y < maxHeight)
            {
                //Debug.Log("up" + height);
                height = (height + Time.deltaTime * 12);
                Vector3 newPos = transform.position;
                newPos.y = height;
                transform.position = newPos;
            }
            else
            {
                atTop = true;
                state = true;
            }
        }
        else if (atTop == true)
        {
            if (state = true && transform.position.y > lowestHeight)
            {
                //Debug.Log("down" + height);
                height = (height - Time.deltaTime * 12);
                Vector3 newPos = transform.position;
                newPos.y = height;
                transform.position = newPos;
            }
            else
            {
                atTop = false;
                state = false;
            }
        }

        //PETTERS SHITSKIT
        if (atLeftestest == false)
        {
            if (state2 == false && transform.position.z < maxLeft)
            {
                //Debug.Log("up" + height);
                brod += Time.deltaTime * 12;
                Vector3 newPos = transform.position;
                newPos.z = brod;
                transform.position = newPos;
            }
            else
            {
                atLeftestest = true;
                state2 = true;
            }
        }
        else if (atLeftestest == true)
        {
            if (state2 = true && transform.position.z > minLeft)
            {
                //Debug.Log("down" + height);
                brod = (brod - Time.deltaTime * 12);
                Vector3 newPos = transform.position;
                newPos.z = brod;
                transform.position = newPos;
            }
            else
            {
                atLeftestest = false;
                state2 = false;
            }
        }
    }
}
