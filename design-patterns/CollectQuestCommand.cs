using UnityEngine;

/// <summary>
/// Ŀ�ǵ� ������ �⺻ �������̽� (��� ����Ʈ ����� ���� ��Ģ)
/// </summary>
public interface IQuestCommand  // 1. IQuestCommand  ��Ģ ����
{
    void Execute(); // ����Ʈ ����
    bool IsCompleted(); // ����Ʈ �Ϸ� ����
}

/// <summary>
/// ���� ������ ����Ʈ�� ��ü���� ��� Ŭ����
/// (��: �丶�� 3�� ����)
/// </summary>
public class CollectQuestCommand : IQuestCommand        // 2. CollectQuestCommand  ���� ����Ʈ ���� ����
{
    private string itemName; // �����ؾ� �ϴ� ������ �̸�
    private int targetAmount; // ��ǥ ����
    private int currentAmount; // ���� ���� ����

    public CollectQuestCommand(string itemName, int targetAmount)
    {
        this.itemName = itemName;
        this.targetAmount = targetAmount;
        currentAmount = 0;
    }

    /// <summary>
    /// �������� �������� �� ���� (����Ʈ ��ô)
    /// </summary>
    public void Execute()
    {
        if (!IsCompleted())
        {
            currentAmount++;
            Debug.Log($"{itemName}��(��) �����߽��ϴ�. ({currentAmount}/{targetAmount})");

            if (IsCompleted())
            {
                Debug.Log($"{itemName} ���� ����Ʈ �Ϸ�!");
            }
        }
    }

    /// <summary>
    /// ����Ʈ �Ϸ� ���� Ȯ��
    /// </summary>
    public bool IsCompleted()
    {
        return currentAmount >= targetAmount;
    }
}

/// <summary>
/// ����Ʈ�� �����Ű�� ��ü (Invoker)
/// </summary>
public class QuestInvoker : MonoBehaviour   // 3.QuestInvoker  ����� �����ϴ� ��ü
{
    private IQuestCommand currentCommand;

    // Ư�� ����Ʈ ��� ����
    public void SetCommand(IQuestCommand command)
    {
        currentCommand = command;
    }

    // ����Ʈ ��� ���� (��: ������ ���� ��)
    public void ExecuteCommand()
    {
        if (currentCommand != null)
        {
            currentCommand.Execute();
        }
    }
}

/// <summary>
/// ���� ���� ������ Ȯ���ϴ� ��ũ��Ʈ
/// (����Ƽ ���� �ٿ��� �׽�Ʈ ����)
/// </summary>
public class QuestExample : MonoBehaviour
{
    private QuestInvoker invoker;
    private IQuestCommand tomatoQuest;

    void Start()
    {
        invoker = gameObject.AddComponent<QuestInvoker>();

        // "�丶�� 3�� ����" ����Ʈ ����
        tomatoQuest = new CollectQuestCommand("�丶��", 3);
        invoker.SetCommand(tomatoQuest);

        // �������� 3�� �����ϴ� �ùķ��̼�
        invoker.ExecuteCommand(); // 1ȸ ����
        invoker.ExecuteCommand(); // 2ȸ ����
        invoker.ExecuteCommand(); // 3ȸ ���� �� �Ϸ�
    }
}
