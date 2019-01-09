using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int currentSceneIndex;

    private void Awake()
    {
        int levelManagerCount = FindObjectsOfType<LevelManager>().Length;
        if (levelManagerCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    private void Start()
    {
        StartCoroutine(UpdateSceneIndex());
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
        StartCoroutine(UpdateSceneIndex());
    }

    public void LoadPreviousLevel()
    {
        if (currentSceneIndex < 1)
            currentSceneIndex = 1;
        SceneManager.LoadScene(currentSceneIndex - 1);
        StartCoroutine(UpdateSceneIndex());
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
        StartCoroutine(UpdateSceneIndex());
    }

    IEnumerator UpdateSceneIndex()
    {
        yield return 0;
        if(currentSceneIndex<SceneManager.sceneCount) //%
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
