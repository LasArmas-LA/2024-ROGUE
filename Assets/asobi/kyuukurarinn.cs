using UnityEngine;

public class kyuukurarinn : MonoBehaviour
{
    [SerializeField]
    Animator[] ririAnim = null;
    [SerializeField]
    GameObject[] ririObj = null;

    float timer = 0;
    int i = 0;

    void Start()
    {
          
    }

    void Update()
    {
        timer += Time.deltaTime;

        for (int k = 0;i < ririAnim.Length; i++)
        {
            if(timer <= 0.3f) { return;}
            Debug.Log(i);
            ririObj[i].SetActive(true);
            ririAnim[i].SetBool("R_TakeDamage", true);
            timer = 0;
        }
    }
}
