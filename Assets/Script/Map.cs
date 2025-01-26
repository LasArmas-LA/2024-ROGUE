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
        //オブジェクトの重複チェック
        if (GameObject.Find("FloorNo") == null)
        {
            //存在しなければ生成してDontDestroyOnLoadで保存
            floorNoSysObjClone = Instantiate(floorNoSysObj);
            DontDestroyOnLoad(floorNoSysObjClone);

            //クローンしたオブジェクトの名前を変更
            floorNoSysObjClone.name = "FloorNo";
        }
        floorNoSys = floorNoSysObjClone.GetComponent<FloorNoSys>();

        //ボタンを生成する処理
        for (int i = 0; i < buttonPos.Length; i++)
        {
            //指定のキャンバス内にボタンをクローンさせ配列に格納
            cloneButtonObj[i] = Instantiate(buttonObj[i], buttonPos[i],Quaternion.identity, backCanvas);
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
        MouseScroll();
        ButtonColorChenge();
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
        floorNoSys.slectButtonNo = buttonNo;
    }

    //シーンの切り替え
    public void SceneChenge (int sceneKindsNo)
    {
        //敵
        if (sceneKindsNo == 0)
        {
            //SceneManager.LoadScene("R_EncountFloorScene Old");
        }
        //イベント
        if(sceneKindsNo == 1)
        {
            //SceneManager.LoadScene("Event");
        }
        //休憩
        if (sceneKindsNo == 2)
        {
           // SceneManager.LoadScene("Stay");
        }
        //宝
        if (sceneKindsNo == 3)
        {
            //SceneManager.LoadScene("Treasure");
        }
        //ボス
        if (sceneKindsNo == 4)
        {
           // SceneManager.LoadScene("Boss");
        }
    }

    public void ButtonControl(int buttonNo)
    {
        for (int i = 0; i <= button.Length - 1; i++)
        {
            button[i].effectColor = Color.white;
            button[i].effectDistance = new Vector2(0, 0);
        }

        if (buttonNo == 0)
        {
            button[0].effectColor = Color.yellow;
            button[0].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 1)
        {
            button[0].effectColor = Color.red;
            button[0].effectDistance = new Vector2(10, 10);


            button[1].effectColor = Color.yellow;
            button[1].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 2)
        {
            button[0].effectColor = Color.red;
            button[0].effectDistance = new Vector2(10, 10);

            button[2].effectColor = Color.yellow;
            button[2].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 3)
        {
            button[1].effectColor = Color.red;
            button[1].effectDistance = new Vector2(10, 10);


            button[3].effectColor = Color.yellow;
            button[3].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 4)
        {
            button[1].effectColor = Color.red;
            button[2].effectColor = Color.red;
            button[1].effectDistance = new Vector2(10, 10);
            button[2].effectDistance = new Vector2(10, 10);


            button[4].effectColor = Color.yellow;
            button[4].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 5)
        {
            button[3].effectColor = Color.red;
            button[3].effectDistance = new Vector2(10, 10);


            button[5].effectColor = Color.yellow;
            button[5].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 6)
        {
            button[3].effectColor = Color.red;
            button[4].effectColor = Color.red;

            button[3].effectDistance = new Vector2(10, 10);
            button[4].effectDistance = new Vector2(10, 10);


            button[6].effectColor = Color.yellow;
            button[6].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 7)
        {
            button[4].effectColor = Color.red;
            button[4].effectDistance = new Vector2(10, 10);


            button[7].effectColor = Color.yellow;
            button[7].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 8)
        {
            button[5].effectColor = Color.red;
            button[6].effectColor = Color.red;
            button[5].effectDistance = new Vector2(10, 10);
            button[6].effectDistance = new Vector2(10, 10);


            button[8].effectColor = Color.yellow;
            button[8].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 9)
        {
            button[6].effectColor = Color.red;
            button[7].effectColor = Color.red;
            button[6].effectDistance = new Vector2(10, 10);
            button[7].effectDistance = new Vector2(10, 10);


            button[9].effectColor = Color.yellow;
            button[9].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 10)
        {
            button[7].effectColor = Color.red;
            button[7].effectDistance = new Vector2(10, 10);


            button[10].effectColor = Color.yellow;
            button[10].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 11)
        {
            button[8].effectColor = Color.red;
            button[8].effectDistance = new Vector2(10, 10);

        
            button[11].effectColor = Color.yellow;
            button[11].effectDistance = new Vector2(10, 10); 
        }
        if (buttonNo == 12)
        {
            button[8].effectColor = Color.red;
            button[9].effectColor = Color.red;
            button[8].effectDistance = new Vector2(10, 10);
            button[9].effectDistance = new Vector2(10, 10);


            button[12].effectColor = Color.yellow;
            button[12].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 13)
        {
            button[9].effectColor = Color.red;
            button[10].effectColor = Color.red;
            button[9].effectDistance = new Vector2(10, 10);
            button[10].effectDistance = new Vector2(10, 10);


            button[13].effectColor = Color.yellow;
            button[13].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 14)
        {
            button[10].effectColor = Color.red;
            button[10].effectDistance = new Vector2(10, 10);


            button[14].effectColor = Color.yellow;
            button[14].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 15)
        {
            button[11].effectColor = Color.red;
            button[12].effectColor = Color.red;
            button[11].effectDistance = new Vector2(10, 10);
            button[12].effectDistance = new Vector2(10, 10);


            button[15].effectColor = Color.yellow;
            button[15].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 16)
        {
            button[13].effectColor = Color.red;
            button[14].effectColor = Color.red;
            button[13].effectDistance = new Vector2(10, 10);
            button[14].effectDistance = new Vector2(10, 10);


            button[16].effectColor = Color.yellow;
            button[16].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 17)
        {
            button[15].effectColor = Color.red;
            button[15].effectDistance = new Vector2(10, 10);


            button[17].effectColor = Color.yellow;
            button[17].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 18)
        {
            button[15].effectColor = Color.red;
            button[16].effectColor = Color.red;
            button[15].effectDistance = new Vector2(10, 10);
            button[16].effectDistance = new Vector2(10, 10);


            button[18].effectColor = Color.yellow;
            button[18].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 19)
        {
            button[16].effectColor = Color.red;
            button[16].effectDistance = new Vector2(10, 10);


            button[19].effectColor = Color.yellow;
            button[19].effectDistance = new Vector2(10, 10);
        }
    }
}
