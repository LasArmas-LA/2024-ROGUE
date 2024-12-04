using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField]
    public BaseEquipment[] randomEquip = null;

    public int rnd1 = 0;
    public int rnd2 = 0;
    public int rnd3 = 0;

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