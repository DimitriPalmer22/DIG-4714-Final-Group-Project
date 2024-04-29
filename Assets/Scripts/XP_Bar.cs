using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class XP_Bar : MonoBehaviour
{
    [SerializeField] private Slider xpBar;
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private GameObject levelUpOptions;

    [Tooltip("The level up options appear every X levels.")] [SerializeField]
    private int powerUpEvery = 1;

    private int _level;

    private static XP_Bar _instance;

    public static XP_Bar Instance => _instance;

    public event Action OnLevelUp;

    public int Level => _level;

    private void Awake()
    {
        // Set the singleton instance
        _instance = this;

        OnLevelUp += EnableLevelUpOptions;
    }

    void Start()
    {
        xpBar.value = 0;
        _level = 1;
    }

    public void AddXP(float xp)
    {
        xpBar.value += (xp / 100);

        while (xpBar.value >= 1)
        {
            xpBar.value -= 1;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        _level += 1;
        Debug.Log("Level up! " + _level);
        levelText.text = $"Aggression Level: {_level}";

        xpBar.maxValue += .1f;

        // Invoke the event
        OnLevelUp?.Invoke();
    }
    
    private void EnableLevelUpOptions()
    {
        // Check if the level is a multiple of the power up every value
        if (_level % powerUpEvery != 0)
            return;
        
        levelUpOptions.SetActive(true);
    }
}