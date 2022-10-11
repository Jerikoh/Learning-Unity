using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    [SerializeField] Material scanMaterial;
    [SerializeField] AudioSource audioBackground;
    [SerializeField][Range(0f, 5f)] float sceneLoadDelay = 1f;
    [SerializeField][Range(0f, 5f)] float blendRiseSpeed = 0.1f;
    [SerializeField][Range(0f, 5f)] float volumeRiseSpeed = 0.1f;
    [SerializeField][Range(0f, 5f)] float volumeFallSpeed = 0.1f;
    [SerializeField][Range(0f, 5f)] float exitGameDelay = 1f;
    bool startScanFade = false;
    bool stopVolumeRise = false;

    void Start()
    {
        audioBackground.volume = 0f;
    }

    void Update()
    {
        if (audioBackground.volume < 0.9f && stopVolumeRise == false)
        {
            audioBackground.volume += Time.deltaTime * volumeRiseSpeed;

            if (audioBackground.volume >= 0.9f)
            {
                stopVolumeRise = true;
            }
        }

        if (startScanFade)
        {
            if (scanMaterial.GetFloat("_MultiplyBlend") < 1f)
            {
                scanMaterial.SetFloat("_MultiplyBlend", scanMaterial.GetFloat("_MultiplyBlend") + Time.fixedDeltaTime * blendRiseSpeed);
            }

            if (audioBackground.volume > 0f)
            {
                audioBackground.volume -= Time.deltaTime * volumeFallSpeed;
            }
        }
    }

    public void OnClickStart()
    {
        startScanFade = true;
        Invoke("StartScene", sceneLoadDelay);
    }

    public void OnClickOptions()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //recarga la escena actual
    }

    public void OnClickExit()
    {
        Invoke("ExitGame", exitGameDelay);
    }

    void StartScene()
    {
        SceneManager.LoadScene("Scene_1"); //o puedo usar el indice de la escena así .LoadScene(1);
    }

    void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    void OnDestroy()
    {
        scanMaterial.SetFloat("_MultiplyBlend", 0f);
    }
}
