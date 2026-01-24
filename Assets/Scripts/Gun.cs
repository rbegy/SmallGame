using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public enum GunType
    {
        Pistol,
        Rifle,
        Shotgun
    }

    [Header("Gun Info")]
    public GunType gunType;
    public int damage = 10;
    [SerializeField] private int range = 50;
    public float reloadTime = 3f;
    public float fireRate = 0.5f;


    [Header("Ammo")]
    public int ammo = 30;
    public int maxAmmo = 30;


    [Header("Misc")]
    public bool reloading = false;
    private bool canShoot = true;
    public TextMeshProUGUI ammoText;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        ammoText.text = "";
    }

    private void OnDisable()
    {
        ammoText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (reloading)
        {
            ammoText.text = "... / " + maxAmmo.ToString();
        }
        else
        {
            ammoText.text = ammo.ToString() + " / " + maxAmmo.ToString();
        }

        if(GunType.Shotgun == gunType || GunType.Pistol == gunType)
        {
            if (Input.GetMouseButtonDown(0) && canShoot)
            {
                StartCoroutine(Shoot());
            }
        }
        else if(GunType.Rifle == gunType)
        {
            if (Input.GetMouseButton(0) && canShoot)
            {
                StartCoroutine(Shoot());
            }
        }


        if (Input.GetKeyDown(KeyCode.R) || ammo == 0)
        {             
            StartCoroutine(Reload());
        }
    }

    public IEnumerator Shoot()
    {
        if(reloading)
        {
            yield break;
        }
        canShoot = false;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("Hit " + hit.collider.name);
            Target target = hit.collider.GetComponent<Target>();
            if (target != null)
            {
                target.health -= damage;
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * 50f);
            }

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);
        }
        Debug.Log("Boom Bang Shadabing");
        ammo--;
        yield return new WaitForSeconds(fireRate);

        canShoot = true;
    }

    public IEnumerator Reload()
    {
        canShoot = false;
        reloading = true;
        Debug.Log("Get mag and shit...where is it");
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        Debug.Log("Oh found it");
        reloading = false;
        canShoot = true;
    }
}
