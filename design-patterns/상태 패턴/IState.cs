using UnityEngine;

// IState: ���� ������ �⺻ �������̽�
// ��� ���´� �� �������̽��� �����ؾ� ��
public interface IState
{
    // ���¿� �� �� ȣ��
    void Enter();

    // ���°� ���ӵǴ� ���� �� ������ ȣ��
    void Update();

    // ���¸� ���� �� ȣ��
    void Exit();
}

