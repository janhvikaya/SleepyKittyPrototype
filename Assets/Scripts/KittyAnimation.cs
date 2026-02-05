using Pathfinding;
using UnityEngine;

public class KittyAnimation : MonoBehaviour
{
    public AIPath aiPath;

    private Rigidbody2D rb;
    public Animator animator;
    private Vector3 lastInputs = new Vector3 (0,0,0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>();
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.useRB)
        {
            if (Mathf.Abs(aiPath.desiredVelocity.x) >= 0.01f || Mathf.Abs(aiPath.desiredVelocity.y) >= 0.01f)
            {
                animator.SetBool("isWalking", true);
                lastInputs = aiPath.desiredVelocity;
                animator.SetFloat("InputX", lastInputs.x);
                animator.SetFloat("InputY", lastInputs.y);
            }

            if (aiPath.desiredVelocity.x == 0f && aiPath.desiredVelocity.y == 0f)
            {
                animator.SetBool("isWalking", false);
                animator.SetFloat("LastInputX", lastInputs.x);
                animator.SetFloat("LastInputY", lastInputs.y);

            }
        }
        else if (GameManager.useRB)
        {
            if (Mathf.Abs(InputManager.direction.x) >= 0.1f || Mathf.Abs(InputManager.direction.y) >= 0.1f)
            {
                animator.SetBool("isWalking", true);
                lastInputs = InputManager.direction * 10f;
                animator.SetFloat("InputX", lastInputs.x);
                animator.SetFloat("InputY", lastInputs.y);
            }

            if (InputManager.direction.x == 0f && InputManager.direction.y == 0f)
            {
                animator.SetBool("isWalking", false);
                animator.SetFloat("LastInputX", lastInputs.x);
                animator.SetFloat("LastInputY", lastInputs.y);
            }

            /*if (Mathf.Abs(rb.linearVelocityX) >= 0.01f || Mathf.Abs(rb.linearVelocityY) >= 0.01f)
            {
                animator.SetBool("isWalking", true);
                lastInputs = rb.linearVelocity;
                animator.SetFloat("InputX", lastInputs.x);
                animator.SetFloat("InputY", lastInputs.y);
            }

            if (rb.linearVelocityX == 0f && rb.linearVelocityY == 0f)
            {
                animator.SetBool("isWalking", false);
                animator.SetFloat("LastInputX", lastInputs.x);
                animator.SetFloat("LastInputY", lastInputs.y);
            }*/
        }
       
    }
}
