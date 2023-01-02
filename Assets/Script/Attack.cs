using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private UnitProfile otherProfile = null;
    private UnitProfile localProfile;
    private Detect localDetectCom;
    private int localAtk;
    private float localAtkCD;
    private bool isAttackAble = false;
    private bool isReady = true;
    private bool timeRecorded = false;
    private float coldDownTiming = 0;
    private GameObject bullet;
    private string otherTag = null;
    

    void Awake()
    {
        localProfile = this.GetComponent<UnitProfile>();
        localAtk = localProfile.atk;
        localAtkCD = localProfile.atkCd;
        localDetectCom = localProfile.detect;
        bullet = localProfile.bullet;
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
        ShootBullet(otherProfile);
        print(this.tag + "Attack");
        }
    }

    public void AttackOrderReciver(bool permissionType,UnitProfile targetProfileInput)
    {
        isAttackAble = permissionType;
         if (permissionType == true)
        {
        targetProfileInput = otherProfile;
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

    private void ShootBullet(UnitProfile otherProfile)
    {
        BulletSpawn(otherProfile);
    }

    private void BulletSpawn(UnitProfile otherProfile)
    {
        //Quaternion rotation =  Quaternion.LookRotation(otherProfile.transform.position, Vector3.zero);
        print(otherProfile.gameObject);
        GameObject instantiatedBullet = Instantiate<GameObject>(bullet,localProfile.rayCastStartPoint.transform);
        instantiatedBullet.transform.LookAt(otherProfile.transform);
    }
    
}
