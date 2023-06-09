using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbarBehaviourPlayer : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 offset;
    public player_movement player;


    public void setHealth(float maxHealth, float health)
    {

        slider.gameObject.SetActive(true);

        slider.maxValue = maxHealth;
        slider.value = health;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }
}
