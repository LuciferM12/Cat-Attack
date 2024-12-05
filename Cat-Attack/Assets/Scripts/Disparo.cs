using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    private Vector3 objetivo;
    [SerializeField] private Camera camara;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject bala;
    public AudioClip shoot;

    // Para duplicar el daño
    private bool isDoubleTapActive = false;  // Bandera para saber si el daño está duplicado
    private float doubleTapDuration = 20f;  // Duración de 20 segundos para el doble daño

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Evitar que el disparo se ejecute si el clic ocurre sobre un elemento UI
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        return;
        // Flip del personaje dependiendo del mouse
        objetivo = camara.ScreenToWorldPoint(Input.mousePosition);
        float anguloRadiantes = Mathf.Atan2(objetivo.y - transform.position.y, objetivo.x - transform.position.x);
        float anguloGrados = (180 / Mathf.PI) * anguloRadiantes - 90;

        if (anguloGrados > 0 || anguloGrados < -180)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            GetComponent<AudioSource>().PlayOneShot(shoot, 2.0f);
            Disparar();
        }
    }

    private void Disparar()
    {
        // Crear la bala
        GameObject balaObject = Instantiate(bala, controladorDisparo.position, controladorDisparo.rotation);

        // Acceder al script de la bala
        Bala balaScript = balaObject.GetComponent<Bala>();

        // Si DoubleTap está activo, duplicamos el daño de la bala
        if (isDoubleTapActive)
        {
            balaScript.daño *= 2;  // Duplica el daño de la bala
        }

        // Configura otras propiedades de la bala (como velocidad, dirección, etc.)
        balaScript.velocidad = 2.0f;

        // Desactivar el doble daño después de un tiempo
        StartCoroutine(RestaurarDaño(balaScript));
    }

    // Inicia la acción del DoubleTap, activando el doble daño
    public void ActivarDoubleTap()
    {
        isDoubleTapActive = true;
        StartCoroutine(DesactivarDoubleTap());
    }

    // Corrutina para restaurar el daño original de la bala después de un breve tiempo
    private IEnumerator RestaurarDaño(Bala bala)
    {
        // Espera un momento para restaurar el daño
        yield return new WaitForSeconds(0.1f);  // Espera un pequeño tiempo para asegurar que el daño se restaure después del disparo

        // Si el doble daño estaba activo, restauramos el daño
        if (isDoubleTapActive)
        {
            bala.daño /= 2;  // Restaura el daño original de la bala
        }
    }

    // Corrutina para desactivar el doble daño después de 20 segundos
    private IEnumerator DesactivarDoubleTap()
    {
        // Espera 20 segundos (durante este tiempo el daño estará duplicado)
        yield return new WaitForSeconds(doubleTapDuration);

        // Desactivar el doble daño
        isDoubleTapActive = false;
    }
}
