using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadSceneByIndex : MonoBehaviour
{
    public GameObject UnlockUI;
    public int maxLevel = 100;
    // General method to load scenes based on build index
    public void LoadLastLevel()
    {
        int lastLevelIndex = 1;
        
        for (int i = 1; i < maxLevel; i++)
        {
            if (PlayerPrefs.GetInt("Level" + i, 0) == 1)
            {
                lastLevelIndex = i;
            }
            else { break; }
        }
        SceneManager.LoadScene(lastLevelIndex+1);
    }
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadLevel(int levelIndex)
    {
        if (PlayerPrefs.GetInt("Level" + levelIndex, 0) == 1)
        {
            SceneManager.LoadScene(levelIndex + 1);
        }
        else
        {
            StartCoroutine(ShowAndHideUI());
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator ShowAndHideUI()
    {
        UnlockUI.SetActive(true);  
        yield return new WaitForSeconds(1f); 
        UnlockUI.SetActive(false);  
    }
}
