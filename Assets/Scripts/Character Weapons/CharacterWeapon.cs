using System;
using System.Collections;
using UnityEngine;

public abstract class CharacterWeapon : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// A reference to the character script
    /// </summary>
    protected Character character;
    
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireCooldown;

    private bool _canShoot = true;
    
    #endregion Fields

    #region Properties

    #endregion Properties

    #region Unity Methods

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Get the character script
        character = GetComponent<Character>();
    }

    #endregion

    #region Methods

    public void Fire()
    {
        // If the weapon can't shoot, return
        if (!_canShoot)
            return;
        
        // Run the custom fire method
        CustomFire();

        // Set the can shoot variable to false
        _canShoot = false;
        
        // Reset the can shoot variable
        Invoke(nameof(ResetCanShoot), fireCooldown);
    }

    protected abstract void CustomFire();
    
    private void ResetCanShoot()
    {
        _canShoot = true;
    }

    #endregion Methods
}