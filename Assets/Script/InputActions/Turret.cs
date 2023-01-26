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
    private bool ifLook = false;
    private bool isReturned;
    public float turretSpeed = 1.5f;
    private Detect detect;

    private void Awake()
    {
        unitProfile = this.GetComponent<UnitProfile>();
        turret = unitProfile.turretObj;
        GetUnitRoot();
        detect = unitProfile.detect;
    }
    private void Update()
    {
        Looking();
    }

    private void Looking()
    {
        if(ifLook)
        turret.transform.LookAt(detect.GetAttackPos());
    }

    private void EndLooking()
    {
        Quaternion.RotateTowards(turret.transform.rotation, root.transform.rotation, turretSpeed);

        if (turret.transform.rotation == root.transform.rotation)
            isReturned = true;
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
        ifLook = false;
        targetPos = Vector3.zero;
    }   

    public void StartTracking()
    {
        ifLook = true;
    }

}
