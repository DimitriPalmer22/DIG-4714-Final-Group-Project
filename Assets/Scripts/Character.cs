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

    #endregion

    #region Properties

    // ! TODO: Change the property to use the CharacterClass enum for speed
    /// <summary>
    /// The character's speed
    /// </summary>
    private float Speed => 5;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        // Get the character's rigid body
        _rb = GetComponent<Rigidbody2D>();
        
        // Get the character's sprite renderer
        _spriteRenderer = GetComponent<SpriteRenderer>();
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

    #endregion
}