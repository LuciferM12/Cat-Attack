using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPlayer : MonoBehaviour
{
    public float cantidadPuntos = 10; 
    private PlayerController playerController; 

    void Start()
    {
       
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("No se encontró el objeto Player en la escena.");
        }
    }

    void Update()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador colisiona
        {

            Debug.Log("Colisión detectada con Player");
            playerController.moneyCount += cantidadPuntos;
            Destroy(gameObject); // Destruye la moneda
            Debug.Log("Moneda destruida");

        }
    }

}
