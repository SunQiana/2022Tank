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
    private Transform rayCastStartPoint;
    private string localFactionTag;
    private bool isEngaging;
    private Vector3 targetPos;

    void Start()
    {
    localProfile = this.GetComponent<UnitProfile>();
    detectTrigger = localProfile.detectTrigger.GetComponent<Collider>();
    localFactionTag = localProfile.faction;
    }

    void OnTriggerStay(Collider other)
    {
        if (IfEngage(other))
        targetPos = trans
    }

    private bool IfEngage(Collider other)
    {
        if(((otherProfile = other.GetComponentInParent<UnitProfile>()) != null)
        return localFactionTag != otherProfile.faction; //比較兩者是否相同
        else return false; //若拿不到Profile
    }
}
