using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform attackPoint = null;
    [SerializeField] float attackRange = 1;
    [SerializeField] LayerMask whatIsRepealable;

    [SerializeField] float knockForce = 5;

    [SerializeField] Animator attackAnimator = null;

    private void Update()
    {
        if (Input.GetKeyDown("z") || Input.GetKeyDown("x") || Input.GetKeyDown("o") || Input.GetKeyDown("p"))
        {
            AudioManager.instance.Play("Player Attack");
            Repel();
            if (attackAnimator != null)
                attackAnimator.SetTrigger("Attack");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Repel()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, whatIsRepealable.value);
        foreach (Collider2D collision in collisions)
        {
            Rigidbody2D rigidbdyCol = collision.GetComponent<Rigidbody2D>();

            Vector2 direction = collision.transform.position - transform.position;

            rigidbdyCol.velocity = Vector2.zero;
            rigidbdyCol.AddForce(direction.normalized * knockForce, ForceMode2D.Impulse);

            AudioManager.instance.Play("Repeal");
        }
    }
}
