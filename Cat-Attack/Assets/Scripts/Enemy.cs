using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float velocidad; 
    [SerializeField] private int danio; 
    [SerializeField] private float vida;

    private Transform personaje; 
    private NavMeshAgent agente; 
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
       
        agente.updateRotation = false;
        agente.updateUpAxis = false;

        
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            personaje = playerObject.transform;
        }
        else
        {
            Debug.LogError("No se encontró un objeto llamado 'Player' en la escena.");
        }
    }

    private void Update()
    {
        if (agente != null && personaje != null && agente.isActiveAndEnabled)
        {
            agente.SetDestination(personaje.position);

            Vector2 velocity = agente.velocity;
            if (velocity.magnitude > 0.01f)
            {
                spriteRenderer.flipX = velocity.x < 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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

            //Destroy(gameObject);
        }
    }

    public void TomarDanio(float daño)
    {
        vida -= daño;

        if (vida <= 0)
        {
            if (agente != null)
            {
                agente.enabled = false; // Desactiva el agente antes de destruir el objeto
            }
            Destroy(gameObject);
        }
    }
}
