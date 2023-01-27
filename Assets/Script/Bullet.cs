using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private UnitProfile localProfile;
    private UnitProfile otherProfile;
    private int damage;
    private int layer;
    public float damageRadius = 1.8f;
    private float speed;
    private int enemyLayerNum;
    private string enemyTag = null;
    private Collider[] hitColliders;
    
    public void SpawnerInfoReciver(UnitProfile spawnerProfile)
    {
        localProfile = spawnerProfile;
        damage = spawnerProfile.atk;
        enemyTag = spawnerProfile.enemyTag;
        enemyLayerNum = spawnerProfile.enemyLayerNum; 
        layer = this.gameObject.layer;
        speed = spawnerProfile.bulletSpeed * 0.1f;
    }
    void OnTriggerEnter(Collider other)
    {
       hitColliders = Physics.OverlapSphere(this.transform.position,damageRadius,1<<enemyLayerNum | 0<< 7,QueryTriggerInteraction.Ignore);

       foreach (var hitCollider in hitColliders)//拆解collider組
        {
            if (IsEnemy(hitCollider))
            SendDamage();
        }
    }

    private bool IsEnemy(Collider other)
    {
        if ((otherProfile = other.GetComponentInParent<UnitProfile>()) != null && other.isTrigger == false)
        return other.tag != this.tag;
        else return false; 
    }
    private void SendDamage()
    {
        otherProfile.health.Damage(damage);
        Destroy(this);
    }

    void FixedUpdate()
    {
        this.transform.Translate(Vector3.forward * speed);
    }
    
}
