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
    private float localAtkCD = 2.0f;
    private bool isAttackAble = false;
    private float coldDownTiming = 0;
    private GameObject bullet;
    private string otherTag = null;
    private Vector3 otherPos = Vector3.zero;
    private Vector3 spwanPos;
    private Turret turret;


    void Awake()
    {
        localProfile = this.GetComponent<UnitProfile>();
        localAtk = localProfile.atk;
        localAtkCD = localProfile.atkCd;
        localDetectCom = localProfile.detect;
        bullet = localProfile.bullet;
        turret = localProfile.turret;
    }   

    private void AttackOrder()
    {
       StartCoroutine(AttackIEnume());
    }

    IEnumerator AttackIEnume()
    {
        while(isAttackAble)
        {
            otherPos = localDetectCom.TargetProfile().transform.position;
            BulletSpawn();
            yield return new WaitForSeconds(localAtkCD);
        }
        yield break;
    }

    public void AttackOrderReciver()
    {
        isAttackAble = true;
        AttackOrder();
    }

    public void ExitAttackOrder() 
    {
        isAttackAble = false;
        turret.StopTracking();
        StopCoroutine(AttackIEnume());
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
        otherPos =  Vector3.zero;
    }
    
}
