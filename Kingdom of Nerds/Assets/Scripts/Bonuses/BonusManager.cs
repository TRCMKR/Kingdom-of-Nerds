using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BonusManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _bonuses;

    private void OnEnable()
    {
        RaiseDeathEvent.DeathEvent += SpawnBonus;
    }

    private void OnDisable()
    {
        RaiseDeathEvent.DeathEvent -= SpawnBonus;
    }

    private void SpawnBonus(GameObject corpse)
    {
        // Spawn bonus logic
        UnityEngine.Random.InitState(seed: DateTime.UtcNow.GetHashCode());
        GameObject bonus = _bonuses[UnityEngine.Random.Range(0, _bonuses.Count - 1)];  // nice logic bro 😎

        bonus.transform.position = corpse.transform.position;
        Instantiate(bonus);
    }
}
