using UnityEngine;
using UnityEngine.UI;

public class Rabbit : EnemyManager
{
    [SerializeField]
    Status enemyStatus = null;



    [Header("�N���X�Q��")]
    [SerializeField]
    Riri riri = null;
    [SerializeField]
    Dhia dhia = null;


    [SerializeField]
    EnemyManager enemySys = null;

    //�U���͂̕␳�l
    float powerValue = 0f;



    public override void InitRabbit()
    {
        Debug.Log("������");
        deathFlag = false;
        
        this.gameObject.transform.localScale =  new Vector3(1,1,1);

        maxhp = enemyStatus.MAXHP;
        maxmp = enemyStatus.MAXMP;
        power = enemyStatus.ATK;
        def = enemyStatus.DEF;

        hp = maxhp;
        mp = maxmp;
    }

    void Update()
    {

    }

    public override void SkilRabbit()
    {
        int skilRnd = 0;
        int slectNo = 0;
        for (int i = 0; i < 1; i++)
        {
            //0����100�̗���
            skilRnd = UnityEngine.Random.Range(1, 100);
        }

        //�X�L��1
        if(skilRnd <= 70)
        {
            float ririDamage = DamageCalculation(power, riri.def);
            float dhiaDamage = DamageCalculation(power, dhia.def);

            //�����[�̕���HP������
            if (riri.hp > dhia.hp)
            {
                slectNo = 0;
            }
            //�f�B�A�̕���HP������
            else
            {
                slectNo = 1;
            }

            //�f�B�A������ł��鎞�U���Ώۂ������[�ɏ㏑��
            if (dhia.deathFlag)
            {
                slectNo = 0;
            }

            //�U���Ώۃ����[
            if (slectNo == 0)
            {
                //70%�y��
                if (dhia.ririDefenseFlag)
                {
                    encountSys.windowsMes.text = "�E�T�M�̂��������I�f�B�A�������[��������I�f�B�A��" + ( ririDamage * 0.3f) + "�̃_���[�W!";
                    dhia.hp -= (ririDamage * 0.3f);
                }
                else
                {
                    encountSys.windowsMes.text = "�E�T�M�̂��������I�����[��" + (ririDamage) + "�̃_���[�W!";
                    riri.hp -= (ririDamage);
                }
            }
            //�U���Ώۃf�B�A
            else if (slectNo == 1)
            {
                if (dhia.defenseFlag)
                {
                    encountSys.windowsMes.text = "�E�T�M�̂��������I�f�B�A��" + (dhiaDamage * 0.5f) + "�̃_���[�W!";
                    dhia.hp -= (dhiaDamage * 0.5f);
                }
                else
                {
                    encountSys.windowsMes.text = "�E�T�M�̂��������I�f�B�A��" + (dhiaDamage) + "�̃_���[�W!";
                    dhia.hp -= (dhiaDamage);
                }
            }

        }

        //�X�L��2
        if (skilRnd >= 71)
        {
            encountSys.windowsMes.text = "�E�T�M�͂ɂ񂶂�V�`���[�����񂾁I\n�E�T�M�̍U���͂�15%�A�b�v�����I";
            powerValue += 0.15f;
        }
    }



    //�_���[�W�v�Z�p
    int DamageCalculation(int attack,int defense)
    {
        //�V�[�h�l�̕ύX
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        //�f�̃_���[�W�v�Z
        int damage = (attack + (attack * (int)powerValue) / 2) - (defense / 4);

        //�_���[�W�U���̌v�Z
        int width = damage / 16 + 1;

        //�_���[�W�U���l�����������v�Z
        damage = UnityEngine.Random.Range(damage - width, damage + width);

        //�Ăяo�����Ƀ_���[�W����Ԃ�
        return damage;
    }
}