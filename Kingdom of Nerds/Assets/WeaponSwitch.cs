using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{

    public int weaponSwitch = 0;

    void Start()
    {
        SelectWeapon();
    }

   
    void Update()
    {
        int currentWeapon = weaponSwitch;


        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (weaponSwitch >= transform.childCount - 1)
            {
                weaponSwitch = 0;
            }
            else
            {
                weaponSwitch++;
            }

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (weaponSwitch <= 0)
            {
                weaponSwitch = transform.childCount -1;
            }
            else
            {
                weaponSwitch--;
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponSwitch = 0;

        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            weaponSwitch = 1;

        }

        if (currentWeapon != weaponSwitch)
        {
            SelectWeapon();
        }

    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if(i == weaponSwitch)
                weapon.gameObject.SetActive(true);
            else 
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
