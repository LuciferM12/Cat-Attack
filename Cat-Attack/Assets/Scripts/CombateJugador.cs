using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombateJugador : MonoBehaviour
{
    [SerializeField] public int vida;
    public event EventHandler MuerteJugador;
    public AudioClip hit;
    private Animator animator;
    
    void Start()
    {

        animator = GetComponent<Animator>();

    }

    public void TomarDanio(int cantidadDanio){

        GetComponent<AudioSource>().PlayOneShot(hit, 2.0f);

        vida-=cantidadDanio;
        if(vida<=0){
            animator.SetBool("Death", true);
            StartCoroutine(Death());
            MuerteJugador?.Invoke(this,EventArgs.Empty);
            Destroy(gameObject);
        }
    }

    public void ReestablecerVida(int cantVida){

        Debug.Log("Restableciendo vida a: " + cantVida);
        vida = cantVida;

    }

    IEnumerator Death(){
        yield return new WaitForSecondsRealtime(2);
    }

}
