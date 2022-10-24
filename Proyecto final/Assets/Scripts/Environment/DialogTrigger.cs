using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)] float stayTime = 1f;
    [SerializeField] string text;
    [SerializeField] UIFadeInOut textDialog;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textDialog.Write(text, stayTime); //podria pasar otros atributos, uno que indique un delay hasta que se muestre, otro que señale prioridad para que no desaparezca ante otro mensaje hasta que fadeout sea true []
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
