using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Node : MonoBehaviour
{
    public static Node selectedNode;
    private Animator anim;
    private bool isSelected = false;
    public bool isOcupado;
    public Tower towerOcuped;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        if (isOcupado)
        {
            UIpanelManager.instance.OpenPanel(towerOcuped);
            return;
        }

        if (selectedNode && selectedNode != this)
        {
            selectedNode.OnCloseSelection();
        }

        selectedNode = this;
        Debug.Log("Node selected: " + selectedNode.name); // Mensaje de depuración

        isSelected = !isSelected;
        if (isSelected)
        {
            TowerRequestManager.Instance.OnOpenRequestPanel();
        }
        else
        {
            TowerRequestManager.Instance.OnCloseRequestPanel();
        }
        anim.SetBool("isSelected", isSelected);
    }

    public void OnCloseSelection()
    {
        isSelected = false;
        anim.SetBool("isSelected", isSelected);
    }

    public void SellTower()
    {
        if (towerOcuped != null)
        {
            Debug.Log("Selling tower: " + towerOcuped.name); // Mensaje de depuración
            PlayerData.instance.AddMoney(towerOcuped.CurrendData.sellPrice);
            Destroy(towerOcuped.gameObject);
            towerOcuped = null;
            isOcupado = false;
            Debug.Log("Tower sold successfully.");
        }
        else
        {
            Debug.LogWarning("No tower to sell on this node.");
        }
    }
}
