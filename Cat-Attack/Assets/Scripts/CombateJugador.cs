using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombateJugador : MonoBehaviour
{
    [SerializeField] private int vida;
    public event EventHandler MuerteJugador;
    public AudioClip hit;
    private AudioSource audio;
    private Animator animator;
    
    void Start()
    {

        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

    }

    public void TomarDanio(int cantidadDanio){

        audio.PlayOneShot(hit, 2.0f);

        vida-=cantidadDanio;
        if(vida<=0){
            animator.SetBool("Death", true);
            StartCoroutine(Death());
            MuerteJugador?.Invoke(this,EventArgs.Empty);
            Destroy(gameObject);
        }
    }

    IEnumerator Death(){
        yield return new WaitForSecondsRealtime(2);
    }

}
