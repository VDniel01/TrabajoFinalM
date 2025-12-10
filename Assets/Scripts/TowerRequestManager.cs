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
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
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

        // Verificamos si tenemos dinero
        if (tower.CurrendData.buyPrice <= PlayerData.instance.money)
        {
            PlayerData.instance.TakeMoney(tower.CurrendData.buyPrice);
        }
        else
        {
            Debug.Log("No tenemos suficiente dinero para comprar " + towerName);
            return;
        }

        // Instanciamos la torre
        var towerGo = Instantiate(tower, Node.selectedNode.transform.position, tower.transform.rotation);

        // Configuramos el nodo
        Node.selectedNode.towerOcuped = towerGo;
        Node.selectedNode.isOcupado = true;

        // Cerramos paneles y limpiamos selección
        OnCloseRequestPanel();
        Node.selectedNode.OnCloseSelection();
        Node.selectedNode = null;

        // ELIMINADO: Ya no iniciamos la oleada automáticamente aquí.
    }
}