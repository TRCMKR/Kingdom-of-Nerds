using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    abstract int Damage { get; set; }

    public void Use(string name = "");
}
