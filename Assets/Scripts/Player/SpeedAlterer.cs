using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedAlterer : MonoBehaviour
{
    [SerializeField] float speedBonus = 5;
    [SerializeField] bool positive = false;
    [SerializeField] TextMeshProUGUI speedText = null;
    [SerializeField] PlayerMovement player = null;
    private void Start()
    {
        if (speedText != null)
            speedText.text = player.speed.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (positive)
                player.speed += speedBonus;
            else
                player.speed -= speedBonus;

            if (player.speed < 1)
                player.speed = 5;

            collision.GetComponent<PlayerMovement>().speed = player.speed;

            if (speedText != null)
                speedText.text = player.speed.ToString();
        }
    }

    public void SetSpeed(float speed)
    {
        player.speed = speed;

        FindObjectOfType<PlayerMovement>().speed = player.speed;

        if (speedText != null)
            speedText.text = player.speed.ToString();
    }
}
