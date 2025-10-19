// ���ϸ�: Queue.cs
// Ŭ������: QueueExample
// ����: Unity���� Queue<T>�� ����ó�� ����ϴ� ����
// - Spawn Queue: ��(������) ������ ť�� �ְ� �ڷ�ƾ���� ���� ����
// - Task Queue: ��������Ʈ(�Ǵ� Action)�� ť�� �־� ���� �������� ���� ����

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueExample : MonoBehaviour
{
    // ----------------------------
    // Spawn Queue ���� (���ӿ��� ���� ���� ����)
    // ----------------------------

    [Header("Spawn Queue Settings")]
    // ������ �����յ��� �̸� �Ҵ��ϰų� Inspector���� �߰�
    [SerializeField] private List<GameObject> spawnPrefabs = new List<GameObject>();

    // ������ �׸���� ť�� ���� (�� �׸��� prefab id�� ��ġ ���� ������ ���� �� ����)
    private Queue<SpawnItem> spawnQueue = new Queue<SpawnItem>();

    // ���� ���� (��)
    [SerializeField] private float spawnInterval = 0.5f;

    // ���� �ڷ�ƾ Ȱ�� ����
    private Coroutine spawnCoroutine;

    // SpawnItem: ť�� ��� ���� ���� ����ü
    [Serializable]
    public struct SpawnItem
    {
        public int prefabIndex;   // spawnPrefabs �ε���
        public Vector3 position;  // ���� ��ġ
        public Quaternion rotation;

        public SpawnItem(int prefabIndex, Vector3 pos, Quaternion rot)
        {
            this.prefabIndex = prefabIndex;
            this.position = pos;
            this.rotation = rot;
        }
    }

    // ----------------------------
    // Task Queue ���� (��������Ʈ ��� �۾� ť)
    // ----------------------------
    private Queue<Action> taskQueue = new Queue<Action>();
    [Header("Task Queue (for demo)")]
    [SerializeField] private KeyCode enqueueTaskKey = KeyCode.T; // TŰ�� �׽�Ʈ �۾� �߰�

    // ----------------------------
    // Unity life cycle
    // ----------------------------
    private void Start()
    {
        // ����: Inspector���� ��ϵ� prefab���� �̿��� SpawnQueue�� �� �� �ֱ�
        EnqueueSpawn(0, new Vector3(0, 1, 0));
        EnqueueSpawn(1, new Vector3(1, 1, 0));
        EnqueueSpawn(0, new Vector3(-1, 1, 0));
        EnqueueSpawn(2, new Vector3(2, 1, 0));

        // ���� �ڷ�ƾ ���� (ť�� �׸��� ���� ����)
        TryStartSpawnCoroutine();

        // Task Queue ����: ���� �۾��� �� �� ���
        EnqueueTask(() => Debug.Log("Task 1 ����"));
        EnqueueTask(() => Debug.Log("Task 2 ����"));
    }

    private void Update()
    {
        // Task Queue ó��: �� ������ �ϳ��� ����(���ϸ� ������ �ٲ� �� ����)
        if (taskQueue.Count > 0)
        {
            Action task = taskQueue.Dequeue();
            try
            {
                task?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogError("Task ���� �� ����: " + ex);
            }
        }

        // Ű �Է����� ���� �׸� �߰�(�׽�Ʈ ��)
        if (Input.GetKeyDown(enqueueTaskKey))
        {
            EnqueueTask(() => Debug.Log("T Ű�� �߰��� �۾� ���� at " + Time.time));
            EnqueueSpawn(UnityEngine.Random.Range(0, Mathf.Max(1, spawnPrefabs.Count)), new Vector3(UnityEngine.Random.Range(-3f, 3f), 1f, 0f));
            TryStartSpawnCoroutine();
        }
    }

    // ----------------------------
    // Spawn Queue ���� �޼���
    // ----------------------------
    /// <summary>
    /// spawnQueue�� �׸� �߰�
    /// </summary>
    public void EnqueueSpawn(int prefabIndex, Vector3 pos, Quaternion rot = default)
    {
        if (spawnPrefabs == null || spawnPrefabs.Count == 0)
        {
            Debug.LogWarning("spawnPrefabs�� �������. Prefab�� �Ҵ��� �ּ���.");
            return;
        }

        // ���� ������ �˻�: �ε��� ���� üũ
        if (prefabIndex < 0 || prefabIndex >= spawnPrefabs.Count)
        {
            Debug.LogWarning($"prefabIndex {prefabIndex} ���� �ʰ�. ��� ������ ����: 0 ~ {spawnPrefabs.Count - 1}");
            return;
        }

        spawnQueue.Enqueue(new SpawnItem(prefabIndex, pos, rot == default ? Quaternion.identity : rot));
        Debug.Log($"SpawnQueue�� �׸� �߰�: prefabIndex={prefabIndex}, queueCount={spawnQueue.Count}");
    }

    /// <summary>
    /// ���� �ڷ�ƾ ���� �õ� (�̹� ���� ���̸� ����)
    /// </summary>
    public void TryStartSpawnCoroutine()
    {
        if (spawnCoroutine == null && spawnQueue.Count > 0)
        {
            spawnCoroutine = StartCoroutine(SpawnRoutine());
        }
    }

    /// <summary>
    /// ������ ť���� �ϳ��� ���� �����ϴ� �ڷ�ƾ
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

            // ���� �������� ���
            yield return new WaitForSeconds(spawnInterval);
        }

        // ť�� �� ��� �ڷ�ƾ ������
        spawnCoroutine = null;
    }

    /// <summary>
    /// ���� ť ����
    /// </summary>
    public void ClearSpawnQueue()
    {
        spawnQueue.Clear();
        Debug.Log("SpawnQueue ���");
    }

    // ----------------------------
    // Task Queue ���� �޼���
    // ----------------------------
    /// <summary>
    /// ������ ��������Ʈ ������ �۾��� ť�� �ִ´�.
    /// ��: "ȭ�� ����", "���� ���", "������ ����" ���� �۾��� ������� ó��
    /// </summary>
    public void EnqueueTask(Action task)
    {
        if (task == null) return;
        taskQueue.Enqueue(task);
        Debug.Log($"TaskQueue�� �۾� �߰�. count={taskQueue.Count}");
    }

    /// <summary>
    /// TaskQueue ����
    /// </summary>
    public void ClearTaskQueue()
    {
        taskQueue.Clear();
    }

    /// <summary>
    /// ���� ť ���� ����� ���
    /// </summary>
    [ContextMenu("PrintQueuesStatus")]
    private void PrintQueuesStatus()
    {
        Debug.Log($"SpawnQueue count = {spawnQueue.Count}, TaskQueue count = {taskQueue.Count}");
    }
}
