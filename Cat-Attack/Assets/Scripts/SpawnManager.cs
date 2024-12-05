using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    // Coordenadas espec�ficas para el rango de generaci�n
    private float minX = -0.00817865f, maxX = 8.174194f;
    private float minY = -4.399909f, maxY = -0.1328556f;

    public int enemyCount;
    private int waveCount;

    public AudioClip round;

    public bool bandera;
    private AudioSource playerAudio;

    [SerializeField] private GameObject boss;

    void Start()
    {
        bandera = false;
        waveCount = 10;
        playerAudio = GetComponent<AudioSource>();
        SpawnEnemyWave(waveCount*2);
    }

    void Update()
    {
        
        enemyCount = FindObjectsOfType<Enemy>().Length;

        // Reproducir el sonido solo una vez cuando no haya enemigos
        if (enemyCount == 0)
        {
            if (waveCount < 17 && !bandera)
            {
                playerAudio.PlayOneShot(round, 8.0f);
                SpawnEnemyWave(waveCount);
            }
            else
            {
                bandera = true; // Evitar que se repita
                Debug.Log("Se llego a la ronda limite.");
                if (boss != null)
                {
                    boss.SetActive(true);
                }
                else
                {
                    Debug.LogError("No se encontr� el objeto Player en la escena.");
                }
            }

            
        }
    }

    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(minX, maxX);
        float yPos = Random.Range(minY, maxY);
        return new Vector3(xPos, yPos, 0);
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // Generar una posici�n inicial
            Vector3 spawnPosition = GenerateSpawnPosition();

            // Verificar si la posici�n est� cerca del NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPosition, out hit, 1.0f, NavMesh.AllAreas))
            {
                // Usar la posici�n ajustada (hit.position) para generar el enemigo
                if(waveCount < 17){
                    Instantiate(enemyPrefab[waveCount-10], hit.position, enemyPrefab[waveCount-10].transform.rotation);
                }
                
            }
            else
            {
                Debug.LogWarning("No se encontr� una posici�n v�lida cerca del NavMesh. Saltando generaci�n.");
            }
        }

        waveCount++;
    }

}
