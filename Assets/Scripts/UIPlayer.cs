using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayer : MonoBehaviour
{
    public TextMeshProUGUI MoneyTxt;
    public static UIPlayer instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateMoneyTexto(string value)
    {
        MoneyTxt.text = "$ " + value;
    }
}
