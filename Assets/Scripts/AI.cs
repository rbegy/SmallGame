using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using static UnityEngine.GraphicsBuffer;

public class AI : MonoBehaviour
{

    public List<GameObject> waypoints = new List<GameObject>();
    [Header("AI Info")]
    public int distance = 30;
    public int currentPosition = 0;
    public float rotateSpeed = 2f;
    public int attackDistance = 5;
    public float speed = 5f;
    public float projectileSpeed = 20f;

    [Header("States")]
    public bool spotted = false;
    public bool idle = true;
    public bool reload = false;
    public bool canShoot = true;

    public GameObject player;
    public GameObject firePoint;
    public GameObject Projectile;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Idle());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * distance, Color.cyan);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                spotted = true;
                idle = false;
            }
        }

        if (spotted)
        {
            Move();
        }
        if(!spotted && !idle)
        {
            idle = true;
            StartCoroutine(Idle());
        }

    }

    public void Move()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < attackDistance)
        {
            Attack();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.LookAt(player.transform);
        }
    }


    public void Attack()
    {
        if (!reload && canShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        transform.LookAt(player.transform);
        GameObject projectile = Instantiate(Projectile, firePoint.transform.position, firePoint.transform.rotation);
        projectile.GetComponent<Rigidbody>().velocity = firePoint.transform.forward * projectileSpeed;
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        reload = true;
        canShoot = false;
        yield return new WaitForSeconds(3);
        reload = false;
        canShoot = true;
    }

    public IEnumerator Idle()
    {
        if (waypoints.Count == 0)
        {
            yield break;
        }

        while(idle)
        {
            transform.LookAt(waypoints[currentPosition].transform);
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentPosition].transform.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, waypoints[currentPosition].transform.position) < 1f)
            {
                currentPosition++;
                yield return new WaitForSeconds(2);
                if (currentPosition >= waypoints.Count)
                {
                    currentPosition = 0;
                }
            }
            yield return null;
        }
    }
}
