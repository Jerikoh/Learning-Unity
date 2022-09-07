using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    CanvasGroup canvas;
    [SerializeField][Range(0f,3f)] float fadeSpeed = 1f;
    bool fade = false;

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (fade)
        {
            if (canvas.alpha <= 0)
            {
                return;
            }
            canvas.alpha -= Time.deltaTime * fadeSpeed;
        }
    }

    public void FadeStart()
    {
        fade = true;
    }
}