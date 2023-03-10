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
    private Vector3 rayCastStartPoint;
    private Vector3 currentTargetPos;
    private bool isEngaging = false; //是否正與上一個敵人交戰
    private string enemyTag;
    private int enemyLayerNum;
 
    void Awake()
    {
        localProfile = this.GetComponent<UnitProfile>();
    }
    void Start()
    {
        enemyLayerNum = localProfile.enemyLayerNum;
        enemyTag = localProfile.enemyTag;
    }
    void OnTriggerStay(Collider other)
    {
        if (IfSendAttackOrder(other)) 
        SendAttackOrder(other);

       if(isEngaging)
            if( IfBlocked(other))
                ExitAttack(); //如果在交戰中，檢測到受阻擋，則脫離
    }

    void OnTriggerExit(Collider other)
    {
        if(isEngaging && otherProfile != null) //如果在交戰中才進行判斷
            if(other.gameObject == otherProfile.gameObject)
            ExitAttack();
    }

    private bool IsEnemy(Collider other)
    {
        if (other.tag == enemyTag && other.isTrigger == false)
            return other.tag == enemyTag; 
        else return false; 
    }

    private void Raycast(Vector3 otherPos, out RaycastHit hitInfo)
    {
        rayCastStartPoint = localProfile.rayCastStartPoint.transform.position; //實時獲取raycast起點
        Ray ray = new Ray(rayCastStartPoint, (otherPos-rayCastStartPoint)); //ray定義
        Physics.Raycast(ray, out hitInfo, Vector3.Distance(rayCastStartPoint, otherPos), 1<<enemyLayerNum | 1<<7, QueryTriggerInteraction.Ignore);
        Debug.DrawLine(ray.origin,hitInfo.point,Color.red,3);
    }

    private bool IfBlocked(Collider other)
    {
        print(otherProfile);
        RaycastHit hitInfo;
        Raycast(otherProfile.transform.position,out hitInfo);
        return otherProfile.gameObject != hitInfo.transform.gameObject; 
    }

    private void SendAttackOrder(Collider other)
    {
        isEngaging = true;
        localProfile.attack.AttackOrderReciver();
    }

    private void ExitAttack ()
    {
        isEngaging = false;
        localProfile.attack.ExitAttackOrder();
        otherProfile = null;
        currentTargetPos = Vector3.zero;
    }

    private bool IsTargetDead()
    {
        return (otherProfile.health.GetHpState() <= 0f);
    }

    public Vector3 GetAttackPos()
    {
        return otherProfile.transform.position;
    }

    private bool IfSendAttackOrder(Collider other)
    {
        //是敵人 且未與上個目標交戰中 //先判斷自身狀態並確認是否為敵人
        if (IsEnemy(other) && (isEngaging == false))
        {
            isEngaging = true;
            GetUnitProfile(other); //確認是敵人再抓 以免重複賦值

            if (IfBlocked(other) == false && (IsTargetDead() == false)) //且未受阻擋 目標未死亡 //再判斷敵人狀態以免null
                return true;
            else return false;
        }
        else return false;
    }
    private void GetUnitProfile (Collider other)
    {
        otherProfile = other.GetComponentInParent<UnitProfile>();
    }
}

