using System.Collections.Generic;
using UnityEngine;

public class HashSet : MonoBehaviour
{
    // 이미 처리한 오브젝트들을 저장
    private HashSet<GameObject> processedObjects = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        // 이미 처리된 오브젝트면 실행 안 함
        if (!processedObjects.Add(other.gameObject))
        {
            return;
        }

        // 최초 1회만 실행되는 로직
        Debug.Log($"처음 트리거 진입: {other.gameObject.name}");

        // 예시: 퀘스트 처리, 아이템 획득, 연출 실행 등
    }

    private void OnTriggerExit(Collider other)
    {
        // 필요하면 나갈 때 제거 가능
        processedObjects.Remove(other.gameObject);
    }
}
