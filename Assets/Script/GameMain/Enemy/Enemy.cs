using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TestEncount;


public class EnemyManager : MonoBehaviour
{

    [NonSerialized]
    public float[] maxhp = new float [2];
    [NonSerialized]
    public float[] maxmp = new float[2];

    public float[] hp = new float[2];
    [NonSerialized]
    public float[] mp = new float[2];
    [NonSerialized]
    public int[] power = new int[2];
    [NonSerialized]
    public int[] def = new int[2];

    public bool deathFlag = false;
    public bool[] deathLook = new bool[2];
    int liveNumber = 0;

    float timer = 0f;

    [SerializeField]
    GameObject enemyMain;

    [SerializeField]
    GameObject[] enemyObj;

    [SerializeField]
    public TestEncount encountSys = null;

    [SerializeField, Tooltip("敵の体力ゲージ")]
    public Slider[] enemySlider;

    [SerializeField]
    Rabbit[] rabbitScript;

    [SerializeField]
    Bird[] birdScript;


    int rnd = 0;
    void Start()
    {   
        Init();
    }


    bool fast = true;
    public float[] enemyHpDef = null;
    void Init()
    {
        if (this.gameObject.name == "EnemyMain")
        {
            enemyObj[0].transform.localScale = Vector3.zero;
            enemyObj[1].transform.localScale = Vector3.zero;
            enemyObj[2].transform.localScale = Vector3.zero;
            enemyObj[3].transform.localScale = Vector3.zero;

            deathFlag = false;

            //敵出現数確認用
            liveNumber = encountSys.numberRnd + 1;

            Debug.Log("親初期化");
            //エネミーの親オブジェクトの初期化
            enemyMain.transform.localScale = new Vector3(1, 1, 1);

            //出現したエネミーの判別
            switch (encountSys.typeRnd[0])
            {
                //うさぎ
                case 0:
                    rabbitScript[0].InitRabbit();
                    maxhp[0] = rabbitScript[0].rabbitMaxhp;
                    maxmp[0] = rabbitScript[0].rabbitMaxmp;
                    power[0] = rabbitScript[0].rabbitPower;
                    def[0] = rabbitScript[0].rabbitDef;
                    hp[0] = maxhp[0];
                    mp[0] = maxmp[0];

                    enemyObj[0].transform.localScale = new Vector3(1, 1, 1);
                    enemyObj[1].transform.localScale = Vector3.zero;
                    break;
                //ふくろう
                case 1:
                    birdScript[0].InitBird();
                    maxhp[0] = birdScript[0].birdMaxhp;
                    maxmp[0] = birdScript[0].birdMaxmp;
                    power[0] = birdScript[0].birdPower;
                    def[0] = birdScript[0].birdDef;
                    hp[0] = maxhp[0];
                    mp[0] = maxmp[0];

                    enemyObj[0].transform.localScale = Vector3.zero;
                    enemyObj[1].transform.localScale = new Vector3(1, 1, 1);

                    break;
                //
            }
            if (encountSys.numberRnd == 1)
            {
                Debug.Log("2体目出現");
                //出現したエネミーの判別
                switch (encountSys.typeRnd[1])
                {
                    //うさぎ
                    case 3:
                        Debug.Log("2体目うさぎ");
                        rabbitScript[1].InitRabbit();
                        maxhp[1] = rabbitScript[1].rabbitMaxhp;
                        maxmp[1] = rabbitScript[1].rabbitMaxmp;
                        power[1] = rabbitScript[1].rabbitPower;
                        def[1] = rabbitScript[1].rabbitDef;
                        hp[1] = maxhp[1];
                        mp[1] = maxmp[1];

                        enemyObj[2].transform.localScale = new Vector3(1, 1, 1);
                        enemyObj[3].transform.localScale = Vector3.zero;
                        break;
                    //ふくろう
                    case 4:
                        Debug.Log("2体目ふくろう");
                        birdScript[1].InitBird();
                        maxhp[1] = birdScript[1].birdMaxhp;
                        maxmp[1] = birdScript[1].birdMaxmp;
                        power[1] = birdScript[1].birdPower;
                        def[1] = birdScript[1].birdDef;
                        hp[1] = maxhp[1];
                        mp[1] = maxmp[1];

                        enemyObj[2].transform.localScale = Vector3.zero;
                        enemyObj[3].transform.localScale = new Vector3(1, 1, 1);

                        break;
                        //
                }
            }
            //Enemy1HPバーの初期化
            enemySlider[0].maxValue = maxhp[0];
            enemySlider[0].minValue = 0;
            enemySlider[0].value = enemySlider[0].maxValue;
            enemySlider[0].value *= (hp[0] / maxhp[0]);

            enemyHpDef[0] = hp[0];

            //Enemy2HPバーの初期化
            enemySlider[1].maxValue = maxhp[1];
            enemySlider[1].minValue = 0;
            enemySlider[1].value = enemySlider[1].maxValue;
            enemySlider[1].value *= (hp[1] / maxhp[1]);

            enemyHpDef[1] = hp[1];
        }
    }

    public bool death = false;
    void Update()
    {
        //敵1のHPが削られた時
        if (enemyHpDef[0] > hp[0])
        {
            //Debug.Log("敵1を攻撃した！");
            //一気に削って値が0以下になった時の処理
            if (hp[0] <= 0)
            {
                hp[0] = 0;
                enemySlider[0].value -= (maxhp[0] * Time.deltaTime);
            }
            else
            {
                enemySlider[0].value -= ((enemySlider[0].maxValue * (hp[0] / maxhp[0])) * Time.deltaTime);

                if (enemySlider[0].value <= hp[0])
                {
                    enemyHpDef[0] = hp[0];
                    enemySlider[0].value = hp[0];
                }
            }
        }
        //敵2のHPが削られた時
        if (enemyHpDef[1] > hp[1])
        {
            //一気に削って値が0以下になった時の処理
            if (hp[1] <= 0)
            {
                Debug.Log("敵2を攻撃した！1");

                hp[1] = 0;
                enemySlider[1].value -= (maxhp[1] * Time.deltaTime);
            }
            else
            {
                Debug.Log("敵2を攻撃した！2");

                enemySlider[1].value -= ((enemySlider[1].maxValue * (hp[1] / maxhp[1])) * Time.deltaTime);

                if (enemySlider[1].value <= hp[1])
                {
                    enemyHpDef[1] = hp[1];
                    enemySlider[1].value = hp[1];
                }
            }
        }

        //エネミー死亡時の処理
        if (hp[0] <= 0 && !deathLook[0])
        {
            deathLook[0] = true;
            liveNumber -= 1;

            //敵1がうさぎの時の死亡アニメーション
            if (encountSys.typeRnd[0] == 0)
            {
                rabbitScript[0].rabbitAnim.SetBool("Destroy", true);
            }
            //敵1が鳥の時の死亡アニメーション
            if (encountSys.typeRnd[0] == 1)
            {
                birdScript[0].birdAnim.SetBool("Eb_Destroy", true);
            }

            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                if (enemyMain.transform.localScale.x >= 0)
                {
                    enemyMain.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime;
                }

                timer = 0f;           
            }
        }
        if (encountSys.numberRnd == 1 && !deathLook[1])
        {
            //エネミー死亡時の処理
            if (hp[1] <= 0)
            {
                deathLook[1] = true;
                liveNumber -= 1;

                //敵2がうさぎの時の死亡アニメーション
                if (encountSys.typeRnd[1] == 3)
                {
                    rabbitScript[1].rabbitAnim.SetBool("Destroy", true);
                }
                //敵2が鳥の時の死亡アニメーション
                if (encountSys.typeRnd[1] == 4)
                {
                    birdScript[1].birdAnim.SetBool("Eb_Destroy", true);
                }

                timer += Time.deltaTime;
                if (timer >= 1f)
                {
                    if (enemyMain.transform.localScale.x >= 0)
                    {
                        enemyMain.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime;
                    }
                    timer = 0f;
                }
            }
        }

        if (liveNumber == 0)
        {
            deathFlag = true;
            encountSys.mainTurn = MainTurn.END;
        }
    }

    public void Move()
    {
        if (encountSys.mainTurn == TestEncount.MainTurn.ENEMY1MOVE)
        {
            switch (encountSys.typeRnd[0])
            {
                //うさぎ
                case 0:
                    rabbitScript[0].SkilRabbit();
                    break;
                //ふくろう
                case 1:
                    birdScript[0].SkilBird();
                    break;
            }
        }
        if (encountSys.mainTurn == TestEncount.MainTurn.ENEMY2MOVE)
        {
            switch (encountSys.typeRnd[1])
            {
                //うさぎ
                case 3:
                    rabbitScript[1].SkilRabbit();
                    break;
                //ふくろう
                case 4:
                    birdScript[1].SkilBird();
                    break;
            }
        }
    }

    public virtual void SkilBird(){ return; }
    public virtual void SkilRabbit(){ return; }
    public virtual void InitBird(){ return; }
    public virtual void InitRabbit(){ return; }
}
