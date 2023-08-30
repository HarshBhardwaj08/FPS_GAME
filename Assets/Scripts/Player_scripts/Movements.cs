using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    public static Movements instance;
    public Transform player_transform;
    Vector2 mouse_point;
    Vector3 move_dir, move_direction;
    [SerializeField] float mousesentivity = 1.0f;
    float Vertical_piviot;
    [SerializeField] float movespeed;
    [SerializeField] float runspeed;
    float actualspeed;
    public Camera cam;
    CharacterController chrac_ctrl;
    [SerializeField] int jump;
    bool isgrounded;
    public bool isFiring = false;
    [SerializeField] LayerMask grnd_layer;
    [SerializeField] Transform ground;
    [SerializeField] Vector3 gravity_mod;
    [SerializeField] GameObject bullet_hits;
    [SerializeField] GameObject crosshair;
    [SerializeField] Sniper snipers;

   // [SerializeField] float Timebetween_shots = 0.1f;
    float shottime;
    public int total_ammo = 30;

    int reload = 30;

     public GUN[] gun;
     int selectedgun;
    float onetap;
    private void Awake()
    {
       
        instance = this;
    }
    void Start()
    {
        cam = Camera.main;
        chrac_ctrl = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        switchgun();
        
    }


    void Update()
    {
       
        mouse_point = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mousesentivity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouse_point.x,
            transform.rotation.eulerAngles.z);
        Vertical_piviot = Vertical_piviot + mouse_point.y;

        Vertical_piviot = Mathf.Clamp(Vertical_piviot, -20.0f, 40.0f);
      

        player_transform.rotation = Quaternion.Euler(-Vertical_piviot, player_transform.eulerAngles.y, player_transform.eulerAngles.z);

      
        move_dir = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical")) ;
       

        if (Input.GetKey(KeyCode.LeftShift))
        {
            actualspeed = runspeed;
        }else
        {
            actualspeed = movespeed;
        }

        isgrounded = Physics.Raycast(ground.position, Vector3.down, 0.25f, grnd_layer);

        float hold_y = move_direction.y;
        move_direction = (transform.forward * move_dir.z + transform.right*move_dir.x).normalized;
        move_direction.y = hold_y;
        if (chrac_ctrl.isGrounded)
        {
            hold_y = 0;
        }
        if (Input.GetButtonDown("Jump") && isgrounded == true)
        {
            move_direction.y = jump;
        }
       
        move_direction.y += Physics.gravity.y *Time.deltaTime;
       chrac_ctrl.Move(move_direction * actualspeed * Time.deltaTime*gravity_mod.y);
        //Switch Weapon 
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            selectedgun++;
            if (selectedgun >= gun.Length)
            {
                selectedgun = 0;
            }
            switchgun();
            sniper();
            snipers.value(selectedgun);

        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            selectedgun--;
            if (selectedgun < 0)
            {
                selectedgun = gun.Length - 1;
            }
            switchgun();
            sniper();
            snipers.value(selectedgun);

        }
        onetap += Time.deltaTime;
        Debug.Log(isFiring);

        //Shoot
        if (total_ammo >= 0 && (onetap >= gun[selectedgun].fire_rate))
        {
            if (Input.GetMouseButtonDown(0))
            {
               

                UI_MANAGER.instance.DecreaseAmmo(total_ammo);
                    shoot();
             
               
              
                    onetap = 0;

                if (gun[selectedgun].gameObject.tag == "Sniper")
                {
                   
                    GetComponentInChildren<Sniper>().anim.SetBool("IsFire",true);
                    //GetComponentInChildren<Sniper>().firing(isFiring);
                  StartCoroutine(onScoped());
                }


            


                // Debug.Log("Retro" +fireammo());

            }
            if (Input.GetMouseButton(0) && gun[selectedgun].isAutomatic)
            {
                shottime -= Time.deltaTime;
                if (shottime < 0)
                {
                   shoot();
                    Ak_47();
                    UI_MANAGER.instance.DecreaseAmmo(total_ammo);
                    //  Debug.Log("Firing" +fireammo());
                }
            }
        } 
        if (total_ammo <= 0)
        {
            UI_MANAGER.instance.reload.gameObject.SetActive(true);
           
            if (Input.GetKeyDown(KeyCode.R))
            {
                total_ammo = 30;
                UI_MANAGER.instance.reload.gameObject.SetActive(false);
                UI_MANAGER.instance.DecreaseAmmo(total_ammo);
            }
        }else
        {

            

            if (Input.GetKeyDown(KeyCode.R))
            {
                total_ammo = 30;
             
                UI_MANAGER.instance.DecreaseAmmo(total_ammo);
            }
        }
       
    }
    private void LateUpdate()
    {   
        cam.transform.position = player_transform.position;
        cam.transform.rotation = player_transform.rotation;
    }
    public void SetMouseSentivity(float value)
    {
        mousesentivity = value;
    }
    void shoot()
    {
        Ray rays = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        rays.origin = cam.transform.position;
        if(Physics.Raycast(rays,out RaycastHit hit))
        {
          
            GameObject bulletsobjects= Instantiate(bullet_hits, hit.point + hit.normal * 0.002f, Quaternion.LookRotation(hit.normal, Vector3.up));
            Destroy(bulletsobjects , 1.0f);
            Debug.DrawRay(rays.origin, transform.forward , Color.green);
        }
        shottime = gun[selectedgun].fire_rate;
        isFiring = true;
        
        total_ammo--;
    }
     
    void switchgun()
    {
        foreach(GUN gunss in gun)
        {
            gunss.gameObject.SetActive(false);
        }
        gun[selectedgun].gameObject.SetActive(true);
    }
  void sniper()
    {
        if(gun[selectedgun].gameObject.tag == "Sniper")
        {
            
            crosshair.gameObject.SetActive(false);
        }
        else
        {
            crosshair.gameObject.SetActive(true);
        }
    }
    void Ak_47()
    {
        if(gun[selectedgun].gameObject.tag == "AK_47")
        {
            if(isFiring == true)
            {
                GetComponentInChildren<Ak_47>().anim.SetBool("isFire",true);
            }
            else
            {
                GetComponentInChildren<Ak_47>().anim.SetBool("isFire", false);
            }
        }
    }
    IEnumerator onScoped()
    {
        yield return new WaitForSeconds(2f);

        if (gun[selectedgun].gameObject.tag == "Sniper")
        {
            GetComponentInChildren<Sniper>().anim.SetBool("IsFire", false);
            isFiring = false;
        }
    }

}
