using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public int money = 100;

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

    private void Start()
    {
        UIPlayer.instance.UpdateMoneyTexto(money.ToString());
    }

    public void TakeMoney(int amount)
    {
        money -= amount;
        if (money <= 0)
        {
            money = 0;
        }
        UIPlayer.instance.UpdateMoneyTexto(money.ToString());
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UIPlayer.instance.UpdateMoneyTexto(money.ToString());
    }
}
