using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private bool isDodging;
    private Animator animator;
    private Rigidbody2D rb;
    
    private float speed = 1f;
    private float dodgeForce = 10f;
    public float dodgeDuration = 0.3f;
    public float dodgeCooldown = 0.3f;
    private float dodgeCooldownTimer = 0f;
    private Vector2 lastMoveDirection;
    
    public AudioClip dodge;
    public AudioClip money;
    public AudioClip RestoreLife;
    public AudioClip DobleDaño;
    public AudioClip SpeedPlus;
    public AudioClip Invincible;

    public float moneyCount;
int contador=25;
    private float minX = -0.00817865f, maxX = 8.174194f;
    private float minY = -4.399909f, maxY = -0.1328556f;

    private bool canInteractVida = false;
    private bool canInteractBota = false;
    private bool canInteractDoble = false;
    private bool canInteractEstrella = false;

    [SerializeField] public Puntaje puntaje; // Referencia al script Puntaje
    [SerializeField] public CombateJugador vida; // Referencia al script CombateJugador
    [SerializeField] public Disparo disparo; // Referencia al script Bala

    [SerializeField] private TextMeshProUGUI interactText; // Cambiado a TextMeshProUGUI


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

        /* Voltear el sprite según la dirección
        if (horizontalInput < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (horizontalInput > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }*/

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

        // Mostrar texto y permitir interacción
        if (canInteractVida)
        {
            interactText.text = "Presiona E para Restaurar Vida (Coste: 1000 puntos)";
            
            if (Input.GetKeyDown(KeyCode.E) && moneyCount >= 1000)
            {
                Debug.Log("Tecla E presionada con puntaje suficiente.");
                contador+=10;
                vida.ReestablecerVida(100+contador);
                GetComponent<AudioSource>().PlayOneShot(RestoreLife,6.0f);
                moneyCount-=1000; // Resta puntos al puntaje
            }
        }
        else
        {
            interactText.text = ""; // Ocultar texto cuando no se puede interactuar
        }

        // Verifica si el jugador puede interactuar (si está dentro de la zona de colisión)
        if (canInteractEstrella && Input.GetKeyDown(KeyCode.E) && moneyCount >= 3000)
        {
            Debug.Log("Tecla E presionada con puntaje suficiente.");
            contador+=10;
            vida.ReestablecerVida(100+contador);
            GetComponent<AudioSource>().PlayOneShot(Invincible,20.0f);
            moneyCount-=3000; // Resta puntos al puntaje

            // Activa la invencibilidad
            StartCoroutine(ActivarInvencibilidad());
            StartCoroutine(SpeedBoost()); // Inicia la corrutina para cambiar los valores
            disparo.ActivarDoubleTap(); // Asegúrate de tener la referencia al script Disparo

        }

        // Activar el DoubleTap si está en la zona y tiene puntos suficientes
        if (canInteractDoble && Input.GetKeyDown(KeyCode.E) && moneyCount >= 1000)
        {
            Debug.Log("Tecla E presionada con puntaje suficiente para DoubleTap.");
            moneyCount-=1000; // Resta puntos al puntaje
            GetComponent<AudioSource>().PlayOneShot(DobleDaño,2.0f);
            // Activar el doble daño en el script de Disparo
            disparo.ActivarDoubleTap(); // Asegúrate de tener la referencia al script Disparo
        }
        
        if (canInteractBota && Input.GetKeyDown(KeyCode.E) && moneyCount >= 1000)
        {
            Debug.Log("Tecla E presionada con puntaje suficiente.");
            moneyCount-=1000; // Resta puntos al puntaje
            GetComponent<AudioSource>().PlayOneShot(SpeedPlus,2.0f);
            StartCoroutine(SpeedBoost()); // Inicia la corrutina para cambiar los valores

        }

        



    }

    private IEnumerator SpeedBoost()
    {
        // Cambia los valores
        speed = 3f;
        dodgeForce = 30f;

        // Espera 10 segundos
        yield return new WaitForSeconds(20);

        // Restaura los valores originales después de 10 segundos
        speed = 1f;
        dodgeForce = 10f;
    }

    // Este método es llamado cuando el jugador recoge el objeto DoubleTap
    public void ActivarDoubleTap()
    {
        disparo.ActivarDoubleTap(); // Llama al método ActivarDoubleTap del script Disparo
    }

    private IEnumerator ActivarInvencibilidad()
    {
        Debug.Log("Invencibilidad activada.");
        vida.isInvincible = true; // Informa a CombateJugador que el jugador es invencible

        yield return new WaitForSeconds(20); // Espera 20 segundos

        vida.isInvincible = false; // Termina la invencibilidad
        Debug.Log("Invencibilidad desactivada.");
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
            canInteractVida = true; // Activa la bandera para permitir la interacción
        }

        if (other.CompareTag("Estrella")) // Verifica si colide con el objeto "Vida"
        {
            Debug.Log("Colisión con Estrella detectada.");
            canInteractEstrella = true; // Activa la bandera para permitir la interacción
        }

        if (other.CompareTag("Velocidad")) // Verifica si colide con el objeto "Vida"
        {
            Debug.Log("Colisión con Velocidad detectada.");
            canInteractBota = true; // Activa la bandera para permitir la interacción
        }

        if (other.CompareTag("DoubleTap")) // Verifica si colide con el objeto "Vida"
        {
            Debug.Log("Colisión con DoubleTap detectada.");
            canInteractDoble = true; // Activa la bandera para permitir la interacción
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
            canInteractVida = false; // Desactiva la bandera cuando el jugador sale de la zona
        }

        if (other.CompareTag("Estrella")) // Verifica si sale de la zona de "Vida"
        {
            Debug.Log("Jugador salió de la zona de Estrella.");
            canInteractEstrella = false; // Desactiva la bandera cuando el jugador sale de la zona
        }

        if (other.CompareTag("Velocidad")) // Verifica si sale de la zona de "Vida"
        {
            Debug.Log("Jugador salió de la zona de Velocidad.");
            canInteractBota = false; // Desactiva la bandera cuando el jugador sale de la zona
        }

        if (other.CompareTag("DoubleTap")) // Verifica si sale de la zona de "Vida"
        {
            Debug.Log("Jugador salió de la zona de DoubleTap.");
            canInteractDoble = false; // Desactiva la bandera cuando el jugador sale de la zona
        }

    }




}