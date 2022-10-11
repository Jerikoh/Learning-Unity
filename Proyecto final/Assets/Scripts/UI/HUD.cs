using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] Image[] energyBar; //se queda en 2 unidades por limitador de minim radio, ver si hacer algo []
    [SerializeField][Range(0, 10)] float lerpSpeed = 3; //velocidad de transicion de un valor a otro
    float lerpChange;

    void Update()
    {
        lerpChange = lerpSpeed * Time.deltaTime;
        HealthBarFiller();
        EnergyBarFiller();
        ColorChange();
    }

    void UpdateBars()
    {
        
    }

    void HealthBarFiller()
    {
        if (GameManager.Health > GameManager.MaxHealth)
        {
            GameManager.Health = GameManager.MaxHealth;
        }
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, Convert.ToSingle(GameManager.Health) / Convert.ToSingle(GameManager.MaxHealth), lerpChange); //para obtener la proporcion
    }

    void EnergyBarFiller()
    {
        if (GameManager.Energy > GameManager.MaxEnergy)
        {
            GameManager.Energy = GameManager.MaxEnergy;
        }
        for (int i = 1; i < energyBar.Length; i++) //PROBANDO CON I = 1 EN VEZ DE = 0
        {
            energyBar[i].enabled = !DisplayEnergyUnit(GameManager.Energy * 22.5f, i);
        }
    }

    private bool DisplayEnergyUnit(float energy, float unitNumber)
    {
        return ((unitNumber * 15) >= energy);
    }

    void ColorChange()
    {
        Color healthColor = Color.Lerp(Color.red, Color.white, Convert.ToSingle(GameManager.Health) / Convert.ToSingle(GameManager.MaxHealth));
        healthColor.a = 0.8f;
        healthBar.color = healthColor;
    }
}
