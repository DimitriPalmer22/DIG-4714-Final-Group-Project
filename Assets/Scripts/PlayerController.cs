using System;
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

    public static PlayerController Instance { get; private set; }

    private const KeyCode FireUp = KeyCode.UpArrow;
    private const KeyCode FireDown = KeyCode.DownArrow;
    private const KeyCode FireLeft = KeyCode.LeftArrow;
    private const KeyCode FireRight = KeyCode.RightArrow;

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

    /// <summary>
    /// A reference to the character's weapon
    /// </summary>
    private CharacterWeapon _characterWeapon;

    public int speedModifier;

    private Vector2 _fireDirection;

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

        // Get the character's weapon
        _characterWeapon = GetComponent<CharacterWeapon>();

        if (Instance == null)
        {
            Instance = this;
        }
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

        // Fire the weapon based on the fire input vector
        GetFireInput();
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
        // Set the movement input to zero
        _actorScript.MovementInput = Vector2.zero;

        // If the game is paused, don't get any input
        // If the player is dead, don't get any input
        if (GlobalLevelScript.Instance.IsPaused ||
            GlobalLevelScript.Instance.IsPlayerDead)
            return;

        // Create a vector2 of the horizontal and vertical input axes
        _actorScript.MovementInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        // Normalize the vector to prevent faster diagonal movement
        _actorScript.MovementInput.Normalize();

        // TODO: Delete Later

        // Take damage if the player presses the "q" key
        // if (Input.GetKeyDown(KeyCode.Q))
        //     _actorScript.ChangeHealth(-1);
        //
        // // Heal if the player presses the "e" key
        // if (Input.GetKeyDown(KeyCode.E))
        //     _actorScript.ChangeHealth(1);
        //
        // // Change the score if the player presses the "r" or "t" key
        // if (Input.GetKeyDown(KeyCode.R))
        //     GlobalLevelScript.Instance.ChangeScore(-5);
        //
        // if (Input.GetKeyDown(KeyCode.T))
        //     GlobalLevelScript.Instance.ChangeScore(5);
        //
        // if (Input.GetKeyDown(KeyCode.L))
        //     GameManagerScript.Instance.SaveGame();
    }

    private void GetFireInput()
    {
        // If the game is paused, don't get any input
        // If the player is dead, don't get any input
        if (GlobalLevelScript.Instance.IsPaused ||
            GlobalLevelScript.Instance.IsPlayerDead)
            return;

        // flag to check if the player has fired
        var fired = false;

        float fireHorizontal = 0;
        float fireVertical = 0;

        // Get the fire input from the player
        if (Input.GetKey(FireUp))
        {
            fireVertical = 1;
            fired = true;
        }
        else if (Input.GetKey(FireDown))
        {
            fireVertical = -1;
            fired = true;
        }

        if (Input.GetKey(FireLeft))
        {
            fireHorizontal = -1;
            fired = true;
        }
        else if (Input.GetKey(FireRight))
        {
            fireHorizontal = 1;
            fired = true;
        }

        _fireDirection = new Vector2(fireHorizontal, fireVertical).normalized;

        // Shoot if the player presses space
        if (fired)
            _characterWeapon.Fire(_fireDirection);
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
        _rb.velocity = _actorScript.MovementInput * (_actorScript.Speed + speedModifier);

        // Change the character's animation if the direction or speed has changed
        if (prevDirection != _direction.normalized ||
            Math.Abs(
                SpeedAnimationHelper(prevSpeed.magnitude) - SpeedAnimationHelper(_rb.velocity.normalized.magnitude)) >
            0.01f
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