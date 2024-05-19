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
        GameObject perk;
        int ball = UnityEngine.Random.Range(0, (Perks.Count) * 100 - 95);
        // There are no plans to balance perk drops, so I implemented the logic using magic numbers
        perk = Perks[ball / 100];
        Perks.RemoveAt(ball / 100);

        perk.transform.position = gameObject.transform.position;
        Instantiate(perk);
    }
}
