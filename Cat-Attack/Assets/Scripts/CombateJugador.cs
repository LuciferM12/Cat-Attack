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
    
    void Start()
    {

        audio = GetComponent<AudioSource>();

    }

    public void TomarDanio(int cantidadDanio){

        audio.PlayOneShot(hit, 2.0f);

        vida-=cantidadDanio;
        if(vida<=0){
            
            MuerteJugador?.Invoke(this,EventArgs.Empty);
            Destroy(gameObject);
        }
    }

}
