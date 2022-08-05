using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    //Variables
    public string entityName = "Nameless";
    public int health = 100;
    public int startDamage = 20;
    public int startHeal = 20;

    // Start is called before the first frame update
    void Start()
    {
        Damage(startDamage);
        Debug.Log("Vida tras daño: " + health);
        Heal(startHeal);
        Debug.Log("Vida tras curación: " + health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Métodos propios
    void Damage(int value)
    {
        health -= value;
    }
        void Heal(int value)
    {
        health += value;
    }
}
