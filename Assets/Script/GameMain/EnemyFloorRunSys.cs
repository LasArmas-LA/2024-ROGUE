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

    string[] partsName = { "RightHand", "LeftHand", "Head", "Body", "Feet" };
    [SerializeField]
    TextMeshProUGUI[] slectText;
    [SerializeField]
    TextMeshProUGUI[] slectNowText;

    [SerializeField]
    bool allPartsSlect;

    bool fastMove = true;

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
        windowMes.text = "探索中";
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
                if (!allPartsSlect)
                {
                    if (fastMove)
                    {
                        slectText[0].text = equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentName + "\nATK :" + equipmentManager.randomEquip[equipmentManager.rnd[0]].ATK;
                        slectText[1].text = equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentName + "\nATK :" + equipmentManager.randomEquip[equipmentManager.rnd[1]].ATK;
                        slectText[2].text = equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentName + "\nATK :" + equipmentManager.randomEquip[equipmentManager.rnd[2]].ATK;

                        for (int i = 0; i < slectNowText.Length;)
                        {
                            //右手
                            if (equipmentManager.randomEquip[equipmentManager.rnd[i]].equipmentType == EquipmentType.RightHand)
                            {
                                if (dhiaStatus.righthandPartsData != null)
                                {
                                    slectNowText[i].text = dhiaStatus.righthandPartsData.equipmentName + "\nATK :" + dhiaStatus.righthandPartsData.ATK;
                                }
                                else
                                {
                                    slectNowText[i].text = "";
                                }
                            }
                            //左手
                            if (equipmentManager.randomEquip[equipmentManager.rnd[i]].equipmentType == EquipmentType.LeftHand)
                            {
                                if (dhiaStatus.lefthandPartsData != null)
                                {
                                    slectNowText[i].text = dhiaStatus.lefthandPartsData.equipmentName + "\nATK :" + dhiaStatus.lefthandPartsData.ATK;
                                }
                                else
                                {
                                    slectNowText[i].text = "";
                                }
                            }
                            //足
                            if (equipmentManager.randomEquip[equipmentManager.rnd[i]].equipmentType == EquipmentType.Feet)
                            {
                                if (dhiaStatus.legPartsData != null)
                                {
                                    slectNowText[i].text = dhiaStatus.legPartsData.equipmentName + "\nATK :" + dhiaStatus.legPartsData.ATK;
                                }
                                else
                                {
                                    slectNowText[i].text = "";
                                }
                            }
                            //体
                            if (equipmentManager.randomEquip[equipmentManager.rnd[i]].equipmentType == EquipmentType.Body)
                            {
                                if (dhiaStatus.bodyPartsData != null)
                                {
                                    slectNowText[i].text = dhiaStatus.bodyPartsData.equipmentName + "\nATK :" + dhiaStatus.bodyPartsData.ATK;
                                }
                                else
                                {
                                    slectNowText[i].text = "";
                                }
                            }
                            //頭
                            if (equipmentManager.randomEquip[equipmentManager.rnd[i]].equipmentType == EquipmentType.Head)
                            {
                                if (dhiaStatus.headPartsData != null)
                                {
                                    slectNowText[i].text = dhiaStatus.headPartsData.equipmentName + "\nATK :" + dhiaStatus.headPartsData.ATK;
                                }
                                else
                                {
                                    slectNowText[i].text = "";
                                }
                            }
                            i++;
                        }
                        fastMove = false;
                    }

                    partsSlectWin.SetActive(true);
                }
                else
                {
                    if (maincamera.transform.position.x <= 300)
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
        }

        //フェードアウト処理
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

        
        if (partsSlect[0])
        {
            //右手
            if(equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.RightHand)
            {
                dhiaStatus.righthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
            //左手
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.LeftHand)
            {
                dhiaStatus.lefthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
            //足
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.Feet)
            {
                dhiaStatus.legPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
            //体
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.Body)
            {
                dhiaStatus.bodyPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
            //頭
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.Head)
            {
                dhiaStatus.headPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
        }
        if (partsSlect[1])
        {
            //右手
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.RightHand)
            {
                dhiaStatus.righthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
            //左手
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.LeftHand)
            {
                dhiaStatus.lefthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
            //足
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.Feet)
            {
                dhiaStatus.legPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
            //体
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.Body)
            {
                dhiaStatus.bodyPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
            //頭
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.Head)
            {
                dhiaStatus.headPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
        }
        if (partsSlect[2])
        {
            //右手
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.RightHand)
            {
                dhiaStatus.righthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
            //左手
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.LeftHand)
            {
                dhiaStatus.lefthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
            //足
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.Feet)
            {
                dhiaStatus.legPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
            //体
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.Body)
            {
                dhiaStatus.bodyPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
            //頭
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.Head)
            {
                dhiaStatus.headPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
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
