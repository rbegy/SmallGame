using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int health = 100;
    public TargetType targetType;
    public int time = 1;
    public ParticleSystem destroyEffect;
    public enum TargetType
    {
        Shatter,
        Explode,
        None
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(targetType)
        {
             case TargetType.Shatter:
                if (health <= 0)
                {                        
                    MeshRenderer mesh = GetComponent<MeshRenderer>();
                    mesh.enabled = false;
                    StartCoroutine(WaitAndDestroy());
                }
                break;
            case TargetType.Explode:
                if (health <= 0)
                {
                    MeshRenderer mesh = GetComponent<MeshRenderer>();
                    mesh.enabled = false;
                    StartCoroutine(Explode());
                }
                break;
            case TargetType.None:
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
                break;
        }

    }

    public IEnumerator Explode()
    {
        //add explosion code idiot do your job
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    public IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
