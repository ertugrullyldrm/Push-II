using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public GameObject[] player;

    public bool IsPlaying;

    private GameObject prevSelected, currSelected;

    [SerializeField] private Slider loading;
    [SerializeField] private GameObject uiContainer;
    [SerializeField] private GameObject prevBtn;

    private void Awake()
    {
        Instance = this;
        IsPlaying = true;
        currSelected = player[0];
        player[0].GetComponent<PlayerManager>().IsSelected = true;
        ChangeCameraTarget(player[0].transform);
    }


    public void PauseMenu(int index)
    {
        switch (index)
        {
            case 0://resume
                IsPlaying = !IsPlaying;
                break;
            case 1://restart
                LoadNextLevel(GameManager.Instance.LevelIndex);
                break;
            case 2://go to main menu
                uiContainer.SetActive(true);
                StartCoroutine(LoadAsynchronously(0));
                break;
        }
    }

    private void Update()
    {
        if (IsPlaying)
        {
            if (Input.GetKeyDown(KeyCode.R) && prevSelected != null)
            {
                GameObject temp = currSelected;
                currSelected = prevSelected;
                prevSelected = temp;
                currSelected.GetComponent<PlayerManager>().IsSelected = true;
                prevSelected.GetComponent<PlayerManager>().IsSelected = false;
                ChangeCameraTarget(currSelected.transform);
            }
        }
    }

    public void GoPrevSelected()
    {
        if (IsPlaying && prevSelected != null)
        {
            GameObject temp = currSelected;
            currSelected = prevSelected;
            prevSelected = temp;
            currSelected.GetComponent<PlayerManager>().IsSelected = true;
            prevSelected.GetComponent<PlayerManager>().IsSelected = false;
            ChangeCameraTarget(currSelected.transform);
        }
    }

    public void DisableAllPlayerSelection(GameObject currentSelected)
    {
        currSelected = currentSelected;
        foreach (GameObject i in player)
        {
            if (i.GetComponent<PlayerManager>().IsSelected)
            {
                prevSelected = i;
            }
            i.GetComponent<PlayerManager>().IsSelected = false;
        }
        if (!prevBtn.activeSelf)
        {
            prevBtn.SetActive(true);
        }
    }
    public void ChangeCameraTarget(Transform target)
    {
        Camera.main.GetComponent<CameraController>().Target = target;
    }

    public void LoadNextLevel(int x)
    {
        uiContainer.SetActive(true);
        StartCoroutine(LoadAsynchronously(x));
    }

    /// <summary>
    /// this method for moving from scene to another
    /// </summary>
    /// <param name="sceneNam">name of the scene you will go to</param>
    /// <returns></returns>
    IEnumerator LoadAsynchronously(int sceneNum)
    {
        yield return new WaitForSeconds(0.2f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNum);
        while (!operation.isDone)
        {

            loading.value = operation.progress;

            yield return null;
        }
    }
}
