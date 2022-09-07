using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    [SerializeField][Range(0f, 3f)] float sceneLoadDelay = 1f;
    [SerializeField][Range(0f, 3f)] float exitGameDelay = 1f;

    public void OnClickStart()
    {
        Invoke("StartScene", sceneLoadDelay);
    }

    public void OnClickOptions()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //recarga la escena actual
    }

    public void OnClickExit()
    {
        Invoke("ExitGame", exitGameDelay);
    }

    void StartScene()
    {
        SceneManager.LoadScene("Scene_1"); //o puedo usar el indice de la escena así .LoadScene(1);
    }

    void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
