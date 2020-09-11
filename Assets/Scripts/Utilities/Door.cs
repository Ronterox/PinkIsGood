using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] bool isOpen = false;
    public Animator animator = null;
    [SerializeField] bool isStart = false;
    public int lastCheckpoint = 0;

    private void Start()
    {
        animator.SetBool("isOpen", isOpen);
        if (isStart)
        {
            if (GameManager.instance != null)
            {
                lastCheckpoint = GameManager.instance.lastCheckpoint;
                Destroy(GameManager.instance.gameObject);
                if (AudioManager.instance.isThemePlaying)
                {
                    AudioManager.instance.Stop("Combat");
                    AudioManager.instance.isThemePlaying = false;
                }
            }
        }
        if (isOpen)
            AudioManager.instance.Play("Door");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOpen && collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance != null)
                GameManager.instance.LoadNextLevel();
            else
                SceneManager.LoadScene(lastCheckpoint);
            if (isStart)
                DataManager.instance.SaveProgress();
        }
    }

    public void OpenDoor()
    {
        isOpen = true;
        AudioManager.instance.Play("Door");
    }
    public void CloseDoor()
    {
        isOpen = false;
        AudioManager.instance.Play("Door");
    }
}
