using System;
using UnityEngine;

public class FloorNoSys : MonoBehaviour
{
    //�t���A�̃J�E���g�p
    public int floorCo = 1;

    public float masterVol = 0.5f;
    public float bgmVol = 0.5f;
    public float seVol = 0.5f;

    //�f�B�A�̃X�e�[�^�X�␳�l
    public float dhiaHp = 0;
    public int dhiaAtk = 0;
    public int dhiaDef = 0;

    //����̃t���A�J�E���g�p
    public int floorNo = 0;
    public int hiFloorNo = 0;


    //�I�𒆂̃{�^���̔ԍ�
    public int slectButtonNo = 0;

    [SerializeField]
    Status dhiaStatus = null;

    void Update()
    {
        KeyIn();    
    }

    void KeyIn()
    {
        //���Z�b�g�L�[
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.R))
            {
                dhiaStatus.headPartsData = null;
                dhiaStatus.bodyPartsData = null;
                dhiaStatus.legPartsData = null;
                dhiaStatus.righthandPartsData = null;
                dhiaStatus.lefthandPartsData= null;
            }
        }
    }
}
