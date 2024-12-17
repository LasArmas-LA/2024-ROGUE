using UnityEngine;

public class LobbyMainSys : MonoBehaviour
{
    [SerializeField]
    GameObject playerObj = null;

    bool mouseFlag = false;

    Vector3 mousePos;
    [SerializeField]
    float speed = 0;

    [SerializeField]
    GameObject mousePosSp = null;
    [SerializeField]
    GameObject mainCanvas = null;

    GameObject mouseSp = null;

    void Start()
    {
        
    }

    void Update()
    {
        KeyDown();
        PlayerMove();
    }

    void KeyDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mouseFlag = true;
            mousePos = Input.mousePosition;

            GameObject cloneMousePos = GameObject.Find("CloneMousePos");

            if(cloneMousePos != null)
            {
                Destroy(cloneMousePos);
            }

            // �Q�[���I�u�W�F�N�g�𕡐�
            mouseSp = Instantiate(mousePosSp);

            // GameManager��e�Ɏw��
            mouseSp.transform.parent = mainCanvas.transform;
                                                            
            // �K�v�ɉ����č��W�̒���
            mouseSp.transform.position = mousePos;

            mouseSp.name = "CloneMousePos";
        }
    }

    void PlayerMove()
    {
        //���[���h���W�Ǝ��g�̍��W���r�����[�v
        if(mouseFlag)
        {
            //�w�肵�����W�Ɍ������Ĉړ�
            playerObj.transform.position = Vector3.MoveTowards(playerObj.transform.position, mousePos, speed * Time.deltaTime);
        }
        if(playerObj.transform.position == mousePos)
        {
            mouseFlag= false;
        }
    }

}
