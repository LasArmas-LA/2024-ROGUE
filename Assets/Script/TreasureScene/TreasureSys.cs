using TMPro;
using UnityEngine.UI;
using UnityEngine;
using static BaseEquipment;
using UnityEngine.SceneManagement;


public class TreasureSys : MonoBehaviour
{

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

    bool button = false;

    void Start()
    {
        Init();
    }

    void Init()
    {
        equipmentManager.LoopInit();
        //ドロップ品の表示タイミング調節
        Invoke("Drop", 2.5f);

        //宝箱の開閉画像切り替えタイミング調節
        Invoke("OpenTresure", 2f);


        arrowObj[0].SetActive(false);
        arrowObj[1].SetActive(false);
        arrowObj[2].SetActive(false);
    }

    void Update()
    {
        
    }

    //パーツのドロップ表示処理
    void Drop()
    {
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

    //宝箱の素材
    //開いてる宝箱
    [SerializeField]
    GameObject openTresure = null;
    //閉じてる宝箱
    [SerializeField]
    GameObject defTresure = null;


    //宝箱オープン
    void OpenTresure()
    {
        openTresure.SetActive(true);
        defTresure.SetActive(false);
    }


    public void PartsSlect1()
    {
        if (!button)
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

    [SerializeField]
    Animator fadeAnim = null;
    public void PartsSlecteEnd()
    {
        fadeAnim.SetBool("FadeOn", true);

        button = true;
        allPartsSlect = true;
        partsSlectWin.SetActive(false);
        Invoke("SceneChenge", 0.5f);

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
    void SceneChenge()
    {
        SceneManager.LoadScene("Map");
    }

}
