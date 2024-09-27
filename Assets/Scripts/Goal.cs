using UnityEngine;

public class Goal : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (LevelManager.Instance.IsPlaying)
            {
                GetComponent<AudioSource>().Play();
                GameManager.Instance.LevelIndex++;
                int x = GameManager.Instance.LevelIndex;
                if (GameManager.Instance.LevelIndex == 7)
                {
                    GameManager.Instance.LevelIndex = 1;
                    PlayerPrefs.SetInt("LevelIndex", GameManager.Instance.LevelIndex);
                    x = 0;
                }
                LevelManager.Instance.IsPlaying = false;
                LevelManager.Instance.LoadNextLevel(x);
            }
        }
    }
}
