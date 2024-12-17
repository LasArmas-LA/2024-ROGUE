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

    //�x�e�K�̃t���O
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
        windowMes.text = "�T����";
        floorNoSysObj = GameObject.Find("FloorNo");
        floorNoSys = floorNoSysObj.GetComponent<FloorNoSys>();

        //�x�e�t���A�t���O�I��
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
                windowMes.text = "�󔠂��������I\n ���ɂ̓p�[�c�������Ă����I";
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
                    windowMes.text = "�T����";
                    maincamera.transform.position += cameraMoveSpeed * Time.deltaTime;
                }
                else
                {
                    windowMes.text = "�x�e��";
                    StartCoroutine(RestStay());
                }
            }
            else
            {
                if (maincamera.transform.position.x <= goalObj.transform.position.x + 5)
                {
                    windowMes.text = "�T����";
                    maincamera.transform.position += cameraMoveSpeed * Time.deltaTime;
                }
                else
                {
                    windowMes.text = "�����������I \n���̊K�ɐi����";
                    StartCoroutine(FloorEnd());
                }
            }
        }

        //�t�F�[�h�A�E�g����
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
