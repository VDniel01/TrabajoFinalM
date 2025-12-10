using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIpanelManager : MonoBehaviour
{
    public static UIpanelManager instance;

    [Header("UI References")]
    public GameObject root;
    public TextMeshProUGUI towerNameTxt;
    public TextMeshProUGUI towerDescripcionTxt;
    public TextMeshProUGUI towerRangeTxt;
    public TextMeshProUGUI towerTowerDmgTxt;
    public TextMeshProUGUI towerVelocidadTxt;
    public TextMeshProUGUI towerSellPriceTxt;
    public TextMeshProUGUI towerUpgradePriceTxt;

    public Button buttonUpgrade;
    public Button buttonSell;

    [Header("Visual Range")]
    public GameObject rangeIndicatorPrefab; // --- NUEVO: Arrastra el prefab del círculo aquí ---
    private GameObject currentRangeIndicator;

    private Tower tower;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void OpenPanel(Tower _tower)
    {
        if (_tower == null) return;

        tower = _tower;
        root.SetActive(true);

        // --- LÓGICA DEL RANGO VISUAL ---
        if (currentRangeIndicator != null) Destroy(currentRangeIndicator);

        if (rangeIndicatorPrefab != null && tower.CurrendData != null)
        {
            Vector3 pos = tower.transform.position;
            pos.y += 0.1f; // Un poquito elevado del suelo
            currentRangeIndicator = Instantiate(rangeIndicatorPrefab, pos, Quaternion.Euler(90, 0, 0));

            // Escalamos el círculo: Rango * 2 (porque rango es radio y scale es diámetro)
            // Ajusta este factor 2.0f si tu imagen base es muy grande o pequeña
            float size = tower.CurrendData.range * 2.0f;
            currentRangeIndicator.transform.localScale = new Vector3(size, size, 1);
        }
        // -------------------------------

        UpdateButtons();
        SetValue();
    }

    private void UpdateButtons()
    {
        buttonUpgrade.onClick.RemoveAllListeners();
        buttonSell.onClick.RemoveAllListeners();

        buttonSell.onClick.AddListener(SellTower);

        if (tower.currentIndexUpgrade < tower.towerUpgradeData.Count)
        {
            buttonUpgrade.gameObject.SetActive(true);
            buttonUpgrade.onClick.AddListener(UpdateTower);

            // Desactiva el botón si no tienes dinero
            int cost = tower.towerUpgradeData[tower.currentIndexUpgrade].upgradePrice;
            buttonUpgrade.interactable = (PlayerData.instance.money >= cost);
        }
        else
        {
            buttonUpgrade.gameObject.SetActive(false);
        }
    }

    public void UpdateTower()
    {
        if (tower == null) return;

        TowerData nextUpgrade = tower.towerUpgradeData[tower.currentIndexUpgrade];

        if (PlayerData.instance.money >= nextUpgrade.upgradePrice)
        {
            PlayerData.instance.TakeMoney(nextUpgrade.upgradePrice);

            tower.CurrendData = nextUpgrade;
            tower.currentIndexUpgrade++;

            OpenPanel(tower); // Refresca panel y rango nuevo
        }
    }

    public void SellTower()
    {
        if (tower != null)
        {
            PlayerData.instance.AddMoney(tower.CurrendData.sellPrice);
            Destroy(tower.gameObject);
            ClosedPanel();

            if (Node.selectedNode != null)
            {
                Node.selectedNode.isOcupado = false;
                Node.selectedNode.towerOcuped = null;
                Node.selectedNode.OnCloseSelection();
                Node.selectedNode = null;
            }
        }
    }

    private void SetValue()
    {
        if (tower == null || tower.CurrendData == null) return;

        towerNameTxt.text = tower.towerName;
        // towerDescripcionTxt.text = tower.towerDescription;

        towerRangeTxt.text = $"Rango: {tower.CurrendData.range}";
        towerTowerDmgTxt.text = $"Daño: {tower.CurrendData.dmg}";
        towerVelocidadTxt.text = $"Velocidad: {tower.CurrendData.timeToShoot}";
        towerSellPriceTxt.text = $"$ {tower.CurrendData.sellPrice}";

        if (tower.currentIndexUpgrade < tower.towerUpgradeData.Count)
        {
            towerUpgradePriceTxt.text = $"Mejorar: $ {tower.towerUpgradeData[tower.currentIndexUpgrade].upgradePrice}";
        }
        else
        {
            towerUpgradePriceTxt.text = "MAX";
        }
    }

    public void ClosedPanel()
    {
        root.SetActive(false);
        // Borramos el círculo al cerrar el panel
        if (currentRangeIndicator != null)
        {
            Destroy(currentRangeIndicator);
        }
    }
}