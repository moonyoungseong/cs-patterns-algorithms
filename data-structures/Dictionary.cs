using System.Collections.Generic;
using UnityEngine;

//  유니티에서 Dictionary를 사용하는 기본 예시
public class DictionaryExample : MonoBehaviour
{
    // 아이템 데이터를 담을 Dictionary (Key: string, Value: ItemData)
    private Dictionary<string, ItemData> itemDictionary = new Dictionary<string, ItemData>();

    void Start()
    {
        //  아이템 추가
        AddItem("potion", new ItemData("체력 포션", 50));
        AddItem("elixir", new ItemData("마나 엘릭서", 100));
        AddItem("sword", new ItemData("강철 검", 250));

        //  특정 아이템 찾기
        ItemData foundItem = GetItem("elixir");
        if (foundItem != null)
        {
            Debug.Log($"[검색 성공] {foundItem.itemName} - 가격: {foundItem.price}");
        }

        //  전체 아이템 출력
        PrintAllItems();
    }

    // 아이템 추가 메서드
    public void AddItem(string key, ItemData data)
    {
        if (!itemDictionary.ContainsKey(key))
        {
            itemDictionary.Add(key, data);
            Debug.Log($"[추가됨] {data.itemName}");
        }
        else
        {
            Debug.LogWarning($"이미 존재하는 키입니다: {key}");
        }
    }

    // 아이템 검색 메서드
    public ItemData GetItem(string key)
    {
        if (itemDictionary.TryGetValue(key, out ItemData data))
        {
            return data;
        }
        Debug.LogWarning($"해당 키({key})를 찾을 수 없습니다.");
        return null;
    }

    // 전체 아이템 출력
    public void PrintAllItems()
    {
        Debug.Log("=== 아이템 목록 ===");
        foreach (KeyValuePair<string, ItemData> item in itemDictionary)
        {
            Debug.Log($"{item.Key} : {item.Value.itemName} ({item.Value.price}G)");
        }
    }
}

//  아이템 데이터 구조
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
