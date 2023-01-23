using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private string enemyTag = null;
    private UnitProfile localProfile;
    private UnitProfile otherProfile;
    private int damage;
    private int layer;
    public float damageRadius = 1.8f;
    private float speed;

    private Collider[] hitColliders;

    public void SpawnerInfoReciver(UnitProfile spawnerProfile)
    {
        localProfile = spawnerProfile;
        damage = spawnerProfile.atk;
        this.tag = spawnerProfile.gameObject.tag;
        layer = this.gameObject.layer;
        speed = spawnerProfile.bulletSpeed * 0.1f;
    }

    private void EnemyInfoSet()
    {
        if (this.tag == "Player")
        enemyTag = "Enemy";

        if(this.tag == "Enemy")
        enemyTag = "Player";
    }


    /*void OnTriggerEnter(Collider other)
    {
       /*hitColliders = Physics.OverlapSphere(this.transform.position,damageRadius,1<<otherProfile.layerNum | 0<< 7,QueryTriggerInteraction.Ignore);

       foreach (var hitCollider in hitColliders)//拆解collider組
        {
            if (IsEnemy(hitCollider))
            SendDamage();
        }

        if (IsEnemy(other))
            SendDamage();
        else if (other.tag == "Ground")
        DistoryThis();
    }*/

    void OnTriggerEnter(Collider other)
    {
       hitColliders = Physics.OverlapSphere(this.transform.position,damageRadius,1<<otherProfile.layerNum | 0<< 7,QueryTriggerInteraction.Ignore);

       foreach (var hitCollider in hitColliders)//拆解collider組
        {
            if (IsEnemy(hitCollider))
            SendDamage();
        }

        if (IsEnemy(other))
            SendDamage();
        else if (other.tag == "Ground")
        DistoryThis();
    }

    private bool IsEnemy(Collider other)
    {
        if ((otherProfile = other.GetComponentInParent<UnitProfile>()) != null && other.isTrigger == false)
        return other.tag != this.tag;
        else return false; 
    }

    private void DistoryThis()
    {
        Destroy(this.gameObject);
    } 

    private void SendDamage()
    {
        otherProfile.health.Damage(damage);
        DistoryThis();
    }

    void FixedUpdate()
    {
        this.transform.Translate(Vector3.forward * speed);
    }
    
}
