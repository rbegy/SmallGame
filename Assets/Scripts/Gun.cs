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
    public GunType gunType;
    public int damage = 10;
    [SerializeField] private int range = 50;
    public float reloadTime = 3f;
    public float fireRate = 0.5f;

    public int ammo = 30;
    public int maxAmmo = 30;

    private bool canShoot = true;
    private bool reloading = false;

    public TextMeshProUGUI ammoText;

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

        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }
        if(Input.GetKeyDown(KeyCode.R) || ammo == 0)
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
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("Hit " + hit.collider.name);
            Target target = hit.collider.GetComponent<Target>();
            if (target != null)
            {
                target.health -= damage;
            }

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
