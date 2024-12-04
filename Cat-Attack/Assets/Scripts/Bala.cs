using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float daño;

    private float minX = -0.11f, maxX= 8.4f;
    private float minY = -4.6f, maxY = 0.01f;
    

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector2.up * velocidad * Time.deltaTime);
        if(transform.position.y > maxY || transform.position.y < minY){
            Destroy(gameObject);
        }
        if(transform.position.x > maxX || transform.position.x < minX){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){
            other.GetComponent<ItemEnemigo>().Tomardaño(daño);
            Destroy(gameObject);
        }
    }

    
}
