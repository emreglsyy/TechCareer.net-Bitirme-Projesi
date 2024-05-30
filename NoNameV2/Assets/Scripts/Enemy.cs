using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int enemyHealth = 10;

    public void Enemyy(int hasarMiktari)
    {
        enemyHealth -= hasarMiktari;
    }

    private void Update()
    {
        if(enemyHealth <= 0) 
        {
            Invoke("enemyDeath", 1f);
        }
    }

    void enemyDeath()
    {
        Destroy(gameObject);
    }
}
