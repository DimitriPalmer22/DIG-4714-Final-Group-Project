using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// A reference to the character script
    /// </summary>
    protected Character character;

    /// <summary>
    /// The direction the bullet will travel in when fired
    /// </summary>
    protected Vector2 fireDirection;

    /// <summary>
    /// A flag to determine if the weapon can shoot
    /// </summary>
    private bool _canShoot = true;

    private CharacterWeaponInfo weaponInfo;

    #endregion Fields

    #region Properties

    public Vector2 FireDirection => fireDirection;

    public Character Character => character;

    #endregion Properties

    #region Unity Methods

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Get the character script
        character = GetComponent<Character>();

        // Initialize the fire direction to down
        fireDirection = Vector2.right;

        // Get the weapon info
        weaponInfo = GetComponent<CharacterWeaponInfo>();

        // Set the weapon info's weapon to this weapon
        weaponInfo.weapon = this;
    }

    protected virtual void Update()
    {
        if (character.MovementInput != Vector2.zero)
            fireDirection = character.MovementInput.normalized;
    }

    #endregion

    #region Methods

    public void Fire()
    {
        // If the weapon can't shoot, return
        if (!_canShoot)
            return;

        // Run the custom fire method
        weaponInfo.CustomFire();

        // Set the can shoot variable to false
        _canShoot = false;

        // Reset the can shoot variable
        Invoke(nameof(ResetCanShoot), weaponInfo.fireCooldown);
    }

    public void ChangeWeaponInfo(CharacterWeaponInfo weaponInfo)
    {
        // Remove the current weapon info
        Destroy(this.weaponInfo);
        this.weaponInfo = null;

        // Add a new weapon info
        this.weaponInfo = (CharacterWeaponInfo)gameObject.AddComponent(weaponInfo.GetType());
        this.weaponInfo.weapon = this;
        
        // Copy the information from the new weapon info to the weapon info
        weaponInfo.CopyInformationTo(this.weaponInfo);
    }

    private void ResetCanShoot()
    {
        _canShoot = true;
    }

    #endregion Methods
}