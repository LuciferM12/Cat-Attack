using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntaje : MonoBehaviour
{
    public float puntos;
    private TextMeshProUGUI textMesh; // Cambiado a TextMeshProUGUI
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>(); // Añadido paréntesis al final
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

    // Update is called once per frame
    void Update()
    {
        textMesh.text = playerController.moneyCount.ToString("0"); // Actualiza el texto en pantalla
    }

    public void SumarPuntos(float puntosEntrada){
        puntos+=puntosEntrada;
    }

    public float ObtenerPuntos()
    {
    return puntos;
    }

}
