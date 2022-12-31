using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 偵測敵人，並確認是否可以攻擊。
/// 1、檢測是否為單位(嘗試抓profile) 2、檢測是否為敵人(比對tag) 3、檢測是否遮擋 4、以collider獲取目標資訊 5、繼續以ray檢測是否遮擋
/// </summary>
public class Detect : MonoBehaviour
{
    private UnitProfile localProfile;
    private UnitProfile otherProfile;
    private Attack localAttackCom;
    private Collider detectTrigger;
    private string localFactionTag;
    private Vector3 otherPos;
    private Vector3 rayCastStartPoint;
    private RaycastHit hitInfo;
    private bool ifEngage;
    private GameObject otherObj;

    void Awake()
    {
        localProfile = this.GetComponent<UnitProfile>();
        detectTrigger = localProfile.detectTrigger.GetComponent<Collider>();
        localFactionTag = localProfile.faction;
        otherProfile = null;
        otherObj = null;
    }

    void OnTriggerStay(Collider other)
    {
        otherPos = other.transform.position;
        if (IsEnemy(other))
        Raycast(otherPos, out hitInfo, otherObj);
        else if (IfBlocked(otherPos) != false)
        ifEngage = true;
    }

    private bool IsEnemy(Collider other)
    {
        if ((otherProfile = other.GetComponentInParent<UnitProfile>()) != null && other.isTrigger == false)
        return other.tag != this.tag;
        else return false; 
    }

    private void Raycast(Vector3 otherPos, out RaycastHit hitInfo, GameObject other)
    {
        rayCastStartPoint = localProfile.rayCastStartPoint.transform.position;
        Ray ray = new Ray(rayCastStartPoint, (otherPos-rayCastStartPoint).normalized);
        Physics.Raycast(ray, out hitInfo, Vector3.Distance(rayCastStartPoint, otherPos), 1<<otherProfile.layerNum | 1<< 7, QueryTriggerInteraction.Ignore);   
        Debug.DrawLine(ray.origin,hitInfo.point,Color.red,3);
    }

    private bool IfBlocked(Vector3 otherPos)
    {
        return otherPos != hitInfo.point;
    }

    public bool IfEngage (out RaycastHit hitinfo)
    {
        hitinfo = hitInfo;
        return ifEngage;
    }

    public bool IfEngage ()
    {
        return ifEngage;
    }

    public bool IfEngage (out GameObject other)
    {
        other = otherObj;
        return ifEngage;
    }

}
