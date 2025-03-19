using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverSys : MonoBehaviour
{
    FloorNoSys floorNoSysScript = null;
    [SerializeField]
    TextMeshProUGUI hiFloor = null;
    [SerializeField]
    TextMeshProUGUI nowFloor = null;

    [SerializeField]
    Status ririStatus = null;
    [SerializeField]
    Status dhiaStatus = null;

    [SerializeField]
    GameObject floorNoSysObj = null;

    void Start()
    {
        Init();
    }

    void Init()
    {
        //�����̏�����
        InitFind();

        //�n�C�X�R�A�K�w�̃`�F�b�N
        InitFloorNoCheck();

        InitStatus();

        InitFloorNo();
    }

    void InitFind()
    {
        if (GameObject.Find("FloorNo") == null)
        {
            //���̉�
            GameObject floorNoSysClone = Instantiate(floorNoSysObj);

            //���O�̕ύX
            floorNoSysClone.name = "FloorNo";

            DontDestroyOnLoad(floorNoSysClone);
        }

        floorNoSysScript = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();
    }

    void InitFloorNoCheck()
    {
        //�n�C�X�R�A�K�w�X�V��
        if (floorNoSysScript.hiFloorNo > floorNoSysScript.floorNo)
        {
            floorNoSysScript.hiFloorNo = floorNoSysScript.floorNo;
        }
        else
        {

        }

        hiFloor.text = floorNoSysScript.hiFloorNo.ToString();
        nowFloor.text = floorNoSysScript.floorNo.ToString();
    }

    void InitStatus()
    {
        ririStatus.HP = ririStatus.MAXHP;
        dhiaStatus.HP = dhiaStatus.MAXHP;
    }

    void InitFloorNo()
    {
        floorNoSysScript.floorCo = 0;
        floorNoSysScript.slectButtonNo = -1;
    }

    public void TitleButtonSlect()
    {
        SceneManager.LoadScene("Title");
    }
}
