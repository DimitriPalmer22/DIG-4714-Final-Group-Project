using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : Actor
{
    #region Fields

    /// <summary>
    /// Enum for character class 
    /// </summary>
    [SerializeField] private CharacterClass characterClass;

    #endregion

    #region Properties

    // ! TODO: Change the property to use the CharacterClass enum for speed
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
            switch (characterClass)
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

                default:
                    break;
            }
            
            return classSpeed;
        }
    }
    

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        // Get the character's sprite renderer
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Get the character's animations
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    #endregion

    #region Methods


    #endregion
}