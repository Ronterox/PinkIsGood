using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelVariables : MonoBehaviour
{
    [SerializeField] int totalEnemies;

    [SerializeField] GameObject monstersHolder = null;
    private void Start()
    {
        foreach (Transform child in monstersHolder.GetComponentInChildren<Transform>())
        {
            totalEnemies++;
        }
        GameManager.instance.totalEnemies = totalEnemies;

        monstersHolder.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        monstersHolder.SetActive(true);

        AudioManager.instance.Play("Plate");
        if (!AudioManager.instance.isThemePlaying)
        {
            AudioManager.instance.isThemePlaying = true;
            AudioManager.instance.Play("Combat", 1);
        }

        Destroy(gameObject);
    }
}
