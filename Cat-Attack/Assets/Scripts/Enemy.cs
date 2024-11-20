using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform personaje;
    private NavMeshAgent agente;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();

    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        agente.updateRotation = false;
        agente.updateUpAxis = false;
    }

    // Update is called once per frame
    private void Update()
    {
        agente.SetDestination(personaje.position);
        Vector2 velocity = agente.velocity;
        if (velocity.magnitude > 0.01f)
        {
            spriteRenderer.flipX = velocity.x < 0;
        }
    }
}
