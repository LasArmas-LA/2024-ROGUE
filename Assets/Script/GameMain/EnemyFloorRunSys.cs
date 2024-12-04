using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static BaseEquipment;

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
    TextMeshProUGUI[] slectText;

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

    [SerializeField]
    EquipmentManager equipmentManager = null;

    [SerializeField]
    Status dhiaStatus = null;


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
        equipmentManager.Start();
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
                    slectText[0].text = equipmentManager.randomEquip[equipmentManager.rnd1].equipmentName + "\nATK :" + equipmentManager.randomEquip[equipmentManager.rnd1].ATK;
                    slectText[1].text = equipmentManager.randomEquip[equipmentManager.rnd2].equipmentName + "\nATK :" + equipmentManager.randomEquip[equipmentManager.rnd2].ATK;
                    slectText[2].text = equipmentManager.randomEquip[equipmentManager.rnd3].equipmentName + "\nATK :" + equipmentManager.randomEquip[equipmentManager.rnd3].ATK;

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

        if(partsSlect[0])
        {
            //âEéË
            if(equipmentManager.randomEquip[equipmentManager.rnd1].equipmentType == EquipmentType.RightHand)
            {
                dhiaStatus.righthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd1];
            }
            //ç∂éË
            if (equipmentManager.randomEquip[equipmentManager.rnd1].equipmentType == EquipmentType.LeftHand)
            {
                dhiaStatus.lefthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd1];
            }
            //ë´
            if (equipmentManager.randomEquip[equipmentManager.rnd1].equipmentType == EquipmentType.Feet)
            {
                dhiaStatus.legPartsData = equipmentManager.randomEquip[equipmentManager.rnd1];
            }
            //ëÃ
            if (equipmentManager.randomEquip[equipmentManager.rnd1].equipmentType == EquipmentType.Body)
            {
                dhiaStatus.bodyPartsData = equipmentManager.randomEquip[equipmentManager.rnd1];
            }
            //ì™
            if (equipmentManager.randomEquip[equipmentManager.rnd1].equipmentType == EquipmentType.Head)
            {
                dhiaStatus.headPartsData = equipmentManager.randomEquip[equipmentManager.rnd1];
            }
        }
        if (partsSlect[1])
        {
            //âEéË
            if (equipmentManager.randomEquip[equipmentManager.rnd2].equipmentType == EquipmentType.RightHand)
            {
                dhiaStatus.righthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd2];
            }
            //ç∂éË
            if (equipmentManager.randomEquip[equipmentManager.rnd2].equipmentType == EquipmentType.LeftHand)
            {
                dhiaStatus.lefthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd2];
            }
            //ë´
            if (equipmentManager.randomEquip[equipmentManager.rnd2].equipmentType == EquipmentType.Feet)
            {
                dhiaStatus.legPartsData = equipmentManager.randomEquip[equipmentManager.rnd2];
            }
            //ëÃ
            if (equipmentManager.randomEquip[equipmentManager.rnd2].equipmentType == EquipmentType.Body)
            {
                dhiaStatus.bodyPartsData = equipmentManager.randomEquip[equipmentManager.rnd2];
            }
            //ì™
            if (equipmentManager.randomEquip[equipmentManager.rnd2].equipmentType == EquipmentType.Head)
            {
                dhiaStatus.headPartsData = equipmentManager.randomEquip[equipmentManager.rnd2];
            }
        }
        if (partsSlect[2])
        {
            //âEéË
            if (equipmentManager.randomEquip[equipmentManager.rnd3].equipmentType == EquipmentType.RightHand)
            {
                dhiaStatus.righthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd3];
            }
            //ç∂éË
            if (equipmentManager.randomEquip[equipmentManager.rnd3].equipmentType == EquipmentType.LeftHand)
            {
                dhiaStatus.lefthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd3];
            }
            //ë´
            if (equipmentManager.randomEquip[equipmentManager.rnd3].equipmentType == EquipmentType.Feet)
            {
                dhiaStatus.legPartsData = equipmentManager.randomEquip[equipmentManager.rnd3];
            }
            //ëÃ
            if (equipmentManager.randomEquip[equipmentManager.rnd3].equipmentType == EquipmentType.Body)
            {
                dhiaStatus.bodyPartsData = equipmentManager.randomEquip[equipmentManager.rnd3];
            }
            //ì™
            if (equipmentManager.randomEquip[equipmentManager.rnd3].equipmentType == EquipmentType.Head)
            {
                dhiaStatus.headPartsData = equipmentManager.randomEquip[equipmentManager.rnd3];
            }
        }
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
