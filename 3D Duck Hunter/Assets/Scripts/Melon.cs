using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melon : MonoBehaviour
{
    public float slomo = 0.5f;
    public float slomoTime = 5;
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
