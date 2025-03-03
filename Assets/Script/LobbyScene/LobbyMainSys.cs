using UnityEngine;

public class LobbyMainSys : MonoBehaviour
{
    //�v���C���[�̃I�u�W�F�N�g
    [SerializeField]
    GameObject playerObj = null;

    //�v���C���[�̈ړ����t���O
    bool mouseFlag = false;

    //�}�E�X�̍��N���b�N���ꂽ���̃|�W�V������ۑ��p
    Vector3 mousePos;

    //�ړ��X�s�[�h
    [SerializeField]
    float speed = 0;

    //�ړ���ɕ\������prefab�I�u�W�F�N�g
    [SerializeField]
    GameObject mousePosSp = null;

    //���C���[���v���C���[�I�u�W�F��1���̃L�����o�X
    [SerializeField]
    GameObject mainCanvas = null;

    //�N���[�����ꂽ�}�E�Xprefab���i�[�p
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
        //�}�E�X�̍��N���b�N�̓��͏�
        if(Input.GetMouseButtonDown(0))
        {
            mouseFlag = true;
            //���͂��ꂽ���̃}�E�X�|�W�V������ۑ�
            mousePos = Input.mousePosition;

            //CloneMousePos�Ƃ������O�̃I�u�W�F�N�g������
            GameObject cloneMousePos = GameObject.Find("CloneMousePos");

            //cloneMousePos�����݂��鎞(�G���[���)
            if (cloneMousePos != null)
            {
                //���̃I�u�W�F�N�g������
                Destroy(cloneMousePos);
            }

            // �Q�[���I�u�W�F�N�g�𕡐�
            mouseSp = Instantiate(mousePosSp);

            // GameManager��e�Ɏw��
            mouseSp.transform.parent = mainCanvas.transform;
                                                            
            // �K�v�ɉ����č��W�̒���
            mouseSp.transform.position = mousePos;

            //�N���[�������I�u�W�F�N�g�̖��O��ύX
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
        //�w�肵���ꏊ�܂ł��ǂ蒅������t���O���I�t�ɂ���
        if(playerObj.transform.position == mousePos)
        {
            mouseFlag= false;
        }
    }

}
