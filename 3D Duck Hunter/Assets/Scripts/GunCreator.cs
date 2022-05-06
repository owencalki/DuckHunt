using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Gun",menuName ="Gun")]
public class GunCreator : ScriptableObject
{
    public new string name;

    public bool isAutomatic;
    public int ammoSize;
    public int ammoCount;
    public float bulletSpread;
    public float firingCoolDown;
    public Sprite crosshair;
    public string shootSound;
    public string pumpSound;
    public string reloadSound;

}
