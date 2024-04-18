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


    public void SelectedPowerup(int i)
    {

        switch (i)
        {
            case 0:
                //Increase damage here 
                PlayerController.Instance.GetComponent<CharacterWeapon>().IncreaseWeaponDamage(1);
                break;
            case 1:
                //Increase Move Speed
                PlayerController.Instance.speedModifier += 1;
                break;
            case 2:
                //Heal 1
                PlayerController.Instance.gameObject.GetComponent<Character>().ChangeHealth(1);
                break;
        }
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
