using System;
using UnityEngine;

public class ClassChoice : MonoBehaviour
{
    [SerializeField] private int classId;
    [SerializeField] private Color color;

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
        EventSystem.events.ClassClick(classId);
    }
}
