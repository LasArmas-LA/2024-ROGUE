using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static BaseEquipment;
using static TestEncount;

public class EnemyFloorRunSys : MonoBehaviour
{
    //メインカメラ
    public Camera maincamera = null;

    //敵の場所まで歩くフラグ
    bool runStratFlag = false;
    //敵を倒して扉まで歩く時のフラグ
    public bool battleEndFlag = false;
    //扉に着いてその階が終了する時のフラグ
    bool floorEndFlag = false;
    //フェードアウト用
    [SerializeField,Header("フェードアウト用")]
    Image fade = null;
    //最初に1回だけ呼び出したい処理
    bool fast = false;
    //ボタンの多段押し防止
    bool button = false;

    [Space(10)]

    //どのドロップしたパーツを選択しているかの確認用
    [SerializeField,Header("パーツ管理用")]
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

    [Space(10)]

    //最初に1回だけ呼び出したい処理
    bool fastMove = true;

    //ゲームオーバーフラグ
    [NonSerialized]
    public bool gameOverFlag = false;


    //コマンドを選択するウィンドウ
    [SerializeField,Header("コマンド管理用")]
    public GameObject commandWin = null;
    [SerializeField]
    public GameObject commandMain = null;

    [Space(10)]
    //カメラの動く速度
    [SerializeField,Header("カメラ管理用")]
    Vector3 characterMoveSpeed = Vector3.zero;

    [Space(10)]

    //階層データ管理システム
    FloorNoSys floorNoSys = null;
    [SerializeField,Header("階層データ管理用システム")]
    GameObject floorNoSysObj = null;

    [Space(10)]

    //エンカウントを管理するシステム
    [SerializeField,Header("エンカウント管理用システム")]
    TestEncount encountSys = null;

    [Space(10)]

    //装備をランダムで入手するロジック組みのシステム
    [SerializeField,Header("ドロップ装備ランダム化システム")]
    EquipmentManager equipmentManager = null;

    [Space(10)]

    //ディアのステータス
    [SerializeField,Header("ディアのステータス管理用")]
    Status dhiaStatus = null;

    [Space(10)]

    [SerializeField,Header("オブジェクト管理用")]
    GameObject enemyObj = null;
    [SerializeField]
    GameObject restObj = null;
    [SerializeField]
    GameObject doorObj = null;

    //各キャラクターのオブジェクト
    [SerializeField]
    GameObject ririObj = null;
    [SerializeField]
    GameObject dhiaObj = null;
    //キャラクターの親オブジェクト
    [SerializeField]
    GameObject characterMainObj = null;


    [Space(10)]
    [SerializeField, Header("スクリプト参照")]
    Riri ririScript = null;
    [SerializeField]
    Dhia dhiaScript = null;

    [Space(10)]

    //アニメーション
    [SerializeField,Header("アニメーション管理用")]
    Animator ririAnim = null;
    [SerializeField]
    Animator dhiaAnim = null;



    public TextMeshProUGUI windowMes = null;

    void Awake()
    {
        Init();
    }

    void Init()
    {

        InitFind();
        InitActive();
        InitAnim();

        //武器の抽選
        equipmentManager.LoopInit();
    }

    void InitFind()
    {
        if (GameObject.Find("FloorNo") == null)
        {
            GameObject floorNoSys = Instantiate(floorNoSysObj);
            DontDestroyOnLoad(floorNoSys);

            floorNoSys.name = "FloorNo";
        }
        floorNoSys = floorNoSysObj.GetComponent<FloorNoSys>();
    }

    void InitActive()
    {
        commandWin.SetActive(false);
        commandMain.SetActive(false);

        //パーツ選択時の初期化処理
        arrowObj[0].SetActive(false);
        arrowObj[1].SetActive(false);
        arrowObj[2].SetActive(false);
    }
    void InitAnim()
    {
        //歩きアニメーションを開始
        ririAnim.SetBool("R_Walk", true);
        dhiaAnim.SetBool("D_Walk", true);
    }

    void Update()
    {
        CharMove();
        //CameraMove();

        //フェードアウト処理
        if (floorEndFlag)
        {
            //Invoke("LoadScene", 1.0f);
            LoadScene();
            if (fade != null)
            {

                floorEndFlag = false;
            }

            if (battleEndFlag)
            {
                //floorNoSys.floorCo += 1;
                Debug.Log("階層アップ");
                battleEndFlag = false;
            }
        }

        if (gameOverFlag)
        {
            GameOver();
        }
    }

    void CameraMove() 
    {
        if (maincamera.transform.position.x <= 227)
        {
            Vector3 cameraPos = maincamera.transform.position;
            cameraPos.x = characterMainObj.transform.position.x + 50;
            maincamera.transform.position = cameraPos;
        }
    }

    bool fastFloorNo = true;
    void CharMove()
    {
        if (encountSys.mainTurn == MainTurn.WAIT)
        {
                //1回だけ呼び出す
                if (!fast)
                {
                    //ステータスの変更
                    encountSys.mainTurn = MainTurn.RIRIMOVE;

                    //歩きアニメーションを停止
                    ririAnim.SetBool("R_Walk", false);
                    dhiaAnim.SetBool("D_Walk", false);

                    //コマンドを表示
                    commandWin.SetActive(true);
                    commandMain.SetActive(true);
                    fast = true;
                    runStratFlag = false;
                }
        }
        if (encountSys.mainTurn == MainTurn.ENDRUN)
        {
            if (encountSys.restFlag)
            {
                if (characterMainObj.transform.position.x <= restObj.transform.position.x + 20)
                {
                    windowMes.text = "探索中";
                    commandWin.SetActive(false);
                    commandMain.SetActive(false);

                    //歩きアニメーションを開始
                    ririAnim.SetBool("R_Walk", true);
                    dhiaAnim.SetBool("D_Walk", true);
                    characterMainObj.transform.position += characterMoveSpeed * Time.deltaTime;
                }
                else
                {
                    windowMes.text = "休憩中";
                    //歩きアニメーションを停止
                    ririAnim.SetBool("R_Walk", false);
                    dhiaAnim.SetBool("D_Walk", false);

                    //HPを回復
                    //ririScript.hp = ririScript.maxhp;
                    //dhiaScript.hp = dhiaScript.maxhp;

                    //コマンドを非表示
                    commandWin.SetActive(false);
                    commandMain.SetActive(false);
                    StartCoroutine(RestStay());
                }
            }
            else
            {
                commandWin.SetActive(false);
                commandMain.SetActive(false);

                if (!allPartsSlect)
                {
                    if (fastMove)
                    {
                        //ドロップ装備の表示処理
                        slectText[0].text = equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentName;
                        slectText[1].text = equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentName;
                        slectText[2].text = equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentName;

                        //ドロップ装備の画像表示処理
                        dropPartsSp[0].sprite = equipmentManager.randomEquip[equipmentManager.rnd[0]].sprite;
                        dropPartsSp[1].sprite = equipmentManager.randomEquip[equipmentManager.rnd[1]].sprite;
                        dropPartsSp[2].sprite = equipmentManager.randomEquip[equipmentManager.rnd[2]].sprite;

                        fastMove = false;
                    }

                    partsSlectWin.SetActive(true);
                }
                //装備が選ばれたら画面外まで移動する処理
                else
                {
                    if (characterMainObj.transform.position.x <= doorObj.transform.position.x + 70)
                    {
                        windowMes.text = "探索中";
                        commandWin.SetActive(false);
                        commandMain.SetActive(false);
                        characterMainObj.transform.position += characterMoveSpeed * Time.deltaTime;
                        //歩きアニメーションを開始
                        dhiaAnim.SetBool("D_Shield", false);
                        ririAnim.SetBool("R_Walk", true);
                        dhiaAnim.SetBool("D_Walk", true);
                    }
                    else
                    {
                        windowMes.text = "扉を見つけた！ \n次の階に進もう";
                        //歩きアニメーションを停止
                        ririAnim.SetBool("R_Walk", false);
                        dhiaAnim.SetBool("D_Walk", false);
                        commandWin.SetActive(false);
                        commandMain.SetActive(false);
                        if (fastFloorNo)
                        {
                            //floorNoSys.floorCo += 1;
                            fastFloorNo = false;
                        }

                        commandMain.SetActive(false);
                        StartCoroutine(FloorEnd());
                    }
                }
            }
        }

    }

    void GameOver()
    {

    }

    public void PartsSlect1()
    {
        if(!button)
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
        if (!button)
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
        if (!button)
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
        if (partsSlect[0] || partsSlect[1] || partsSlect[2])
        {
            button = true;
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
    }

    void LoadScene()
    {
        SceneManager.LoadScene("LoadScene");
    }

    //フェード処理用
    [SerializeField]
    Animator fadeAnim = null;
    //ドアまで到着した時の処理
    IEnumerator FloorEnd()
    {
        fadeAnim.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1.0f);
        floorEndFlag = true;
    }
    IEnumerator RestStay()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        encountSys.restFlag = false;
    }
}
