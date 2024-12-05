using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    private bool juegoPausado =false;

    public void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(juegoPausado){
                Reanudar();
            }else{
                Pausa();
            }
        }
    }
    public void Pausa(){
        juegoPausado =true;
        Time.timeScale=0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void Reanudar(){
        juegoPausado =false;
        Time.timeScale=1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Reiniciar(){
        juegoPausado =false;
        Time.timeScale=1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit(){
        Debug.Log("Cerrando juego");
        UnityEditor.EditorApplication.isPlaying=false;
        Application.Quit();
    }
}
