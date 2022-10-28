using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip _pistolShoot;
    [SerializeField]
    private AudioClip _rifleShoot;
    [SerializeField]
    private AudioClip _shotgunShoot;
    [SerializeField]
    private AudioClip _pistolReload;
    [SerializeField]
    private AudioClip _rifleReload;
    [SerializeField]
    private AudioClip _shotgunReload;
    private AudioSource _audioSource;
    Shooting weaponScript;



    public void shootAudio(){
        if (weaponScript.weapontype == WeaponType.Pistol){
            _audioSource.clip = _pistolShoot;
        }
        else if (weaponScript.weapontype == WeaponType.Rifle){
            _audioSource.clip = _rifleShoot;
        }
        else if (weaponScript.weapontype == WeaponType.Shotgun){
            _audioSource.clip = _shotgunShoot;
        }
        _audioSource.Play();
    }

    public void reloadAudio(){
        if (weaponScript.weapontype == WeaponType.Pistol){
            _audioSource.clip = _pistolReload;
        }
        else if (weaponScript.weapontype == WeaponType.Rifle){
            _audioSource.clip = _rifleReload;
        }
        else if (weaponScript.weapontype == WeaponType.Shotgun){
            _audioSource.clip = _shotgunReload;
        }
        _audioSource.Play();
    }


    // Start is called before the first frame update
    void Start()
    {

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null){
            Debug.LogError("no audiosource");
        }
    }

    // Update is called once per frame
    void Update()
    {
        weaponScript = GetComponentInParent<Shooting>();
        
    }
}
