using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Spawn etmek istediðiniz düþman prefabý
    public int numberOfEnemies; // Kaç adet düþman spawn edilecek
    public float spawnInterval; // Spawn iþlemi aralýðý
    public Transform[] spawnPoints; // Düþmanlarýn spawn edileceði noktalar

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
