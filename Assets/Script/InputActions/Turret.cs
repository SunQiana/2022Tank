using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private UnitProfile unitProfile;
    private Vector3 targetPos;
    private Quaternion originalRota;
    private GameObject root;
    private GameObject turret;
    private bool isTracking = false;
    private bool isReturned = true;
    public float turretSpeed = 1.5f;
    private Detect detect;
    private bool isExitingTracking = false;

    private void Awake()
    {
        unitProfile = this.GetComponent<UnitProfile>();
    }
    void Start()
    {
        detect = unitProfile.detect;
        turret = unitProfile.turretObj;
        GetUnitRoot();
    }

    private void ExitingTracking()
    {
        if(turret.transform.rotation.y > 0) //&& turret.transform.rotation.y > turretSpeed)
        turret.transform.Rotate(0,turretSpeed,0);
        if(turret.transform.rotation.y < 0 )//&& turret.transform.rotation.y < -turretSpeed)
        turret.transform.Rotate(0,-turretSpeed,0);
        else
        {
        turret.transform.rotation = unitProfile.gameObject.transform.rotation;
        isExitingTracking = false;
        }
    }

    private void GetUnitRoot()
    {
        for (int i = 0; i < unitProfile.gameObject.transform.childCount; i++)
        {
            if (unitProfile.gameObject.transform.GetChild(i).name == "Root")
                root = unitProfile.gameObject.transform.GetChild(i).gameObject;
        }

        if (root == null)
            Debug.LogWarning("Turret Can't Find Local Unit Model Root !");
    }

    public void StopTracking()
    {
        isTracking = false;
        isExitingTracking = true;
        targetPos = Vector3.zero;
    }   

    public void StartTracking()
    {
        isTracking = true;
        StartCoroutine(Tracking());
    }

    IEnumerator Tracking()
    {
        while (isTracking)
        {
            turret.transform.LookAt(detect.GetAttackPos());
            yield return new WaitForFixedUpdate(); //等待一幀
        }
        yield return new WaitForSeconds(1.5f); //等待1.5秒後回正

        while(isExitingTracking && isTracking == false) //如果還未轉正就接觸新敵人，則取消轉正。
        {
            ExitingTracking();
            yield return new WaitForFixedUpdate();
        }
    }

}
