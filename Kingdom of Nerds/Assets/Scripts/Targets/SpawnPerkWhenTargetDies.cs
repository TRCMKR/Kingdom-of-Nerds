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
        int ball = UnityEngine.Random.Range(1, 101);
        // There are no plans to balance perk drops, so I implemented the logic using magic numbers
        if (ball <= 32)
            perk = Perks[1];
        else if (ball <= 64)
            perk = Perks[2];
        else if (ball <= 96)
            perk = Perks[3];

        perk.transform.position = gameObject.transform.position;
        Instantiate(perk);
    }
}
