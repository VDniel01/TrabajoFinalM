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

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        OnTouch();
                    }
                }
            }
        }
    }

    private void OnTouch()
    {
        if (selectedNode && selectedNode != this)
        {
            selectedNode.OnCloseSelection();
        }

        selectedNode = this;
        isSelected = !isSelected;
        anim.SetBool("isSelected", isSelected);

        if (isSelected)
        {
            if (isOcupado)
            {
                UIpanelManager.instance.OpenPanel(towerOcuped);
            }
            else
            {
                TowerRequestManager.Instance.OnOpenRequestPanel();
            }
        }
        else
        {
            TowerRequestManager.Instance.OnCloseRequestPanel();
            UIpanelManager.instance.ClosedPanel();
        }
    }

    public void OnCloseSelection()
    {
        isSelected = false;
        anim.SetBool("isSelected", isSelected);
    }
}
