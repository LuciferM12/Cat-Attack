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
    public GameObject[] coin;
    private Animator animator; // Referencia al Animator del enemigo

    private bool isPlayerInRange = false; // Bandera para verificar si el jugador está en rango
    private Coroutine damageCoroutine; // Para gestionar la corutina de daño

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Obtener el Animator del enemigo
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
            // Comienza a hacer daño al jugador
            isPlayerInRange = true;
            damageCoroutine = StartCoroutine(DoDamageOverTime(other.GetComponent<CombateJugador>(), other.GetComponent<Animator>()));

            // Daño inicial inmediato al entrar en contacto
            CombateJugador combateJugador = other.GetComponent<CombateJugador>();
            if (combateJugador != null)
            {
                combateJugador.TomarDanio(danio);
            }

            // Activar animación de ataque en el enemigo
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }

            Animator animatorPlayer = other.GetComponent<Animator>();
            if (animatorPlayer != null)
            {
                animatorPlayer.SetTrigger("Damage");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Detiene el daño al salir del rango del enemigo
            isPlayerInRange = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
            }
        }
    }

    private IEnumerator DoDamageOverTime(CombateJugador combateJugador, Animator animator)
    {
        while (isPlayerInRange && combateJugador != null)
        {
            yield return new WaitForSeconds(2f); // Espera 2 segundos entre daños

            combateJugador.TomarDanio(danio);

            if (animator != null)
            {
                animator.SetTrigger("Damage");
            }
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
            int randomNumber = Random.Range(1, 11);
            Vector2 location = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            if (randomNumber == 7)
                Instantiate(coin[1], location, coin[1].transform.rotation);
            else
                Instantiate(coin[0], location, coin[0].transform.rotation);

            Destroy(gameObject);
        }
    }
}
