using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    //�����̃f�[�^���ۑ�����Ă���f�[�^�ׁ[�X���`
    public BaseEquipment[] randomEquip = null;

    //���������_�����I�p
    public int[] rnd;

    public void LoopInit()
    {
        //���������ɗ������o����
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        //3�������������_���Œ��I
        rnd[0] = Random.Range(1, randomEquip.Length);
        rnd[1] = Random.Range(1, randomEquip.Length);
        rnd[2] = Random.Range(1, randomEquip.Length);
    }
}