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
        
        isSelected = !isSelected;
        if (isSelected)
        {
            TowerRequestManager.Instance.OnOpenRequestPanel();
        }
        else
            TowerRequestManager.Instance.OnCloseRequestPanel();
        anim.SetBool("isSelected", isSelected);
    }
    public void OnCloseSelection()
    {
        isSelected = false;
        anim.SetBool("isSelected", isSelected);
    }
}
