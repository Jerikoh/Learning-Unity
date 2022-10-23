using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIFadeInOut : MonoBehaviour
{
    CanvasGroup canvas;
    TextMeshProUGUI textMesh;
    [SerializeField][Range(0f, 3f)] float fadeSpeed = 1f;
    [SerializeField][Range(0f, 10f)] float stayTime = 2f;
    bool fadeOut = false;
    bool fadeIn = false;
    bool invoked = false;

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void Write(string newText)
    {
        textMesh.text = newText;
        fadeIn = true;
        fadeOut = false;
        invoked = false;
        Debug.Log("WRITE LLAMADO");
    }

    void Update()
    {
        if (fadeIn && canvas.alpha < 1)
        {
            canvas.alpha += Time.deltaTime * fadeSpeed;
            Debug.Log("FADE IN");
        }

        if (!invoked && canvas.alpha >= 1)
        {
            Invoke("FadeOutStart", stayTime);
            invoked = true;
            Debug.Log("INVOKED FADE OUT");
        }

        if (fadeOut && canvas.alpha > 0)
        {
            canvas.alpha -= Time.deltaTime * fadeSpeed;
            Debug.Log("FADE OUT");
        }

        if (fadeOut && canvas.alpha <= 0)
        {
            fadeOut = false;
        }
    }

    void FadeOutStart()
    {
        fadeIn = false;
        fadeOut = true;
    }
}