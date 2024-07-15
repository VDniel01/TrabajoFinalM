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
        {
            Destroy(Instance);
        }

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
        if (tower.CurrendData.buyPrice <= PlayerData.instance.money)
        {
            PlayerData.instance.TakeMoney(tower.CurrendData.buyPrice);
        }
        else
        {
            Debug.Log("no tenemos nada vieja... tamos pobre" + towerName);
            return;
        }

        var towerGo = Instantiate(tower, Node.selectedNode.transform.position, tower.transform.rotation);
        Node.selectedNode.towerOcuped = towerGo;
        Node.selectedNode.isOcupado = true;
        OnCloseRequestPanel();
        Node.selectedNode.OnCloseSelection();
        Node.selectedNode = null;

        if (!WaveManager.instance.isWaitingForNextWave && !WaveManager.instance.wavesFinish && WaveManager.instance.currentWave == 0)
        {
            WaveManager.instance.StartWaveProcess(); // Iniciar la primera oleada
        }
    }
}
