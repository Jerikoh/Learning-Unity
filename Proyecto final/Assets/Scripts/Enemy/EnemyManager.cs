using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //acá lo correcto sería meter todas las variables que competen al daño efectuado, velocidad, deteccion, tipo de movimiento, etc [] ++
    [SerializeField][Range(1, 1000)] int health = 100;

    public void Damage(int weaponDamage)
    {
        if (health - weaponDamage > 0)
        {
            health -= weaponDamage;
            Debug.Log("ENEMY HITTED, hp: " + health);
        }
        else if (health - weaponDamage <= 0)
        {
            Kill();
        }
    }

    void Kill()
    {
        Debug.Log("ENEMY KILLED");
        //!! matar con timer invoke por anims y sonido
        //!! ANIM & SOUND HERE
        gameObject.SetActive(false);
    }

    public int GetHealthPoints() //deberia ser propiedad []
    {
        return health;
    }
}
