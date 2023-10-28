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

    void StartGame(int classID)
    {
        PlayerPrefs.SetInt("classID", classID);
        SceneManager.LoadSceneAsync("Game");
    }
}
