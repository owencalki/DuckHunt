using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Animator animator;
    GameObject character;
    public GameObject player; 
    public Camera cam;
    public float sensitivity;
    public float range;
    float xRot = 0;
    float yRot = 0f;
    public bool isReloading = false;
    public int ammoCount = 8;

    public float firingCoolDown;
    float lastFire = 0f;
    public float bulletSpread;

    public ParticleSystem Shells;
    public ParticleSystem Smoke;
    public ParticleSystem Blast;

    Ray ray;



    void Start()
    {
        character = gameObject;
        animator = character.GetComponent<Animator>();
        
        //Updates Crosshair with bullet spread
        RectTransform crosshair = GameObject.Find("Crosshair").GetComponent<RectTransform>();
        //crosshair.sizeDelta = new Vector2(bulletSpread, bulletSpread) * 40;                   Commented Out But a Possiblity for scaling crosshair
        //40 is a multiplier so that the crosshair matches the spread
    }

    void Update()
    {
        //Looking Around ---------------------------------------------------------------------------------------------------------------
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRot += mouseY;
        xRot = Mathf.Clamp(xRot, -30, 60);
        yRot += mouseX;
        yRot = Mathf.Clamp(yRot, -85, 80);

        character.transform.localRotation = Quaternion.Euler(-xRot,180f, 0f);
        player.transform.localRotation = Quaternion.Euler(0f, yRot, 0f);
        //--------------------------------------------------------------------------------------------------------------------------------

        //Shooting------------------------------------------------------------------------------------------------------------------------
        if (Input.GetMouseButtonDown(0) && Time.time - lastFire > firingCoolDown && PauseGame.GameIsPaused == false && isReloading == false && ammoCount>0)
        { 
            Shoot(); 
            lastFire = Time.time;

            //sphere casting to see what colliders were hit. Using sphere for bullet spread
            RaycastHit[] hit = Physics.SphereCastAll(cam.transform.position, bulletSpread, cam.transform.forward, range);
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

        int ammoNeeded = 8-ammoCount;
        float t;

        if (Input.GetKeyDown(KeyCode.R) && ammoCount != 8) 
        {
            isReloading = true;
            animator.SetFloat("ReloadNum", ammoNeeded);
            animator.SetTrigger("Reload");
            ammoCount = 8;
            t = Time.time;
        }
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) { isReloading = true; }
        else { isReloading = false; }


    }
 
    void Shoot()    //Handles sounds and animations for shooting shotgun---------------------------------------------------------------
    {
        ammoCount -= 1;
        Smoke.Play();
        Blast.Play();
        animator.SetTrigger("MouseDown");
        StartCoroutine(ShootingSounds(false));
    }
    IEnumerator ShootingSounds(bool reloading)
    {
        if (!reloading)
        {
            FindObjectOfType<AudioManager>().Play("Shotgun shot");
            yield return new WaitForSeconds(0.9f);
            Shells.Play();
            FindObjectOfType<AudioManager>().Play("Shotgun pump");
        }
        if(reloading)
        {
            //Add dryfire sound
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------
}
