using UnityEngine;

public class SimpleWeapon : CharacterWeapon
{
    protected override void CustomFire()
    {
        Debug.Log("SHOOTING!");
    }
}