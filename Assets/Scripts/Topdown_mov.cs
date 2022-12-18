using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Topdown_mov : MonoBehaviour {

    [SerializeField] private float speed;

    [SerializeField] private Vector2 direction;

    private Rigidbody2D rb;

    public static float movX;
    public static float movY;

    public static bool stopMoving = false;

    private Animator animator;



    // Start is called before the first frame update
    private void Start() {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("last_y",1);

    }

    // Update is called once per frame
    private void Update() {

        if(stopMoving){

            movX = Input.GetAxisRaw("Horizontal");
            movY = Input.GetAxisRaw("Vertical");

            animator.SetFloat("mov_x",movX);
            animator.SetFloat("mov_y",movY);

            if(movX != 0 || movY != 0){

                animator.SetFloat("last_x",movX);
                animator.SetFloat("last_y",movY);

            }

            direction = new Vector2(movX, movY).normalized;

        }

    }

    private	void FixedUpdate() {

        if(stopMoving){

            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        }

    }
    
}
