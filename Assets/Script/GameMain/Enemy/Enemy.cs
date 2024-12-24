using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyManager : MonoBehaviour
{

    public float maxhp = 0;
    public float maxmp = 0;

    public float hp = 0;
    public float mp = 0;
    public float power = 0;

    public bool deathFlag = false;

    float timer = 0f;

    [SerializeField]
    GameObject enemyMain = null;

    [SerializeField]
    public EnemyManager[] enemy = null;

    [SerializeField]
    public TestEncount encountSys = null;

    [SerializeField, Tooltip("敵の体力ゲージ")]
    public Slider enemySlider = null;


    int rnd = 0;
    void Start()
    {   
        Init();
    }


    bool fast = true;
    void Init()
    {
        if (this.gameObject.name == "Enemy")
        {
            enemy[0].transform.localScale = Vector3.zero;
            enemy[1].transform.localScale = Vector3.zero;
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
                    hp = maxhp;
                    mp = maxmp;
                    break;
                //ふくろう
                case 1:
                    Debug.Log("ふくろう");
                    enemy[1].InitBird();
                    maxhp = enemy[1].maxhp;
                    maxmp = enemy[1].maxmp;
                    power = enemy[1].power;
                    hp = maxhp;
                    mp = maxmp;
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
        }
    }

    void Update()
    {
        enemySlider.value = (enemySlider.maxValue * (hp / maxhp));

        //エネミー死亡時の処理
        if(hp <= 0)
        {
            if (enemyMain.transform.localScale.x >= 0)
            {
                enemyMain.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime;
            }

            timer += Time.deltaTime;
            if(timer >= 1f)
            {
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
                enemy[0].Skil();
                Skil();
                break;
            //ふくろう
            case 1:
                enemy[1].Skil();
                break;
            case 2:
                Skil();
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

    public virtual void Skil(){ return; }
    public virtual void InitBird(){ return; }
    public virtual void InitRabbit(){ return; }
}
