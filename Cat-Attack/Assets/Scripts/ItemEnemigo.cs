using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEnemigo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private int danio;
    [SerializeField] private float vida;
    //[SerializeField] private float tiempoDeVida;//ocupamos modificar esto, esto nomas es de prueba

    private void Start(){
        //Destroy(gameObject,tiempoDeVida);
    }

    private void Update(){
        //transform.Translate(Vector2.down*velocidad*tiempoDeVida.deltaTime); //esto si fuera un item hacia abajo
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            other.GetComponent<CombateJugador>().TomarDanio(danio);
            other.GetComponent<Animator>().SetTrigger("Damage");
            Destroy(gameObject);
        }
    }

    public void Tomardaño(float daño){
        vida -= daño;

        if(vida<=0){
            Destroy(gameObject);
        }
    }
}
