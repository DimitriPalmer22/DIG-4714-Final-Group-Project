using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Fields

    // Enum for character class
    [SerializeField] private CharacterClass characterClass;

    /// <summary>
    /// The character's rigid body 
    /// </summary>
    private Rigidbody2D _rb;

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
    /// The character's speed
    /// </summary>
    public float Speed => 5;
    
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

        // Determine the character's direction
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

    public void ChangeHealth(int amt)
    {
        // If the amount is negative, the character is taking damage
        if (amt < 0)
        {
            // Start a coroutine to flash the character's sprite red
            StartCoroutine(FlashRed());
        }
        
        // If the amount is positive, the character is healing
        else if (amt > 0)
        {

        }
        
        // Update the character's health
        _currentHealth += amt;
        
        // clamp the player's current Health to the range 0 to _maxHealth
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }
    
    // Create a coroutine to flash the character's sprite red
    private IEnumerator FlashRed()
    {
        float blinkTime = 0.2f;

        float totalFlashTime = 1f;
        
        for (float currentFlashTime = 0; currentFlashTime < totalFlashTime; currentFlashTime += blinkTime)
        {
            // Set the sprite color to red
            _spriteRenderer.color = Color.red;
        
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