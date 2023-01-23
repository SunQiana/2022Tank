using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
[RequireComponent(typeof(Detect))]
[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody))]
*/
public class UnitProfile : MonoBehaviour
{
    [Header("單位當前資訊")]
    [Space]
    [Header("當前生命")]
    [SerializeField] private int hpNow;
     [Header("當前目標座標")]
    public Vector3 targetPos;
     [Header("是否死亡")]
    public bool isDead = false;
   
[Header("----------------------------------------------------------------------------------------------------")]
    [Header("單位預設數據")]
    [Space]
    [Header("陣營")]
    public string faction;
    [Header("血量")]
    public int hp = 100;
    [Header("攻擊力")]
    public int atk = 10;
    [Header("攻擊CD")]
    [Range(0.1f, 100f)]
    public float atkCd = 10f;
    [Header("子彈速度")]
    [Range(0.1f, 100f)]
    public float bulletSpeed;

[Header("----------------------------------------------------------------------------------------------------")]
    [Header("物件索引")]
    [Space]
    public Collider detectTrigger;
    public GameObject rayCastStartPoint;
    public int layerNum;
    public GameObject bullet;


[Header("----------------------------------------------------------------------------------------------------")]
    [Header("腳本索引")]
    [Space]
    [SerializeField] public Detect detect;
    [SerializeField] public Attack attack; 
    [SerializeField] public Health health;

    void Awake()
    {
        faction = this.tag;
        layerNum = this.gameObject.layer;
        detect = GetComponent<Detect>();
        attack = GetComponent<Attack>();
        health = GetComponent<Health>();
    }

    void note()
    {
        //子彈系統wip
        //子彈尚未能摧毀自身
        //子彈尚未能傳出傷害
    }

    void Update()
    {
       /* if(this.tag == "Enemy" )
        print (health.GetHpState());*/

        if(this.tag == "Player")
        targetPos = detect.GetAttackPos();

        hpNow = health.GetHpState(out isDead);
    }
}
