using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "NewEquipment", menuName = "Equipment/Create New Equipment")]
public class BaseEquipment : ScriptableObject
{
    public string equipmentName;        // 武器の名前
    public EquipmentType equipmentType; // 装備の種類
    public int HP;                      // ヒットポイント
    public int ATK;                     // 攻撃力
    public int DEF;                     // 防御力
    public int ROT;                     // 回避率（？）
    public Image sprite = null;

    public enum EquipmentType
    {
        RightHand, // 右手（盾）
        LeftHand,  // 左手（武器）
        Head,      // 頭
        Body,      // 体
        Feet       // 足
    }
}



