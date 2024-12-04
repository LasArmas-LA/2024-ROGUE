using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Status[] enemyStatus = null;

    public float maxhp = 0;
    public float maxmp = 0;

    public float hp = 0;
    public float mp = 0;
    public float power = 0;

    public bool deathFlag = false;

    int rnd = 0;
    void Awake()
    {
        Init();
    }

    void Init()
    {
        Debug.Log(enemyStatus.Length);
        rnd = Random.Range(0, enemyStatus.Length);

        maxhp = enemyStatus[rnd].MAXHP;
        maxmp = enemyStatus[rnd].MAXMP;
        power = enemyStatus[rnd].ATK;
        hp = maxhp;
        mp = maxmp;
        deathFlag = false;
    }


    void Update()
    {
        if(hp <= 0)
        {
            deathFlag = true;
        }
    }
}
