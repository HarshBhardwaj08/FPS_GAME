using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_MANAGER : MonoBehaviour
{
    public static UI_MANAGER instance;
    public TextMeshProUGUI reload;
    public TextMeshProUGUI ammo;
    int bullets;

    private void Awake()
    {
        instance = this;
   
    }
    void Start()
    {
        reload.gameObject.SetActive(false);
        
    }


    public void DecreaseAmmo(int value)
    {
      

        ammo.text = "BROCODE : " + value;

        
    }

   
}
