using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Character[] classes;
    private bool paused = false;

    public void Awake()
    {
        GameObject.Instantiate(classes[PlayerPrefs.GetInt("classID")]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Time.timeScale = 0f;
            paused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            Time.timeScale = 1.0f;
            paused = false;
        }
    }
}