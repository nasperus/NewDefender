using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delay;
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {


        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();



    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(2);
    }


}
