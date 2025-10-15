using UnityEngine;

// IState: 상태 패턴의 기본 인터페이스
// 모든 상태는 이 인터페이스를 구현해야 함
public interface IState
{
    // 상태에 들어갈 때 호출
    void Enter();

    // 상태가 지속되는 동안 매 프레임 호출
    void Update();

    // 상태를 나갈 때 호출
    void Exit();
}

