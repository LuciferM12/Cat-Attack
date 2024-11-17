using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private Animator animator;
    public float speed;
    private float minX = -0.00817865f, maxX = 8.174194f;
    private float minY = -4.399909f, maxY = -0.1328556f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            if (transform.position.x > minX)
            {
                transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
            }
        }
        else
        {
            if (horizontalInput > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            if (transform.position.x < maxX)
            {
                transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
            }
        }

        verticalInput = Input.GetAxis("Vertical");
        if (verticalInput > 0.0f && transform.position.y < maxY)
        {
            transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed);
        }
        else if (verticalInput < 0.0f && transform.position.y > minY)
        {
            transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed);
        }

        if (Input.GetKeyDown(KeyCode.F)){
            animator.SetBool("isDodge", true);
        } else {
            animator.SetBool("isDodge", false);
        }
        
    }
}
