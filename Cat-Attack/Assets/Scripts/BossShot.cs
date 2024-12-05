using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShot : MonoBehaviour
{
    [SerializeField] public GameObject enemyBullet;
    [SerializeField] public Transform spawnBulletPoint;
    private Transform playerPosition;
    [SerializeField] public float bulletVelocity = 15.0f;
    void Start()
    {
        playerPosition = FindObjectOfType<PlayerController>().transform;

        Invoke("ShootPlayer", 2);
    }

    void Update()
    {
        
    }

    void ShootPlayer(){
        Vector2 playerDirection = playerPosition.position - transform.position;

        GameObject newBullet;

        newBullet = Instantiate(enemyBullet, spawnBulletPoint.position, spawnBulletPoint.rotation);
        newBullet.GetComponent<Rigidbody2D>().AddForce(playerDirection*bulletVelocity, ForceMode2D.Force);
        Invoke("ShootPlayer", 2);
    }
}
