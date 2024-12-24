using UnityEngine;
using UnityEngine.UI;

public class Bird : EnemyManager
{
    [SerializeField]
    Status enemyStatus = null;

    [Header("クラス参照")]
    [SerializeField]
    Riri riri = null;
    [SerializeField]
    Dhia dhia = null;

    [SerializeField]
    EnemyManager enemySys = null;


    public override void InitBird()
    {
        Debug.Log("初期化");

        deathFlag = false;

        this.gameObject.transform.localScale = new Vector3(1,1,1);

        maxhp = enemyStatus.MAXHP;
        maxmp = enemyStatus.MAXMP;
        power = enemyStatus.ATK;


        hp = maxhp;
        mp = maxmp;
    }

    void Update()
    {
        if (hp <= 0)
        {
            enemySys.deathFlag = true;
        }
    }

    public override void Skil()
    {
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