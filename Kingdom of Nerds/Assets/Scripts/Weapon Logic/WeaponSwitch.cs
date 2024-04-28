using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{

    public int weaponSwitch = 0;

    [SerializeField] private List<GameObject> _weapons;
    [SerializeField] public GameObject _currentWeapon;
    public Animator animator;

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
            weaponSwitch = 1;

        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            weaponSwitch = 0;

        }

        if (currentWeapon != weaponSwitch)
        {
            SelectWeapon();
        }
        
        if (Input.GetMouseButton(0))
        {
            _currentWeapon.GetComponent<IWeapon>().Use(_currentWeapon.name);
        }

    }

    void SelectWeapon()
    {
        // int i = 0;
        // foreach (Transform weapon in transform)
        // {
        //     if (i == weaponSwitch)
        //         weapon.gameObject.SetActive(true);
        //     else 
        //         weapon.gameObject.SetActive(false);
        //     i++;
        // }
        _currentWeapon = _weapons[weaponSwitch];
        if (_currentWeapon.name.Contains("Bat")) animator.SetBool("Bat", true);
        else animator.SetBool("Bat", false);
    }
}
