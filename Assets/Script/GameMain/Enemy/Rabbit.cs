using UnityEngine;
using UnityEngine.UI;

public class Rabbit : EnemyManager
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

    //攻撃力の補正値
    float powerValue = 0f;



    public override void InitRabbit()
    {
        Debug.Log("初期化");
        deathFlag = false;

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
        int skilRnd = 0;
        int slectNo = 0;
        for (int i = 0; i < 1; i++)
        {
            //0か1の乱数
            skilRnd = UnityEngine.Random.Range(1, 101);
        }

        //スキル1
        if(skilRnd <= 70)
        {
            //リリーの方がHP多い時
            if (riri.hp > dhia.hp)
            {
                slectNo = 0;
            }
            //ディアの方がHP多い時
            else
            {
                slectNo = 1;
            }

            //ディアが死んでいる時攻撃対象をリリーに上書き
            if (dhia.deathFlag)
            {
                slectNo = 0;
            }

            //攻撃対象リリー
            if (slectNo == 0)
            {
                //70%軽減
                if (dhia.ririDefenseFlag)
                {
                    encountSys.windowsMes.text = "ウサギのこうげき！ディアがリリーを守った！ディアに" + ((power + (power * powerValue)) * 0.3f) + "のダメージ!";
                    dhia.hp -= ((power +(power * powerValue)) * 0.3f);
                }
                else
                {
                    encountSys.windowsMes.text = "ウサギのこうげき！リリーに" + (power + (power * powerValue)) + "のダメージ!";
                    riri.hp -= (power + (power * powerValue));
                }

            }
            //攻撃対象ディア
            else if (slectNo == 1)
            {
                if (dhia.defenseFlag)
                {
                    encountSys.windowsMes.text = "ウサギのこうげき！ディアに" + ((power + (power * powerValue)) * 0.5f) + "のダメージ!";
                    dhia.hp -= ((power + (power * powerValue)) * 0.5f);
                }
                else
                {
                    encountSys.windowsMes.text = "ウサギのこうげき！ディアに" + (power + (power * powerValue)) + "のダメージ!";
                    dhia.hp -= (power + (power * powerValue));
                }
            }

        }

        //スキル2
        if (skilRnd >= 71)
        {
            encountSys.windowsMes.text = "ウサギはにんじんシチューを飲んだ！\nウサギの攻撃力が15%アップした！";
            powerValue += 0.15f;
        }
    }


    public override void AddWords()
    {
        
    }
}