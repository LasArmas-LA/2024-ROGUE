using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [Header("レアリティの範囲")]
    public Vector2Int star1Range = new Vector2Int(10, 30);
    public Vector2Int star2Range = new Vector2Int(20, 60);
    public Vector2Int star3Range = new Vector2Int(30, 90);

    [SerializeField]
    public BaseEquipment[] randomEquip = null;

    public int rnd1 = 0;
    public int rnd2 = 0;
    public int rnd3 = 0;

    //[SerializeField]
    //BaseEquipment[] baseEquipment = null;

    public BaseEquipment GenerateRandomEquipment(BaseEquipment baseData, int rarity)
    {
        BaseEquipment randomEquipment = ScriptableObject.CreateInstance<BaseEquipment>();
        randomEquipment.equipmentName = $"{baseData.equipmentName} ★{rarity}";
        randomEquipment.equipmentType = baseData.equipmentType;

        // レアリティに応じた数値範囲
        Vector2Int range = GetRarityRange(rarity);

        // 基本値に応じた数値範囲
        /*
        randomEquipment.HP  = baseData.HP  + Random.Range(range.x,range.y + 1);
        randomEquipment.ATK = baseData.ATK + Random.Range(range.x,range.y + 1);
        randomEquipment.DEF = baseData.DEF + Random.Range(range.x,range.y + 1);
        randomEquipment.ROT = baseData.ROT + Random.Range(range.x,range.y + 1);
        */

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

    public void Start()
    {
        rnd1 = Random.Range(1, randomEquip.Length);
        rnd2 = Random.Range(1, randomEquip.Length);
        rnd3 = Random.Range(1, randomEquip.Length);

        // テスト用: 仮の装備データ

        Debug.Log($"Generated: {randomEquip[rnd1].equipmentName} | ATK: {randomEquip[rnd1].ATK} | DEF: {randomEquip[rnd1].DEF}");
        Debug.Log($"Generated: {randomEquip[rnd2].equipmentName} | ATK: {randomEquip[rnd2].ATK} | DEF: {randomEquip[rnd2].DEF}");
        Debug.Log($"Generated: {randomEquip[rnd3].equipmentName} | ATK: {randomEquip[rnd3].ATK} | DEF: {randomEquip[rnd3].DEF}");
    }
}