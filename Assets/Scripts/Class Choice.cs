using System;
using UnityEngine;

public class ClassChoice : MonoBehaviour
{
    [SerializeField]
    private int classID;

    public Color color;
    public static Action<int> OnClick;

    public void Enable()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    public void Disable()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void ChooseClass()
    {
        OnClick?.Invoke(classID);
    }
}
