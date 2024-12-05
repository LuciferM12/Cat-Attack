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
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Flip del personaje dependiendo del mouse
        objetivo = camara.ScreenToWorldPoint(Input.mousePosition);
        float anguloRadiantes = Mathf.Atan2(objetivo.y - transform.position.y, objetivo.x - transform.position.x);
        float anguloGrados = (180 / Mathf.PI) * anguloRadiantes - 90;

        if(anguloGrados > 0 || anguloGrados < -180){
            spriteRenderer.flipX = true;
        }else{
            spriteRenderer.flipX = false;
        }

        if(Input.GetButtonDown("Fire1")){
            GetComponent<AudioSource>().PlayOneShot(shoot, 2.0f);
            Disparar();
        }
    }

    private void Disparar(){
        Instantiate(bala, controladorDisparo.position, controladorDisparo.rotation);
    }
}
