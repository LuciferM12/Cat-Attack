using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEnemigo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private int danio;
    [SerializeField] private float tiempoDeVida;

    private void Start(){
        Destroy(gameObject,tiempoDeVida);
    }

    private void Update(){
        //transform.Translate(Vector2.down*velocidad*tiempoDeVida.deltaTime); //esto si fuera un item hacia abajo
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            other.GetComponent<CombateJugador>().TomarDanio(danio);
            Destroy(gameObject);
        }
    }
}
