using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleSceneSys : MonoBehaviour
{
    //フェード用のイメージ
    [SerializeField]
    Image fade = null;

    //階層データ保存用
    [SerializeField]
    GameObject floorNoSys = null;
    FloorNoSys floorNoSysScript = null;

    //MasterとBGMとSEの音量調節用のスライダー
    [SerializeField]
    Slider masterVolObj = null;
    [SerializeField]
    Slider bgmVolObj = null;
    [SerializeField]
    Slider seVolObj = null;

    //オーディオソース
    //MASTER、BGM、SE
    [SerializeField]
    AudioSource[] audioSources = null;

    //オーディオクリップ
    [SerializeField]
    AudioClip[] audioClip = null;

    //オーディオボリュームの表示テキスト用
    [SerializeField]
    TextMeshProUGUI[] audioVolText = null;

    bool fast = true;

    //オプション画面
    [SerializeField]
    GameObject optionMenu = null;
    //ディア画面
    [SerializeField]
    GameObject dhiaMenu = null;

    //ディア画面のステータステキスト表示用
    [SerializeField]
    Status ririStatus = null;
    [SerializeField]
    Status dhiaStatus = null;
    [SerializeField]
    [NamedArrayAttribute(new string[] { "MAXHP", "HP", "ATK", "DEF"})]
    TextMeshProUGUI[] ririStatusText = new TextMeshProUGUI[4];
    [SerializeField]
    [NamedArrayAttribute(new string[] { "MAXHP", "HP", "ATK", "DEF"})]
    TextMeshProUGUI[] dhiaStatusText = new TextMeshProUGUI[4];


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
        masterVolObj.maxValue = 1;
        masterVolObj.minValue = 0;
        bgmVolObj.maxValue = 1;
        bgmVolObj.minValue = 0;
        seVolObj.maxValue = 1;
        seVolObj.minValue = 0;

        //保存されているボリュームを代入
        masterVolObj.value = floorNoSysScript.masterVol;
        bgmVolObj.value = floorNoSysScript.bgmVol;
        seVolObj.value = floorNoSysScript.seVol;
    }


    void Update()
    {
        KeyIn();
        VolChenge();
    }

    void KeyIn()
    {
        if (Input.GetKeyDown(KeyCode.S) && fast)
        {
            fast = false;
            OnStratButton();
        }

        if (Input.GetKeyDown(KeyCode.Delete) && fast)
        {
            PlayerPrefs.DeleteKey("sceneKindsNo");
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //オプション
            optionMenu.SetActive(false);
            //ディア
            dhiaMenu.SetActive(false);

        }
    }

    void VolChenge() 
    {
        audioSources[0].volume = masterVolObj.value;
        audioSources[1].volume = bgmVolObj.value;
        audioSources[2].volume = seVolObj.value;

        audioVolText[0].text = "" + (masterVolObj.value * 100).ToString("F0") + "%";
        audioVolText[1].text = "" + (bgmVolObj.value * 100).ToString("F0") + "%";
        audioVolText[2].text = "" + (seVolObj.value * 100).ToString("F0") + "%";

        floorNoSysScript.masterVol = audioSources[0].volume;
        floorNoSysScript.bgmVol = audioSources[1].volume;
        floorNoSysScript.seVol = audioSources[2].volume;
    }

    void StatusChenge()
    {
        //リリーのステータス表示用
        ririStatusText[0].text = ririStatus.MAXHP.ToString();
        ririStatusText[1].text = ririStatus.HP.ToString();
        ririStatusText[2].text = ririStatus.ATK.ToString();
        ririStatusText[3].text = ririStatus.DEF.ToString();

        //ディアのステータス表示用
        dhiaStatusText[0].text = dhiaStatus.MAXHP.ToString();
        dhiaStatusText[1].text = dhiaStatus.HP.ToString();
        dhiaStatusText[2].text = dhiaStatus.ATK.ToString();
        dhiaStatusText[3].text = dhiaStatus.DEF.ToString();
    }


    //オプション画面用
    public void MenuBackButton()
    {
        optionMenu.SetActive(false);
    }

    public void OptionButton()
    {
        optionMenu.SetActive(true);
    }


    //ディアメニュー表示用
    public void DhiaMenu()
    {
        StatusChenge();

        dhiaMenu.SetActive(true);
    }
    public void BackDhiaMenu()
    {
        dhiaMenu.SetActive(false);
    }


    //アニメーション用
    [SerializeField]
    Animator fadeAnim = null;
    public void OnStratButton()
    {
        fadeAnim.SetBool("FadeOut", true);
        //1秒待ってから関数を呼び出し
        Invoke("LoadScene", 0.8f);
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
