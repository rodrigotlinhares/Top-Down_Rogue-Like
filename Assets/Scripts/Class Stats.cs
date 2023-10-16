using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassStats : MonoBehaviour
{
    public readonly struct Stats
    {
        public readonly int health;
        public readonly Color color;

        public Stats(int h, Color c)
        {
            health = h;
            color = c;
        }
    }

    public static Dictionary<string, Stats> stats = new Dictionary<string, Stats>()
    {
        {"Warrior", new Stats(5, new Color(0.5019608f, 0.2862745f, 0.1921569f))},
        {"Mage", new Stats(5, new Color(0.227451f, 0.6235294f, 0.6431373f))},
        {"Rogue", new Stats(5, new Color(0.772549f, 0.7686275f, 0.2f))},
        {"Blood Mage", new Stats(5, new Color(0.8980392f, 0.1764706f, 0.07058824f))},
        {"Warlock", new Stats(5, new Color(0.3686275f, 0.1607843f, 0.5568628f))}
    };
}
