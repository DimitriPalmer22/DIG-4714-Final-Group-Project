using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Static Fields

    // These are used by the animator to determine the character's animation.
    // These should only be used in the DetermineAnimation method.
    private static readonly int XInput = Animator.StringToHash("XInput");
    private static readonly int YInput = Animator.StringToHash("YInput");
    private static readonly int SpeedForAnimation = Animator.StringToHash("Speed");
    private static readonly int ChangeAnimation = Animator.StringToHash("ChangeAnimation");

    #endregion

    #region Fields

    /// <summary>
    /// The character's rigid body 
    /// </summary>
    private Rigidbody2D _rb;

    /// <summary>
    /// The character's actor script
    /// </summary>
    private Actor _actorScript;

    /// <summary>
    /// The animator for the player
    /// </summary>
    /// <returns></returns>
    private Animator _animator;

    /// <summary>
    /// The vector for the direction the character is facing
    /// </summary>
    private Vector2 _direction;

    #endregion Fields

    #region Unity Methods

    // Start is called before the first frame update
    private void Start()
    {
        // Get the character's rigid body
        _rb = GetComponent<Rigidbody2D>();

        // Get the character's actor script
        _actorScript = GetComponent<Actor>();

        // Get the character's animator
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Get the movement input from the player
        GetMovementInput();
    }

    private void FixedUpdate()
    {
        // Move the character based on the movement input vector
        MovePlayer();
    }

    private void LateUpdate()
    {
        // Determine the character's current animation 
        DetermineAnimation();
    }

    #endregion Unity Methods

    #region Methods

    /// <summary>
    /// Get the movement input vector from the player's input.
    /// Supposed to be called in Update.
    /// </summary>
    private void GetMovementInput()
    {
        // If the game is paused, don't get any input
        if (GameManagerScript.Instance.IsPaused)
            return;

        // Create a vector2 of the horizontal and vertical input axes
        _actorScript.MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Normalize the vector to prevent faster diagonal movement
        _actorScript.MovementInput.Normalize();

        // TODO: Delete Later

        // Take damage if the player presses the "q" key
        if (Input.GetKeyDown(KeyCode.Q))
            _actorScript.ChangeHealth(-1);

        // Heal if the player presses the "e" key
        if (Input.GetKeyDown(KeyCode.E))
            _actorScript.ChangeHealth(1);
        
        // Change the score if the player presses the "r" or "t" key
        if (Input.GetKeyDown(KeyCode.R))
            GameManagerScript.Instance.ChangeScore(-5);
        
        if (Input.GetKeyDown(KeyCode.T))
            GameManagerScript.Instance.ChangeScore(5);

    }

    /// <summary>
    /// Apply the movement input to the character's rigid body.
    /// Supposed to be called in FixedUpdate.
    /// </summary>
    private void MovePlayer()
    {
        // Get the character's current direction
        var prevDirection = _direction.normalized;

        var prevSpeed = _rb.velocity;

        // Apply the movement input to the character's rigid body
        _rb.velocity = _actorScript.MovementInput * _actorScript.Speed;

        // Change the character's animation if the direction or speed has changed
        if (prevDirection != _direction.normalized ||
            Math.Abs(SpeedAnimationHelper(prevSpeed.magnitude) - SpeedAnimationHelper(_rb.velocity.normalized.magnitude)) > 0.01f
        )
            _animator.SetTrigger(ChangeAnimation);

        // Change the direction the character is facing
        if (_actorScript.MovementInput.magnitude >= 0.1f)
            _direction = _actorScript.MovementInput.normalized;
    }

    private void DetermineAnimation()
    {
        // Determine the direction the sprite is facing
        _animator.SetFloat(XInput, _direction.x);
        _animator.SetFloat(YInput, _direction.y);

        // Determine if the character is idle or moving
        _animator.SetFloat(SpeedForAnimation, _actorScript.MovementInput.magnitude);
    }

    private float SpeedAnimationHelper(float amt)
    {
        return Mathf.Abs(amt) > 0.1f ? 1 : 0;
    }

    #endregion Methods
}