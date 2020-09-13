using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedAlterer : MonoBehaviour
{
    [SerializeField] float speedBonus = 5;
    [SerializeField] TextMeshProUGUI speedTextValue = null;
    [SerializeField] PlayerMovement player = null;
    private void Start()
    {
        if (speedTextValue != null)
            speedTextValue.text = player.speed.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            player.speed += speedBonus;

            if (player.speed < 1)
                player.speed = 5;

            if (speedTextValue != null)
                speedTextValue.text = player.speed.ToString();
        }
    }

    public void SetSpeed(float speed)
    {
        player.speed = speed;

        if (speedTextValue != null)
            speedTextValue.text = player.speed.ToString();
    }
}
