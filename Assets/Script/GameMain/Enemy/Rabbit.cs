using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Rabbit : EnemyManager
{
    [SerializeField]
    Status enemyStatus = null;

    public float rabbitMaxhp = 0;
    public float rabbitMaxmp = 0;

    public float rabbitHp = 0;
    public float rabbitMp = 0;
    public int rabbitPower = 0;
    public int rabbitDef = 0;


    [Header("クラス参照")]
    [SerializeField]
    Riri riri = null;
    [SerializeField]
    Dhia dhia = null;


    [SerializeField]
    EnemyManager enemySys = null;

    //攻撃力の補正値
    float powerValue = 0f;

    //うさぎのアニメーション
    public Animator rabbitAnim = null;

    float timerRabbit = 0;
    public bool timerFlag = false;



    public override void InitRabbit()
    {
        Debug.Log("初期化");
        deathFlag = false;
        
        this.gameObject.transform.localScale =  new Vector3(1,1,1);

        rabbitMaxhp = enemyStatus.MAXHP;
        rabbitMaxmp = enemyStatus.MAXMP;
        rabbitPower = enemyStatus.ATK;
        rabbitDef = enemyStatus.DEF;

        hp = maxhp;
        mp = maxmp;
    }

    void Update()
    {
        if (timerFlag)
        {
            timerRabbit += Time.deltaTime;

            if(timerRabbit >= 1.0f)
            {
                rabbitAnim.SetBool("Attack", false);
                rabbitAnim.SetBool("Damage2", false);
            }
            if (timerRabbit >= 3.5f)
            {
                riri.ririAnim.SetBool("R_TakeDamage", false);
                dhia.dhiaAnim.SetBool("D_TakeDamage", false);

                timerRabbit = 0;
                timerFlag = false;
            }
        }
    }


    //ダメージテキスト表示用
    [SerializeField]
    TextMeshProUGUI[] damageText = null;
    [SerializeField]
    GameObject[] damageTextObj = null;


    public override void SkilRabbit()
    {
        int skilRnd = 0;
        int slectNo = 0;

        timerFlag = true;
        rabbitAnim.SetBool("Attack", true);
        for (int i = 0; i < 1; i++)
        {
            //0から100の乱数
            skilRnd = UnityEngine.Random.Range(1, 100);
        }

        //スキル1
        if(skilRnd <= 100)
        {
            
            float ririDamage = DamageCalculation(rabbitPower, riri.def);
            float dhiaDamage = DamageCalculation(rabbitPower, dhia.def);

            //ダメージが0を下回ってる時は0ダメージに書き換え
            if(ririDamage <= 0)
            {
                ririDamage = 0;
            }
            if (dhiaDamage <= 0)
            {
                dhiaDamage = 0;
            }

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
                Invoke("RiriDamage", 1.2f);

                //テキストの表示処理
                damageTextObj[0].SetActive(true);

                //70%軽減
                if (dhia.ririDefenseFlag)
                {
                    encountSys.windowsMes.text = "ウサギのこうげき！ディアがリリーを守った！ディアに" + ( ririDamage * 0.3f) + "のダメージ!";
                    dhia.hp -= (ririDamage * 0.3f);
                    damageText[0].text = (ririDamage * 0.3f).ToString();
                }
                else
                {
                    encountSys.windowsMes.text = "ウサギのこうげき！リリーに" + (ririDamage) + "のダメージ!";
                    riri.hp -= (ririDamage);
                    damageText[0].text = (ririDamage).ToString();
                }


                encountSys.HpMoveWait("Riri");
            }
            //攻撃対象ディア
            else if (slectNo == 1)
            {
                Invoke("DhiaDamage", 1.2f);

                //テキストの表示処理
                damageTextObj[1].SetActive(true);

                if (dhia.defenseFlag)
                {
                    encountSys.windowsMes.text = "ウサギのこうげき！ディアに" + (dhiaDamage * 0.5f) + "のダメージ!";
                    dhia.hp -= (dhiaDamage * 0.5f);
                    damageText[1].text = (dhiaDamage * 0.5f).ToString();
                }
                else
                {
                    encountSys.windowsMes.text = "ウサギのこうげき！ディアに" + (dhiaDamage) + "のダメージ!";
                    dhia.hp -= (dhiaDamage);
                    damageText[1].text = (dhiaDamage).ToString();
                }

                encountSys.HpMoveWait("Dhia");
            }

            StartCoroutine("DamageInit");
        }

        //スキル2
        if (skilRnd >= 710)
        {
            encountSys.windowsMes.text = "ウサギはにんじんシチューを飲んだ！\nウサギの攻撃力が15%アップした！";
            powerValue += 0.15f;
        }

    }

    void RiriDamage()
    {
        riri.ririAnim.SetBool("R_TakeDamage", true);
    }
    void DhiaDamage()
    {
        dhia.dhiaAnim.SetBool("D_TakeDamage", true);
    }

    //ダメージ計算用
    int DamageCalculation(int attack,int defense)
    {
        //シード値の変更
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        //素のダメージ計算
        int damage = ((attack + (attack * (int)powerValue))/2) - (defense / 4);

        //ダメージ振幅の計算
        int width = damage / 16 + 1;

        //ダメージ振幅値を加味した計算
        damage = UnityEngine.Random.Range(damage - width, damage + width);

        //呼び出し側にダメージ数を返す
        return damage;
    }

    IEnumerator DamageInit()
    {
        yield return new WaitForSeconds(0.5f);

        damageTextObj[0].SetActive(false);
        damageTextObj[1].SetActive(false);

        damageText[0].text = "0";
        damageText[1].text = "0";
    }

}