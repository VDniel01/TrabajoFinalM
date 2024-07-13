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
            Destroy(this);
        }
    }

    public void OpenPanel(Tower tower)
    {
        if (tower == null)
        {
            Debug.Log("Es necesario pasar un tower");
            return;
        }
        this.tower = tower;

        if (tower.currentIndexUpgrade >= tower.towerUpgradeData.Count)
        {
            buttonUpgrade.gameObject.SetActive(false);
        }
        else
        {
            buttonUpgrade.onClick.AddListener(UpdateTower);
        }

        buttonSell.onClick.RemoveAllListeners();
        buttonSell.onClick.AddListener(SellTower);

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
        if (tower != null)
        {
            PlayerData.instance.AddMoney(tower.CurrendData.sellPrice);
            Destroy(tower.gameObject);
            if (Node.selectedNode != null)
            {
                Node.selectedNode.isOcupado = false;
                Node.selectedNode.towerOcuped = null;
                Node.selectedNode.OnCloseSelection();
                Node.selectedNode = null;
            }
            ClosedPanel();
        }
        else
        {
            Debug.LogError("Tower o Node.selectedNode es nulo. No se puede vender la torre.");
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
    }
}
