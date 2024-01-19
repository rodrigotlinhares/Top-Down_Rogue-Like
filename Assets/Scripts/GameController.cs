using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Character[] classes;
    private bool paused = false;

    public void Awake()
    {
        GameObject.Instantiate(classes[PlayerPrefs.GetInt("classID")]);
    }

    private void Start()
    {
        EventSystem.events.OnStageCleared += Pause;
        EventSystem.events.OnMenuClosed += Unpause;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnStageCleared -= Pause;
        EventSystem.events.OnMenuClosed -= Unpause;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
            Pause();
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
            Unpause();
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        paused = true;
        EventSystem.events.GamePaused();
    }

    private void Unpause()
    {
        Time.timeScale = 1.0f;
        paused = false;
        EventSystem.events.GameUnpaused();
    }
}