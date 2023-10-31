using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Player[] classes;

    public void Awake()
    {
        GameObject.Instantiate(classes[PlayerPrefs.GetInt("classID")]);
    }
}