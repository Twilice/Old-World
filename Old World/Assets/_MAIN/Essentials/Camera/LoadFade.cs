using UnityEngine;
using System.Collections;

public class LoadFade : MonoBehaviour
{
    public float fadeOutSpeed = 0.5f;          // Speed that the screen fades to and from black.
    public float fadeInSpeed = 0.5f;
    [HideInInspector]
    public bool sceneStarting = true;      // Whether or not the scene is still fading in.
    [HideInInspector]
    public bool sceneEnding = false;
    GUITexture texture;
    [HideInInspector]
    public float fadeVal = 1f;
    [HideInInspector]
    public bool FadedToBlack = false;

    void Start()
    {
        texture = GetComponent<GUITexture>();
        // Set the texture so that it is the the size of the screen and covers it.
        texture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        texture.enabled = true;

        FadeToClear();
    }


    void Update()
    {
        texture.color = Color.clear;
        /* if (sceneStarting)
             FadeToClear_inner();
         if (sceneEnding)
             FadeToBlack_inner();*/
    }

    public void FadeToBlack()
    {
        sceneStarting = false;
        sceneEnding = true;
        fadeVal = 0f;
    }

    public void FadeToClear()
    {
        sceneStarting = true;
        sceneEnding = false;
        fadeVal = 1f;
    }

    void FadeToClear_inner()
    {
        // Lerp the colour of the texture between itself and transparent.
        fadeVal -= fadeInSpeed * Time.deltaTime;
        texture.color = Color.Lerp(Color.clear, Color.black, fadeVal);

        // If the texture is almost clear...
        if (fadeVal < 0.05f)
        {
            // ... set the colour to clear and disable the GUITexture.
            texture.color = Color.clear;
            texture.enabled = false;

            // The scene is no longer starting.
            sceneStarting = false;
            fadeVal = 0f;
        }
    }

    void FadeToBlack_inner()
    {
        
        // Make sure the texture is enabled.
        texture.enabled = true;

        fadeVal += fadeOutSpeed * Time.deltaTime;
        // Lerp the colour of the texture between itself and black.
        texture.color = Color.Lerp(Color.clear, Color.black, fadeVal);

        if(fadeVal > 0.95f)
        {
            FadedToBlack = true;
            texture.color = Color.black;
            fadeVal = 1f;
        }
    }
}