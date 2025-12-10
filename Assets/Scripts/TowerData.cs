using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerData", menuName = "Tower Defense/Tower Data")]
public class TowerData : ScriptableObject
{
    [Header("Info")]
    public string dataName = "Nivel 1";

    [Header("Economy")]
    public int buyPrice = 10;
    public int upgradePrice = 50;
    public int sellPrice = 8;

    [Header("Combat Stats")]
    public float range = 10f;
    public float dmg = 20f;
    public float timeToShoot = 1f;

    [Header("Audio")] // --- NUEVO ---
    public AudioClip shootSound; // Arrastra aquí el sonido del disparo
}