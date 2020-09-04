using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] int specificCheckpoint = 0;
    [SerializeField] bool useSpecific = false;
    [SerializeField] bool lastCheckpoint = false;
    private int newCheckpoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lastCheckpoint)
            DataManager.instance.gameCompleted = true;

        if (useSpecific)
            newCheckpoint = specificCheckpoint;
        else
            newCheckpoint = SceneManager.GetActiveScene().buildIndex;

        if (newCheckpoint != GameManager.instance.lastCheckpoint)
        {
            GameManager.instance.lastCheckpoint = newCheckpoint;
            DataManager.instance.SaveProgress();
        }

        Destroy(gameObject);
    }
}
