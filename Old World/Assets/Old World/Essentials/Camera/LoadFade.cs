using UnityEngine;
using System.Collections;

public class LoadFade : MonoBehaviour
{
    public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.

    public bool sceneStarting = true;      // Whether or not the scene is still fading in.
    public bool sceneEnding = false;
    GUITexture texture;
    public float fadeVal = 0f;

    void Awake()
    {
        texture = GetComponent<GUITexture>();
        // Set the texture so that it is the the size of the screen and covers it.
        texture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        texture.enabled = true;
    }


    void Update()
    {
        if (sceneStarting)
            FadeToClear();
        if (sceneEnding)
            FadeToBlack();
    }

    void FadeToClear()
    {
        // Lerp the colour of the texture between itself and transparent.
        fadeVal += fadeSpeed * Time.deltaTime;
        texture.color = Color.Lerp(texture.color, Color.clear, fadeVal);

        // If the texture is almost clear...
        if (fadeVal > 0.95f)
        {
            // ... set the colour to clear and disable the GUITexture.
            texture.color = Color.clear;
            texture.enabled = false;

            // The scene is no longer starting.
            sceneStarting = false;
            fadeVal = 1f;
        }
    }

    void FadeToBlack()
    {
        // Make sure the texture is enabled.
        texture.enabled = true;

        fadeVal -= fadeSpeed * Time.deltaTime;
        // Lerp the colour of the texture between itself and black.
        texture.color = Color.Lerp(Color.black, texture.color, fadeVal);

        if(fadeVal < 0.05f)
        {
            texture.color = Color.black;
            fadeVal = 0;
        }
    }
}