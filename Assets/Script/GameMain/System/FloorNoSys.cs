using System;
using UnityEngine;

public class FloorNoSys : MonoBehaviour
{
    //フロアのカウント用
    public int floorCo = 1;

    public float masterVol = 0.5f;
    public float bgmVol = 0.5f;
    public float seVol = 0.5f;

    //ディアのステータス補正値
    public float dhiaHp = 0;
    public int dhiaAtk = 0;
    public int dhiaDef = 0;

    //現状のフロアカウント用
    public int floorNo = 0;
    public int hiFloorNo = 0;


    //選択中のボタンの番号
    public int slectButtonNo = 0;

    [SerializeField]
    Status dhiaStatus = null;

    void Update()
    {
        KeyIn();    
    }

    void KeyIn()
    {
        //リセットキー
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
