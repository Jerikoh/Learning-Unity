using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CustomPostProcessing : MonoBehaviour
{
    public Material material; //ver si puedo pasarlos a SerializeField sin perder los parametros y referencias

    [ImageEffectOpaque] // propio, para que se aplique antes que los demas, aunque no es su funcionalidad principal
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}

