using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeadsUpDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text currencyCounter;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
    }

    private void UpdateCounterText(int amount)
    {
        currencyCounter.text = amount.ToString("0000");
    }
}
