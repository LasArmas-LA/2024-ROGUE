using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    [System.Serializable]
    protected class ItemList
    {
        [SerializeField]
        public int id;

        [SerializeField]
        public string name;

        [SerializeField]
        public Sprite image;

        [SerializeField]
        public string description;
    };

    [SerializeField]
    private ItemList[] itemlist = null;

    [SerializeField]
    private GameObject diaImage = null;

    [SerializeField]
    private GameObject ririImage = null;

    [SerializeField]
    private TMPro.TMP_Text name = null;

    [SerializeField]
    private Image image = null;

    [SerializeField]
    private TMPro.TMP_Text description = null;



    private bool dispChara = false;

    //�A�C�e���ǉ�
    public void AddItemList(string name)
    {
        itemlist[itemlist.Length].name = name;
    }

    //�A�C�e���폜
    public void RemoveItemList(string name)
    {
        return;
    }

    //�A�C�e���̕\��
    private void DisplayingSystem()
    {
        for (int i = 0; i < itemlist.Length; i++)
        {

        }
    }



    public void ChangeCharactor()
    {
        if(!dispChara)
        {
            diaImage.SetActive(false);

            ririImage.SetActive(true);

            dispChara = true;
        }
        else if(dispChara)
        {
            ririImage.SetActive(false);

            diaImage.SetActive(true);

            dispChara = false;
        }
    }


    public void Start()
    {
        Test();
    }

    //�������牼�u���i�����j

    private void Test()
    {
        name.text = itemlist[0].name;

        description.text = itemlist[0].description;

        image.sprite = itemlist[0].image;
    }
}
