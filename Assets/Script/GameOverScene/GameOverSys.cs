using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverSys : MonoBehaviour
{
    FloorNoSys floorNoSys = null;
    [SerializeField]
    TextMeshProUGUI hiFloor = null;
    [SerializeField]
    TextMeshProUGUI nowFloor = null;

    void Start()
    {
        Init();
    }

    void Init()
    {
        floorNoSys = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();

        //�n�C�X�R�A�K�w�X�V��
        if(floorNoSys.hiFloorNo > floorNoSys.floorNo)
        {
            floorNoSys.hiFloorNo = floorNoSys.floorNo;
        }
        else
        {

        }

        hiFloor.text = floorNoSys.hiFloorNo.ToString();
        nowFloor.text = floorNoSys.floorNo.ToString();
    }

    void Update()
    {
        
    }

    public void TitleButtonSlect()
    {
        SceneManager.LoadScene("Title");
    }
}
