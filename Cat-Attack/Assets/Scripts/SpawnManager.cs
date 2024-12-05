using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    // Coordenadas específicas para el rango de generación
    private float minX = -0.00817865f, maxX = 8.174194f;
    private float minY = -4.399909f, maxY = -0.1328556f;

    public int enemyCount;
    private int waveCount = 10;

    public AudioClip round;

    public bool bandera;
    private AudioSource playerAudio;

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        SpawnEnemyWave(waveCount);
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        // Reproducir el sonido solo una vez cuando no haya enemigos
        if (enemyCount == 0)
        {
            bandera = true; // Evitar que se repita
            playerAudio.PlayOneShot(round, 8.0f);
            SpawnEnemyWave(waveCount);
        }
    }

    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(minX, maxX);
        float yPos = Random.Range(minY, maxY);
        return new Vector3(xPos, yPos, 0); // Generar posición inicial
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // Generar una posición inicial
            Vector3 spawnPosition = GenerateSpawnPosition();

            // Verificar si la posición está cerca del NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPosition, out hit, 1.0f, NavMesh.AllAreas))
            {
                // Usar la posición ajustada (hit.position) para generar el enemigo
                Instantiate(enemyPrefab, hit.position, enemyPrefab.transform.rotation);
            }
            else
            {
                Debug.LogWarning("No se encontró una posición válida cerca del NavMesh. Saltando generación.");
            }
        }

        waveCount++;
    }
}
