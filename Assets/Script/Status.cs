using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "Status", menuName = "Character Scriptable Objects/Status")]

[System.Serializable]
public class Status : ScriptableObject
{
    [Header("�X�e�[�^�X")]
    public string NAME = null;
    public float MAXHP = 0;
    public float HP = 0;
    public float MAXMP = 0;
    public float MP = 0;
    public float ATK = 0;
    public float DEF = 0;

    [Space(5)]

    [Header("���p�[�c")]
    public ScriptableObject headPartsData;
    [Header("�̃p�[�c")]
    public ScriptableObject bodyPartsData;
    [Header("���p�[�c")]
    public ScriptableObject legPartsData;
    [Header("�E��p�[�c")]
    public ScriptableObject righthandPartsData;
    [Header("����p�[�c")]
    public ScriptableObject lefthandPartsData;
}
