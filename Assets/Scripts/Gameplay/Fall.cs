using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            AudioManager.instance.Play("Player Fall");
            GameManager.instance.KillPlayer();
        }
    }
}
