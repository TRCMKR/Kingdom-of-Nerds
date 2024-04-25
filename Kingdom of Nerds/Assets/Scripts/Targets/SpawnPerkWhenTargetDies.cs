using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPerkWhenTargetDies : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Perks;

    private void OnEnable()
    {
        RaiseDeathEvent.DeathEvent += SpawnPerk;
    }

    private void OnDisable()
    {
        RaiseDeathEvent.DeathEvent -= SpawnPerk;
    }

    private void SpawnPerk(GameObject corpse)
    {
        GameObject perk = Perks[0];
        if (corpse.tag == "smth")
        {
            perk = Perks[1];
        }

        perk.transform.position = gameObject.transform.position;
        Instantiate(perk);
    }
}
