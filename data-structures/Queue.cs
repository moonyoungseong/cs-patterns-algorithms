// 파일명: Queue.cs
// 클래스명: QueueExample
// 목적: Unity에서 Queue<T>를 실전처럼 사용하는 예시
// - Spawn Queue: 적(프리팹) 스폰을 큐에 넣고 코루틴으로 순차 생성
// - Task Queue: 델리게이트(또는 Action)를 큐에 넣어 메인 루프에서 순차 실행

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueExample : MonoBehaviour
{
    // ----------------------------
    // Spawn Queue 예제 (게임에서 가장 흔한 사용법)
    // ----------------------------

    [Header("Spawn Queue Settings")]
    // 스폰할 프리팹들을 미리 할당하거나 Inspector에서 추가
    [SerializeField] private List<GameObject> spawnPrefabs = new List<GameObject>();

    // 스폰할 항목들을 큐로 관리 (각 항목은 prefab id와 위치 같은 정보를 담을 수 있음)
    private Queue<SpawnItem> spawnQueue = new Queue<SpawnItem>();

    // 스폰 간격 (초)
    [SerializeField] private float spawnInterval = 0.5f;

    // 스폰 코루틴 활성 여부
    private Coroutine spawnCoroutine;

    // SpawnItem: 큐에 담길 스폰 정보 구조체
    [Serializable]
    public struct SpawnItem
    {
        public int prefabIndex;   // spawnPrefabs 인덱스
        public Vector3 position;  // 스폰 위치
        public Quaternion rotation;

        public SpawnItem(int prefabIndex, Vector3 pos, Quaternion rot)
        {
            this.prefabIndex = prefabIndex;
            this.position = pos;
            this.rotation = rot;
        }
    }

    // ----------------------------
    // Task Queue 예제 (델리게이트 기반 작업 큐)
    // ----------------------------
    private Queue<Action> taskQueue = new Queue<Action>();
    [Header("Task Queue (for demo)")]
    [SerializeField] private KeyCode enqueueTaskKey = KeyCode.T; // T키로 테스트 작업 추가

    // ----------------------------
    // Unity life cycle
    // ----------------------------
    private void Start()
    {
        // 예제: Inspector에서 등록된 prefab들을 이용해 SpawnQueue에 몇 개 넣기
        EnqueueSpawn(0, new Vector3(0, 1, 0));
        EnqueueSpawn(1, new Vector3(1, 1, 0));
        EnqueueSpawn(0, new Vector3(-1, 1, 0));
        EnqueueSpawn(2, new Vector3(2, 1, 0));

        // 스폰 코루틴 시작 (큐에 항목이 있을 때만)
        TryStartSpawnCoroutine();

        // Task Queue 예제: 샘플 작업을 몇 개 등록
        EnqueueTask(() => Debug.Log("Task 1 실행"));
        EnqueueTask(() => Debug.Log("Task 2 실행"));
    }

    private void Update()
    {
        // Task Queue 처리: 매 프레임 하나씩 실행(원하면 조건을 바꿀 수 있음)
        if (taskQueue.Count > 0)
        {
            Action task = taskQueue.Dequeue();
            try
            {
                task?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogError("Task 실행 중 예외: " + ex);
            }
        }

        // 키 입력으로 스폰 항목 추가(테스트 용)
        if (Input.GetKeyDown(enqueueTaskKey))
        {
            EnqueueTask(() => Debug.Log("T 키로 추가된 작업 실행 at " + Time.time));
            EnqueueSpawn(UnityEngine.Random.Range(0, Mathf.Max(1, spawnPrefabs.Count)), new Vector3(UnityEngine.Random.Range(-3f, 3f), 1f, 0f));
            TryStartSpawnCoroutine();
        }
    }

    // ----------------------------
    // Spawn Queue 관련 메서드
    // ----------------------------
    /// <summary>
    /// spawnQueue에 항목 추가
    /// </summary>
    public void EnqueueSpawn(int prefabIndex, Vector3 pos, Quaternion rot = default)
    {
        if (spawnPrefabs == null || spawnPrefabs.Count == 0)
        {
            Debug.LogWarning("spawnPrefabs가 비어있음. Prefab을 할당해 주세요.");
            return;
        }

        // 간단 안전성 검사: 인덱스 범위 체크
        if (prefabIndex < 0 || prefabIndex >= spawnPrefabs.Count)
        {
            Debug.LogWarning($"prefabIndex {prefabIndex} 범위 초과. 사용 가능한 범위: 0 ~ {spawnPrefabs.Count - 1}");
            return;
        }

        spawnQueue.Enqueue(new SpawnItem(prefabIndex, pos, rot == default ? Quaternion.identity : rot));
        Debug.Log($"SpawnQueue에 항목 추가: prefabIndex={prefabIndex}, queueCount={spawnQueue.Count}");
    }

    /// <summary>
    /// 스폰 코루틴 시작 시도 (이미 실행 중이면 무시)
    /// </summary>
    public void TryStartSpawnCoroutine()
    {
        if (spawnCoroutine == null && spawnQueue.Count > 0)
        {
            spawnCoroutine = StartCoroutine(SpawnRoutine());
        }
    }

    /// <summary>
    /// 실제로 큐에서 하나씩 꺼내 스폰하는 코루틴
    /// </summary>
    private IEnumerator SpawnRoutine()
    {
        while (spawnQueue.Count > 0)
        {
            SpawnItem item = spawnQueue.Dequeue();
            GameObject prefab = spawnPrefabs[item.prefabIndex];
            if (prefab != null)
            {
                Instantiate(prefab, item.position, item.rotation);
                Debug.Log($"Spawned prefabIndex={item.prefabIndex} at {item.position}. remaining={spawnQueue.Count}");
            }
            else
            {
                Debug.LogWarning($"Prefab null at index {item.prefabIndex}");
            }

            // 다음 스폰까지 대기
            yield return new WaitForSeconds(spawnInterval);
        }

        // 큐가 다 비면 코루틴 끝내기
        spawnCoroutine = null;
    }

    /// <summary>
    /// 스폰 큐 비우기
    /// </summary>
    public void ClearSpawnQueue()
    {
        spawnQueue.Clear();
        Debug.Log("SpawnQueue 비움");
    }

    // ----------------------------
    // Task Queue 관련 메서드
    // ----------------------------
    /// <summary>
    /// 간단히 델리게이트 형태의 작업을 큐에 넣는다.
    /// 예: "화면 연출", "사운드 재생", "데이터 저장" 등의 작업을 순서대로 처리
    /// </summary>
    public void EnqueueTask(Action task)
    {
        if (task == null) return;
        taskQueue.Enqueue(task);
        Debug.Log($"TaskQueue에 작업 추가. count={taskQueue.Count}");
    }

    /// <summary>
    /// TaskQueue 비우기
    /// </summary>
    public void ClearTaskQueue()
    {
        taskQueue.Clear();
    }

    /// <summary>
    /// 현재 큐 상태 디버그 출력
    /// </summary>
    [ContextMenu("PrintQueuesStatus")]
    private void PrintQueuesStatus()
    {
        Debug.Log($"SpawnQueue count = {spawnQueue.Count}, TaskQueue count = {taskQueue.Count}");
    }
}
