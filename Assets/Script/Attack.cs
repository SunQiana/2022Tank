using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    UnitProfile otherProfile;
    UnitProfile localProfile;
    Detect localDetectCom;
    private int localAtk;
    private float localAtkCD;
    private bool isAttackAble = false;
    private bool isReady = true;
    private bool timeRecorded = false;
    private float coldDownTiming = 0;

    

    void Awake()
    {
        localProfile = this.GetComponent<UnitProfile>();
        localAtk = localProfile.atk;
        localAtkCD = localProfile.atkCd;
        localDetectCom = localProfile.detect;
    }   

    void FixedUpdate()
    {
        CheckIfReloaded();
        AttackOrder();
    }

    private void AttackOrder()
    {
        if(isReady && isAttackAble)
        {
        isReady = timeRecorded = false; //重設bool
        RecordRloadTime();
        print(this.tag + "Attack");
        }
    }

    public void AttackOrderReciver(bool permissionType,UnitProfile otherProfileInput)
    {
        isAttackAble = permissionType;
         if (permissionType == true)
        {
        otherProfileInput = otherProfile;
        AttackOrder();
        }
    }

    public void AttackOrderReciver(bool permissionType)
    {
        isAttackAble = false;
    }

    private void RecordRloadTime()
    {
        coldDownTiming = Time.fixedTime + localAtkCD;
        timeRecorded = true;
        print (this.tag + "timeRecorded!" + coldDownTiming);
    }

    private void CheckIfReloaded()
    {
        if(Time.fixedTime >= coldDownTiming)
        {
            isReady = true;
            timeRecorded = false;
            coldDownTiming = 0f; //裝填完成，清空時間紀錄
        }
    }
}
