using UnityEngine;

// ---------------------
// 1) Idle ����
// ---------------------
public class IdleState : IState
{
    public void Enter()
    {
        Debug.Log("Idle ���� ����");
        // �ִϸ��̼� Idle ��� ����
    }

    public void Update()
    {
        // �Է� üũ �� �ൿ ����
        Debug.Log("Idle ���� ������Ʈ");
    }

    public void Exit()
    {
        Debug.Log("Idle ���� ����");
    }
}

// ---------------------
// 2) Run ����
// ---------------------
public class RunState : IState
{
    public void Enter()
    {
        Debug.Log("Run ���� ����");
        // �ִϸ��̼� Run ���
    }

    public void Update()
    {
        // �̵� ó�� ����
        Debug.Log("Run ���� ������Ʈ");
    }

    public void Exit()
    {
        Debug.Log("Run ���� ����");
    }
}

// ---------------------
// 3) Attack ����
// ---------------------
public class AttackState : IState
{
    public void Enter()
    {
        Debug.Log("Attack ���� ����");
        // ���� �ִϸ��̼� ����
    }

    public void Update()
    {
        // ���� �� ó�� ����
        Debug.Log("Attack ���� ������Ʈ");
    }

    public void Exit()
    {
        Debug.Log("Attack ���� ����");
    }
}
