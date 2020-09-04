using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Quit Game");
        DataManager.instance.SaveProgress();
        Application.Quit();
    }
}
