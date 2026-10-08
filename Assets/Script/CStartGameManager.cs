using UnityEngine;
using UnityEngine.SceneManagement;

public class CStartGameManager : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void GameStart()
    {
        Time.timeScale = 1;
    }
}
