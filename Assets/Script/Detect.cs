using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 偵測敵人，並確認是否可以攻擊
/// </summary>
public class Detect : MonoBehaviour
{
    private UnitProfile localProfile;
    private UnitProfile otherProfile;
    private Collider detectTrigger;
    private string localFactionTag;
    private bool isEngaging;
    private Vector3 targetPos;
    private Vector3 rayCastStartPoint;
    private int rayCastLayerInt;

    void Awake()
    {
    localProfile = this.GetComponent<UnitProfile>();
    detectTrigger = localProfile.detectTrigger.GetComponent<Collider>();
    localFactionTag = localProfile.faction;
    }

    void OnTriggerStay(Collider other)
    {
        if (IfEngage(other))
        Raycast(other.gameObject);
    }

    private bool IfEngage(Collider other)
    {
        if((otherProfile = other.GetComponentInParent<UnitProfile>()) != null && other.isTrigger == false)
        return localFactionTag != otherProfile.faction; //比較兩者是否相同
        else return false; //若拿不到Profile
    }

    void Raycast(GameObject other)
    {
        targetPos = other.transform.position;
        rayCastStartPoint = localProfile.rayCastStartPoint.transform.position;
        Ray ray = new Ray(rayCastStartPoint, targetPos-rayCastStartPoint);
        RaycastHit hitInfo;

        Physics.Raycast(ray, out hitInfo, Vector3.Distance(rayCastStartPoint,targetPos)*1.5f, other.layer, QueryTriggerInteraction.Ignore);   
        Debug.DrawRay(ray.origin,ray.direction*10,Color.red,1);

        print(hitInfo.collider);
    }
}
