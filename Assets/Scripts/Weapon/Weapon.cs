using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] float distance_camera;
    public int damage ;
    int value = 1;
    public ParticleSystem gunfire;
    [SerializeField] GameObject hiteffect;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot();
           
           
        }
         
    }
    void shoot()
    {
        fire_particlesystem();
        Fire();
    }
    void fire_particlesystem()
    {
        gunfire.Play();
    }
    private void Fire()
    {
        RaycastHit ray;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out ray, distance_camera))
            {
            hit_effect(ray);
            }

      if(  Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) , out ray, distance_camera) && (ray.transform.name == "Enemy"))
        {
            HealthBar.healthbar.damage(damage);
            value++;
           
        }
      
        Debug.DrawRay(transform.position, transform.forward * distance_camera, Color.green);
    }

    private void hit_effect(RaycastHit hits)
    {
        GameObject hit = Instantiate(hiteffect, hits.point, Quaternion.LookRotation(-hits.normal));
        Destroy(hit, 2.0f);
    }
}
