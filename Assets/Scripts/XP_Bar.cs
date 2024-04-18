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
    private int level;
    
    public int Level => level;

    void Start()
    {
        xpBar.value = 0;
        level = 1;
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
        level += 1;
        Debug.Log("Level up! " + level);
        levelText.text = $"Aggression Level: {level}";
        levelUpOptions.SetActive(true);
    }
}