using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentGen : MonoBehaviour
{
    public List <GameObject> poi;
    public GameObject target;
    public GameObject spawner;
    
    [Range(0,1000)]
    public int poiNum;
    public int targetNum;
    public float bigPoiDistance = 150f;
    public float smallPoiDistance = 25f;
    Ray ray;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Music");
        FindObjectOfType<AudioManager>().Play("Forest Noise");

        //POI Generation--------------------------------------------------------------------------------------------------------------------------------------------------
        for (int i = 0; i < poiNum; i++)
        {
            int xSpot=Random.Range(-600,600);
            int zSpot = Random.Range(-750,300);
            float ySpot = 0f;
            int n = Random.Range(0, poi.Count);

            ray.origin = new Vector3(xSpot, 50, zSpot);
            ray.direction = Vector3.down;
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,100,LayerMask.GetMask("Ground")))
            {
                ySpot = hit.point.y;
            }


            GameObject newPoi=Instantiate(poi[n], new Vector3(xSpot,ySpot,zSpot), Quaternion.Euler(new Vector3(0,Random.Range(0,360),0)));
            float size = Random.Range(1, 1.5f);
            newPoi.transform.localScale = new Vector3(size, Random.Range(1, 3), size);
            if (Vector3.Distance(gameObject.transform.position, newPoi.transform.position) < bigPoiDistance && newPoi.CompareTag("BigPoi")) { Destroy(newPoi); }
            if (Vector3.Distance(gameObject.transform.position, newPoi.transform.position) < smallPoiDistance && newPoi.CompareTag("SmallPoi")) { Destroy(newPoi); }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Target Generation--------------------------------------------------------------------------------------------------------------------------------------------------
        for (int i = 0; i < targetNum; i++)
        {
            int xSpot = Random.Range(-300, 300);
            int zSpot = Random.Range(-50, -150);
            float ySpot = Random.Range(5, 35);
            Instantiate(target, new Vector3(xSpot,ySpot,zSpot),Quaternion.identity);
        }
       //---------------------------------------------------------------------------------------------------------------------------------------------------------------------

    }

    private void Update()
    {
        spawner.transform.position = spawner.transform.position + new Vector3(0,0,3*Mathf.Sin(Time.time));
    }

}
