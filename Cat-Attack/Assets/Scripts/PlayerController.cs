using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private Animator animator;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if(horizontalInput > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        transform.Translate(Vector3.right*horizontalInput*Time.deltaTime*speed);

        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up*verticalInput*Time.deltaTime*speed);

        if(Input.GetKeyDown(KeyCode.F)){
            animator.SetBool("isDodge", true);
        }else{
            animator.SetBool("isDodge", false);
        }
        
    }
}
