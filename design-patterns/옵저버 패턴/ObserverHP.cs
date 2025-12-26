using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ===============================================
/// 옵저버 패턴(Observer Pattern) - UI 기반 예제
/// ===============================================
/// 
/// [옵저버 패턴이란?]
/// - 어떤 객체(Subject)의 상태가 변경되면
/// - 그 변화를 구독(Observer)하고 있는 객체들에게
/// - 자동으로 알림을 보내는 디자인 패턴이다.
///
/// [이 예제의 목적]
/// - Player의 HP가 변경될 때
/// - UI 코드가 Player를 직접 참조하지 않고
/// - 이벤트를 통해 자동으로 UI를 갱신하도록 만든다.
///
/// [핵심 포인트]
/// 1. Player는 UI가 존재하는지 모른다.
/// 2. UI는 Player의 HP 변경 이벤트를 "구독"한다.
/// 3. 결합도가 낮아지고, 확장성이 좋아진다.
/// </summary>
public class ObserverHP : MonoBehaviour
{
    // =========================
    // Player (Subject 역할)
    // =========================

    /// <summary>
    /// Player의 최대 HP
    /// </summary>
    private int maxHP = 100;

    /// <summary>
    /// 현재 HP
    /// </summary>
    private int currentHP;

    /// <summary>
    /// 옵저버 패턴의 핵심
    /// HP가 변경되었을 때 호출되는 이벤트
    /// 
    /// int 매개변수:
    /// - 변경된 현재 HP 값을 전달
    /// </summary>
    public event Action<int> OnHPChanged;

    // =========================
    // UI (Observer 역할)
    // =========================

    /// <summary>
    /// HP를 표시할 UI Text
    /// (Canvas 안에 있는 Text 또는 TMP_Text 연결)
    /// </summary>
    [SerializeField] private Text hpText;

    // =========================
    // Unity Life Cycle
    // =========================

    private void Start()
    {
        // HP 초기화
        currentHP = maxHP;

        // 옵저버 등록 (구독)
        // HP가 변경되면 UpdateHPUI 메서드가 자동 호출
        OnHPChanged += UpdateHPUI;

        // 최초 UI 갱신
        OnHPChanged?.Invoke(currentHP);
    }

    private void OnDestroy()
    {
        // 옵저버 패턴에서 매우 중요 
        // 반드시 구독 해제하지 않으면
        // 메모리 누수 또는 에러 발생 가능
        OnHPChanged -= UpdateHPUI;
    }

    // =========================
    // Player Logic
    // =========================

    /// <summary>
    /// 데미지를 받는 메서드
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        // HP 변경 알림 (Notify)
        // Player는 "누가" 반응하는지 모름
        OnHPChanged?.Invoke(currentHP);
    }

    /// <summary>
    /// 회복 메서드
    /// </summary>
    public void Heal(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        // HP 변경 알림 (Notify)
        OnHPChanged?.Invoke(currentHP);
    }

    // =========================
    // UI Logic (Observer)
    // =========================

    /// <summary>
    /// HP 변경 시 자동으로 호출되는 UI 갱신 메서드
    /// 
    /// 이 메서드는 Player가 직접 호출하지 않는다.
    /// 오직 이벤트(OnHPChanged)를 통해 호출된다.
    /// </summary>
    private void UpdateHPUI(int hp)
    {
        hpText.text = $"HP : {hp}";
    }

    // =========================
    // 테스트용 입력
    // =========================

    private void Update()
    {
        // A 키 → 데미지
        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(10);
        }

        // H 키 → 회복
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(5);
        }
    }
}
