using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
        TIRED

    }

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


    void Awake()
    {
        if (GameObject.Find("FloorNo") == null)
        {
            GameObject floorNoSys = Instantiate(floorNoSysObj);
            DontDestroyOnLoad(floorNoSys);

            floorNoSys.name = "FloorNo";
        }

    }

    void Start()
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
                        buttonText[0].text = "使えそうな装備があった！まだ探す？";

                        buttonText[1].text = "はい";
                        buttonText[2].text = "いいえ";

                        battleNo += 10;
                        partsNo -= 10;

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
                    eventKinds = EventKinds.WAIT;
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
                    eventKinds = EventKinds.WAIT;
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
                    buttonText[0].text = "いたっ！物にぶつかってケガしてしまった！\nしかし装備を見つけた！";
                }
                if (slectNo == 1)
                {
                    buttonText[0].text = "物をよけて先に進んだ";
                    eventKinds = EventKinds.WAIT;
                }
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

                }
                if(slectNo == 1)
                {
                    //装備を1つ選んでレベルアップ

                }
                eventKinds = EventKinds.WAIT;
            }
        }
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
