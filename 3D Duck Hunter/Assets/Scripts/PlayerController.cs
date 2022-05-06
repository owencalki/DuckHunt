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
    float xRot = 0;
    float yRot = 0f;
    public bool isReloading = false;
    public int ammoCount = 8;


    GunManager gunManager;


    void Start()
    {
        gunManager = FindObjectOfType<GunManager>();
        character = gameObject;
        animator = character.GetComponent<Animator>();
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
        if (Input.GetMouseButton(0)) { gunManager.UseGun("shoot"); }

        if (Input.GetKeyDown(KeyCode.R)) { gunManager.UseGun("reload"); }

    }
}
