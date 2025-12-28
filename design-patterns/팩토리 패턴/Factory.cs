using UnityEngine;

/// <summary>
/// ===============================
/// 팩토리 패턴 (Factory Pattern)
/// ===============================
/// 
/// 목적:
/// - 객체 생성 로직을 한 곳(Factory)에 모은다
/// - new 키워드를 사용하는 책임을 분리한다
/// - 객체 생성 방식 변경 시, 사용 코드를 수정하지 않게 한다
/// 
/// Unity에서 주 사용처:
/// - 적 생성 (EnemyFactory)
/// - 아이템 생성 (ItemFactory)
/// - 작물 생성 (CropFactory)
/// - UI 패널 생성
/// 
/// 이 예제에서는:
/// - Enemy 타입 객체를 Factory가 생성한다
/// </summary>
public class Factory : MonoBehaviour
{
    void Start()
    {
        // 어떤 적을 만들지는 "타입"만 전달
        Enemy goblin = EnemyFactory.CreateEnemy(EnemyType.Goblin);
        goblin.Attack();

        Enemy orc = EnemyFactory.CreateEnemy(EnemyType.Orc);
        orc.Attack();
    }
}

/// <summary>
/// ===============================
/// 공통 부모 클래스 (또는 인터페이스)
/// ===============================
/// 
/// Factory 패턴의 핵심:
/// - "구체 클래스"가 아니라
/// - "공통 타입"으로 다룬다
/// </summary>
public abstract class Enemy
{
    public abstract void Attack();
}

/// <summary>
/// ===============================
/// 구체 클래스 1 : Goblin
/// ===============================
/// </summary>
public class Goblin : Enemy
{
    public override void Attack()
    {
        Debug.Log("고블린이 단검으로 공격한다!");
    }
}

/// <summary>
/// ===============================
/// 구체 클래스 2 : Orc
/// ===============================
/// </summary>
public class Orc : Enemy
{
    public override void Attack()
    {
        Debug.Log("오크가 도끼로 공격한다!");
    }
}

/// <summary>
/// ===============================
/// 생성 타입 구분용 enum
/// ===============================
/// 
/// - 어떤 객체를 만들지 구분하기 위한 용도
/// - 문자열보다 enum이 안전하고 관리가 쉽다
/// </summary>
public enum EnemyType
{
    Goblin,
    Orc
}

/// <summary>
/// ===============================
/// Factory 클래스 (핵심)
/// ===============================
/// 
/// 역할:
/// - 객체 생성 책임을 전담
/// - new 키워드가 여기만 존재
/// 
/// 장점:
/// - 새로운 Enemy 추가 시
///   Factory만 수정
/// - 사용부 코드는 수정 
/// </summary>
public static class EnemyFactory
{
    public static Enemy CreateEnemy(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Goblin:
                return new Goblin();

            case EnemyType.Orc:
                return new Orc();

            default:
                Debug.LogError("알 수 없는 Enemy 타입");
                return null;
        }
    }
}
