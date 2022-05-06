using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunManager : MonoBehaviour
{
    public Camera cam;
    public Animator animator;
    public GameObject player;
    public GunCreator[] guns;
    public GunCreator equipedGun;
    float lastFired = 0;


    public void UseGun(string action)
    {
    //IF CALLED ACTION IS TO SHOOT A GUN
        if (action == "shoot")
        {
            Shoot();

        }
    //==================================================

    //IF CALLED ACTION IS TO RELOAD A GUN RUN SWITCH
    else if(action=="reload")
        {
            Reload();
        }
    //==================================================

        else if(action=="swap")
        {

        }
    
    
    
    
    }

     void Shoot()
     {
        if (equipedGun.ammoCount > 0 && Time.time-lastFired>equipedGun.firingCoolDown && PauseGame.GameIsPaused==false && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            lastFired = Time.time;
            animator.SetTrigger("Shoot_" + equipedGun.name);
            FindObjectOfType<AudioManager>().Play(equipedGun.shootSound,0f);
            FindObjectOfType<AudioManager>().Play(equipedGun.pumpSound, 0.9f);
            equipedGun.ammoCount -= 1;
            RaycastHit[] hit = Physics.SphereCastAll(cam.transform.position, equipedGun.bulletSpread, cam.transform.forward, 300);
            if (hit.Length > 0)
            {
                //Debug.Log(hit[0].collider.name);                                                                           Can be used for DEBUGING aiming
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].collider.gameObject.GetComponentInParent<duck>())
                    {
                        hit[i].collider.gameObject.GetComponentInParent<duck>().Shot();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
     }
    
    void Reload()
    {
        if (equipedGun.ammoCount < equipedGun.ammoSize && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            Debug.Log("RELOADING");
            animator.SetFloat("ReloadNum_"+equipedGun.name, equipedGun.ammoSize - equipedGun.ammoCount);
            animator.SetTrigger("Reload_" + equipedGun.name);
            equipedGun.ammoCount = equipedGun.ammoSize;


        }
    }
}
