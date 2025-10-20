// ���ϸ�: Stack.cs
// Ŭ������: StackExample
// ����:
// - Stack<T>�� ����Ƽ �ǹ� ��� ���ø� �����ݴϴ�.
// - 1) placedStack: �ν��Ͻ�ȭ�� ������Ʈ���� �׾Ƶΰ� Undo(������ ���� ���) ��� ����
// - 2) uiStack: UI �г� �̸��� �׾Ƶΰ� Back(�ڷΰ���) ��� �ùķ��̼�
// ���:
// - ���� �� GameObject ���� �� �� ��ũ��Ʈ�� ���̰�, Prefabs�� ���� ������ ���.
// - Play ��忡�� Ű�� ���� ���� Ȯ�� (P: Place, U: Undo, I: Push UI, O: Pop UI, K: Print)

using System.Collections.Generic;
using UnityEngine;

public class StackExample : MonoBehaviour
{
    [Header("Prefabs for placing (for placedStack demo)")]
    [SerializeField] private List<GameObject> placePrefabs = new List<GameObject>();

    [Header("Place position settings")]
    [SerializeField] private Vector3 spawnBase = new Vector3(0, 1, 0);
    [SerializeField] private float spawnOffset = 1.2f;

    // Stack for instantiated GameObjects => Undo ���(������ ���� ���)�� ���
    private Stack<GameObject> placedStack = new Stack<GameObject>();

    // UI Back stack (��: �г� �̸� ����)
    private Stack<string> uiStack = new Stack<string>();

    private void Start()
    {
        // �ʱ� UI ����(���� �޴�)
        uiStack.Push("MainMenu");
        Debug.Log("�ʱ� UI: MainMenu");
    }

    private void Update()
    {
        // P Ű: ���� ������ ����(Place) -> placedStack.Push
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlaceRandomPrefab();
        }

        // U Ű: Undo -> placedStack.Pop + Destroy
        if (Input.GetKeyDown(KeyCode.U))
        {
            UndoPlace();
        }

        // I Ű: UI ���ÿ� �г� �߰� (��: Inventory, Settings)
        if (Input.GetKeyDown(KeyCode.I))
        {
            // ����: Inventory Ȥ�� Settings ������ �ֱ�
            string panel = (Random.value > 0.5f) ? "Inventory" : "Settings";
            PushUIPanel(panel);
        }

        // O Ű: UI Back(�ڷΰ���)
        if (Input.GetKeyDown(KeyCode.O))
        {
            PopUIPanel();
        }

        // K Ű: ���� ���� ��� (�����)
        if (Input.GetKeyDown(KeyCode.K))
        {
            PrintStacksStatus();
        }
    }

    // -----------------------------
    // placedStack ���� �޼��� (Undo ����)
    // -----------------------------
    private void PlaceRandomPrefab()
    {
        if (placePrefabs == null || placePrefabs.Count == 0)
        {
            Debug.LogWarning("placePrefabs�� �������� �־��ּ���.");
            return;
        }

        int idx = Random.Range(0, placePrefabs.Count);
        GameObject prefab = placePrefabs[idx];
        Vector3 pos = spawnBase + Vector3.right * (placedStack.Count * spawnOffset);
        GameObject go = Instantiate(prefab, pos, Quaternion.identity);
        go.name = prefab.name + "_Inst";

        // ������ ������Ʈ�� ���ÿ� Push (���߿� Undo �� �� �ְ�)
        placedStack.Push(go);
        Debug.Log($"Placed {go.name}. placedStack count = {placedStack.Count}");
    }

    private void UndoPlace()
    {
        if (placedStack.Count == 0)
        {
            Debug.Log("Undo: ������ ������Ʈ�� �����ϴ�.");
            return;
        }

        // ���� �������� ������(�� ��) ������Ʈ�� Pop�ϰ� ����
        GameObject last = placedStack.Pop();
        if (last != null)
        {
            Destroy(last);
            Debug.Log($"Undo: Destroyed {last.name}. placedStack count = {placedStack.Count}");
        }
        else
        {
            Debug.LogWarning("Undo: ������Ʈ�� �̹� null �Դϴ�.");
        }
    }

    // -----------------------------
    // uiStack ���� �޼��� (Back stack ����)
    // -----------------------------
    private void PushUIPanel(string panelName)
    {
        // �� �г��� Ǫ���ϸ� �� �г��� "����" �г��� ��
        uiStack.Push(panelName);
        Debug.Log($"UI Push: {panelName}. Current = {uiStack.Peek()} (count={uiStack.Count})");

        // �����δ� ���⼭ �ش� �г��� Ȱ��ȭ/��Ȱ��ȭ ó��
        // ��: panel.SetActive(true); ���� �г� ���� ��
    }

    private void PopUIPanel()
    {
        if (uiStack.Count <= 1)
        {
            Debug.Log("UI Back: ���̻� �ڷ� �� �г��� �����ϴ�.");
            return;
        }

        // ���� �г� ����(�ݱ�)
        string closed = uiStack.Pop();
        string now = uiStack.Peek(); // ���� �г��� ���� ���� �г�
        Debug.Log($"UI Pop: Closed {closed}. Now showing {now} (count={uiStack.Count})");

        // �����δ� closed �г� ��Ȱ��ȭ, now �г� Ȱ��ȭ �� ó��
    }

    // -----------------------------
    // ����� ��ƿ
    // -----------------------------
    private void PrintStacksStatus()
    {
        Debug.Log($"placedStack count = {placedStack.Count}, uiStack count = {uiStack.Count}");

        // placedStack ���� ��� (�����ؼ� ���)
        if (placedStack.Count > 0)
        {
            Debug.Log("placedStack (top -> bottom):");
            foreach (var go in placedStack)
            {
                Debug.Log($" - {go?.name}");
            }
        }

        // uiStack ���� ��� (top -> bottom)
        if (uiStack.Count > 0)
        {
            Debug.Log("uiStack (top -> bottom):");
            foreach (var panel in uiStack)
            {
                Debug.Log($" - {panel}");
            }
        }
    }
}
