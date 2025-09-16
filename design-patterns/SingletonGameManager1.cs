using UnityEngine;

/// <summary>
/// SingletonGameManager는 싱글톤 패턴을 활용한 기본적인 게임 매니저 예제입니다.
/// - 씬이 바뀌어도 파괴되지 않도록 설정
/// - 다른 스크립트에서 GameManager.Instance 로 쉽게 접근 가능
/// </summary>
/// 
public class SingletonGameManager1 : MonoBehaviour
{
    //  인스턴스를 외부에서 접근할 수 있도록 static 프로퍼티로 선언
    public static SingletonGameManager1 Instance { get; private set; }

    //  게임 상태 예시용 필드
    public bool isGameStarted = false;
    public int playerScore = 0;

    //  Awake는 인스턴스 생성 및 중복 방지를 담당
    private void Awake()
    {
        // 이미 인스턴스가 존재할 경우 현재 오브젝트 제거
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 인스턴스가 없다면 현재 오브젝트를 할당
        Instance = this;

        // 씬이 바뀌어도 파괴되지 않게 설정
        DontDestroyOnLoad(gameObject);
    }

    //  게임 시작 함수
    public void StartGame()
    {
        isGameStarted = true;
        playerScore = 0;
        Debug.Log("게임이 시작되었습니다!");
    }

    //  점수 추가 함수
    public void AddScore(int amount)
    {
        playerScore += amount;
        Debug.Log("현재 점수: " + playerScore);
    }

    //  게임 종료 함수
    public void EndGame()
    {
        isGameStarted = false;
        Debug.Log("게임이 종료되었습니다!");
    }
}
