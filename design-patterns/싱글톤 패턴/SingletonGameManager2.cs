using UnityEngine;

public class SingletonGameManager2 : MonoBehaviour
{
    private void Start()
    {
        // ���� ����
        SingletonGameManager1.Instance.StartGame();

        // ���� �߰�
        SingletonGameManager1.Instance.AddScore(10);

        // ���� ����
        SingletonGameManager1.Instance.EndGame();
    }
}
