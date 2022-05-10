using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melon : MonoBehaviour
{
    public float slomo = 0.5f;
    public float slomoTime = 5f;
    public float yForce;
    public float xForce;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-Mathf.Clamp(gameObject.transform.position.x,-1,1)*xForce,yForce,xForce),ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.transform.position = gameObject.transform.position;
        Destroy(gameObject, 10f);
    }


    public void HitMelon()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        StartCoroutine(Slomo());
    }

    IEnumerator Slomo()
    {
        Time.timeScale = slomo;
        AudioSource[] sources = FindObjectOfType<AudioManager>().GetComponents<AudioSource>();
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].pitch = sources[i].pitch*Time.timeScale;
        }

        yield return new WaitForSeconds(slomoTime);

        Time.timeScale = 1;
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].pitch = sources[i].pitch * 1/slomo;
        }
        Destroy(gameObject, 10f);
    }
}
