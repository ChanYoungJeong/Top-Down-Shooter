using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//mutiweapon things
public enum WeaponType
{
    Pistol,
    Rifle,
    Shotgun
}

public class Shooting : MonoBehaviour
{

    public GameObject hud;

    public WeaponType weapontype;

    //object
    public Transform firePoint;
    //object normal bullet and rifle bullet
    public GameObject bulletPerfab;
    public GameObject Rifle_bulletPerfab;
    public GameObject Shotgun_bulletPerfab;
    GunAudio gunAudio;

    //shooting module
    public float PistolbulletForce = 20f;
    public float RiflebulletForce = 30f;
    public float ShotgunbulletForce = 20f;

    //reload module
    public int clips_per_Pistol = 10;
    public int clips_per_Rifle = 10;
    public int clips_per_Shotgun = 10;

    //How many ammo we have in this two weapon
    public int total_Pistol_Ammo_num = 100;
    public int total_Rifle_Ammo_num = 0;
    public int total_Shotgun_Ammo_num = 0;

    public float reloadTime_Pistol = 1f;
    public float reloadTime_Rifle = 1f;
    public float reloadTime_Shotgun = 1f;

    public int currentAmmo_Pistol = -1;
    public int currentAmmo_Rifle = -1;
    public int currentAmmo_Shotgun = -1;

    private bool isReloading = false;
    public bool reloadFlag = false;

    public bool isRunOutBullet_Rifle = false;
    public bool isRunOutBullet_Shotgun = false;


    public bool shoot = false; //Made by Chan Young
    private Animator anim;

    //holding shooting
    public float PistolDelta = 0.5F;
    public float RifleDelta = 0.2F;
    public float ShotgunDelta = 0.5F;
    private float nextFire = 0.5F;
    private float myTime = 0.0F;

    void Start()
    {
        PlayerBodyAnimation rel = GetComponentInParent<PlayerBodyAnimation>();
        if (rel != null){
            anim = rel.animatorr;
        }
        gunAudio = GetComponentInChildren<GunAudio>();

        if (PersistentManager.isInitialized)
        {
            total_Pistol_Ammo_num = PersistentManager.total_Pistol_Ammo_num;
            total_Rifle_Ammo_num = PersistentManager.total_Rifle_Ammo_num;
            total_Shotgun_Ammo_num = PersistentManager.total_Shotgun_Ammo_num;

            currentAmmo_Pistol = PersistentManager.currentAmmo_Pistol;
            currentAmmo_Rifle = PersistentManager.currentAmmo_Rifle;
            currentAmmo_Shotgun = PersistentManager.currentAmmo_Shotgun;

            weapontype = PersistentManager.weapontype;
        }
        else
        {
            if (currentAmmo_Pistol == -1)
                currentAmmo_Pistol = clips_per_Pistol;

            weapontype = WeaponType.Pistol;
        }

        UpdateHUD();

        changeWeaponAnimation(weapontype);
    }

    void changeWeaponAnimation(WeaponType wep){
        if(wep == WeaponType.Pistol){
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
        }
        else if(wep == WeaponType.Rifle){
            anim.SetLayerWeight(1, 2);
            anim.SetLayerWeight(2, 0);
        }
        else if(wep == WeaponType.Shotgun){
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f && isReloading != true)
        {
            if (weapontype >= WeaponType.Shotgun)
                weapontype = WeaponType.Pistol;
            else
                weapontype++;
            changeWeaponAnimation(weapontype);
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0f && isReloading != true)
        {
            if (weapontype <= WeaponType.Pistol)
                weapontype = WeaponType.Shotgun;
            else
                weapontype--;
            changeWeaponAnimation(weapontype);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapontype = WeaponType.Pistol;
            changeWeaponAnimation(weapontype);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapontype = WeaponType.Rifle;
            changeWeaponAnimation(weapontype);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weapontype = WeaponType.Shotgun;
            changeWeaponAnimation(weapontype);
        }

        if (isReloading)
            return;
         switch (weapontype)
            { 
                case WeaponType.Pistol:
                if(currentAmmo_Pistol<=0 && total_Pistol_Ammo_num>0)
                {
                    StartCoroutine(Reload_Pistol());
                    return;
                }
                break;

                case WeaponType.Rifle:
                if (currentAmmo_Rifle <= 0 && isRunOutBullet_Rifle == false && total_Rifle_Ammo_num>0 )
                {
                    StartCoroutine(Reload_Rifle());
                    return;
                }
                break;

                case WeaponType.Shotgun:
                if (currentAmmo_Shotgun <= 0 && isRunOutBullet_Shotgun == false && total_Shotgun_Ammo_num>0) 
                {
                    StartCoroutine(Reload_Shotgun());
                    return;
                }
                break;
            default:break;
            }

        myTime = myTime + Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                switch (weapontype)
                {
                    case WeaponType.Pistol:
                        nextFire = myTime + PistolDelta;
                        Shoot_Pistol();
                        break;
                    case WeaponType.Rifle:
                        nextFire = myTime + RifleDelta;
                        Shoot_Rifle();
                        break;
                    case WeaponType.Shotgun:
                        nextFire = myTime + ShotgunDelta;
                        Shoot_Shotgun();
                        break;
                    default: break;
                }

                nextFire = nextFire - myTime;
                myTime = 0.0F;
            }
        }
        return;
    }


    IEnumerator Reload_Pistol()
    {
        anim.Play("Player_Reload");
        gunAudio.reloadAudio();
        isReloading = true;
        Debug.Log("Reloading Pistol");

        yield return new WaitForSeconds(reloadTime_Pistol);

        if(total_Pistol_Ammo_num > clips_per_Pistol)
        {
            currentAmmo_Pistol = clips_per_Pistol;
            total_Pistol_Ammo_num -= clips_per_Pistol;
        } 
        else
        {
            currentAmmo_Pistol += total_Pistol_Ammo_num;
            total_Pistol_Ammo_num += 100;
        }

        isReloading = false;
        shoot = false;

        UpdateHUD();
    }

    IEnumerator Reload_Rifle()
    {
        anim.Play("Player_Reload");
        gunAudio.reloadAudio();
        isReloading = true;
        Debug.Log("Reloading Rifle");

        yield return new WaitForSeconds(reloadTime_Rifle);

        if(total_Rifle_Ammo_num > clips_per_Rifle)
        {
            currentAmmo_Rifle = clips_per_Rifle;
            total_Rifle_Ammo_num -= clips_per_Rifle;
        } 
        else
        {
            currentAmmo_Rifle += total_Rifle_Ammo_num;
            total_Rifle_Ammo_num = 0;
            isRunOutBullet_Rifle = true;
        }
        
        isReloading = false;
        shoot = false;
        UpdateHUD();
    }

    IEnumerator Reload_Shotgun()
    {
        anim.Play("Player_Reload");
        gunAudio.reloadAudio();
        isReloading = true;
        Debug.Log("Reloading Shotgun");

        yield return new WaitForSeconds(reloadTime_Shotgun);

        if(total_Shotgun_Ammo_num > clips_per_Shotgun)
        {
            currentAmmo_Shotgun = clips_per_Shotgun;
            total_Shotgun_Ammo_num -= clips_per_Shotgun;
        } 
        else
        {
            currentAmmo_Shotgun += total_Shotgun_Ammo_num;
            total_Shotgun_Ammo_num = 0;
            isRunOutBullet_Shotgun = true;
        }

        isReloading = false;
        shoot = false;
        UpdateHUD();
    }

    void Shoot_Pistol()
    {
        if(currentAmmo_Pistol > 0)
        {
            anim.Play("Player_Shoot");
            gunAudio.shootAudio();
            //shooting
            GameObject bullet = Instantiate(bulletPerfab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * PistolbulletForce, ForceMode2D.Impulse);

            //decrease one ammo after shoot
            currentAmmo_Pistol--;
            shoot = true;
        }

        UpdateHUD();
    }

    void Shoot_Rifle()
    {
        if(currentAmmo_Rifle > 0)
        {
            anim.Play("Player_Shoot");
            gunAudio.shootAudio();
            //shooting
            GameObject bullet = Instantiate(Rifle_bulletPerfab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * RiflebulletForce, ForceMode2D.Impulse);

            //decrease one ammo after shoot
            currentAmmo_Rifle--;
            shoot = true;
        }

        UpdateHUD();
    }

    void Shoot_Shotgun()
    {
        if(currentAmmo_Shotgun > 0)
        {
            anim.Play("Player_Shoot");
            gunAudio.shootAudio();
            //shooting
            for(int i = 0; i <= 4; i++)
            {
                GameObject bullet = Instantiate(Shotgun_bulletPerfab, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                switch (i)
                {
                    case 0:
                        rb.AddForce(firePoint.up * ShotgunbulletForce + new Vector3(0f,-5f, 0f), ForceMode2D.Impulse);
                        break;
                    case 1:
                        rb.AddForce(firePoint.up * ShotgunbulletForce + new Vector3(0f, -2.5f, 0f),ForceMode2D.Impulse);
                        break;
                    case 2:
                        rb.AddForce(firePoint.up * ShotgunbulletForce + new Vector3(0f, 0f, 0f),ForceMode2D.Impulse);
                        break;
                    case 3:
                        rb.AddForce(firePoint.up * ShotgunbulletForce + new Vector3(0f, 2.5f, 0f),ForceMode2D.Impulse);
                        break;
                    case 4:
                        rb.AddForce(firePoint.up * ShotgunbulletForce + new Vector3(0f, 5f, 0f),ForceMode2D.Impulse);
                        break;

                }
            }
            //decrease one ammo after shoot
            currentAmmo_Shotgun--;
            shoot = true;
        }

        UpdateHUD();
    }

    private void UpdateHUD()
    {
        if(hud)
        {
            int ammoValue = -1;

            switch (weapontype)
            {
                case WeaponType.Pistol:
                    ammoValue = currentAmmo_Pistol;
                    break;
                case WeaponType.Rifle:
                    ammoValue = currentAmmo_Rifle;
                    break;
                case WeaponType.Shotgun:
                    ammoValue = currentAmmo_Shotgun;
                    break;
                default:
                    break;
            }

            var uiComp = hud.GetComponent<UI_HUD>();
            uiComp.UpdateAmmoAmount(ammoValue);
        }
        else
        {
            Debug.LogWarning("HUD object not set in Shooting script!");
        }
    }

    public bool AddRifleAmmo(int addRifleAmmo)
    {

        // Apply damage
        total_Rifle_Ammo_num += addRifleAmmo;
        Debug.Log($"Ammo {this.gameObject.name} add {addRifleAmmo}, current ammo = {total_Rifle_Ammo_num} + {clips_per_Rifle}");
        return true;
    }

        public bool AddShotgunAmmo(int addShotgunAmmo)
    {

        // Apply damage
        total_Shotgun_Ammo_num += addShotgunAmmo;
        Debug.Log($"Ammo {this.gameObject.name} add {addShotgunAmmo}, current ammo = {total_Shotgun_Ammo_num} + {clips_per_Shotgun}");
        return true;
    }
}

