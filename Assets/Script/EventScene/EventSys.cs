using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using static BaseEquipment;
using System.ComponentModel;

public class EventSys : MonoBehaviour
{
    //イベントの種類管理用
    public enum EventKinds
    {
        //入力弾く用
        WAIT,
        //死体漁り
        CADAVER,
        //再度死体漁り用
        RECADAVER,
        //荒らされた階
        DIRTY,
        //リリー疲れた
        TIRED,
        //イベント終了処理
        END

    }

    //↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓パーツ用

    //どのドロップしたパーツを選択しているかの確認用
    [SerializeField, Header("パーツ管理用")]
    bool[] partsSlect;

    //パーツ選択時の表示切り替え用
    [SerializeField]
    GameObject[] partsObj = null;
    [SerializeField]
    Image[] partsImage = null;
    [SerializeField]
    Sprite slectOnSp = null;
    [SerializeField]
    Sprite slectOffSp = null;
    [SerializeField]
    GameObject[] arrowObj = null;

    //パーツの名前
    string[] partsName = { "RightHand", "LeftHand", "Head", "Body", "Feet" };

    //ドロップしたパーツの情報表示用
    [SerializeField]
    TextMeshProUGUI[] slectText;

    //ドロップしたパーツの画像表示用
    [SerializeField]
    Image[] dropPartsSp = null;

    //現在装備しているパーツの情報表示用
    [SerializeField]
    TextMeshProUGUI[] slectNowText;

    //パーツを選択後確定させた時の判断
    bool allPartsSlect;

    //パーツを選択するウィンドウ
    [SerializeField]
    GameObject partsSlectWin = null;

    //装備をランダムで入手するロジック組みのシステム
    [SerializeField, Header("ドロップ装備ランダム化システム")]
    EquipmentManager equipmentManager = null;

    //ディアのステータス
    [SerializeField, Header("ディアのステータス管理用")]
    Status dhiaStatus = null;
    //リリーのステータス
    [SerializeField, Header("リリーのステータス管理用")]
    Status ririStatus = null;

    bool partsButton = false;

    //↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑パーツ用


    public EventKinds eventKinds;

    int slectNo = 0;
    bool button = false;

    [SerializeField]
    [NamedArrayAttribute(new string[] {"メイン","ボタン1", "ボタン2", "ボタン3", "ボタン4"})]
    TextMeshProUGUI[] buttonText = null;

    [SerializeField]
    [NamedArrayAttribute(new string[] {"ボタン1", "ボタン2", "ボタン3", "ボタン4" })]
    GameObject[] buttonObj = null;

    [SerializeField, Header("階層データ管理用システム")]
    GameObject floorNoSysObj = null;

    //フェードアニメーション用
    [SerializeField]
    Animator fadeAnim = null;

    void Awake()
    {

    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        //パーツのランダムドロップ
        equipmentManager.LoopInit();

        //検索の初期化
        InitFind();

        //イベントの初期化
        InitEvent();
    }

    void InitFind()
    {
        if (GameObject.Find("FloorNo") == null)
        {
            GameObject floorNoSys = Instantiate(floorNoSysObj);
            DontDestroyOnLoad(floorNoSys);

            floorNoSys.name = "FloorNo";
        }
    }

    void InitEvent()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        int rnd = UnityEngine.Random.Range(1, 4);

        switch (rnd)
        {
            case 1:
                eventKinds = EventKinds.CADAVER;
                buttonText[0].text = "機能停止している機械があるみたい…\nどうしようか…";
                buttonText[1].text = "漁る (装備を獲得、00%で戦闘)";
                buttonText[2].text = "なにもしない";

                //不要なボタン削除
                buttonObj[2].SetActive(false);
                buttonObj[3].SetActive(false);
                break;
            case 2:
                eventKinds = EventKinds.DIRTY;
                buttonText[0].text = "やけに物が散らかっている…\n何かないか探してみようか…？";
                buttonText[1].text = "探す (HP減少、装備獲得)";
                buttonText[2].text = "探さない";

                //不要なボタン削除
                buttonObj[2].SetActive(false);
                buttonObj[3].SetActive(false);
                break;
            case 3:
                eventKinds = EventKinds.TIRED;
                buttonText[0].text = "「ねぇディア少し休憩にしない？私疲れちゃった」…\nリリーは疲れているようだ";
                buttonText[1].text = "休憩 (2人のHPを50%回復)";
                buttonText[2].text = "武器を強化(装備を1つ選んでレベルアップ)";

                //不要なボタン削除
                buttonObj[2].SetActive(false);
                buttonObj[3].SetActive(false);
                break;
        }
    }

    void Update()
    {
        switch(eventKinds)
        {
            case EventKinds.CADAVER:
                Cadaver();
                break;
            case EventKinds.RECADAVER:
                ReCadaver();
                break;
            case EventKinds.DIRTY:
                Dirty();
                break;
            case EventKinds.TIRED:
                Tired();
                break;
            case EventKinds.END:
                Invoke("SceneEnd", 1f);
                fadeAnim.SetBool("FadeIn", true);
                break;
        }
    }

    void Drop()
    {
        //初期化処理
        partsImage[0].sprite = slectOffSp;
        partsImage[1].sprite = slectOffSp;
        partsImage[2].sprite = slectOffSp;
        partsObj[0].transform.localScale = new Vector3(1f, 1f, 1f);
        partsObj[1].transform.localScale = new Vector3(1f, 1f, 1f);
        partsObj[2].transform.localScale = new Vector3(1f, 1f, 1f);

        arrowObj[0].SetActive(false);
        arrowObj[1].SetActive(false);
        arrowObj[2].SetActive(false);

        partsSlect[0] = false;
        partsSlect[1] = false;
        partsSlect[2] = false;
        partsButton = false;

        //パーツセレクトのウィンドウ表示
        partsSlectWin.SetActive(true);

        //ドロップ装備の表示処理
        slectText[0].text = equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentName;
        slectText[1].text = equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentName;
        slectText[2].text = equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentName;

        //ドロップ装備の画像表示処理
        dropPartsSp[0].sprite = equipmentManager.randomEquip[equipmentManager.rnd[0]].sprite;
        dropPartsSp[1].sprite = equipmentManager.randomEquip[equipmentManager.rnd[1]].sprite;
        dropPartsSp[2].sprite = equipmentManager.randomEquip[equipmentManager.rnd[2]].sprite;
    }


    public void PartsSlect1()
    {
        if (!partsButton)
        {
            partsImage[0].sprite = slectOnSp;
            partsImage[1].sprite = slectOffSp;
            partsImage[2].sprite = slectOffSp;
            partsObj[0].transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
            partsObj[1].transform.localScale = new Vector3(1f, 1f, 1f);
            partsObj[2].transform.localScale = new Vector3(1f, 1f, 1f);
            arrowObj[0].SetActive(true);
            arrowObj[1].SetActive(false);
            arrowObj[2].SetActive(false);

            partsSlect[0] = true;
            partsSlect[1] = false;
            partsSlect[2] = false;
        }
    }
    public void PartsSlect2()
    {
        if (!partsButton)
        {
            partsImage[0].sprite = slectOffSp;
            partsImage[1].sprite = slectOnSp;
            partsImage[2].sprite = slectOffSp;
            partsObj[0].transform.localScale = new Vector3(1f, 1f, 1f);
            partsObj[1].transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
            partsObj[2].transform.localScale = new Vector3(1f, 1f, 1f);
            arrowObj[0].SetActive(false);
            arrowObj[1].SetActive(true);
            arrowObj[2].SetActive(false);

            partsSlect[0] = false;
            partsSlect[1] = true;
            partsSlect[2] = false;
        }
    }
    public void PartsSlect3()
    {
        if (!partsButton)
        {
            partsImage[0].sprite = slectOffSp;
            partsImage[1].sprite = slectOffSp;
            partsImage[2].sprite = slectOnSp;
            partsObj[0].transform.localScale = new Vector3(1f, 1f, 1f);
            partsObj[1].transform.localScale = new Vector3(1f, 1f, 1f);
            partsObj[2].transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
            arrowObj[0].SetActive(false);
            arrowObj[1].SetActive(false);
            arrowObj[2].SetActive(true);

            partsSlect[0] = false;
            partsSlect[1] = false;
            partsSlect[2] = true;
        }
    }

    public void PartsSlecteEnd()
    {
        partsButton = true;
        allPartsSlect = true;
        partsSlectWin.SetActive(false);

        //該当する部位にパーツデータを格納する処理
        if (partsSlect[0])
        {
            //右手
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.RightHand)
            {
                dhiaStatus.righthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
            //左手
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.LeftHand)
            {
                dhiaStatus.lefthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
            //足
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.Feet)
            {
                dhiaStatus.legPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
            //体
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.Body)
            {
                dhiaStatus.bodyPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
            //頭
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.Head)
            {
                dhiaStatus.headPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
        }
        if (partsSlect[1])
        {
            //右手
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.RightHand)
            {
                dhiaStatus.righthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
            //左手
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.LeftHand)
            {
                dhiaStatus.lefthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
            //足
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.Feet)
            {
                dhiaStatus.legPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
            //体
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.Body)
            {
                dhiaStatus.bodyPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
            //頭
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.Head)
            {
                dhiaStatus.headPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
        }
        if (partsSlect[2])
        {
            //右手
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.RightHand)
            {
                dhiaStatus.righthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
            //左手
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.LeftHand)
            {
                dhiaStatus.lefthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
            //足
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.Feet)
            {
                dhiaStatus.legPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
            //体
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.Body)
            {
                dhiaStatus.bodyPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
            //頭
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.Head)
            {
                dhiaStatus.headPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
        }
    }


    float timer = 0f;
    int battleNo = 30;
    int partsNo = 70;
    bool fast = true;
    int rnd = 0;

    //死体漁り
    void Cadaver()
    {
        if (button)
        {
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                if (slectNo == 0)
                {
                    UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
                    rnd = UnityEngine.Random.Range(1, 100);

                    //戦闘
                    if (rnd <= battleNo)
                    {
                        buttonText[0].text = "機械が動き出した！";
                        SceneManager.LoadScene("EncountScene");

                    }
                    //装備獲得
                    if (rnd > partsNo)
                    {
                        Invoke("Drop", 1.0f);

                        buttonText[0].text = "使えそうな装備があった！まだ探す？";

                        buttonText[1].text = "はい";
                        buttonText[2].text = "いいえ";

                        battleNo += 10;
                        partsNo -= 10;

                        //そのまま流れないように初期化
                        slectNo = 100;

                        timer = 0f;

                        eventKinds = EventKinds.RECADAVER;
                        button = false;
                    }
                }
                if(slectNo == 1)
                {
                    buttonText[0].text = "何もしないことにした！";
                    button = false;
                    eventKinds = EventKinds.END;
                }
            }
        }
    }

    //死体再漁り
    void ReCadaver()
    {
        if (button)
        {
            timer += Time.deltaTime;

            if (fast)
            {
                UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
                rnd = UnityEngine.Random.Range(1, 100);
                fast = false;
            }

            if (timer >= 1)
            {
                if (slectNo == 0)
                {
                    //戦闘
                    if (rnd <= battleNo)
                    {
                        buttonText[0].text = "機械が動き出した！";
                        SceneManager.LoadScene("EncountScene");
                        button = false;
                    }
                    //装備獲得
                    if (rnd > partsNo)
                    {
                        equipmentManager.LoopInit();
                        Invoke("Drop", 1.0f);

                        buttonText[0].text = "使えそうな装備があった！まだ探す？";

                        battleNo += 10;
                        partsNo -= 10;

                        slectNo = 100;

                        timer = 0f;
                        button = false;
                        fast = true;
                    }
                }
                if (slectNo == 1)
                {
                    buttonText[0].text = "そろそろやめておこうかな…";
                    button = false;
                    timer = 0f;
                    eventKinds = EventKinds.END;
                }
            }
        }
    }


    //荒らされた階
    void Dirty()
    {
        if (button)
        {
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                if(slectNo == 0)
                {
                    //3割減
                    ririStatus.HP -= (ririStatus.MAXHP * 0.3f);
                    dhiaStatus.HP -= (dhiaStatus.MAXHP * 0.3f);

                    equipmentManager.LoopInit();
                    Invoke("Drop", 1.0f);

                    buttonText[0].text = "いたっ！物にぶつかってケガしてしまった！\nしかし装備を見つけた！";
                }
                if (slectNo == 1)
                {
                    buttonText[0].text = "物をよけて先に進んだ";
                }
                eventKinds = EventKinds.END;
            }
        }
    }

    //リリー疲れた
    void Tired()
    {
        if (button)
        {
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                buttonText[0].text = "「だいぶ休憩できたわ！ありがとうディア！」";

                if (slectNo == 0)
                {
                    //HPを50%回復
                    //リリーの回復量がマックスHPを超えてしまう時
                    if(ririStatus.HP + ririStatus.MAXHP * 0.5f >= ririStatus.MAXHP)
                    {
                        ririStatus.HP = ririStatus.MAXHP;
                    }
                    //リリーのMaxHPを超えない時
                    else
                    {
                        ririStatus.HP += (ririStatus.MAXHP * 0.5f);
                    }

                    //ディアの回復量がマックスHPを超えてしまう時
                    if (dhiaStatus.HP + dhiaStatus.MAXHP * 0.5f >= dhiaStatus.MAXHP)
                    {
                        dhiaStatus.HP = dhiaStatus.MAXHP;
                    }
                    //ディアのMaxHPを超えない時
                    else
                    {
                        dhiaStatus.HP += (dhiaStatus.MAXHP * 0.5f);
                    }
                }
                if (slectNo == 1)
                {
                    //装備を1つ選んでレベルアップ

                }
                eventKinds = EventKinds.END;
            }
        }
    }

    void SceneEnd()
    {
        SceneManager.LoadScene("LoadScene");
    }


    public void Button1Slect(int number)
    {
        if (!button)
        {
            slectNo = number;
            button = true;
        }
    }
}
