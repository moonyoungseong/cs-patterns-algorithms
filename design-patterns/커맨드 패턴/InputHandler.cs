using UnityEngine;
using System.Collections.Generic;

// 사용자의 입력을 감지하고, 해당하는 명령을 실행하는 클래스
public class InputHandler : MonoBehaviour
{
    public Transform player; // 이동시킬 플레이어 오브젝트 (에디터에서 연결)
    private Stack<ICommand> commandStack = new Stack<ICommand>(); // 실행된 명령들을 저장하는 스택 (Undo용)

    // 매 프레임마다 키 입력을 체크
    void Update()
    {
        // 방향키에 따라 다른 이동 명령을 생성하고 실행
        if (Input.GetKeyDown(KeyCode.W))
            ExecuteCommand(new MoveCommand(player, Vector3.forward));
        else if (Input.GetKeyDown(KeyCode.S))
            ExecuteCommand(new MoveCommand(player, Vector3.back));
        else if (Input.GetKeyDown(KeyCode.A))
            ExecuteCommand(new MoveCommand(player, Vector3.left));
        else if (Input.GetKeyDown(KeyCode.D))
            ExecuteCommand(new MoveCommand(player, Vector3.right));

        // Z 키를 누르면 마지막 명령을 취소 (Undo)
        else if (Input.GetKeyDown(KeyCode.Z))
            UndoLastCommand();
    }

    // 명령을 실행하고 스택에 저장
    void ExecuteCommand(ICommand command)
    {
        command.Execute();
        commandStack.Push(command); // Undo를 위해 스택에 저장
    }

    // 마지막 명령 취소
    void UndoLastCommand()
    {
        if (commandStack.Count > 0)
        {
            ICommand command = commandStack.Pop(); // 가장 최근 명령 가져오기
            command.Undo();                         // 실행 취소
        }
    }
}
