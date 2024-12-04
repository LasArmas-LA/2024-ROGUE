using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyFloorRunSys : MonoBehaviour
{
    public Camera maincamera = null;

    bool runStratFlag = false;
    public bool battleEndFlag = false;
    bool floorEndFlag = false;
    [SerializeField]
    Image fade = null;
    bool fast = false;
    bool button = false;

    [SerializeField]
    bool[] partsSlect;

    [SerializeField]
    bool allPartsSlect;

    [SerializeField]
    GameObject partsSlectWin = null;

    [SerializeField]
    Vector3 cameraMoveSpeed = Vector3.zero;

    [SerializeField]
    GameObject chestObj = null;

    FloorNoSys floorNoSys = null;
    GameObject floorNoSysObj = null;

    [SerializeField]
    EncountSys encountSys = null;

    public TextMeshProUGUI windowMes = null;

    void Start()
    {
        Init();
    }

    void Init()
    {
        windowMes.text = "íTçıíÜ";
        floorNoSysObj = GameObject.Find("FloorNo");
        floorNoSys = floorNoSysObj.GetComponent<FloorNoSys>();
    }
    void Update()
    {
        KeyIn();
        if (runStratFlag)
        {
            if (maincamera.transform.position.x >= 150)
            {
                if(!fast)
                {
                    encountSys.RiriMove();
                    fast = true;
                    runStratFlag = false;
                }
                //StartCoroutine(ChestWait());
            }
            else
            {
                maincamera.transform.position += cameraMoveSpeed * Time.deltaTime;
            }
        }
        if (battleEndFlag)
        {
            if (encountSys.restFlag)
            {
                if (maincamera.transform.position.x <= 200)
                {
                    windowMes.text = "íTçıíÜ";
                    maincamera.transform.position += cameraMoveSpeed * Time.deltaTime;
                }
                else
                {
                    windowMes.text = "ãxåeíÜ";
                    StartCoroutine(RestStay());
                }
            }
            else
            {
                if (!allPartsSlect)
                {
                    partsSlectWin.SetActive(true);
                }
                else
                {
                    if (maincamera.transform.position.x <= 300)
                    {
                        windowMes.text = "íTçıíÜ";
                        maincamera.transform.position += cameraMoveSpeed * Time.deltaTime;
                    }
                    else
                    {
                        windowMes.text = "î‡Çå©Ç¬ÇØÇΩÅI \néüÇÃäKÇ…êiÇ‡Ç§";
                        StartCoroutine(FloorEnd());
                    }
                }
            }
        }

        //ÉtÉFÅ[ÉhÉAÉEÉgèàóù
        if (floorEndFlag)
        {
            Invoke("LoadScene", 1.0f);
            if (fade != null)
            {

                floorEndFlag = false;
            }
        }
    }

    void KeyIn()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !battleEndFlag)
        {
            runStratFlag = true;
        }
    }

    public void PartsSlect1()
    {
        if(!button)
        {
            partsSlect[0] = true;
            partsSlect[1] = false;
            partsSlect[2] = false;
        }
    }
    public void PartsSlect2()
    {
        if (!button)
        {
            partsSlect[0] = false;
            partsSlect[1] = true;
            partsSlect[2] = false;
        }
    }
    public void PartsSlect3()
    {
        if (!button)
        {
            partsSlect[0] = false;
            partsSlect[1] = false;
            partsSlect[2] = true;
        }
    }

    public void PartsSlecteEnd()
    {
        button = true;
        allPartsSlect = true;
        partsSlectWin.SetActive(false);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("LoadScene");
    }

    IEnumerator FloorEnd()
    {
        yield return new WaitForSeconds(1.0f);
        if (battleEndFlag)
        {
            floorNoSys.floorNo += 1;
        }
        battleEndFlag = false;
        floorEndFlag = true;
    }
    IEnumerator RestStay()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        encountSys.restFlag = false;
    }
}
