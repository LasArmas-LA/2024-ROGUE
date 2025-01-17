using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneSys : MonoBehaviour
{
    //フェード用のイメージ
    [SerializeField]
    Image fade = null;

    //階層データ保存用
    [SerializeField]
    GameObject floorNoSys = null;
    FloorNoSys floorNoSysScript = null;

    //BGMとSEの音量調節用のスライダー
    [SerializeField]
    Slider bgmVolObj = null;
    [SerializeField]
    Slider seVolObj = null;

    //オーディオソース
    [SerializeField]
    AudioSource[] audioSources = null;

    //オーディオクリップ
    [SerializeField]
    AudioClip[] audioClip = null;
    bool fast = true;

    void Start()
    {
        Init();
    }

    void Init()
    {
        if (GameObject.Find("FloorNo") == null)
        {
            //実体化
            GameObject floorNoSysClone = Instantiate(floorNoSys);

            //名前の変更
            floorNoSysClone.name = "FloorNo";

            DontDestroyOnLoad(floorNoSysClone);
        }

        floorNoSysScript = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();

        //Audioの初期化
        bgmVolObj.maxValue = 1;
        bgmVolObj.minValue = 0;
        bgmVolObj.value = 0.5f;
        seVolObj.maxValue = 1;
        seVolObj.minValue = 0;
        seVolObj.value = 0.5f;
    }


    void Update()
    {
        KeyIn();
    }

    void KeyIn()
    {
        if (Input.anyKeyDown && fast && !Input.GetKey(KeyCode.Escape))
        {
            fast = false;
            OnStratButton();
        }
    }

    public void OnStratButton()
    {
        //1秒待ってから関数を呼び出し
        Invoke("LoadScene", 1.0f);
    }

    public void OnEndButton()
    {
        //ビルドデータかエディターモードの判別
#if UNITY_EDITOR
        //ゲームプレイ終了
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //ゲームプレイ終了
            Application.Quit();//ゲームプレイ終了
#endif
    }

    void LoadScene()
    {
        //ロードシーンの読み込み
        SceneManager.LoadScene("LoadScene");
    }
}
