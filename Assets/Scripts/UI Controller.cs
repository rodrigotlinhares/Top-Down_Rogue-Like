using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    void OnEnable()
    {
        ClassChoice.OnClick += StartGame;
    }

    void OnDisable()
    {
        ClassChoice.OnClick -= StartGame;
    }

    void StartGame(string playerClass)
    {
        PlayerController.className = playerClass;
        SceneManager.LoadSceneAsync("Game");
    }
}
