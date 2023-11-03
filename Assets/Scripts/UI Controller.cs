using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private void Start()
    {
        EventSystem.events.OnClassClick += StartGame;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnClassClick -= StartGame;
    }

    private void StartGame(int classID)
    {
        PlayerPrefs.SetInt("classID", classID);
        SceneManager.LoadSceneAsync("Game");
    }
}
