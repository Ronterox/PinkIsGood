using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] Door door = null;

    public TextMeshProUGUI healthText = null;
    [SerializeField] HealthManager playerHealth = null;
    public Animator heartAnimator = null;

    [SerializeField] float resetTime = 5;
    [SerializeField] float timer = 0;

    public int lastCheckpoint = 0;

    public int totalEnemies;
    private int currentKills;
    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        CheckResetButton();

        if (playerHealth == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerHealth = player.GetComponent<HealthManager>();
        }
    }

    public void GainKill()
    {
        currentKills++;
        if (currentKills == totalEnemies)
        {
            door.OpenDoor();
            door.animator.SetBool("isOpen", true);
            AudioManager.instance.Play("Door");
        }
    }

    public void LoadNextLevel()
    {
        currentKills = 0;
        door.CloseDoor();
        door.animator.SetBool("isOpen", false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void FullReset()
    {
        Time.timeScale = 1;
        lastCheckpoint = 1;
        SceneManager.LoadScene(0);
    }

    private void CheckResetButton()
    {
        if (Input.GetKeyDown("r"))
        {
            timer = resetTime;
        }
        if (Input.GetKey("r"))
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0 && Input.GetKey("r"))
            FullReset();
    }

    public void KillPlayer()
    {
        StartCoroutine(playerHealth.Die());
    }
}
