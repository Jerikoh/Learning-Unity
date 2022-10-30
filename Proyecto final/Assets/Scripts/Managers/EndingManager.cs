using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    [SerializeField] Material pulsePlayerConstant;
    [SerializeField] Material pulsePlayerSonar;
    [SerializeField] Material pulseOphanim;
    [SerializeField] Animator playerAnimator;
    [SerializeField] GameObject revolverGO;
    [SerializeField] GameObject hatchetGO;
    [SerializeField] AudioSource audioWind;
    [SerializeField] AudioSource audioBackground;
    float regBlendPlayerConstant, regBlendPlayerSonar, regBlendOphanim, regPowerOphanim;
    [SerializeField][Range(0f, 2f)] float blendRiseSpeed = 0.02f;
    [SerializeField][Range(0f, 2f)] float powerFallSpeed = 0.02f;
    [SerializeField][Range(0f, 2f)] float volumeFallSpeed = 0.02f;
    [SerializeField][Range(-1f, 0f)] float startinOphanimPower = -0.2f;
    [SerializeField][Range(-1f, 0f)] float finalOphanimPower = -0.7f;
    bool startFade = false;

    void Start()
    {
        LightRCManager.EventEnding += StartEnding;

        regBlendPlayerConstant = pulsePlayerConstant.GetFloat("_MultiplyBlend");
        regBlendPlayerSonar = pulsePlayerSonar.GetFloat("_MultiplyBlend");
        regBlendOphanim = pulseOphanim.GetFloat("_MultiplyBlend");
        regPowerOphanim = pulseOphanim.GetFloat("_Power");
    }

    void FixedUpdate()
    {
        //"bajando" constante
        if (pulsePlayerSonar.GetFloat("_MultiplyBlend") < 1f)
        {
            pulsePlayerSonar.SetFloat("_MultiplyBlend", pulsePlayerSonar.GetFloat("_MultiplyBlend") + Time.fixedDeltaTime * blendRiseSpeed);
        }

        //"subiendo" ophanim
        if (pulseOphanim.GetFloat("_Power") > finalOphanimPower)
        {
            pulseOphanim.SetFloat("_Power", pulseOphanim.GetFloat("_Power") - Time.fixedDeltaTime * powerFallSpeed);
        }

        //iniciando desvanecimiento total
        if (startFade)
        {
            //de player const
            if (pulsePlayerConstant.GetFloat("_MultiplyBlend") < 1f)
            {
                pulsePlayerConstant.SetFloat("_MultiplyBlend", pulsePlayerConstant.GetFloat("_MultiplyBlend") + Time.fixedDeltaTime * blendRiseSpeed);
            }
            //de oph
            if (pulseOphanim.GetFloat("_MultiplyBlend") < 1f)
            {
                pulseOphanim.SetFloat("_MultiplyBlend", pulseOphanim.GetFloat("_MultiplyBlend") + Time.fixedDeltaTime * blendRiseSpeed);
            }
            //de music
            if (audioBackground.volume > 0f)
            {
                audioBackground.volume -= Time.fixedDeltaTime * volumeFallSpeed;
            }
            //de wind
            if (audioWind.volume > 0f)
            {
                audioWind.volume -= Time.fixedDeltaTime * volumeFallSpeed;
            }
            //cuando una llegue a 0 salida a menu 
            if (pulseOphanim.GetFloat("_MultiplyBlend") >= 1f)
            {
                Invoke("SceneLoader", 0f);
            }
        }
    }

    void StartEnding()
    {
        Debug.Log("Ending started");
        PlayerController.CanPlayerMove = false;
        WeaponManager.PlayerCanMove = false;
        WeaponManager.Weapon1 = false;
        WeaponManager.Weapon2 = false;
        WeaponManager.Weapon3 = false;
        WeaponManager.Weapon4 = false;
        revolverGO.SetActive(false);
        hatchetGO.SetActive(false);
        playerAnimator.SetTrigger("Start Praying");
        audioWind.Play();
        pulseOphanim.SetFloat("_Power", startinOphanimPower);
    }

    void SceneLoader()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void OnDestroy()
    {
        pulsePlayerConstant.SetFloat("_MultiplyBlend", regBlendPlayerConstant);
        pulsePlayerSonar.SetFloat("_MultiplyBlend", regBlendPlayerSonar);
        pulseOphanim.SetFloat("_MultiplyBlend", regBlendOphanim);
        pulseOphanim.SetFloat("_Power", regPowerOphanim);
    }

    void OnDisable()
    {
        LightRCManager.EventEnding -= StartEnding;
    }
}
