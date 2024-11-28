using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [Header("レアリティの範囲")]
    public Vector2Int star1Range = new Vector2Int(10, 30);
    public Vector2Int star2Range = new Vector2Int(20, 60);
    public Vector2Int star3Range = new Vector2Int(30, 90);

    public BaseEquipment GenerateRandomEquipment(BaseEquipment baseData, int rarity)
    {
        BaseEquipment randomEquipment = ScriptableObject.CreateInstance<BaseEquipment>();
        randomEquipment.equipmentName = $"{baseData.equipmentName} ★{rarity}";
        randomEquipment.equipmentType = baseData.equipmentType;

        // レアリティに応じた数値範囲
        Vector2Int range = GetRarityRange(rarity);

        // 基本値に応じた数値範囲
        randomEquipment.HP  = baseData.HP  + Random.Range(range.x,range.y + 1);
        randomEquipment.ATK = baseData.ATK + Random.Range(range.x,range.y + 1);
        randomEquipment.DEF = baseData.DEF + Random.Range(range.x,range.y + 1);
        randomEquipment.ROT = baseData.ROT + Random.Range(range.x,range.y + 1);

        return randomEquipment;
    }

    private Vector2Int GetRarityRange(int rarity)
    {
        switch (rarity)
        {
            case 1: 
                return star1Range;
            case 2:
                return star2Range;
            case 3:
                return star3Range;
            default: return new Vector2Int(0, 0);
        }
    }

    private void Start()
    {
        // テスト用: 仮の装備データ
        BaseEquipment testData = ScriptableObject.CreateInstance<BaseEquipment>();
        testData.equipmentName = "Test Weapon";
        testData.equipmentType = BaseEquipment.EquipmentType.LeftHand;
        testData.HP = 0;
        testData.ATK = 20;
        testData.DEF = 5;
        testData.ROT = 2;

        BaseEquipment randomEquip = GenerateRandomEquipment(testData, 2); // ★2の装備を生成
        Debug.Log($"Generated: {randomEquip.equipmentName} | ATK: {randomEquip.ATK} | DEF: {randomEquip.DEF}");
    }
}