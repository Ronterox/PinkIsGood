

[System.Serializable]
public class PlayerData
{
    public float speed;
    public int lastCheckpoint;
    public bool completedGame;

    public PlayerData(PlayerData data)
    {
        speed = data.speed;
        lastCheckpoint = data.lastCheckpoint;
        completedGame = data.completedGame;
    }
    public PlayerData(float speed, int checkpoint, bool gameCompleted)
    {
        this.speed = speed;
        lastCheckpoint = checkpoint;
        completedGame = gameCompleted;
    }
}
