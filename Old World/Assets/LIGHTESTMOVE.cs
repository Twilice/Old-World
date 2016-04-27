using UnityEngine;
using System.Collections;

public class LIGHTESTMOVE : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Material[] mats = GetComponent<Renderer>().materials;
        for (int i = 0; i < mats.Length; i++)
        {
         //   mats[i].SetTextureOffset("_MainTex", new Vector2(-Time.time * 0.2f + i * 1f / (float)mats.Length, 0));
         //   mats[i].SetTextureOffset("_DetailTex", new Vector2(-Time.time * 0.2f + i * 1f / (float)mats.Length, 0));
         //   mats[i].SetTextureOffset("_BumpTex", new Vector2(-Time.time * 0.2f + i * 1f / (float)mats.Length, 0));
            mats[i].mainTextureOffset = new Vector2(-Time.time * 0.2f + i * 1f / (float)mats.Length, 0);
        }
    }
}
