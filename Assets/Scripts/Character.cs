using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    #region Fields

    // Enum for character class
    [SerializeField] private CharacterClass characterClass;

    // The character's rigid body
    private Rigidbody2D _rb;

    // The movement input vector
    private Vector2 _movementInput;

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
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
    }

    void FixedUpdate()
    {
        MovePlayer();
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

#endregion
    

}
