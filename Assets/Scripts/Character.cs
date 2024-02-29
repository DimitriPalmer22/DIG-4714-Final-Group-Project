using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : Actor
{
    #region Fields

    // Enum for character class
    [SerializeField] private CharacterClass characterClass;

    /// <summary>
    /// The character's rigid body 
    /// </summary>
    private Rigidbody2D _rb;

    /// <summary>
    /// The animator for the player
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// The character's sprite renderer
    /// </summary>
    private SpriteRenderer _spriteRenderer;

    /// <summary>
    /// The movement input vector 
    /// </summary>
    private Vector2 _movementInput;

    /// <summary>
    /// The direction the character is facing.
    /// false = right, true = left
    /// </summary>
    private bool _direction;

    /// <summary>
    /// The character's health
    /// </summary>
    [SerializeField] private int _currentHealth;

    /// <summary>
    /// The character's max health
    /// </summary>
    [SerializeField] private int _maxHealth;
    
    #endregion

    #region Properties

    // ! TODO: Change the property to use the CharacterClass enum for speed
    /// <summary>
    /// The character's speed.
    /// The speed is determined by the character's class.
    /// </summary>
    public float Speed
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
    
    /// <summary>
    /// Test if the character is facing left
    /// </summary>
    public bool FacingLeft => _direction;

    /// <summary>
    /// // Test if the character is facing right
    /// </summary>
    public bool FacingRight => !_direction;
    
    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        // Get the character's rigid body
        _rb = GetComponent<Rigidbody2D>();
        
        // Get the character's sprite renderer
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Set the character's health to the max health
        _currentHealth = _maxHealth;

        // Get the character's animations
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the movement input from the player
        GetMovementInput();
    }

    void FixedUpdate()
    {
        // Move the character based on the movement input vector
        MovePlayer();
        DetermineSpriteDirection();

    }

    #endregion

    #region Methods

    /// <summary>
    /// Get the movement input vector from the player's input.
    /// Supposed to be called in Update.
    /// </summary>
    private void GetMovementInput()
    {
        // Create a vector2 of the horizontal and vertical input axes
        _movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Normalize the vector to prevent faster diagonal movement
        _movementInput.Normalize();
        
        // Take damage if the player presses the "q" key
        if (Input.GetKeyDown(KeyCode.Q))
            ChangeHealth(-10);
        
        // Heal if the player presses the "e" key
        if (Input.GetKeyDown(KeyCode.E))
            ChangeHealth(10);
    }

    /// <summary>
    /// Apply the movement input to the character's rigid body.
    /// Supposed to be called in FixedUpdate.
    /// </summary>
    private void MovePlayer()
    {
        // Apply the movement input to the character's rigid body
        _rb.velocity = _movementInput * Speed;
    }

    /// <summary>
    /// Determine the character's direction and flip the sprite based on the direction.
    /// </summary>
    private void DetermineSpriteDirection()
    {
        // Determine the character's direction
        // Moving left
        if (_movementInput.x < 0)
            _direction = true;
        // Moving right
        else if (_movementInput.x > 0)
            _direction = false;

        // Flip the character's sprite based on the direction
        _spriteRenderer.flipX = _direction;
    }

    /// <summary>
    /// Change the character's health by a specific amount.
    /// </summary>
    /// <param name="changeAmount">The amount of health to change the character's health by</param>
    public void ChangeHealth(int changeAmount)
    {
        // If the amount is negative, the character is taking damage
        if (changeAmount < 0)
        {
            // Start a coroutine to flash the character's sprite red
            StartCoroutine(FlashSpriteColor(Color.red));
        }
        
        // If the amount is positive, the character is healing
        else if (changeAmount > 0)
        {
            // Start a coroutine to flash the character's sprite red
            StartCoroutine(FlashSpriteColor(Color.green));
        }
        
        // Update the character's health
        _currentHealth += changeAmount;
        
        // clamp the player's current Health to the range 0 to _maxHealth
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }
    
    /// <summary>
    /// A coroutine to flash the character's sprite to a specific color.
    /// </summary>
    /// <returns></returns>
    private IEnumerator FlashSpriteColor(Color flashColor)
    {
        float blinkTime = 0.2f;
        float totalFlashTime = 1f;
        
        // Loop through the flash time to make the sprite blink
        for (float currentFlashTime = 0; currentFlashTime < totalFlashTime; currentFlashTime += blinkTime)
        {
            // Set the sprite color to the flash color
            _spriteRenderer.color = flashColor;
        
            // Wait for half of the blink time
            yield return new WaitForSeconds(blinkTime / 2);
        
            // Set the sprite color back to white
            _spriteRenderer.color = Color.white;
            
            // Wait for half of the blink time
            yield return new WaitForSeconds(blinkTime / 2);
        }

    }
    
    #endregion
}