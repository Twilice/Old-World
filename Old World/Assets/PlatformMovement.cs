using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour
{
    float height;
    float maxHeight;
    float lowestHeight;
    bool state;
    bool atTop;

	// Use this for initialization
	void Awake ()
    {
        height = transform.position.y;
        maxHeight = 8;
        lowestHeight = 1;
        state = false;
        atTop = false;

    }

    public void Move()
    {
        // Move the platform up and down
        if (atTop == false)
        {
            if (state == false && transform.position.y < maxHeight)
            {
                Debug.Log("up" + height);
                height = (height + Time.deltaTime * 5);
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
                Debug.Log("down" + height);
                height = (height - Time.deltaTime * 5);
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
    }
}
