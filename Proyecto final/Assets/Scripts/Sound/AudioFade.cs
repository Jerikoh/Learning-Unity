using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour
{
    AudioSource audios;
    [SerializeField][Range(0f, 3f)] float fadeSpeed = 1f;
    bool fade = false;


    void Start()
    {
        audios = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (fade)
        {
            if (audios.volume <= 0) //lol nunca hacer if > 0
            {
                return;
            }
            audios.volume -= Time.deltaTime * fadeSpeed;
        }
    }

    public void FadeStart()
    {
        fade = true;
    }
}
