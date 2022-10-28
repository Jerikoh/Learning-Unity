using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class DeathPlaneKill : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Health = 0;
            WeaponManager.Weapon1 = false;
            WeaponManager.Weapon2 = false;
            WeaponManager.Weapon3 = false;
            WeaponManager.Weapon4 = false;
            WeaponManager.EquippedItem = 5;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
