using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int currentSceneIndex;
    bool sceneUpdated = false;

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
        UpdateSceneIndex();
    }

    private void Update()
    {
        if (!sceneUpdated)
            UpdateSceneIndex();
    }

    public void LoadNextLevel()
    {
        sceneUpdated = false;
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings -1)
            SceneManager.LoadScene(currentSceneIndex + 1);
        else
            SceneManager.LoadScene(0);
    }

    public void LoadPreviousLevel()
    {
        sceneUpdated = false;
        if (currentSceneIndex < 1)
            SceneManager.LoadScene(currentSceneIndex);
        else
            SceneManager.LoadScene(currentSceneIndex - 1);
    }

    public void RestartLevel()
    {
        sceneUpdated = false;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void UpdateSceneIndex()
    {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
