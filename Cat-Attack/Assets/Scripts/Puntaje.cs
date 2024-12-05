using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntaje : MonoBehaviour
{
    public float puntos;
    private TextMeshProUGUI textMesh; // Cambiado a TextMeshProUGUI

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>(); // Añadido paréntesis al final
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = puntos.ToString("0"); // Actualiza el texto en pantalla
    }

    public void SumarPuntos(float puntosEntrada){
        puntos+=puntosEntrada;
    }

    public float ObtenerPuntos()
    {
    return puntos;
    }

}
