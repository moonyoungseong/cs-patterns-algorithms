using System.Collections.Generic; // List<T>를 사용하려면 필수
using UnityEngine;

/// <summary>
/// 적(Enemy)들을 리스트로 관리하는 예시 클래스
/// </summary>
public class EnemyList : MonoBehaviour
{
    // 현재 씬에 존재하는 모든 적을 담는 리스트
    private List<Enemy> enemies = new List<Enemy>();

    void Start()
    {
        // 예시로 3명의 적 생성 (Instantiate 대신 가상 객체 생성)
        enemies.Add(new Enemy("슬라임", 10));
        enemies.Add(new Enemy("고블린", 20));
        enemies.Add(new Enemy("오크", 30));

        Debug.Log("적 리스트 초기화 완료");
    }

    void Update()
    {
        // 매 프레임마다 적 리스트 업데이트 (죽은 적 제거 등)
        UpdateEnemies();
    }

    /// <summary>
    /// 리스트의 모든 적을 순회하며 상태 갱신
    /// </summary>
    void UpdateEnemies()
    {
        // 적이 죽었는지 검사 후 제거 (뒤에서부터 순회!)
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            enemies[i].UpdateEnemy();

            if (enemies[i].IsDead)
            {
                Debug.Log($"{enemies[i].name}이(가) 죽었습니다.");
                enemies.RemoveAt(i); // 리스트에서 제거
            }
        }
    }

    /// <summary>
    /// 새로운 적을 리스트에 추가
    /// </summary>
    public void AddEnemy(Enemy newEnemy)
    {
        enemies.Add(newEnemy);
        Debug.Log($"{newEnemy.name}이(가) 전투에 합류했습니다!");
    }

    /// <summary>
    /// 리스트 내 적의 수를 확인
    /// </summary>
    public int GetEnemyCount()
    {
        return enemies.Count;
    }
}

/// <summary>
/// 간단한 Enemy 클래스 (실제 게임에선 별도 스크립트로 존재)
/// </summary>
public class Enemy
{
    public string name;   // 적 이름
    public int hp;        // 체력
    public bool IsDead => hp <= 0; // 죽음 판정 (프로퍼티)

    public Enemy(string name, int hp)
    {
        this.name = name;
        this.hp = hp;
    }

    /// <summary>
    /// 적의 상태를 갱신하는 메서드 (여기선 단순히 체력 감소 예시)
    /// </summary>
    public void UpdateEnemy()
    {
        hp -= 1; // 매 프레임마다 1씩 체력 감소 (예시용)
        Debug.Log($"{name}의 현재 체력: {hp}");
    }
}
