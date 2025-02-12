using UnityEngine;
using UnityEngine.UI;

public class Bird : EnemyManager
{

    public float birdMaxhp = 0;
    public float birdMaxmp = 0;

    public float birdHp = 0;
    public float birdMp = 0;
    public int birdPower = 0;
    public int birdDef = 0;

    [SerializeField]
    Status enemyStatus = null;

    [Header("クラス参照")]
    [SerializeField]
    Riri riri = null;
    [SerializeField]
    Dhia dhia = null;

    [SerializeField]
    EnemyManager enemySys = null;

    //アニメーション管理用
    [SerializeField]
    public Animator birdAnim = null;
    float timerBird = 0;
    public bool timerFlag = false;


    //攻撃力の補正値
    float powerValue = 0f;


    public override void InitBird()
    {
        Debug.Log("初期化");

        deathFlag = false;

        this.gameObject.transform.localScale = new Vector3(1,1,1);

        birdMaxhp = enemyStatus.MAXHP;
        birdMaxmp = enemyStatus.MAXMP;
        birdPower = enemyStatus.ATK;
        birdDef = enemyStatus.DEF;


        hp = maxhp;
        mp = maxmp;
    }

    void Update()
    {
        if (timerFlag)
        {
            timerBird += Time.deltaTime;

            if (timerBird >= 3.5f)
            {
                birdAnim.SetBool("Eb_Attack1", false);
                birdAnim.SetBool("Eb_Damage2", false);
                riri.ririAnim.SetBool("R_TakeDamage", false);
                dhia.dhiaAnim.SetBool("D_TakeDamage", false);

                timerBird = 0;
                timerFlag = false;
            }
        }
    }

    public override void SkilBird()
    {
        timerFlag = true;
        birdAnim.SetBool("Eb_Attack1",true);
        int skilRnd = 0;
        int slectNo = 0;
        for (int i = 0; i < 1; i++)
        {
            //0から100の乱数
            skilRnd = UnityEngine.Random.Range(1, 100);
        }

        //スキル1
        if (skilRnd <= 100)
        {
            int ririDamage = DamageCalculation(birdPower, riri.def);
            float dhiaDamage = DamageCalculation(birdPower, dhia.def);

            //リリーの方がHP多い時
            if (riri.hp > dhia.hp)
            {
                slectNo = 1;
            }
            //ディアの方がHP多い時
            else
            {
                slectNo = 0;
            }

            //ディアが死んでいる時攻撃対象をリリーに上書き
            if (dhia.deathFlag)
            {
                slectNo = 0;
            }

            //攻撃対象リリー
            if (slectNo == 0)
            {
                Invoke("RiriDamage", 1f);
                timerFlag = true;

                //70%軽減
                if (dhia.ririDefenseFlag)
                {
                    encountSys.windowsMes.text = "ふくろうのこうげき！ディアがリリーを守った！ディアに" + (ririDamage * 0.3f) + "のダメージ!";
                    dhia.hp -= (ririDamage * 0.3f);
                }
                else
                {
                    encountSys.windowsMes.text = "ふくろうのこうげき！リリーに" + (ririDamage) + "のダメージ!";
                    riri.hp -= (ririDamage);
                }
                encountSys.HpMoveWait("Riri");
            }
            //攻撃対象ディア
            else if (slectNo == 1)
            {
                Invoke("DhiaDamage", 1f);

                timerFlag = true;

                if (dhia.defenseFlag)
                {
                    encountSys.windowsMes.text = "ふくろうのこうげき！ディアに" + (dhiaDamage * 0.5f) + "のダメージ!";
                    dhia.hp -= (dhiaDamage * 0.5f);
                }
                else
                {
                    encountSys.windowsMes.text = "ふくろうのこうげき！ディアに" + (dhiaDamage) + "のダメージ!";
                    dhia.hp -= (dhiaDamage);
                }
                encountSys.HpMoveWait("Dhia");
            }
        }

        //スキル2
        if (skilRnd >= 1000)
        {

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
    int DamageCalculation(int attack, int defense)
    {
        //シード値の変更
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        //素のダメージ計算
        int damage = ((attack + (attack * (int)powerValue)) / 2) - (defense / 4);

        //ダメージ振幅の計算
        int width = damage / 16 + 1;

        //ダメージ振幅値を加味した計算
        damage = UnityEngine.Random.Range(damage - width, damage + width);

        //呼び出し側にダメージ数を返す
        return damage;
    }

}