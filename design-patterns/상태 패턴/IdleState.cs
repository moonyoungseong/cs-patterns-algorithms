using UnityEngine;

// ---------------------
// 1) Idle 상태
// ---------------------
public class IdleState : IState
{
    public void Enter()
    {
        Debug.Log("Idle 상태 시작");
        // 애니메이션 Idle 재생 가능
    }

    public void Update()
    {
        // 입력 체크 등 행동 가능
        Debug.Log("Idle 상태 업데이트");
    }

    public void Exit()
    {
        Debug.Log("Idle 상태 종료");
    }
}

// ---------------------
// 2) Run 상태
// ---------------------
public class RunState : IState
{
    public void Enter()
    {
        Debug.Log("Run 상태 시작");
        // 애니메이션 Run 재생
    }

    public void Update()
    {
        // 이동 처리 가능
        Debug.Log("Run 상태 업데이트");
    }

    public void Exit()
    {
        Debug.Log("Run 상태 종료");
    }
}

// ---------------------
// 3) Attack 상태
// ---------------------
public class AttackState : IState
{
    public void Enter()
    {
        Debug.Log("Attack 상태 시작");
        // 공격 애니메이션 실행
    }

    public void Update()
    {
        // 공격 중 처리 가능
        Debug.Log("Attack 상태 업데이트");
    }

    public void Exit()
    {
        Debug.Log("Attack 상태 종료");
    }
}
