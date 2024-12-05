using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private bool isDodging;
    private Animator animator;
    private Rigidbody2D rb;
    
    public float speed = 5f;
    public float dodgeForce = 0.5f;
    public float dodgeDuration = 0.3f;
    public float dodgeCooldown = 0.3f;
    private float dodgeCooldownTimer = 0f;
    private Vector2 lastMoveDirection;
    
    public AudioClip dodge;
    public AudioClip money;

    private float minX = -0.00817865f, maxX = 8.174194f;
    private float minY = -4.399909f, maxY = -0.1328556f;

    private bool canInteract = false;
    [SerializeField] public Puntaje puntaje; // Referencia al script Puntaje
    [SerializeField] public CombateJugador vida; // Referencia al script CombateJugador

    void Start()
    {

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0f;
        rb.drag = 15f;
        rb.mass = 2f;

    }

    void Update()
    {
        // Actualizar cooldown
        if (dodgeCooldownTimer > 0)
        {
            dodgeCooldownTimer -= Time.deltaTime;
        }

        // Obtener input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        // Calcular movimiento
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        
        // Guardar dirección del movimiento si nos estamos moviendo
        if (movement.magnitude > 0.1f)
        {
            lastMoveDirection = movement.normalized;
        }

        // Voltear el sprite según la dirección
        if (horizontalInput < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (horizontalInput > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        // Mover el personaje si no está haciendo dodge
        if (!isDodging)
        {
            // movimiento diagonal
            movement = Vector2.ClampMagnitude(movement, 1f);
            
            // Calcular y aplicar nueva posición
            Vector2 newPosition = rb.position + movement * speed * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
            
            transform.position = newPosition;
        }
        // Si está haciendo dodge, asegurar que se mantenga dentro de los límites
        else
        {
            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, minX, maxX);
            position.y = Mathf.Clamp(position.y, minY, maxY);
            transform.position = position;
        }

        // Detectar input de dodge
        if (Input.GetKeyDown(KeyCode.F) && !isDodging && dodgeCooldownTimer <= 0)
        {
            StartCoroutine(PerformMinimalDodge());
        }

        // Verifica si el jugador puede interactuar (si está dentro de la zona de colisión)
        if (canInteract && Input.GetKeyDown(KeyCode.E) && puntaje.puntos >= 1000)
        {
            Debug.Log("Tecla E presionada con puntaje suficiente.");
            vida.ReestablecerVida(100);
            puntaje.SumarPuntos(-1000); // Resta puntos al puntaje
        }
        
    }

    IEnumerator PerformMinimalDodge()
    {
        isDodging = true;

        GetComponent<AudioSource>().PlayOneShot(dodge, 2.0f);

        animator.SetBool("isDodge", true);
        

        Vector2 dodgeDirection = lastMoveDirection;
        if (dodgeDirection == Vector2.zero)
        {
            dodgeDirection = new Vector2(transform.localScale.x, 0);
        }
        
        rb.velocity = Vector2.zero;
        rb.AddForce(dodgeDirection * dodgeForce, ForceMode2D.Impulse);
        
        yield return new WaitForSeconds(dodgeDuration);
        
        rb.velocity *= 0.3f;
        animator.SetBool("isDodge", false);
        
        yield return new WaitForSeconds(0.1f);
        
        isDodging = false;
        dodgeCooldownTimer = dodgeCooldown;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Vida")) // Verifica si colide con el objeto "Vida"
        {
            Debug.Log("Colisión con Vida detectada.");
            canInteract = true; // Activa la bandera para permitir la interacción
        }

        if (other.CompareTag("Coins")) // Verifica si colide con las monedas
        {
            GetComponent<AudioSource>().PlayOneShot(money, 2.0f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Vida")) // Verifica si sale de la zona de "Vida"
        {
            Debug.Log("Jugador salió de la zona de Vida.");
            canInteract = false; // Desactiva la bandera cuando el jugador sale de la zona
        }
    }
    
}