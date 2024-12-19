using System;
using UnityEngine;

public class Dhia : MonoBehaviour
{
    //ディアのステータス
    [SerializeField]
    Status dhiaStatus = null;
    //階層データ管理システム
    [SerializeField]
    FloorNoSys floorNoSys = null;

    [NonSerialized]
    public float maxhp = 0;
    [NonSerialized]
    public float maxmp = 0;

    [NonSerialized]
    public float hp = 0;
    [NonSerialized]
    public float mp = 0;
    [NonSerialized]
    public float power = 0;
    [NonSerialized]
    public float def = 0;

    [NonSerialized]
    public bool deathFlag = false;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        floorNoSys = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();
        maxhp = dhiaStatus.MAXHP;
        maxmp = dhiaStatus.MAXMP;
        power = dhiaStatus.ATK;
        def = dhiaStatus.DEF;

        if (floorNoSys != null)
        {
            if (floorNoSys.floorNo == 0)
            {
                hp = maxhp;
                mp = maxmp;
                deathFlag = false;
            }
            else
            {
                hp = dhiaStatus.HP;
                mp = dhiaStatus.MP;
            }
        }
    }

    void Update()
    {
        if(hp <= 0)
        {
            deathFlag = true;
        }
        dhiaStatus.HP = hp;
        dhiaStatus.MP = mp;
    }

    public void Skil1()
    {

    }
    public void Skil2()
    {

    }
    public void Skil3()
    {

    }
}
