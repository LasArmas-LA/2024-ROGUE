using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyManager : MonoBehaviour
{

    public float maxhp = 0;
    public float maxmp = 0;

    public float hp = 0;
    public float mp = 0;
    public int power = 0;
    public int def = 0;

    public bool deathFlag = false;

    float timer = 0f;

    [SerializeField]
    GameObject enemyMain = null;

    [SerializeField]
    public EnemyManager[] enemy = null;
    [SerializeField]
    GameObject[] enemyObj = null;

    [SerializeField]
    public TestEncount encountSys = null;

    [SerializeField, Tooltip("敵の体力ゲージ")]
    public Slider enemySlider = null;

    [SerializeField]
    Bird birdScript = null;
    [SerializeField]
    Rabbit rabbitScript = null;



    int rnd = 0;
    void Start()
    {   
        Init();
    }


    bool fast = true;
    public float enemyHpDef = 0;
    void Init()
    {
        if (this.gameObject.name == "Enemy")
        {
            enemy[0].transform.localScale = Vector3.zero;
            enemy[1].transform.localScale = Vector3.zero;
            deathFlag = false;
            Debug.Log("親初期化");
            //エネミーの親オブジェクトの初期化
            enemyMain.transform.localScale = new Vector3(1, 1, 1);

            //出現したエネミーの判別
            switch (encountSys.rnd)
            {
                //うさぎ
                case 0:
                    Debug.Log("うさぎ");
                    enemy[0].InitRabbit();
                    maxhp = enemy[0].maxhp;
                    maxmp = enemy[0].maxmp;
                    power = enemy[0].power;
                    def = enemy[0].def;
                    hp = maxhp;
                    mp = maxmp;

                    enemyObj[0].transform.localScale = new Vector3(1, 1, 1);
                    enemyObj[1].transform.localScale = Vector3.zero;
                    break;
                //ふくろう
                case 1:
                    Debug.Log("ふくろう");
                    enemy[1].InitBird();
                    maxhp = enemy[1].maxhp;
                    maxmp = enemy[1].maxmp;
                    power = enemy[1].power;
                    def = enemy[1].def;
                    hp = maxhp;
                    mp = maxmp;

                    enemyObj[0].transform.localScale = Vector3.zero;
                    enemyObj[1].transform.localScale = new Vector3(1, 1, 1);

                    break;
                //
                case 2:
                    break;
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
            //HPバーの初期化
            enemySlider.maxValue = maxhp;
            enemySlider.minValue = 0;
            enemySlider.value = enemySlider.maxValue;
            enemySlider.value *= (hp / maxhp);

            enemyHpDef = hp;
        }
    }

    public bool death = false;
    void Update()
    {

        //敵のHPが削られた時
        if (enemyHpDef > hp)
        {
            Debug.Log("敵を攻撃した！");
            //一気に削って値が0以下になった時の処理
            if (hp <= 0)
            {
                hp = 0;
                enemySlider.value -= (maxhp * Time.deltaTime);
            }

            else
            {
                enemySlider.value -= ((enemySlider.maxValue * (hp / maxhp)) * Time.deltaTime);

                if (enemySlider.value <= hp)
                {
                    enemyHpDef = hp;
                    enemySlider.value = hp;
                }
            }
        }



        //エネミー死亡時の処理
        if (hp <= 0)
        {   
            //うさぎの死亡アニメーション
            if(encountSys.rnd == 0)
            {
                rabbitScript.rabbitAnim.SetBool("Destroy", true);
            }
            //鳥の死亡アニメーション
            if(encountSys.rnd == 1)
            {
                birdScript.birdAnim.SetBool("Eb_Destroy", true);
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

        switch (encountSys.rnd)
        {
            //うさぎ
            case 0:
                enemy[0].SkilRabbit();
                break;
            //ふくろう
            case 1:
                enemy[1].SkilBird();
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
