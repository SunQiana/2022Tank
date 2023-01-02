using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 偵測敵人，並確認是否可以攻擊。
/// 1、檢測是否為單位(嘗試抓profile) 2、檢測是否為敵人(比對tag) 3、檢測是否遮擋 4、以collider獲取目標資訊 5、繼續以ray檢測是否遮擋
/// </summary>
public class Detect : MonoBehaviour
{
    //攻擊進行中受到遮擋，退出戰鬥的條件尚未寫
    private UnitProfile localProfile;
    private UnitProfile otherProfile = null;
    private Attack localAttackCom;
    private Collider detectTrigger;
    private string localFactionTag;
    private Vector3 otherPos;
    private Vector3 rayCastStartPoint;
    private RaycastHit hitInfo;
    private bool isEngaging = false; //是否正與上一個敵人交戰
    private string enemyTag;
 
    void Awake()
    {
        localProfile = this.GetComponent<UnitProfile>();
        detectTrigger = localProfile.detectTrigger;
        localFactionTag = localProfile.faction;  
        EnemyInfoSet(); 
    }

    private void EnemyInfoSet()
    {
        if (this.tag == "Player")
        enemyTag = "Enemy";

        if(this.tag == "Enemy")
        enemyTag = "Player";
    }

    void OnTriggerStay(Collider other)
    {
        if (IsEnemy(other) && IsTargetDead() == false)
        SendAttackOrder(other);
    }

    void OnTriggerExit(Collider other)
    {
        if (IsEnemy(other)) 
            if(other.GetComponentInParent<UnitProfile>().gameObject == otherProfile.gameObject)
                ExitAttack();
    }

    private bool IsEnemy(Collider other)
    {
        if ((otherProfile = other.GetComponentInParent<UnitProfile>()) != null && other.isTrigger == false)
        return other.tag == enemyTag;
        else return false; 
    }

    private void Raycast(Vector3 otherPos, Collider other)
    {
        rayCastStartPoint = localProfile.rayCastStartPoint.transform.position; //實時獲取raycast起點

        Ray ray = new Ray(rayCastStartPoint, (otherPos-rayCastStartPoint).normalized); //ray定義
        Physics.Raycast(ray, out hitInfo, Vector3.Distance(rayCastStartPoint, otherPos), 1<<otherProfile.layerNum | 1<< 7, QueryTriggerInteraction.Ignore);
        IfBlocked(other);

        Debug.DrawLine(ray.origin,hitInfo.point,Color.red,3);
    }

    private bool IfBlocked(Collider other)
    {
        return other.GetComponentInParent<UnitProfile>().gameObject != hitInfo.transform.gameObject;
    }

    public RaycastHit AccessHitIfo()
    {
        return hitInfo;
    }

    private void SendAttackOrder(Collider other)
    {
        if (IfAttack(other))
        {
            isEngaging = true;
            localProfile.attack.AttackOrderReciver(true,otherProfile);
        }
    }

    private bool IfAttack (Collider other)
    {
        otherPos = other.transform.position;
        Raycast(otherPos, other);

        if (IfBlocked(other) == false && isEngaging == false )
        return true;
        else return false;
    }

    private void ExitAttack ()
    {
        localProfile.attack.AttackOrderReciver(false);
        isEngaging = false;
        otherProfile = null;
        otherPos = Vector3.zero;
    }

    private bool IsTargetDead()
    {
        return (otherProfile.health.GetHpState() <= 0f);
    }

}
