using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Para trabajar con TextMeshPro

public class MostrarVida : MonoBehaviour
{
    private TextMeshProUGUI textMesh; // Componente de texto para mostrar la vida
    private CombateJugador combateJugador; // Referencia al script CombateJugador
    private int ultimaVida; // Almacena el último valor de vida

    // Start se ejecuta al inicio
    void Start()
    {
        // Encuentra el componente TextMeshProUGUI asociado al objeto de la UI
        textMesh = GetComponent<TextMeshProUGUI>();

        // Busca el objeto del jugador y obtiene el componente CombateJugador
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            combateJugador = player.GetComponent<CombateJugador>();

            if (combateJugador == null)
            {
                Debug.LogError("No se encontró el componente CombateJugador en el Player.");
            }
            else
            {
                // Inicializa la última vida con el valor actual
                ultimaVida = combateJugador.vida;
            }
        }
        else
        {
            Debug.LogError("No se encontró el objeto Player en la escena.");
        }
    }

    // Update se ejecuta en cada frame
    void Update()
    { 
        if (combateJugador != null)
        {
            // Actualiza la última vida con el valor actual del jugador
            ultimaVida = combateJugador.vida;
            Debug.Log("Vida actual desde MostrarVida: " + ultimaVida);
        }

        // Actualiza el texto de la vida, incluso si el Player ya no existe
        textMesh.text = ultimaVida.ToString();
    }
}
