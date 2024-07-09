using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIpanelManager : MonoBehaviour
{
    private Tower tower;
    public TextMeshProUGUI towerNameTxt;
    public TextMeshProUGUI towerDescripcionTxt;
    public TextMeshProUGUI towerRangeTxt;
    public TextMeshProUGUI towerTowerDmgTxt;
    public TextMeshProUGUI towerVelocidadTxt;
    public TextMeshProUGUI towerSellPriceTxt;
    public TextMeshProUGUI towerUpgradePriceTxt;
    public GameObject root;
    public static UIpanelManager instance;
    public Button buttonUpgrade;
    public Button buttonSell;

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

    public void OpenPanel(Tower tower)
    {
        if (tower == null)
        {
            Debug.LogWarning("Es necesario pasar un tower");
            return;
        }
        this.tower = tower;

        // Asegúrate de limpiar y agregar los listeners
        buttonUpgrade.onClick.RemoveAllListeners();
        buttonUpgrade.onClick.AddListener(UpdateTower);

        buttonSell.onClick.RemoveAllListeners();
        buttonSell.onClick.AddListener(SellTower);

        if (tower.currentIndexUpgrade >= tower.towerUpgradeData.Count)
        {
            buttonUpgrade.gameObject.SetActive(false);
        }
        else
        {
            buttonUpgrade.gameObject.SetActive(true);
        }

        SetValue();
        root.SetActive(true);
    }

    public void UpdateTower()
    {
        if (tower == null)
        {
            return;
        }
        if (PlayerData.instance.money >= tower.towerUpgradeData[tower.currentIndexUpgrade].upgradePrice)
        {
            tower.CurrendData = tower.towerUpgradeData[tower.currentIndexUpgrade];
            PlayerData.instance.TakeMoney(tower.towerUpgradeData[tower.currentIndexUpgrade].upgradePrice);
            if (tower.currentIndexUpgrade + 1 >= tower.towerUpgradeData.Count)
            {
                buttonUpgrade.gameObject.SetActive(false);
            }
            else
            {
                tower.currentIndexUpgrade++;
            }
            OpenPanel(tower);
        }
        else
        {
            Debug.Log("plata... ... ... no tenemos");
        }
    }

    public void SellTower()
    {
        Debug.Log("Attempting to sell tower..."); // Mensaje de depuración
        if (Node.selectedNode != null)
        {
            Debug.Log("Selected node found: " + Node.selectedNode.name); // Mensaje de depuración
            Node.selectedNode.SellTower();
            ClosedPanel();
        }
        else
        {
            Debug.LogWarning("No selected node to sell the tower.");
        }
    }

    private void SetValue()
    {
        towerNameTxt.text = tower.towerName;
        towerDescripcionTxt.text = tower.towerDescription;
        towerRangeTxt.text = "Rango : " + tower.CurrendData.range.ToString();
        towerTowerDmgTxt.text = "Daño : " + tower.CurrendData.dmg.ToString();
        towerVelocidadTxt.text = "Velocidad : " + tower.CurrendData.timeToShoot.ToString();
        towerSellPriceTxt.text = "$ : " + tower.CurrendData.sellPrice.ToString();
        towerUpgradePriceTxt.text = "Mejorar : " + tower.CurrendData.upgradePrice.ToString();
    }

    public void ClosedPanel()
    {
        root.SetActive(false);
        tower = null;
        if (Node.selectedNode != null)
        {
            Node.selectedNode.OnCloseSelection();
            Node.selectedNode = null;
        }
    }
}
