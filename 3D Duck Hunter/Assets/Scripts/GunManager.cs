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
    public GameObject ammoPanel;
    public List<GameObject> ammoImages;
    float lastFired = 0;
    GameObject hitObject;

    private void Start()
    {
        equipedGun.ammoCount = equipedGun.ammoSize;
        ammoPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(25*equipedGun.ammoSize,65);

        ammoImages = new List<GameObject>();
        for (int i = 0; i < equipedGun.ammoCount; i++)
        {
            ammoImages.Add(Instantiate(equipedGun.ammoSprite, ammoPanel.transform));
        }
    }
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

            ammoImages[equipedGun.ammoSize-equipedGun.ammoCount].SetActive(false);

            lastFired = Time.time;
            animator.SetTrigger("Shoot_" + equipedGun.name);
            FindObjectOfType<AudioManager>().Play(equipedGun.shootSound,0f);
            StartCoroutine(PumpTime());
            equipedGun.ammoCount -= 1;
            RaycastHit[] hit = Physics.SphereCastAll(cam.transform.position, equipedGun.bulletSpread, cam.transform.forward, 300);
            if (hit.Length > 0)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    hitObject = hit[i].collider.gameObject;
                    //Debug.Log(hitObject.name);
                    if (hitObject.CompareTag("Shootable"))
                    {
                        if(hitObject.GetComponentInParent<duck>())
                        {
                            hitObject.GetComponentInParent<duck>().Shot();
                        }
                        else if(hitObject.GetComponentInParent<Melon>())
                        {
                            hitObject.GetComponent<Melon>().HitMelon();
                        }
                    }
                }
            }
        }
        else if (equipedGun.ammoCount == 0) { FindObjectOfType<AudioManager>().Play(equipedGun.emptySound,.1f); }
     }
    bool isPumping;
    IEnumerator PumpTime()
    {
        isPumping = true;
        yield return new WaitForSeconds(1.2f);
        FindObjectOfType<AudioManager>().Play(equipedGun.pumpSound,0f);
        yield return new WaitForSeconds(1);
        isPumping = false;
    }
    void Reload()
    {
        if (equipedGun.ammoCount < equipedGun.ammoSize && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !isPumping)
        {
            
            animator.SetFloat("ReloadNum_"+equipedGun.name, equipedGun.ammoSize - equipedGun.ammoCount);
            animator.SetTrigger("Reload_" + equipedGun.name);
            StartCoroutine(LoadSounds(equipedGun.ammoSize-equipedGun.ammoCount));
            equipedGun.ammoCount = equipedGun.ammoSize;
        }
    }
    IEnumerator LoadSounds(int reloadTimes)
    {
        yield return new WaitForSeconds(0.55f);
        for (int i = 0; i < reloadTimes; i++)
        {
            ammoImages[i].SetActive(true);
            FindObjectOfType<AudioManager>().Play(equipedGun.reloadSound,0);
            yield return new WaitForSeconds(.45f);
        }
    }
}
