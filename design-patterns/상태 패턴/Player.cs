using UnityEngine;

public class Player : MonoBehaviour
{
    // ���� ���¸� ����
    private IState currentState;

    // ���� ���� �޼���
    public void SetState(IState newState)
    {
        // ���� ���°� ������ Exit ȣ��
        if (currentState != null)
            currentState.Exit();

        // ���ο� ���·� ��ü
        currentState = newState;

        // ���ο� ���� ����
        currentState.Enter();
    }

    // �� ������ Update���� ���� ���� Update ȣ��
    void Update()
    {
        if (currentState != null)
            currentState.Update();

        // ����: �Է¿� ���� ���� ����
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
