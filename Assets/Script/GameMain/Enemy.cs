using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Status[] enemyStatus = null;

    public float maxhp = 0;
    public float maxmp = 0;

    public float hp = 0;
    public float mp = 0;
    public float power = 0;

    public bool deathFlag = false;

    [SerializeField]
    GameObject enemyMain = null;

    [Header("クラス参照")]
    [SerializeField]
    Riri riri = null;
    [SerializeField]
    Dhia dhia = null;

    [SerializeField]
    TestEncount encountSys = null;

    int rnd = 0;
    void Awake()
    {
        Init();
    }

    void Init()
    {
        Debug.Log(enemyStatus.Length);
        rnd = Random.Range(0, enemyStatus.Length);

        maxhp = enemyStatus[rnd].MAXHP;
        maxmp = enemyStatus[rnd].MAXMP;
        power = enemyStatus[rnd].ATK;
        hp = maxhp;
        mp = maxmp;
        enemyMain.transform.localScale = new Vector3(1, 1, 1);
        deathFlag = false;
    }


    void Update()
    {
        if(hp <= 0)
        {
            deathFlag = true;
            enemyMain.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void Skil()
    {
        Debug.Log("エネミー");

        int rnd = 0;
        for (int i = 0; i < 1; i++)
        {
            rnd = UnityEngine.Random.Range(0, 2);
        }
        //ディアが死んでいる時攻撃対象をリリーに上書き
        if (dhia.deathFlag)
        {
            rnd = 0;
        }

        //攻撃対象リリー
        if (rnd == 0)
        {
            //70%軽減
            if (dhia.ririDefenseFlag)
            {
                encountSys.windowsMes.text = "てきのこうげき！ディアがリリーを守った！ディアに" + power * 0.3f + "のダメージ!";
                dhia.hp -= (power * 0.3f);
            }
            else
            {
                encountSys.windowsMes.text = "てきのこうげき！リリーに" + power + "のダメージ!";
                riri.hp -= power;
            }

        }
        //攻撃対象ディア
        else if (rnd == 1)
        {
            if (dhia.defenseFlag)
            {
                encountSys.windowsMes.text = "てきのこうげき！ディアに" + power * 0.5f + "のダメージ!";
                dhia.hp -= (power * 0.5f);
            }
            else
            {
                encountSys.windowsMes.text = "てきのこうげき！ディアに" + power + "のダメージ!";
                dhia.hp -= power;
            }
        }
    }
}
