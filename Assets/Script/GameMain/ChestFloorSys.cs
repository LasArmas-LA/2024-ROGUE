using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChestFloorSys : MonoBehaviour
{
    public Camera maincamera = null;

    bool runStratFlag = false;
    bool chestEndFlag = false;
    bool floorEndFlag = false;
    [SerializeField]
    Image fade = null;

    //休憩階のフラグ
    public bool restFlag = false;


    [SerializeField]
    Vector3 cameraMoveSpeed = Vector3.zero;

    [SerializeField]
    GameObject chestObj = null;

    FloorNoSys floorNoSys = null;
    GameObject floorNoSysObj = null;

    public TextMeshProUGUI windowMes = null;

    [SerializeField]
    GameObject goalObj = null;

    void Start()
    {
        Init();
    }

    void Init()
    {
        windowMes.text = "探索中";
        floorNoSysObj = GameObject.Find("FloorNo");
        floorNoSys = floorNoSysObj.GetComponent<FloorNoSys>();

        //休憩フロアフラグオン
        if (floorNoSys.floorNo % 5 == 0 && floorNoSys.floorNo != 0)
        {
            restFlag = true;
        }
        else
        {
            restFlag = false;
        }
    }
    void Update()
    {
        KeyIn();
        if (runStratFlag)
        {
            if(maincamera.transform.position.x >= chestObj.transform.position.x - 5)
            {
                windowMes.text = "宝箱を見つけた！\n 中にはパーツが入っていた！";
                chestObj.SetActive(false);
                runStratFlag = false;
                StartCoroutine(ChestWait());
            }
            else
            {
                maincamera.transform.position += cameraMoveSpeed * Time.deltaTime;
            }
        }
        if (chestEndFlag)
        {
            if (restFlag)
            {
                if (maincamera.transform.position.x <= 20)
                {
                    windowMes.text = "探索中";
                    maincamera.transform.position += cameraMoveSpeed * Time.deltaTime;
                }
                else
                {
                    windowMes.text = "休憩中";
                    StartCoroutine(RestStay());
                }
            }
            else
            {
                if (maincamera.transform.position.x <= goalObj.transform.position.x + 5)
                {
                    windowMes.text = "探索中";
                    maincamera.transform.position += cameraMoveSpeed * Time.deltaTime;
                }
                else
                {
                    windowMes.text = "扉を見つけた！ \n次の階に進もう";
                    StartCoroutine(FloorEnd());
                }
            }
        }

        //フェードアウト処理
        if(floorEndFlag)
        {
            Invoke("LoadScene", 1.0f);
            floorEndFlag = false;
        }
    }

    void KeyIn()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !chestEndFlag)
        {
            runStratFlag = true;
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("LoadScene");
    }

    IEnumerator ChestWait()
    {
        yield return new WaitForSeconds(1.5f);
        chestEndFlag = true;
    }
    IEnumerator FloorEnd()
    {
        yield return new WaitForSeconds(1.0f);
        if(chestEndFlag)
        {
            floorNoSys.floorNo += 1;
        }
        chestEndFlag = false;
        floorEndFlag = true;
    }

    IEnumerator RestStay()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        restFlag = false;
    }

}
