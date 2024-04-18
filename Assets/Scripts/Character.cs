using System;
using TMPro;
using UnityEngine;

[Serializable]
public class Character : Actor
{
    #region Fields

    /// <summary>
    /// A text object to display the character's health / lives
    /// </summary>
    [SerializeField] private TMP_Text _healthText;

    private Rigidbody2D _rb;
    
    private bool _invincible;
    
    #endregion

    #region Properties

    private CharacterClass CharacterClass => GameManagerScript.Instance.CharacterClass;
    
    /// <summary>
    /// The character's speed.
    /// The speed is determined by the character's class.
    /// </summary>
    public override float Speed
    {
        get
        {
            // The speed of the character's class
            float classSpeed = 0;

            // The speed is determined by the character's class
            switch (CharacterClass)
            {
                case CharacterClass.Balanced:
                    classSpeed = 6;
                    break;
                
                case CharacterClass.Melee:
                    classSpeed = 8;
                    break;
                
                case CharacterClass.Spread:
                    classSpeed = 4;
                    break;
            }
            
            return classSpeed;
        }
    }
    
    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        
        // Get the character's rigid body
        _rb = GetComponent<Rigidbody2D>();
        
        // set the invincible flag to false
        _invincible = false;
        
        // Get the character's animations
        _animator = GetComponent<Animator>();
        
        // Set the character's health text
        UpdateHealthText();
        
        OnDeath += LogDeath;
        OnDeath += FlashBlackOnDeath;
        OnDeath += FreezeRigidBodyOnDeath;
        OnDeath += GlobalLevelScript.Instance.OnPlayerDeath;
    }

    #endregion

    #region Methods


    public override void ChangeHealth(int changeAmount)
    {
        // If the character is dead, return
        if (_currentHealth <= 0)
            return;
        
        // if the character is invincible, return
        if (_invincible)
            return;
        
        base.ChangeHealth(changeAmount);
        
        UpdateHealthText();
        
        // set the invincible flag to true
        _invincible = true;
        
        // invoke the function to set the invincible flag to false after a delay
        Invoke(nameof(SetInvincibleFalse), INVINCIBILITY_DURATION);
    }
    
    private void SetInvincibleFalse()
    {
        _invincible = false;
    }
    
    private void UpdateHealthText()
    {
        _healthText.text = $"x{_currentHealth}";
    }

    private void LogDeath()
    {
        // Log the character's death
        Debug.Log("The character has died!");
    }
    
    
    private void FlashBlackOnDeath()
    {
        Flash(Color.black, 30, .5f);
    }
    
    private void FreezeRigidBodyOnDeath()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    #endregion
}