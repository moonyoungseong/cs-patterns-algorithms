using UnityEngine;

public class SingletonGameManager2 : MonoBehaviour
{
    private void Start()
    {
        // 게임 시작
        SingletonGameManager1.Instance.StartGame();

        // 점수 추가
        SingletonGameManager1.Instance.AddScore(10);

        // 게임 종료
        SingletonGameManager1.Instance.EndGame();
    }
}
