using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerData
{
    [Header("Price")]
    public int upgradePrice;
    public int buyPrice = 10;
    public int sellPrice = 8;

    [Header("TowerSetting")]
    public float range;
    public int dmg;
    public float timeToShoot;
}
