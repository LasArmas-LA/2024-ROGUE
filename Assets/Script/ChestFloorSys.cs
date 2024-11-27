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
    

    [SerializeField]
    Vector3 cameraMoveSpeed = Vector3.zero;

    [SerializeField]
    GameObject chestObj = null;

    FloorNoSys floorNoSys = null;
    GameObject floorNoSysObj = null;


    public TextMeshProUGUI windowMes = null;

    void Start()
    {
        Init();
    }

    void Init()
    {
        windowMes.text = "�T����";
        floorNoSysObj = GameObject.Find("FloorNo");
        floorNoSys = floorNoSysObj.GetComponent<FloorNoSys>();
    }
    void Update()
    {
        KeyIn();
        if (runStratFlag)
        {
            if(maincamera.transform.position.x >= 15)
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
            if (maincamera.transform.position.x <= 30)
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

        //�t�F�[�h�A�E�g����
        if(floorEndFlag)
        {
            Invoke("LoadScene", 1.0f);
            floorEndFlag = false;
        }
    }

    void KeyIn()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
}
