using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "Status", menuName = "Character Scriptable Objects/Status")]

[System.Serializable]
public class Status : ScriptableObject
{
    [Header("ステータス")]
    public string NAME = null;
    public float MAXHP = 0;
    public float HP = 0;
    public float MAXMP = 0;
    public float MP = 0;
    public int ATK = 0;
    public int DEF = 0;

    public GameObject obj = null;

    [Space(5)]

    [Header("頭パーツ")]
    public BaseEquipment headPartsData;
    [Header("体パーツ")]
    public BaseEquipment bodyPartsData;
    [Header("足パーツ")]
    public BaseEquipment legPartsData;
    [Header("右手パーツ")]
    public BaseEquipment righthandPartsData;
    [Header("左手パーツ")]
    public BaseEquipment lefthandPartsData;
}
