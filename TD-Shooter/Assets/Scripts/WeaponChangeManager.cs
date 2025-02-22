using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChangeManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponControllSystem.Instance.SelectWeapon(0);
            Debug.Log("Pistol");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponControllSystem.Instance.SelectWeapon(1);
            Debug.Log("Machine Gun");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            WeaponControllSystem.Instance.SelectWeapon(2);
            Debug.Log("Shotgun");
        }

    }
}
