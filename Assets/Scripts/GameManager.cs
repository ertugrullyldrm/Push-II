using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public int LevelIndex;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        if (PlayerPrefs.HasKey("LevelIndex"))
        {
            LevelIndex = PlayerPrefs.GetInt("LevelIndex");
        }
        else
        {
            LevelIndex = 1;
            PlayerPrefs.SetInt("LevelIndex", 1);
        }
    }
}
