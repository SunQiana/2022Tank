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

    private void DamageInput(int damage, out bool isDeadOut)
    {
        healthPoint -= damage;
        if (healthPoint <= 0)
        isDead = isDeadOut = true;
        else
        isDeadOut = false;
    }

    public int GetHealthStatus(out bool isDeadOut)
    {
        isDeadOut = isDead;
        return healthPoint;
    }

    public void DamageDealOrder(int damage, out bool isDeadOut)
    {
        if (isDead != false)
        DamageInput(damage,out isDeadOut);
        else
        isDeadOut = false;
    }

    //private int DamageCalculate (Vector3 attackSource, Vector3 locaPos, int damage, )
    //傷害擴散 待實作
}
