using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    //装備のデータが保存されているデータべースを定義
    public BaseEquipment[] randomEquip = null;

    //装備ランダム抽選用
    public int[] rnd;

    public void LoopInit()
    {
        //時刻を元に乱数をバラす
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        //3つ分装備をランダムで抽選
        rnd[0] = Random.Range(1, randomEquip.Length);
        rnd[1] = Random.Range(1, randomEquip.Length);
        rnd[2] = Random.Range(1, randomEquip.Length);
    }
}