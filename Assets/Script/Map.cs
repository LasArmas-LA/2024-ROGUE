using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField]
    Outline[] button = null;
    [SerializeField]
    Image[] buttonImage = null;

    [SerializeField]
    float scrollSpeed = 1;
    [SerializeField]
    GameObject mainCamera;

    [SerializeField]
    GameObject floorNoSysObj = null;
    [SerializeField]
    FloorNoSys floorNoSys = null;

    //ボタンの位置を保存
    [SerializeField]
    Vector3[] buttonPos = null;

    
    [SerializeField]
    GameObject[] buttonObj = null;

    [SerializeField]
    GameObject[] buttonKinds = null;

    //ボタンを生成するキャンバス
    [SerializeField]
    Transform backCanvas = null;

    //クローンしたボタンを格納
    [SerializeField]
    GameObject[] cloneButtonObj = null;

    [SerializeField]
    Button[] cloneButton = null;

    [SerializeField]
    GameObject floorNoSysObjClone = null;

    void Start()
    {
        Init();
    }

    void Init()
    {
        //オブジェクトの重複チェック
        if (GameObject.Find("FloorNo") == null)
        {
            //存在しなければ生成してDontDestroyOnLoadで保存
            floorNoSysObjClone = Instantiate(floorNoSysObj);
            DontDestroyOnLoad(floorNoSysObjClone);

            //クローンしたオブジェクトの名前を変更
            floorNoSysObjClone.name = "FloorNo";
        }
        
        floorNoSys = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();
        

        //ボタンを生成する処理
        for (int i = 0; i < buttonPos.Length; i++)
        {
            //指定のキャンバス内にボタンをクローンさせ配列に格納
            cloneButtonObj[i] = Instantiate(buttonObj[i], buttonPos[i], Quaternion.identity, backCanvas);

            //わかりやすいように名前を1,2,3,4…のように変更
            cloneButtonObj[i].name = (i + 1).ToString();

            cloneButton[i] = cloneButtonObj[i].GetComponent<Button>();

            int ii = i + 0;

            //ボタンクリック時のイベントを関数と戻り値の設定
            cloneButton[i].onClick.AddListener(() => ButtonChecker((ii)));

            //アウトラインで現在位置の情報を表示するので取得
            button[i] = cloneButtonObj[i].GetComponent<Outline>();
        }

    }

    void Update()
    {
        ButtonColorChenge();
        MouseScroll();
    }

    void MouseScroll()
    {
        //マウススクロール
        if (mainCamera.transform.position.y >= -500 && mainCamera.transform.position.y <= 500)
        {
            var scroll = Input.mouseScrollDelta.y;
            mainCamera.transform.position -= -mainCamera.transform.up * scroll * scrollSpeed;
        }
        if (mainCamera.transform.position.y <= -500)
        {
            mainCamera.transform.position = new Vector3(0, -500, -10);
        }
        if (mainCamera.transform.position.y >= 500)
        {
            mainCamera.transform.position = new Vector3(0, 500, -10);
        }
    }

    void ButtonColorChenge()
    {
        button[floorNoSys.slectButtonNo].effectColor = Color.yellow;
        button[floorNoSys.slectButtonNo].effectDistance = new Vector2(10, 10);
    }

    //どのボタンが押されたかの判別
    public void ButtonChecker(int buttonNo)
    {
        //選ばれたボタンの番号をDontDestroyOnLoadオブジェクトの変数に格納
        Debug.Log(buttonNo);
        floorNoSys.slectButtonNo = buttonNo;
    }

    //シーンの切り替え
    public void SceneChenge (int sceneKindsNo)
    {
        //敵
        if (sceneKindsNo == 0)
        {
            SceneManager.LoadScene("EncountScene");
        }
        //イベント
        if(sceneKindsNo == 1)
        {
            SceneManager.LoadScene("Event");
        }
        //休憩
        if (sceneKindsNo == 2)
        {
            SceneManager.LoadScene("Stay");
        }
        //宝
        if (sceneKindsNo == 3)
        {
            SceneManager.LoadScene("Treasure");
        }
        //ボス
        if (sceneKindsNo == 4)
        {
           SceneManager.LoadScene("Boss");
        }
    }
}
