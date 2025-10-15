using UnityEngine;

/// <summary>
/// 역할을 수행하는 클래스.
/// 플레이어를 특정 방향으로 이동시키는 명령을 캡슐화합니다.
/// </summary>
/// 

// MoveCommand는 플레이어를 특정 방향으로 이동시키는 명령을 나타냄
public class MoveCommand : ICommand
{
    private Transform _player;       // 이동시킬 대상 (Receiver)
    private Vector3 _direction;      // 이동 방향 (예: Vector3.forward)

    // 생성자에서 대상과 방향을 받아 저장
    public MoveCommand(Transform player, Vector3 direction)
    {
        _player = player;
        _direction = direction;
    }

    // Execute: 실제로 플레이어를 해당 방향으로 이동시킴
    public void Execute()
    {
        _player.position += _direction;
    }

    // Undo: 이전 이동을 되돌림 (반대 방향으로 이동)
    public void Undo()
    {
        _player.position -= _direction;
    }
}