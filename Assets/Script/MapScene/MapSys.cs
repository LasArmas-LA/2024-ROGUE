using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSys : MonoBehaviour
{
    enum eMapMode
    {
        Stay,
        Disp,
        MoveWait,
        End
    }

    
    [Header("生成するボタンの座標を指定してください"),SerializeField]
    private Vector3[] buttonPos = null;

    [Header("マップのスクロールスピードを指定してください"), SerializeField]
    float scrollSpeed = 1;


    [Space(20)]

    [Tooltip("クローン生成されたボタンオブジェクトを格納するキャンバスを指定してください"), SerializeField]
    private Transform buttonCanvas = null;

    [Tooltip("ギズモ表示用のマップアイコンを指定してください"), SerializeField]
    private Texture mapTexture = null;

    [Tooltip("ボタンのゲームオブジェクトを指定してください"), SerializeField]
    private List<ButtonList> buttonNoList = new List<ButtonList>();

    [SerializeField]
    int buttonListNo;

    [Tooltip("クローン生成されたボタンのオブジェクトが格納されています")]
    private GameObject[] cloneButtonObj = null;
    [Tooltip("クローンされたボタンオブジェクトのボタンコンポーネントが格納されています")]
    private Button[] cloneButton = null;

    [Tooltip("メインカメラを指定してください"),SerializeField]
    GameObject mainCamera;
    [Tooltip("カメラY軸のの最大移動座標を指定してください"), SerializeField]
    int cameraYMoveMax = 0;
    [Tooltip("カメラY軸のの最小移動座標を指定してください"), SerializeField]
    int cameraYMoveMin = 0;


    [System.Serializable]
    public class ButtonList
    {
        public List<GameObject> buttonObjList = new List<GameObject>();
    }

    void Start()
    {
        Init();
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Init()
    {
        //配列のサイズを拡張
        Array.Resize(ref cloneButtonObj, buttonPos.Length);
        Array.Resize(ref cloneButton, buttonPos.Length);

        InitDisp();
        InitLimited();
    }

    /// <summary>
    /// マップの表示初期化処理
    /// </summary>
    private void InitDisp()
    {
        //ボタンを生成する処理
        for (int i = 0; i < buttonPos.Length; i++)
        {
            //指定のキャンバス内にボタンをクローンさせ配列に格納
            cloneButtonObj[i] = Instantiate(buttonNoList[buttonListNo].buttonObjList[i], buttonPos[i], Quaternion.identity, buttonCanvas);

            //わかりやすいように名前を1,2,3,4…のように変更
            cloneButtonObj[i].name = (i).ToString();

            cloneButton[i] = cloneButtonObj[i].GetComponent<Button>();

            int ii = i + 0;

            //ボタンクリック時のイベントを関数と戻り値の設定
            cloneButton[i].onClick.AddListener(() => ButtonChecker((ii)));
        }
    }

    //仮
    FloorNoSys floorNoSys;
    int[] limitlessNo;
    int slectButtonNo;

    /// <summary>
    /// マップで次に進める場所の制限をする処理
    /// </summary>
    private void InitLimited()
    {
        slectButtonNo = PlayerPrefs.GetInt("FloorNo", -1);

        //一旦ボタンを全て押せなくする処理
        for (int i = 0; i < buttonPos.Length; i++)
        {
            cloneButton[i].interactable = false;
        }

        #region
        if (slectButtonNo == -1)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 0;
        }
        if (slectButtonNo == 0)
        {
            limitlessNo = new int[2];

            limitlessNo[0] = 1;
            limitlessNo[1] = 2;
        }
        if (slectButtonNo == 1)
        {
            limitlessNo = new int[2];

            limitlessNo[0] = 3;
            limitlessNo[1] = 4;
        }
        if (slectButtonNo == 2)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 5;
        }
        if (slectButtonNo == 3)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 6;
        }
        if (slectButtonNo == 4)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 6;
        }
        if (slectButtonNo == 5)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 7;
        }
        if (slectButtonNo == 6)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 8;
        }
        if (slectButtonNo == 7)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 9;
        }
        if (slectButtonNo == 8)
        {
            limitlessNo = new int[2];

            limitlessNo[0] = 10;
            limitlessNo[1] = 11;
        }
        if (slectButtonNo == 9)
        {
            limitlessNo = new int[2];

            limitlessNo[0] = 11;
            limitlessNo[1] = 12;
        }
        if (slectButtonNo == 10)
        {
            limitlessNo = new int[2];

            limitlessNo[0] = 13;
            limitlessNo[1] = 14;
        }
        if (slectButtonNo == 11)
        {
            limitlessNo = new int[2];

            limitlessNo[0] = 14;
            limitlessNo[1] = 15;
        }
        if (slectButtonNo == 12)
        {
            limitlessNo = new int[2];

            limitlessNo[0] = 15;
            limitlessNo[1] = 16;
        }
        if (slectButtonNo == 13)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 17;
        }
        if (slectButtonNo == 14)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 17;
        }
        if (slectButtonNo == 15)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 18;
        }
        if (slectButtonNo == 16)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 18;
        }
        if (slectButtonNo == 17)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 19;
        }
        if (slectButtonNo == 18)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 19;
        }
        if (slectButtonNo == 19)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 20;
        }
        #endregion

        //次に進めるボタンを押せるようにする処理
        for (int k = 0; k < limitlessNo.Length; ++k)    
        {
            cloneButton[limitlessNo[k]].interactable = true;
        }
    }

    /// <summary>
    /// ボタンが押された時に呼ばれます
    /// </summary>
    /// <param name="buttonNo">ボタンごとに設定された識別番号</param>
    private void ButtonChecker(int buttonNo)
    {
        PlayerPrefs.SetInt("FloorNo", buttonNo);

        

        //一旦ボタンを全て押せなくする処理
        for (int i = 0; i < buttonPos.Length; i++)
        {
            cloneButton[i].interactable = false;
        }

    }

    void Update()
    {
        MouseScroll();
    }

    /// <summary>
    /// マップのマウススクロール処理
    /// </summary>
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


    private void OnDrawGizmos()
    {
        for(int i = 0; i < buttonPos.Length;++i)
        {
            Gizmos.DrawGUITexture(new Rect(buttonPos[i].x-2, buttonPos[i].y+2, 6, -6), mapTexture);
        }
    }
}
