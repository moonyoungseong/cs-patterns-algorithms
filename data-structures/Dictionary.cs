using System.Collections.Generic;
using UnityEngine;

//  ����Ƽ���� Dictionary�� ����ϴ� �⺻ ����
public class DictionaryExample : MonoBehaviour
{
    // ������ �����͸� ���� Dictionary (Key: string, Value: ItemData)
    private Dictionary<string, ItemData> itemDictionary = new Dictionary<string, ItemData>();

    void Start()
    {
        //  ������ �߰�
        AddItem("potion", new ItemData("ü�� ����", 50));
        AddItem("elixir", new ItemData("���� ������", 100));
        AddItem("sword", new ItemData("��ö ��", 250));

        //  Ư�� ������ ã��
        ItemData foundItem = GetItem("elixir");
        if (foundItem != null)
        {
            Debug.Log($"[�˻� ����] {foundItem.itemName} - ����: {foundItem.price}");
        }

        //  ��ü ������ ���
        PrintAllItems();
    }

    // ������ �߰� �޼���
    public void AddItem(string key, ItemData data)
    {
        if (!itemDictionary.ContainsKey(key))
        {
            itemDictionary.Add(key, data);
            Debug.Log($"[�߰���] {data.itemName}");
        }
        else
        {
            Debug.LogWarning($"�̹� �����ϴ� Ű�Դϴ�: {key}");
        }
    }

    // ������ �˻� �޼���
    public ItemData GetItem(string key)
    {
        if (itemDictionary.TryGetValue(key, out ItemData data))
        {
            return data;
        }
        Debug.LogWarning($"�ش� Ű({key})�� ã�� �� �����ϴ�.");
        return null;
    }

    // ��ü ������ ���
    public void PrintAllItems()
    {
        Debug.Log("=== ������ ��� ===");
        foreach (KeyValuePair<string, ItemData> item in itemDictionary)
        {
            Debug.Log($"{item.Key} : {item.Value.itemName} ({item.Value.price}G)");
        }
    }
}

//  ������ ������ ����
[System.Serializable]
public class ItemData
{
    public string itemName;
    public int price;

    public ItemData(string name, int price)
    {
        this.itemName = name;
        this.price = price;
    }
}
