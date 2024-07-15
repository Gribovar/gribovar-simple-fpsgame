using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/NewGun")]
public class GunInfo : ItemInfo
{
    public string type;
    public float damage;
    public float RoF;
    public int magSize;
    public int maxAmmo;
    public float reloadTime;
    public float switchTime;
    public int maxDistance;
}
