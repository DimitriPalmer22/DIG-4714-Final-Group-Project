using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelUpScript : MonoBehaviour
{
    [SerializeField] private Button[] levelUpButtons = new Button[3];
    // Start is called before the first frame update
    void OnEnable()
    {
        if (this.gameObject.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(levelUpButtons[0].gameObject);
            Time.timeScale = 0;
        }

    }


    public void SelectedPowerup()
    {
        //edit player stats here
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
