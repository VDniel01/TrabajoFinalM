using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TowerRequestManager : MonoBehaviour
{
    public List<Tower> towers = new List<Tower>();
    private Animator anim;
    public static TowerRequestManager Instance;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
            Destroy(Instance);
        
        anim = GetComponent<Animator>();

    }

    public void OnOpenRequestPanel()
    {
        anim.SetBool("IsOpen", true);
    }
    public void OnCloseRequestPanel()
    {
        anim.SetBool("IsOpen", false);
    }

    public void RequestTowerBuy(string towerName)
    {
        var tower = towers.Find(x => x.towerName == towerName);
        var towerGo = Instantiate(tower, Node.selectedNode.transform.position, tower.transform.rotation);
        OnCloseRequestPanel();
        Node.selectedNode.OnCloseSelection();
        Node.selectedNode = null;
    }
}