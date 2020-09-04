using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] Rigidbody2D rgbdy = null;
    [SerializeField] Animator animator = null;
    private Vector2 moveInput;
    [SerializeField] bool isFacingRight;
    private bool isWalking;
    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (rgbdy.velocity.x != 0 || rgbdy.velocity.y != 0)
            isWalking = true;
        else if (isWalking)
        {
            isWalking = false;
            AudioManager.instance.Play("Step");
        }

        if (moveInput.x > 0 && !isFacingRight)
            Flip();
        else if (moveInput.x < 0 && isFacingRight)
            Flip();

        if (animator != null)
            animator.SetBool("isWalking", isWalking);

    }

    private void FixedUpdate()
    {
        rgbdy.velocity = moveInput * speed;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
