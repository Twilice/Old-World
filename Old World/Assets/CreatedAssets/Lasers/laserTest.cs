using UnityEngine;
using System.Collections;

public class laserTest : MonoBehaviour {

    LineRenderer line;

	void Start ()
    {
        line = GetComponent<LineRenderer>();
	}

    // Update is called once per frame
    void Update()
    {
        Material[] mats = line.GetComponent<Renderer>().materials;
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].mainTextureOffset = new Vector2(-Time.time*2 + i*1f/(float)mats.Length, 0);
        }
    }
}
