using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class MenuGameOver : MonoBehaviour
{
    

    [SerializeField] private GameObject menuGameOver;
    private CombateJugador combateJugador;

    public AudioClip GameOver;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        
        playerAudio=GetComponent<AudioSource>();
        
        combateJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<CombateJugador>();
        combateJugador.MuerteJugador+=ActivarMenu;

    }

    private void ActivarMenu(object sender, EventArgs e){

        playerAudio.PlayOneShot(GameOver,5.0f);
        menuGameOver.SetActive(true);

    }

    public void Reiniciar(){
        combateJugador.MuerteJugador -= ActivarMenu;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuInicial(string nombre){
        combateJugador.MuerteJugador -= ActivarMenu;
        SceneManager.LoadScene(nombre);
    }

    public void Salir(){
        Debug.Log("Cerrando juego");
        //UnityEditor.EditorApplication.isPlaying=false;
        Application.Quit();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }


}
