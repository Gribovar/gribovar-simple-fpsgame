using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SingleShotGun : Gun
{

    [SerializeField] TMP_Text currentAmmoText;
    [SerializeField] TMP_Text totalAmmoText;

    [SerializeField] Camera cam;

    [SerializeField] AudioSource gunSound;

    [SerializeField] public Animation animationMain;
    [SerializeField] AnimationClip[] animations;
    // 0 - shooting, 1 - reloading, 2 - switching
    

    PhotonView PV;

    private bool isFiring = false;
    private bool isReloading = false;
    private bool isSwitching = false;

    public int mag;
    public int totalAmmo;
    public int currentAmmo;

    int negativeTotalAmmo;

    
    


    void Awake()
    {
        PV = GetComponent<PhotonView>();
         
        mag = ((GunInfo)itemInfo).magSize;
        totalAmmo = ((GunInfo)itemInfo).maxAmmo;

        currentAmmo = mag;

        currentAmmoText.text = currentAmmo.ToString();
        totalAmmoText.text = totalAmmo.ToString();


    }


    public override void UpdateGunState()
    {
        currentAmmoText.text = currentAmmo.ToString();
        totalAmmoText.text = totalAmmo.ToString();

    }


    public override void Reload()
    {

         if (totalAmmo > 0 && currentAmmo != mag)
         {  
            if(totalAmmo-(mag-currentAmmo) < 0)
            {
                negativeTotalAmmo = totalAmmo - (mag - currentAmmo);
            }
            else
            {
                negativeTotalAmmo = 0;
            }


            totalAmmo = totalAmmo - negativeTotalAmmo - (mag - currentAmmo);
            currentAmmo = mag + negativeTotalAmmo;
            //if i will add picking up weapons or smh like this might cause issues due to not being updated?
            currentAmmoText.text = currentAmmo.ToString();
            totalAmmoText.text = totalAmmo.ToString();
            animationMain.Play(animations[1].name);
            StartCoroutine(ReloadCooldown());

         }
 
    }



    public override void Use()
    {
        Shoot();
        Debug.Log(currentAmmo);
    }

    void Shoot()
        {

            if (currentAmmo > 0)
            {
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                ray.origin = cam.transform.position;

                if(isFiring | isReloading | isSwitching)
                    return;
                
                currentAmmo--;
                currentAmmoText.text = currentAmmo.ToString();
                if(PhotonNetwork.IsConnected && PV.IsMine)
                {
                    PV.RPC("RPC_PlayGunSound", RpcTarget.All);
                }
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    
                    hit.collider.gameObject.GetComponent<IDamageble>()?.TakeDamage(((GunInfo)itemInfo).damage); 
                    PV.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);   
                }
                animationMain.Play(animations[0].name);
                StartCoroutine(GunCooldown());
            }
            else
            {
                Debug.Log("No ammo");
            }

        }

        IEnumerator GunCooldown()
        {
            isFiring = true;
            yield return new WaitForSeconds((((GunInfo)itemInfo).RoF));
            isFiring = false;
        }

        IEnumerator ReloadCooldown()
        {
            isReloading = true;
            yield return new WaitForSeconds((((GunInfo)itemInfo).reloadTime));
            isReloading = false;
        }

        
        [PunRPC]
        void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
        {
            Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);

            if(colliders.Length != 0 )
            {
                GameObject BulletImpactObj = Instantiate(bulletImpactPrefab, hitPosition + hitNormal * 0.001f, Quaternion.LookRotation(hitNormal, Vector3.up)*bulletImpactPrefab.transform.rotation);
                Destroy(BulletImpactObj, 5f);
                
                BulletImpactObj.transform.SetParent(colliders[0].transform);
                
            }
        }

        [PunRPC]
        void RPC_PlayGunSound()
            {
                gunSound.Play();
            }


}
