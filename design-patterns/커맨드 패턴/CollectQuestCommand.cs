using UnityEngine;

/// <summary>
/// 커맨드 패턴의 기본 인터페이스 (모든 퀘스트 명령의 공통 규칙)
/// </summary>
public interface IQuestCommand  // 1. IQuestCommand  규칙 선언
{
    void Execute(); // 퀘스트 진행
    bool IsCompleted(); // 퀘스트 완료 여부
}

/// <summary>
/// 실제 수집형 퀘스트의 구체적인 명령 클래스
/// (예: 토마토 3개 수집)
/// </summary>
public class CollectQuestCommand : IQuestCommand        // 2. CollectQuestCommand  실제 퀘스트 로직 구현
{
    private string itemName; // 수집해야 하는 아이템 이름
    private int targetAmount; // 목표 수량
    private int currentAmount; // 현재 수집 수량

    public CollectQuestCommand(string itemName, int targetAmount)
    {
        this.itemName = itemName;
        this.targetAmount = targetAmount;
        currentAmount = 0;
    }

    /// <summary>
    /// 아이템을 수집했을 때 실행 (퀘스트 진척)
    /// </summary>
    public void Execute()
    {
        if (!IsCompleted())
        {
            currentAmount++;
            Debug.Log($"{itemName}을(를) 수집했습니다. ({currentAmount}/{targetAmount})");

            if (IsCompleted())
            {
                Debug.Log($"{itemName} 수집 퀘스트 완료!");
            }
        }
    }

    /// <summary>
    /// 퀘스트 완료 조건 확인
    /// </summary>
    public bool IsCompleted()
    {
        return currentAmount >= targetAmount;
    }
}

/// <summary>
/// 퀘스트를 실행시키는 주체 (Invoker)
/// </summary>
public class QuestInvoker : MonoBehaviour   // 3.QuestInvoker  명령을 실행하는 주체
{
    private IQuestCommand currentCommand;

    // 특정 퀘스트 명령 설정
    public void SetCommand(IQuestCommand command)
    {
        currentCommand = command;
    }

    // 퀘스트 명령 실행 (예: 아이템 수집 시)
    public void ExecuteCommand()
    {
        if (currentCommand != null)
        {
            currentCommand.Execute();
        }
    }
}

/// <summary>
/// 실제 예시 동작을 확인하는 스크립트
/// (유니티 씬에 붙여서 테스트 가능)
/// </summary>
public class QuestExample : MonoBehaviour
{
    private QuestInvoker invoker;
    private IQuestCommand tomatoQuest;

    void Start()
    {
        invoker = gameObject.AddComponent<QuestInvoker>();

        // "토마토 3개 수집" 퀘스트 생성
        tomatoQuest = new CollectQuestCommand("토마토", 3);
        invoker.SetCommand(tomatoQuest);

        // 아이템을 3번 수집하는 시뮬레이션
        invoker.ExecuteCommand(); // 1회 수집
        invoker.ExecuteCommand(); // 2회 수집
        invoker.ExecuteCommand(); // 3회 수집 → 완료
    }
}
