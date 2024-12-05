using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuVictory : MonoBehaviour
{
    [SerializeField] private GameObject menuVictoria;

    public void ActivarMenuVictoria()
    {
        menuVictoria.SetActive(true);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuInicial(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void Salir()
    {
        Debug.Log("Cerrando juego");
        Application.Quit();
    }
}
