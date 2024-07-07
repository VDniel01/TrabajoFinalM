using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TowerRequestManager : MonoBehaviour
{
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
}
