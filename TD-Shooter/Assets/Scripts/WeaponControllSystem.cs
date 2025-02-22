using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControllSystem : Singleton<WeaponControllSystem>
{
    public List<Weapon> weaponList = new List<Weapon>();
    private Weapon currentWeapon;


    private void Start()
    {
        currentWeapon = weaponList[0];
        UIManager.Instance.UpdateWeaponName(currentWeapon.name);
    }

    public void SelectWeapon(int weaponIndex)
    {
        if (weaponIndex < weaponList.Count)
        {
            currentWeapon = weaponList[weaponIndex];

            // chache selected weapon index
            PlayerInfoKeeper.selectedWeaponIndex = weaponIndex;
            //chache selected weapon name
            PlayerInfoKeeper.selectedWeaponName = weaponList[weaponIndex].name;
            //update UI
            UIManager.Instance.UpdateWeaponName(currentWeapon.name);
        }
    }

    private void Update()
    {
        // Weapon shooting
        if (Input.GetMouseButton(0))
        {
            WeaponFire();
        }
    }

    public void WeaponFire()
    {
        currentWeapon.Fire();
    }
}
