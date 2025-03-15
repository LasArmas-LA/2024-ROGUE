using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    enum MapMode
    {
        CameraMove,
        Wait,
        End
    }
    //実体化
    MapMode mapMode = MapMode.CameraMove;

    [Space(10), Header("ボタン")]

    [SerializeField]
    Outline[] button = null;
    [SerializeField]
    Image[] buttonImage = null;

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

    //選べるマス、選べないマスを制御する処理
    int[] limitlessNo = null;


    [Space(10), Header("マウス操作")]
    [SerializeField]
    float scrollSpeed = 1;


    [Space(10),Header("カメラ")]
    //メインカメラのゲームオブジェクト
    [SerializeField]
    GameObject mainCamera;
    //カメラY軸移動の最大座標
    [SerializeField]
    int cameraYMoveMax = 0;
    //カメラY軸移動の最小座標
    [SerializeField]
    int cameraYMoveMin = 0;

    [Space(10), Header("階層データ")]

    [SerializeField]
    GameObject floorNoSysObj = null;
    [SerializeField]
    FloorNoSys floorNoSys = null;

    [SerializeField]
    GameObject floorNoSysObjClone = null;

    [Space(10), Header("フェード")]

    //フェード用
    [SerializeField]
    Animator fadeAnim = null;

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

        floorNoSys.floorCo++;

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

        //一旦ボタンを全て押せなくする処理
        for (int i = 0; i < buttonPos.Length; i++)
        {
            cloneButton[i].interactable = false;
            cloneButton[i].GetComponent<Image>().color = Color.clear;
        }

        //進める場所を格納
        {
            if (floorNoSys.slectButtonNo == -1)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 0;
            }
            if (floorNoSys.slectButtonNo == 0)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 1;
                limitlessNo[1] = 2;
            }
            if (floorNoSys.slectButtonNo == 1)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 3;
                limitlessNo[1] = 4;
            }
            if (floorNoSys.slectButtonNo == 2)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 5;
            }
            if (floorNoSys.slectButtonNo == 3)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 6;
            }
            if (floorNoSys.slectButtonNo == 4)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 6;
            }
            if (floorNoSys.slectButtonNo == 5)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 7;
            }
            if (floorNoSys.slectButtonNo == 6)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 8;
            }
            if (floorNoSys.slectButtonNo == 7)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 9;
            }
            if (floorNoSys.slectButtonNo == 8)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 10;
                limitlessNo[1] = 11;
            }
            if (floorNoSys.slectButtonNo == 9)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 11;
                limitlessNo[1] = 12;
            }
            if (floorNoSys.slectButtonNo == 10)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 13;
                limitlessNo[1] = 14;
            }
            if (floorNoSys.slectButtonNo == 11)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 14;
                limitlessNo[1] = 15;
            }
            if (floorNoSys.slectButtonNo == 12)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 15;
                limitlessNo[1] = 16;
            }
            if (floorNoSys.slectButtonNo == 13)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 17;
            }
            if (floorNoSys.slectButtonNo == 14)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 17;
            }
            if (floorNoSys.slectButtonNo == 15)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 18;
            }
            if (floorNoSys.slectButtonNo == 16)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 18;
            }
            if (floorNoSys.slectButtonNo == 17)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 19;
            }
            if (floorNoSys.slectButtonNo == 18)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 19;
            }
            if (floorNoSys.slectButtonNo == 19)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 20;
            }
        }


        //次に進めるボタンを押せるようにする処理
        for (int k = 0; k < limitlessNo.Length; k++)
        {
            cloneButton[limitlessNo[k]].interactable = true;
            cloneButton[limitlessNo[k]].GetComponent<Image>().color = Color.yellow;
        }


    }

    void Update()
    {
        switch(mapMode)
        {
            case MapMode.CameraMove:
                CameraMove();
                break;
            case MapMode.Wait:
                MouseScroll();
                break;
            case MapMode.End:
                break;
        }
    }

    float cameraSpeed = 100f;
    void CameraMove()
    {
        if(mainCamera.transform.position.y <= cloneButtonObj[limitlessNo[0]].transform.position.y && mainCamera.transform.position.y < 500)
        {
            Vector3 cameraPos = mainCamera.transform.position;
            cameraPos.y += cameraSpeed * Time.deltaTime;
            mainCamera.transform.position = cameraPos;

            cameraSpeed += 350f * Time.deltaTime;
        }
        else 
        {
            mapMode = MapMode.Wait;
        }
    }

    void MouseScroll()
    {
        //マウススクロール
        if (mainCamera.transform.position.y >= cameraYMoveMin && mainCamera.transform.position.y <= cameraYMoveMax)
        {
            var scroll = Input.mouseScrollDelta.y;
            mainCamera.transform.position -= -mainCamera.transform.up * scroll * scrollSpeed;
        }

        Vector3 cameraPos = mainCamera.transform.position;

        //移動できる範囲を制限
        if (mainCamera.transform.position.y <= cameraYMoveMin)
        {
            cameraPos.y = cameraYMoveMin;
        }
        if (mainCamera.transform.position.y >= cameraYMoveMax)
        {
            cameraPos.y = cameraYMoveMax;
        }

        mainCamera.transform.position = cameraPos;
    }

    //どのボタンが押されたかの判別
    public void ButtonChecker(int buttonNo)
    {
        //選ばれたボタンの番号をDontDestroyOnLoadオブジェクトの変数に格納
        Debug.Log(buttonNo);
        floorNoSys.slectButtonNo = buttonNo;

        //一旦ボタンを全て押せなくする処理
        for (int i = 0; i < buttonPos.Length; i++)
        {
            cloneButton[i].interactable = false;
        }

        //進める場所を格納
        {
            if (floorNoSys.slectButtonNo == -1)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 0;
            }
            if (floorNoSys.slectButtonNo == 0)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 1;
                limitlessNo[1] = 2;
            }
            if (floorNoSys.slectButtonNo == 1)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 3;
                limitlessNo[1] = 4;
            }
            if (floorNoSys.slectButtonNo == 2)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 5;
            }
            if (floorNoSys.slectButtonNo == 3)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 6;
            }
            if (floorNoSys.slectButtonNo == 4)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 6;
            }
            if (floorNoSys.slectButtonNo == 5)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 7;
            }
            if (floorNoSys.slectButtonNo == 6)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 8;
            }
            if (floorNoSys.slectButtonNo == 7)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 9;
            }
            if (floorNoSys.slectButtonNo == 8)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 10;
                limitlessNo[1] = 11;
            }
            if (floorNoSys.slectButtonNo == 9)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 11;
                limitlessNo[1] = 12;
            }
            if (floorNoSys.slectButtonNo == 10)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 13;
                limitlessNo[1] = 14;
            }
            if (floorNoSys.slectButtonNo == 11)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 14;
                limitlessNo[1] = 15;
            }
            if (floorNoSys.slectButtonNo == 12)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 15;
                limitlessNo[1] = 16;
            }
            if (floorNoSys.slectButtonNo == 13)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 17;
            }
            if (floorNoSys.slectButtonNo == 14)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 17;
            }
            if (floorNoSys.slectButtonNo == 15)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 18;
            }
            if (floorNoSys.slectButtonNo == 16)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 18;
            }
            if (floorNoSys.slectButtonNo == 17)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 19;
            }
            if (floorNoSys.slectButtonNo == 18)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 19;
            }
            if (floorNoSys.slectButtonNo == 19)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 20;
            }
        }
            //次に進めるボタンを押せるようにする処理
            for (int k = 0; k < limitlessNo.Length; k++)
            {
                cloneButton[limitlessNo[k]].interactable = true;
            }

    }

    //分岐用
    String sceneName = null;
    //シーンの切り替え
    public void SceneChenge (int sceneKindsNo)
    {
        //敵
        if (sceneKindsNo == 0)
        {
            sceneName = ("EncountScene");
        }
        //イベント
        if(sceneKindsNo == 1)
        {
            sceneName = ("Event");
        }
        //休憩
        if (sceneKindsNo == 2)
        {
            sceneName = ("Stay");
        }
        //宝
        if (sceneKindsNo == 3)
        {
            sceneName = ("Treasure");
        }
        //ボス
        if (sceneKindsNo == 4)
        {
            sceneName = ("Boss");
        }

        Invoke("SceneCheger", 1.0f);
    }

    void SceneCheger()
    {
        SceneManager.LoadScene(sceneName);
    }
}
