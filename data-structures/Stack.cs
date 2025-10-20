// 파일명: Stack.cs
// 클래스명: StackExample
// 설명:
// - Stack<T>의 유니티 실무 사용 예시를 보여줍니다.
// - 1) placedStack: 인스턴스화한 오브젝트들을 쌓아두고 Undo(마지막 생성 취소) 기능 구현
// - 2) uiStack: UI 패널 이름을 쌓아두고 Back(뒤로가기) 기능 시뮬레이션
// 사용:
// - 씬에 빈 GameObject 생성 후 이 스크립트를 붙이고, Prefabs에 샘플 프리팹 등록.
// - Play 모드에서 키를 눌러 동작 확인 (P: Place, U: Undo, I: Push UI, O: Pop UI, K: Print)

using System.Collections.Generic;
using UnityEngine;

public class StackExample : MonoBehaviour
{
    [Header("Prefabs for placing (for placedStack demo)")]
    [SerializeField] private List<GameObject> placePrefabs = new List<GameObject>();

    [Header("Place position settings")]
    [SerializeField] private Vector3 spawnBase = new Vector3(0, 1, 0);
    [SerializeField] private float spawnOffset = 1.2f;

    // Stack for instantiated GameObjects => Undo 기능(마지막 생성 취소)에 사용
    private Stack<GameObject> placedStack = new Stack<GameObject>();

    // UI Back stack (예: 패널 이름 보관)
    private Stack<string> uiStack = new Stack<string>();

    private void Start()
    {
        // 초기 UI 상태(메인 메뉴)
        uiStack.Push("MainMenu");
        Debug.Log("초기 UI: MainMenu");
    }

    private void Update()
    {
        // P 키: 임의 프리팹 생성(Place) -> placedStack.Push
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlaceRandomPrefab();
        }

        // U 키: Undo -> placedStack.Pop + Destroy
        if (Input.GetKeyDown(KeyCode.U))
        {
            UndoPlace();
        }

        // I 키: UI 스택에 패널 추가 (예: Inventory, Settings)
        if (Input.GetKeyDown(KeyCode.I))
        {
            // 예시: Inventory 혹은 Settings 번갈아 넣기
            string panel = (Random.value > 0.5f) ? "Inventory" : "Settings";
            PushUIPanel(panel);
        }

        // O 키: UI Back(뒤로가기)
        if (Input.GetKeyDown(KeyCode.O))
        {
            PopUIPanel();
        }

        // K 키: 스택 상태 출력 (디버그)
        if (Input.GetKeyDown(KeyCode.K))
        {
            PrintStacksStatus();
        }
    }

    // -----------------------------
    // placedStack 관련 메서드 (Undo 예시)
    // -----------------------------
    private void PlaceRandomPrefab()
    {
        if (placePrefabs == null || placePrefabs.Count == 0)
        {
            Debug.LogWarning("placePrefabs에 프리팹을 넣어주세요.");
            return;
        }

        int idx = Random.Range(0, placePrefabs.Count);
        GameObject prefab = placePrefabs[idx];
        Vector3 pos = spawnBase + Vector3.right * (placedStack.Count * spawnOffset);
        GameObject go = Instantiate(prefab, pos, Quaternion.identity);
        go.name = prefab.name + "_Inst";

        // 생성된 오브젝트를 스택에 Push (나중에 Undo 할 수 있게)
        placedStack.Push(go);
        Debug.Log($"Placed {go.name}. placedStack count = {placedStack.Count}");
    }

    private void UndoPlace()
    {
        if (placedStack.Count == 0)
        {
            Debug.Log("Undo: 제거할 오브젝트가 없습니다.");
            return;
        }

        // 가장 마지막에 생성한(맨 위) 오브젝트를 Pop하고 제거
        GameObject last = placedStack.Pop();
        if (last != null)
        {
            Destroy(last);
            Debug.Log($"Undo: Destroyed {last.name}. placedStack count = {placedStack.Count}");
        }
        else
        {
            Debug.LogWarning("Undo: 오브젝트가 이미 null 입니다.");
        }
    }

    // -----------------------------
    // uiStack 관련 메서드 (Back stack 예시)
    // -----------------------------
    private void PushUIPanel(string panelName)
    {
        // 새 패널을 푸시하면 그 패널이 "현재" 패널이 됨
        uiStack.Push(panelName);
        Debug.Log($"UI Push: {panelName}. Current = {uiStack.Peek()} (count={uiStack.Count})");

        // 실제로는 여기서 해당 패널을 활성화/비활성화 처리
        // 예: panel.SetActive(true); 이전 패널 숨김 등
    }

    private void PopUIPanel()
    {
        if (uiStack.Count <= 1)
        {
            Debug.Log("UI Back: 더이상 뒤로 갈 패널이 없습니다.");
            return;
        }

        // 현재 패널 제거(닫기)
        string closed = uiStack.Pop();
        string now = uiStack.Peek(); // 이전 패널이 이제 현재 패널
        Debug.Log($"UI Pop: Closed {closed}. Now showing {now} (count={uiStack.Count})");

        // 실제로는 closed 패널 비활성화, now 패널 활성화 등 처리
    }

    // -----------------------------
    // 디버그 유틸
    // -----------------------------
    private void PrintStacksStatus()
    {
        Debug.Log($"placedStack count = {placedStack.Count}, uiStack count = {uiStack.Count}");

        // placedStack 내용 출력 (복사해서 출력)
        if (placedStack.Count > 0)
        {
            Debug.Log("placedStack (top -> bottom):");
            foreach (var go in placedStack)
            {
                Debug.Log($" - {go?.name}");
            }
        }

        // uiStack 내용 출력 (top -> bottom)
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
