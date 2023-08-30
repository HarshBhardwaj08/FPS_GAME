using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int damage;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(health <=0)
        {
            Destroy(gameObject, 3.0f);
           
        }
    }
    private void OnTriggerEnter(Collider other)
    {   
        if(other.gameObject.tag == "Enemy")
        {
            health -= damage;
            Debug.Log(health);
        }
      
    }
}
