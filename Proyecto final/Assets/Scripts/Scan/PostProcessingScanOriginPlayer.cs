using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PostProcessingScanOriginPlayer : MonoBehaviour
{
    //PODRIA USAR UN ENUM PARA CORREGIR EL PROBLEMA DE LOS MULTIPLES "CUSTOM PP"? []
    public Material material;
    private static float radius; //lo setea desde un principio porque los cambios ingame sobre el material-shader permanecen, preguntar como evitarlo []
    [SerializeField][Range(1f, 10f)] float startingRadius = 6f;

    void Start()
    {
        Radius = startingRadius;
    }

    void LateUpdate()
    {
        material.SetVector("_Origin", transform.position);
    }

    void FixedUpdate()
    {
        material.SetFloat("_MaskRadius", Radius);
    }

    public static float Radius { get => radius; set => radius = value; } //para cambiarlo desde el GameManager
}
