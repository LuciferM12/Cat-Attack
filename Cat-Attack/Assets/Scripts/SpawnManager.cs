using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    private float spawnRangeX = 10;
    private float spawnYMin = 15; // set min spawn Z
    private float spawnYMax = 25; // set max spawn Z

    public int enemyCount;
    public int waveCount = 1;

    public AudioClip round;

    public bool bandera;
    private AudioSource playerAudio;

    void Start()
    {
        playerAudio=GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        // Reproducir el sonido solo una vez cuando no haya enemigos
        if (enemyCount == 0 && !bandera)
        {
            bandera = true; // Evitar que se repita
            playerAudio.PlayOneShot(round, 8.0f);
            SpawnEnemyWave(waveCount);
        }

    }

    Vector2 GenerateSpawnPosition ()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float yPos = Random.Range(spawnYMin, spawnYMax);
        return new Vector2(xPos, yPos);
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        
        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

        waveCount++;

    }    


}
