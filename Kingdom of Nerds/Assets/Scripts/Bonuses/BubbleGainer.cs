using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGainer : MonoBehaviour
{
    void OnDisable()
    {
        GameObject bm = GameObject.FindGameObjectWithTag("BonusManager");
        Transform[] ts = bm.GetComponent<BonusManager>().Player.GetComponentsInChildren<Transform>(true);
        // Debug.Log(ts.Length);
        foreach (Transform t in ts)
        {
            // Debug.Log(t.gameObject.tag);
            if (t.gameObject.tag == "Bubble")
            {
                // Debug.Log(t.gameObject);
                t.gameObject.SetActive(true);
                break;
            }
        }
    }
}
