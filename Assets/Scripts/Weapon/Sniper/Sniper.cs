using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    int selected;
    public Animator anim;
    public GameObject scope_sniper;
    public GameObject sniper_gun;
    public GameObject kuch_bhi;
   public bool isaiming;
   
    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

   
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            anim.SetBool("isScoping", true);
            Movements.instance.cam.fieldOfView = 15;
            isaiming = true;
          
            StartCoroutine(onScoped());
            sniper_gun.gameObject.SetActive(false);
        }
        else
        {
           
            anim.SetBool("isScoping", false);
            Movements.instance.cam.fieldOfView = 36;
          
               scope_sniper.SetActive(false);
                sniper_gun.gameObject.SetActive(true);
        }
        if(isaiming == true)
        {
          
        }
        else
        {
          
        }
    }

    IEnumerator onScoped()
    {
        yield return new WaitForSeconds(.08f);
        
        scope_sniper.SetActive(true);
      
            yield return new WaitForSeconds(1.0f);
            scope_sniper.SetActive(false);
         
          
       
    
    }

    
    public void value (int val)
    {
         selected = val;
        
    }
   /* public void firing(bool firing)
    {    
        
        if(firing == true && isaiming==true)
        {
            scope_sniper.SetActive(false);
            Debug.Log("isworking");
        }
    }*/
}
