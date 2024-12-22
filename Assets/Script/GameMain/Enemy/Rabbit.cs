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

        maxhp = enemyStatus.MAXHP;
        maxmp = enemyStatus.MAXMP;
        power = enemyStatus.ATK;


        hp = maxhp;
        mp = maxmp;

    }

    void Update()
    {
        if (hp <= 0)
        {
            enemySys.deathFlag = true;
        }
    }

    public override void Skil()
    {
        int skilRnd = 0;
        int slectNo = 0;
        for (int i = 0; i < 1; i++)
        {
            //0��1�̗���
            skilRnd = UnityEngine.Random.Range(1, 101);
        }

        //�X�L��1
        if(skilRnd <= 70)
        {
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
                    encountSys.windowsMes.text = "�E�T�M�̂��������I�f�B�A�������[��������I�f�B�A��" + ((power + (power * powerValue)) * 0.3f) + "�̃_���[�W!";
                    dhia.hp -= ((power +(power * powerValue)) * 0.3f);
                }
                else
                {
                    encountSys.windowsMes.text = "�E�T�M�̂��������I�����[��" + (power + (power * powerValue)) + "�̃_���[�W!";
                    riri.hp -= (power + (power * powerValue));
                }

            }
            //�U���Ώۃf�B�A
            else if (slectNo == 1)
            {
                if (dhia.defenseFlag)
                {
                    encountSys.windowsMes.text = "�E�T�M�̂��������I�f�B�A��" + ((power + (power * powerValue)) * 0.5f) + "�̃_���[�W!";
                    dhia.hp -= ((power + (power * powerValue)) * 0.5f);
                }
                else
                {
                    encountSys.windowsMes.text = "�E�T�M�̂��������I�f�B�A��" + (power + (power * powerValue)) + "�̃_���[�W!";
                    dhia.hp -= (power + (power * powerValue));
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


    public override void AddWords()
    {
        
    }
}