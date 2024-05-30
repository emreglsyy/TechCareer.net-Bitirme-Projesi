using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Spawn etmek istedi�iniz d��man prefab�
    public int numberOfEnemies; // Ka� adet d��man spawn edilecek
    public float spawnInterval; // Spawn i�lemi aral���
    public Transform[] spawnPoints; // D��manlar�n spawn edilece�i noktalar

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

   

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
