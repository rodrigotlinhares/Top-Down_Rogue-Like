using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Character[] classes;

    public void Awake()
    {
        GameObject.Instantiate(classes[PlayerPrefs.GetInt("classID")]);
    }
}