using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    bool camType = true;
    public GameObject[] cameras;
    /*int index = 0;
    int indexTraverser = 1;*/

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (camType)
            {
                cameras[0].SetActive(false);
                cameras[1].SetActive(true);
                camType = !camType;
            }
            else
            {
                cameras[0].SetActive(true);
                cameras[1].SetActive(false);
                camType = !camType;
            }
            /*int length = cameras.Length;
            for (int i = 0; i < length; i++)
            {
                cameras[i].SetActive(false);
            }
           
            if ((index + indexTraverser <= length) && (index + indexTraverser >= 0)) //debe haber una solucion mas simple xD []
            {
                
                index += indexTraverser;
                cameras[index].SetActive(true);
            }
            else if ((index + indexTraverser <= length) && (index + indexTraverser >= 0))
            {
                
                indexTraverser *= -1;
                index += indexTraverser;
                cameras[index].SetActive(true);
            } */
        }
    }

}
