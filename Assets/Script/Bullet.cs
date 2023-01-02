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
    private Collider rangeDamageCollider;

    public void SpawnerInfoReciver(UnitProfile spawnerProfile)
    {
        localProfile = spawnerProfile;
        damage = spawnerProfile.atk;
        this.tag = spawnerProfile.gameObject.tag;
        layer = this.gameObject.layer;
    }

    private void EnemyInfoSet()
    {
        if (this.tag == "Player")
        enemyTag = "Enemy";

        if(this.tag == "Enemy")
        enemyTag = "Player";
    }

    void OnTriggerEnter(Collider other)
    {
        if(IsEnemy(other))
        {

        }
    }

    private bool IsEnemy(Collider other)
    {
        if ((otherProfile = other.GetComponentInParent<UnitProfile>()) != null && other.isTrigger == false)
        return other.tag != this.tag;
        else return false; 
    }

    public void DistoryThis()
    {

    } 

    private void SendDamage()
    {
        //otherProfile.health.DamageDealOrder(,)
    }
}
