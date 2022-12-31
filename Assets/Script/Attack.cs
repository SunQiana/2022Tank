using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    UnitProfile otherProfile;
    UnitProfile localProfile;
    Detect localDetectCom;
    int localAtk;
    float localAtkCD;
    

    void Awake()
    {
        localProfile = this.GetComponent<UnitProfile>();
        localAtk = localProfile.atk;
        localAtkCD = localProfile.atkCd;
        localDetectCom = localProfile.detect;
    }   

    
    
}
