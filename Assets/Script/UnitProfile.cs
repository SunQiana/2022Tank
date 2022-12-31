using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Debug用單位履歷
/// </summary>
public class UnitProfile : MonoBehaviour
{
    [Header("陣營")]
    public string faction;
    [Header("血量")]
    public int hp = 100;
    [Header("攻擊力")]
    public int atk = 10;
    [Header("攻擊CD")]
    [Range(0.1f, 100f)]
    public float atkCd = 10f;
    [Header("是否死亡")]
    public bool isDead = false;
    [Header("當前目標座標")]
    public Vector3 targetPos;


    [Space]
    [Header("物件索引")]
    public GameObject turretObject;
    public AudioClip fireSound;
    public GameObject fireLight;
    public GameObject detectTrigger;
    public GameObject rayCastStartPoint;
    public int layerNum;

    public Detect detect;
    public Attack attack; 

    void Start()
    {
        faction = this.tag;
        layerNum = this.gameObject.layer;
        detect = GetComponent<Detect>();
        attack = GetComponent<Attack>();
    }


}
