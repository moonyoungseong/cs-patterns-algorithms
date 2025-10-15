using UnityEngine;

public class Player : MonoBehaviour
{
    // 현재 상태를 저장
    private IState currentState;

    // 상태 변경 메서드
    public void SetState(IState newState)
    {
        // 기존 상태가 있으면 Exit 호출
        if (currentState != null)
            currentState.Exit();

        // 새로운 상태로 교체
        currentState = newState;

        // 새로운 상태 시작
        currentState.Enter();
    }

    // 매 프레임 Update에서 현재 상태 Update 호출
    void Update()
    {
        if (currentState != null)
            currentState.Update();

        // 예시: 입력에 따라 상태 변경
        if (Input.GetKeyDown(KeyCode.W))
        {
            SetState(new RunState());
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            SetState(new AttackState());
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SetState(new IdleState());
        }
    }
}
