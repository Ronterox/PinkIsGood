using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D rgbdy = null;
    [SerializeField] float speed = 5;
    [SerializeField] int damage = 10;
    [SerializeField] TypeOfBullet bulletType = TypeOfBullet.FollowPlayer;

    public enum TypeOfBullet
    {
        FollowPlayer,
        Rectline
    }

    void Start()
    {
        if (bulletType.Equals(TypeOfBullet.FollowPlayer))
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            if (player != null)
            {
                Vector2 direction = player.position - transform.position;
                rgbdy.AddForce(direction.normalized * speed, ForceMode2D.Impulse);
            }
        }
        else
        {
            rgbdy.AddForce(new Vector2(1, 0) * speed, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
