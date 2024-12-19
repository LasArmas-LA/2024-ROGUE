using System;
using UnityEngine;

public class Riri : MonoBehaviour
{
    [SerializeField]
    Status ririStatus = null;
    [SerializeField]
    FloorNoSys floorNoSys = null;

    [NonSerialized]
    public float maxhp = 0;
    [NonSerialized]
    public float maxmp = 0;

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
        maxhp = ririStatus.MAXHP;
        maxmp = ririStatus.MAXMP;
        power = ririStatus.ATK;
        def = ririStatus.DEF;

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
                hp = ririStatus.HP;
                mp = ririStatus.MP;
            }
        }
    }

    void Update()
    {
        if(hp <= 0)
        {
            deathFlag = true;
        }
        ririStatus.HP = hp;
        ririStatus.MP = mp;
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
