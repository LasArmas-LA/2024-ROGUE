using UnityEngine;

public class FloorNoSys : MonoBehaviour
{
    [SerializeField]
    public int floorNo = 1;

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
