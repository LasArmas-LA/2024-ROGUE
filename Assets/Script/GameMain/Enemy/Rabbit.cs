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
    TestEncount encountSys = null;

    [SerializeField]
    EnemyManager enemySys = null;



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
        Debug.Log("�G�l�~�[");

        int rnd = 0;
        for (int i = 0; i < 1; i++)
        {
            rnd = UnityEngine.Random.Range(0, 2);
        }
        //�f�B�A������ł��鎞�U���Ώۂ������[�ɏ㏑��
        if (dhia.deathFlag)
        {
            rnd = 0;
        }

        //�U���Ώۃ����[
        if (rnd == 0)
        {
            //70%�y��
            if (dhia.ririDefenseFlag)
            {
                encountSys.windowsMes.text = "�Ă��̂��������I�f�B�A�������[��������I�f�B�A��" + power * 0.3f + "�̃_���[�W!";
                dhia.hp -= (power * 0.3f);
            }
            else
            {
                encountSys.windowsMes.text = "�Ă��̂��������I�����[��" + power + "�̃_���[�W!";
                riri.hp -= power;
            }

        }
        //�U���Ώۃf�B�A
        else if (rnd == 1)
        {
            if (dhia.defenseFlag)
            {
                encountSys.windowsMes.text = "�Ă��̂��������I�f�B�A��" + power * 0.5f + "�̃_���[�W!";
                dhia.hp -= (power * 0.5f);
            }
            else
            {
                encountSys.windowsMes.text = "�Ă��̂��������I�f�B�A��" + power + "�̃_���[�W!";
                dhia.hp -= power;
            }
        }
    }


    public override void AddWords()
    {
        
    }
}