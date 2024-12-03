using UnityEngine;

public class Riri : MonoBehaviour
{
    public float maxhp = 0;
    public float maxmp = 0;

    public float hp = 0;
    public float mp = 0;
    public float power = 0;

    public bool deathFlag = false;

    void Start()
    {
        Init();
    }

    void Init()
    {
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
