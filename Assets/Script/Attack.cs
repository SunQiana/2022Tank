using System;
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
    private Vector3 otherPos = Vector3.zero;
    Vector3 spwanPos;
    

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
        BulletSpawn();
        }
    }

    public void AttackOrderReciver(bool isAttackAbleIn, Vector3 otherPosIn)
    {
        isAttackAble = true;
        otherPos = otherPosIn;
        AttackOrder();
    }

    public void AttackOrderReciver(bool isAttackAbleIn) //如未輸入v3則自動設為false
    {
        isAttackAble = isAttackAbleIn;
    }

    private void RecordRloadTime()
    {
        coldDownTiming = Time.fixedTime + localAtkCD;
        timeRecorded = true;
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

    private void BulletSpawn()
    {   
        OtherPosUpdate();
        spwanPos = localProfile.rayCastStartPoint.transform.position; //每次生成前都要獲取位置
        Quaternion rotation =  Quaternion.LookRotation(otherPos - spwanPos);
        
        GameObject instantiatedBullet = Instantiate<GameObject>(bullet,spwanPos,rotation);
        instantiatedBullet.transform.SetParent(null);//取消子彈物件父級 
        instantiatedBullet.GetComponent<Bullet>().SpawnerInfoReciver(localProfile);//傳遞資料給bullet
    }

    private void OtherPosUpdate()
    {
        otherPos =  localProfile.detect.GetAttackPos();
    }
    
}
