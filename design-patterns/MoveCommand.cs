using UnityEngine;

/// <summary>
/// ������ �����ϴ� Ŭ����.
/// �÷��̾ Ư�� �������� �̵���Ű�� ����� ĸ��ȭ�մϴ�.
/// </summary>
/// 

// MoveCommand�� �÷��̾ Ư�� �������� �̵���Ű�� ����� ��Ÿ��
public class MoveCommand : ICommand
{
    private Transform _player;       // �̵���ų ��� (Receiver)
    private Vector3 _direction;      // �̵� ���� (��: Vector3.forward)

    // �����ڿ��� ���� ������ �޾� ����
    public MoveCommand(Transform player, Vector3 direction)
    {
        _player = player;
        _direction = direction;
    }

    // Execute: ������ �÷��̾ �ش� �������� �̵���Ŵ
    public void Execute()
    {
        _player.position += _direction;
    }

    // Undo: ���� �̵��� �ǵ��� (�ݴ� �������� �̵�)
    public void Undo()
    {
        _player.position -= _direction;
    }
}