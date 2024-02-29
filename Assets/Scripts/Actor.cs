using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{

    #region Fields
    
    /// <summary>
    /// The animator for the player
    /// </summary>
    protected Animator _animator;

    /// <summary>
    /// The character's sprite renderer
    /// </summary>
    protected SpriteRenderer _spriteRenderer;

    /// <summary>
    /// The movement input vector 
    /// </summary>
    protected Vector2 _movementInput;

    /// <summary>
    /// The direction the character is facing.
    /// false = right, true = left
    /// </summary>
    protected bool _direction;
    
    /// <summary>
    /// The character's health
    /// </summary>
    [SerializeField] protected int _currentHealth;

    /// <summary>
    /// The character's max health
    /// </summary>
    [SerializeField] protected int _maxHealth;

    #endregion 
    
    #region Properties

    /// <summary>
    /// The actor's speed.
    /// Implemented differently based on the actor's type (Player Character vs Enemy).
    /// </summary>
    public abstract float Speed { get; }
    
    /// <summary>
    /// The character's movement input vector
    /// </summary>
    public Vector2 MovementInput
    {
        get => _movementInput;
        set => _movementInput = value;
    }
    
    /// <summary>
    /// Test if the character is facing left
    /// </summary>
    public bool FacingLeft => _direction;

    /// <summary>
    /// // Test if the character is facing right
    /// </summary>
    public bool FacingRight => !_direction;
    
    #endregion Properties

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        // Set the character's health to the max health
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        // Determine the character's direction and flip the sprite based on the direction
        DetermineSpriteDirection();
    }
    
    #endregion
    
    #region Methods

    
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

    
    #endregion Methods
    
}
