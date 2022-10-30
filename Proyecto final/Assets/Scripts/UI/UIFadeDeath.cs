using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeDeath : MonoBehaviour
{
    CanvasGroup canvas;
    [SerializeField][Range(0f, 3f)] float fadeSpeed = 1f;
    bool fade = false;
    bool fadeAlt = false;

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        PlayerCollision.EventPlayerDeath += FadeStart;
        LightRCManager.EventEnding += FadeStart;
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
        else if (fadeAlt)
        {
            if (canvas.alpha >= 1)
            {
                return;
            }
            canvas.alpha += Time.deltaTime * fadeSpeed;
        }
    }

    public void FadeStart()
    {
        fade = true;
    }

    void OnDisable()
    {
        PlayerCollision.EventPlayerDeath -= FadeStart;
        LightRCManager.EventEnding -= FadeStart;
    }
}