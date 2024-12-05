using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] private int danio = 25; 

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,3);
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
