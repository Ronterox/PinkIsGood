using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;
    [SerializeField] Door door = null;
    [SerializeField] SpeedAlterer speedAlterer = null;
    [SerializeField] PlayerMovement player = null;
    public bool gameCompleted = false;

    [SerializeField] Animator animator = null;

    private void Awake()
    {
        if (MakeSingleton())
            LoadProgress();
    }
    private bool MakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return true;
        }
        else
        {
            Destroy(gameObject);
            return false;
        }
    }

    private void LoadProgress()
    {
        PlayerData data = SaveSystem.LoadGame();

        if (data != null)
        {
            door.lastCheckpoint = data.lastCheckpoint;
            speedAlterer.SetSpeed(data.speed);
            gameCompleted = data.completedGame;

            Debug.Log("Loaded: Player Speed " + data.speed + ". Checkpoint " + data.lastCheckpoint + ". GameCompleted " + data.completedGame);
        }
        else
        {
            door.lastCheckpoint = 1;
            speedAlterer.SetSpeed(20);
            gameCompleted = false;
        }
    }

    public void SaveProgress()
    {
        int checkpoint;
        if (GameManager.instance == null)
        {
            if (door == null)
                door = FindObjectOfType<Door>();
            checkpoint = door.lastCheckpoint;
        }
        else
            checkpoint = GameManager.instance.lastCheckpoint;

        if (player == null)
            player = FindObjectOfType<PlayerMovement>();

        PlayerData data = new PlayerData(player.speed, checkpoint, gameCompleted);
        SaveSystem.SaveGame(data);
        Debug.Log("Game saved with: Player Speed " + player.speed + ". Checkpoint " + checkpoint + ". GameCompleted " + gameCompleted);

        if (animator != null)
            animator.SetTrigger("Saved");
    }
}
