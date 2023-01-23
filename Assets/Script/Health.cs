using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private UnitProfile localProfile;
    private UnitProfile otherProfile;
    private int healthPoint;
    private bool isDead;
    private Vector3 attackSource;
    private Vector3 localPos;

    void Awake()
    {
        localProfile = this.GetComponent<UnitProfile>();
        healthPoint = localProfile.hp;
        isDead = localProfile.isDead;
    }

    public void Damage(int damagePoint)
    {
        healthPoint -= damagePoint;

        if (healthPoint <= 0)
        {
        isDead = true;
        localProfile.isDead = true;
        }
    }

    public int GetHpState()
    {
        return healthPoint;
    }

    public int GetHpState(out bool isDeadOut)
    {
        isDeadOut = isDead;
        return healthPoint;
    }

    //private void DestoryThis()
    //單位死亡 待實作


    //private int DamageCalculate (Vector3 attackSource, Vector3 locaPos, int damage, )
    //傷害擴散 待實作
}
