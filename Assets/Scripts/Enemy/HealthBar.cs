using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar healthbar;
    public Slider sider;
    int health = 100;
    private void Awake()
    {
        healthbar = this;
    }
    void Start()
    {
        sider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void damage(int value)
    {
        sider.value  -= value;
        if(sider.value < 0)
        {
            Destroy(this.gameObject);

        }
    }
}
