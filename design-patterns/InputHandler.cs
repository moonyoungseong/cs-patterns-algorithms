using UnityEngine;
using System.Collections.Generic;

// ������� �Է��� �����ϰ�, �ش��ϴ� ����� �����ϴ� Ŭ����
public class InputHandler : MonoBehaviour
{
    public Transform player; // �̵���ų �÷��̾� ������Ʈ (�����Ϳ��� ����)
    private Stack<ICommand> commandStack = new Stack<ICommand>(); // ����� ��ɵ��� �����ϴ� ���� (Undo��)

    // �� �����Ӹ��� Ű �Է��� üũ
    void Update()
    {
        // ����Ű�� ���� �ٸ� �̵� ����� �����ϰ� ����
        if (Input.GetKeyDown(KeyCode.W))
            ExecuteCommand(new MoveCommand(player, Vector3.forward));
        else if (Input.GetKeyDown(KeyCode.S))
            ExecuteCommand(new MoveCommand(player, Vector3.back));
        else if (Input.GetKeyDown(KeyCode.A))
            ExecuteCommand(new MoveCommand(player, Vector3.left));
        else if (Input.GetKeyDown(KeyCode.D))
            ExecuteCommand(new MoveCommand(player, Vector3.right));

        // Z Ű�� ������ ������ ����� ��� (Undo)
        else if (Input.GetKeyDown(KeyCode.Z))
            UndoLastCommand();
    }

    // ����� �����ϰ� ���ÿ� ����
    void ExecuteCommand(ICommand command)
    {
        command.Execute();
        commandStack.Push(command); // Undo�� ���� ���ÿ� ����
    }

    // ������ ��� ���
    void UndoLastCommand()
    {
        if (commandStack.Count > 0)
        {
            ICommand command = commandStack.Pop(); // ���� �ֱ� ��� ��������
            command.Undo();                         // ���� ���
        }
    }
}
