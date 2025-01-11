using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyManager : MonoBehaviour
{

    public float[] maxhp = null;
    public float[] maxmp = null;

    public float[] hp = null;
    public float[] mp = null;
    public int[] power = null;
    public int[] def = null;

    public bool deathFlag = false;

    float timer = 0f;

    [SerializeField]
    GameObject enemyMain = null;

    [SerializeField]
    GameObject[] enemyObj = null;

    [SerializeField]
    public TestEncount encountSys = null;

    [SerializeField, Tooltip("敵の体力ゲージ")]
    public Slider[] enemySlider = null;

    [SerializeField]
    Rabbit[] rabbitScript = null;

    [SerializeField]
    Bird[] birdScript = null;



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
                    maxhp = birdScript[0].maxhp;
                    maxmp = birdScript[0].maxmp;
                    power = birdScript[0].power;
                    def = birdScript[0].def;
                    hp = maxhp;
                    mp = maxmp;

                    enemyObj[0].transform.localScale = Vector3.zero;
                    enemyObj[1].transform.localScale = new Vector3(1, 1, 1);

                    break;
                //
            }
            if (encountSys.numberRnd == 1)
            {
                //出現したエネミーの判別
                switch (encountSys.typeRnd[1])
                {
                    //うさぎ
                    case 0:
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
                    case 1:
                        birdScript[1].InitBird();
                        maxhp = birdScript[1].maxhp;
                        maxmp = birdScript[1].maxmp;
                        power = birdScript[1].power;
                        def = birdScript[1].def;
                        hp = maxhp;
                        mp = maxmp;

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

        //敵のHPが削られた時
        if (enemyHpDef[0] > hp[0])
        {
            Debug.Log("敵を攻撃した！");
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



        //エネミー死亡時の処理
        if (hp[0] <= 0)
        {
            //うさぎの死亡アニメーション
            if (encountSys.typeRnd[0] == 0)
            {
                rabbitScript[0].rabbitAnim.SetBool("Destroy", true);
            }
            //鳥の死亡アニメーション
            if (encountSys.typeRnd[0] == 1)
            {
                birdScript[0].birdAnim.SetBool("Eb_Destroy", true);
            }


            timer += Time.deltaTime;
            if(timer >= 1f)
            {
                if (enemyMain.transform.localScale.x >= 0)
                {
                    enemyMain.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime;
                }

                deathFlag = true;
            }
        }
    }

    public void Move()
    {

        switch (encountSys.typeRnd[0])
        {
            //うさぎ
            case 0:
                rabbitScript[0].SkilRabbit();
                break;
            //ふくろう
            case 1:
                birdScript[1].SkilBird();
                break;
            case 2:
                break;
            //
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
        }
    }

    public virtual void SkilBird(){ return; }
    public virtual void SkilRabbit(){ return; }
    public virtual void InitBird(){ return; }
    public virtual void InitRabbit(){ return; }
}
