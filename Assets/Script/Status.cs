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
    public float ATK = 0;
    public float DEF = 0;

    [Space(5)]

    [Header("頭パーツ")]
    public ScriptableObject headPartsData;
    [Header("体パーツ")]
    public ScriptableObject bodyPartsData;
    [Header("足パーツ")]
    public ScriptableObject legPartsData;
    [Header("右手パーツ")]
    public ScriptableObject righthandPartsData;
    [Header("左手パーツ")]
    public ScriptableObject lefthandPartsData;
}
