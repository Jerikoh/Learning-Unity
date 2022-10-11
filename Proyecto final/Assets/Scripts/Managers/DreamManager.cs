using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class DreamManager : MonoBehaviour
{
    //!! DIRTY CODE [] podria seccionar y usar eventos

    [Header("GameObject tarjets:")]
    [SerializeField] GameObject hallGO; //deberia aclarar a los materiales en la var en vez de a los GO jaja []
    [SerializeField] GameObject embodiedGO;
    [SerializeField] GameObject ophanimGO;

    [Header("Audio tarjets:")]
    [SerializeField] AudioSource audioBackground;
    [SerializeField] AudioSource audioTransformation;
    [SerializeField] GameObject audioTransfCompleted; //no pude hacerlo andar sin delay como audiosource []*
    [SerializeField] AudioSource audioOphanimBackground;

    [Header("Audio play delays:")]
    [SerializeField][Range(0f, 10f)] float audioTransfDelay = 0;
    //[SerializeField][Range(0f, 10f)] float audioTransfCompDelay = 0; //*
    [SerializeField][Range(0f, 10f)] float audioOpBackgDelay = 0.8f;
    [SerializeField][Range(0f, 5f)] float volumeRiseSpeed = 0.1f; //del background al inicio de escena 

    [Header("Sonar material tarjets:")]
    [SerializeField] Material hall;
    [SerializeField] Material embodied;
    [SerializeField] Material ophanim;

    float currentPowHall, currentPowEmbodied, currentPowOphanim, currentBleHall, currentBleEmbodied, currentBleOphanim, currentSpeHall, currentSpeEmbodied, currentSpeOphanim; //para guardar los valores iniciales de cada material y restaurarlos luego, [] deberia llamarlas "regular"
    float powHall, powEmbodied, powOphanim, bleHall, bleEmbodied, bleOphanim, speHall, speEmbodied, speOphanim;

    [Header("PostProcessing volume tarjets:")]
    [SerializeField] PostProcessVolume volumeOpExit;

    [Header("Transition variables:")]
    [SerializeField][Range(0f, 10f)] float riseSpeed = 0.1f;
    [SerializeField][Range(0f, 10f)] float fallSpeed = 0.1f;
    [SerializeField][Range(0f, 10f)] float stayTime = 0f;

    [Header("Transition meta values:")]
    [SerializeField][Range(0f, 10f)] float maxRisePow = 2f;
    [SerializeField][Range(-10f, 10f)] float maxFallPow = -2f;
    [SerializeField][Range(0f, 10f)] float maxRiseBle = 2f;
    [SerializeField][Range(-10f, 10f)] float maxFallBle = -2f;
    [SerializeField][Range(0f, 10f)] float maxRiseSpe = 2f;
    [SerializeField][Range(-10f, 10f)] float maxFallSpe = -2f;

    [Header("PingPong animation:")]
    [SerializeField][Range(0f, 5f)] float pingPongSpeed = 0f;
    [SerializeField][Range(-5f, 5f)] float pingPongA = 0.5f;
    [SerializeField][Range(-5f, 5f)] float pingPongB = -0.5f;

    [Header("Ending timer:")]
    [SerializeField][Range(0f, 60f)] float endingTimer = 20f;

    [Header("Ending effects speed:")]
    [SerializeField][Range(0f, 5f)] float powRiseSpeed = 0.2f;
    [SerializeField][Range(0f, 5f)] float blendRiseSpeed = 0.1f;
    [SerializeField][Range(0f, 5f)] float postpRiseSpeed = 0.3f;
    [SerializeField][Range(0f, 5f)] float pitchFallSpeed = 0.3f;


    bool rise = false;
    bool fall = false;

    bool contact = false;
    bool transformed = false;
    bool transFinished = false;
    bool ending = false;
    bool stopVolumeRise = false;



    void Start()
    {
        ContactTrigger.EventPlayerContact += DoContact;

        ophanim.SetFloat("_Power", maxFallPow);

        //volumeOpExit = GetComponent<PostProcessVolume>(); //ni idea por que aca no necesita esto y en pp rise down script si []

        //audioTransformation = GetComponent<AudioSource>(); //idem
        //audioTransfCompleted = GetComponent<AudioSource>();
        //audioOphanimBackground = GetComponent<AudioSource>();

        currentPowHall = hall.GetFloat("_Power");
        currentPowEmbodied = embodied.GetFloat("_Power");
        currentPowOphanim = ophanim.GetFloat("_Power");

        currentBleHall = hall.GetFloat("_MultiplyBlend");
        currentBleEmbodied = embodied.GetFloat("_MultiplyBlend");
        currentBleOphanim = ophanim.GetFloat("_MultiplyBlend");

        currentSpeHall = hall.GetFloat("_Speed");
        currentSpeEmbodied = embodied.GetFloat("_Speed");
        currentSpeOphanim = ophanim.GetFloat("_Speed");
    }

    void FixedUpdate()
    {
        if (audioBackground.volume < 0.9f && stopVolumeRise == false)
        {
            audioBackground.volume += Time.deltaTime * volumeRiseSpeed;

            if (audioBackground.volume >= 0.9f)
            {
                stopVolumeRise = true;
            }
        }

        if (contact)
        {
            if (embodied.GetFloat("_Power") > maxFallPow && transformed == false) //convertir en metodos para poder reutilizar []
            {
                powEmbodied = embodied.GetFloat("_Power");
                powEmbodied -= Time.fixedDeltaTime * fallSpeed;
                embodied.SetFloat("_Power", powEmbodied);

            }
            if (embodied.GetFloat("_Power") <= maxFallPow && transformed == false)
            {
                Invoke("Transformation", stayTime);
                transformed = true;
                Debug.Log("TRANSFORMED = TRUE");
            }
        }

        if (transformed && transFinished == false)
        {
            if (ophanim.GetFloat("_Power") < maxRisePow)
            {
                powOphanim = ophanim.GetFloat("_Power");
                powOphanim += Time.fixedDeltaTime * riseSpeed;
                ophanim.SetFloat("_Power", powOphanim);
            }
            if (ophanim.GetFloat("_Power") >= maxRisePow)
            {
                //ophanim.SetFloat("_Power", maxRisePow); 
                transFinished = true;
            }
        }

        if (transFinished && ending == false)
        {
            float pingPower = ophanim.GetFloat("_Power");
            pingPower = Mathf.PingPong(Time.time * pingPongSpeed, pingPongA) - pingPongB;
            ophanim.SetFloat("_Power", pingPower);

            audioTransfCompleted.SetActive(true);

            Invoke("EndingFlag", endingTimer);
        }

        if (ending)
        {
            Ending();
        }
    }

    void DoContact()
    {
        contact = true;
        audioTransformation.PlayDelayed(audioTransfDelay);
        audioOphanimBackground.PlayDelayed(audioOpBackgDelay);

        //audioTransformation.SetActive(true);
        //ACA DEBERIA BAJAR EL VOLUMEN DE SFX STATUE Y BACKGROUND EN PLAYER []
    }

    void Transformation()
    {
        ophanimGO.SetActive(true);
        embodiedGO.SetActive(false);
        embodied.SetFloat("_Power", 0f);
        hall.SetFloat("_MultiplyBlend", 1f);
    }

    void EndingFlag()
    {
        ending = true;
    }

    private void Ending()
    {
        //bajando power ophanim
        if (ophanim.GetFloat("_Power") > -1f)
        {
            powOphanim = ophanim.GetFloat("_Power");
            powOphanim -= Time.fixedDeltaTime * powRiseSpeed;
            ophanim.SetFloat("_Power", powOphanim);
        }
        //subiendo blend ophanim
        if (ophanim.GetFloat("_MultiplyBlend") < 1f)
        {
            bleOphanim = ophanim.GetFloat("_MultiplyBlend");
            bleOphanim += Time.fixedDeltaTime * blendRiseSpeed;
            ophanim.SetFloat("_MultiplyBlend", bleOphanim);
        }
        //subiendo PP volume de OphanimExit
        if (volumeOpExit.weight < 1)
        {
            volumeOpExit.weight += Time.deltaTime * postpRiseSpeed; //deberia usar un rise/fallSpeed propio para cada efecto [] para mayor control
        }
        //bajando volumen al sonido de background y background oph
        if (audioOphanimBackground.volume > 0f)
        {
            audioOphanimBackground.volume -= Time.deltaTime * pitchFallSpeed; //podria tener var propia de fallspeed
            audioBackground.volume -= Time.deltaTime * pitchFallSpeed;
        }
        //bajando pitch al sonido de background
        if (audioOphanimBackground.pitch > 0.52f)
        {
            audioOphanimBackground.pitch -= Time.deltaTime * pitchFallSpeed;
        }

        //llamando SceneLoad(); al comprobar que el sonido haya llegado a 0
        if (audioOphanimBackground.volume <= 0f)
        {
            SceneManager.LoadScene("Scene_1");
        }

        //agregando "trauma"/shake screen, por la mitad? (si lo hago usarlo en transformacion tambien), PARA ESO copiar otro script PerlinCameraShake, ponerlo en la cam, hacerlo oir un evento que se dispare desde aca cuando PPvolume tenga X presencia

        //LLAMANDO SceneLoad(); con Invoke para cargar proximo nivel //y al prox nivel tambien hacerle una transicion
    }

    void OnDestroy()
    {
        hall.SetFloat("_MultiplyBlend", 0.8f);
        ophanim.SetFloat("_Power", 0f);
        ophanim.SetFloat("_MultiplyBlend", 0f);
        embodied.SetFloat("_Power", 0f);

        volumeOpExit.weight = 0;

        /* hall.SetFloat("_Power", currentPowHall);
        hall.SetFloat("_MultiplyBlend", currentBleHall);
        hall.SetFloat("_Speed", currentSpeHall);

        embodied.SetFloat("_Power", currentPowEmbodied);
        embodied.SetFloat("_MultiplyBlend", currentBleEmbodied);
        embodied.SetFloat("_Speed", currentSpeEmbodied);

        ophanim.SetFloat("_Power", currentPowOphanim);
        ophanim.SetFloat("_MultiplyBlend", currentBleOphanim);
        ophanim.SetFloat("_Speed", currentSpeOphanim); */
    }

    void OnDisable()
    {
        ContactTrigger.EventPlayerContact -= DoContact;
    }
}
