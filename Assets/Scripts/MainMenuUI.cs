using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private Slider loading;

    public void MainMenu(int index)
    {
        switch (index)
        {
            case 0:
                StartCoroutine(LoadAsynchronously(GameManager.Instance.LevelIndex));
                break;
            case 1:
                Application.Quit();
                break;
        }
    }

    /// <summary>
    /// this method for moving from scene to another
    /// </summary>
    /// <param name="sceneNam">name of the scene you will go to</param>
    /// <returns></returns>
    IEnumerator LoadAsynchronously(int sceneNum)
    {
        yield return new WaitForSeconds(0.6f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNum);
        while (!operation.isDone)
        {
            loading.value = operation.progress;
            yield return null;
        }
    }
}
