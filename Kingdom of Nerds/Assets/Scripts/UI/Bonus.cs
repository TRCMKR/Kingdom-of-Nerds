using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    public TextMeshProUGUI bonusNameText;
    public TextMeshProUGUI bonusCostText;
    public Image bonusImageSpace;

    public Sprite bonusImage;
    public string bonusName;
    public int bonusCost;

    void Start()
    {
        bonusNameText.text = bonusName;
        bonusCostText.text = bonusCost.ToString();
        bonusImageSpace.sprite = bonusImage; 
    }

    public void BuyBonus()
    {
        StoreManager.BuyBonus(this);
    }
}
