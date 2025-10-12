using UnityEngine;

/// <summary>
/// 커맨드 패턴(Command Pattern)
/// 
/// 커맨드 패턴은 요청을 객체로 캡슐화하여,
/// 서로 다른 요청, 큐잉, 로그 기록, 실행 취소(Undo) 등을 지원할 수 있도록 해주는 디자인 패턴입니다.
///
/// 핵심 구성 요소:
/// - Command (인터페이스): 실행할 명령의 인터페이스 정의
/// - ConcreteCommand: 실제 실행 내용을 담은 클래스
/// - Receiver: 실제 동작을 수행하는 객체
/// - Invoker: 명령을 요청하는 객체
/// - Client: 명령 객체를 생성하고 설정하는 클라이언트
/// 
/// Unity에서는 입력 처리, 행동 기록, Undo/Redo 기능 등에 유용하게 사용됩니다.
/// </summary>

// 모든 명령(Command)이 따라야 할 인터페이스
public interface ICommand
{
    void Execute(); // 명령 실행
    void Undo();    // 명령 취소 (되돌리기)
}
