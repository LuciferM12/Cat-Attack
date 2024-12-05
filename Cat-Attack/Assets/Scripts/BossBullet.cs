using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBullet : MonoBehaviour
{
    [SerializeField] private int danio = 25; 
    private float minX = -0.11f, maxX= 8.4f;
    private float minY = -4.6f, maxY = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,3);
    }

    void Update()
    {
        
        
        if(transform.position.y > maxY || transform.position.y < minY){
            Destroy(gameObject);
        }
        if(transform.position.x > maxX || transform.position.x < minX){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player"))
        {
            CombateJugador combateJugador = other.GetComponent<CombateJugador>();
            if (combateJugador != null)
            {
                combateJugador.TomarDanio(danio);
            }

            Animator animator = other.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Damage");
            }

            Destroy(gameObject);
        }
    }

}
