using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour //CHANGE SCRIPT
{
    [SerializeField] private float cantidadPuntos = 10; // Puntos que otorga
    [SerializeField] private Puntaje puntaje; // Referencia al script Puntaje

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador colisiona
        {

            Debug.Log("Colisión detectada con Player");
            puntaje.SumarPuntos(cantidadPuntos); // Suma puntos al puntaje
            Destroy(gameObject); // Destruye la moneda
            Debug.Log("Moneda destruida");
            
        }
    }
}
