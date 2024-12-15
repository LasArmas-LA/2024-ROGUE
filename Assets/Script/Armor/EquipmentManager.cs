using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField]
    public BaseEquipment[] randomEquip = null;

    public int[] rnd;

    public void Start()
    {
    }
    void Update()
    {
        for (int i = 0; i < rnd.Length; i++)
        {
            //—”‚ªŒÅ‚Ü‚é‚©‚ç“{‚è‚Ìˆ—
            rnd[i] = Random.Range(1, randomEquip.Length);
            rnd[i] = Random.Range(1, randomEquip.Length);
            rnd[i] = Random.Range(1, randomEquip.Length);
            rnd[i] = Random.Range(1, randomEquip.Length);
            rnd[i] = Random.Range(1, randomEquip.Length);
            rnd[i] = Random.Range(1, randomEquip.Length);
        }
    }
}