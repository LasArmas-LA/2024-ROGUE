using UnityEngine;

public class FloorNoSys : MonoBehaviour
{
    [SerializeField]
    public int floorNo = 0;

    void Start()
    {
        if(GameObject.Find("FloorNo") == null)
        {
            this.gameObject.name = "FloorNo";
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {

    }
}
