using UnityEngine;

/// <summary>
/// SingletonGameManager�� �̱��� ������ Ȱ���� �⺻���� ���� �Ŵ��� �����Դϴ�.
/// - ���� �ٲ� �ı����� �ʵ��� ����
/// - �ٸ� ��ũ��Ʈ���� GameManager.Instance �� ���� ���� ����
/// </summary>
/// 
public class SingletonGameManager1 : MonoBehaviour
{
    //  �ν��Ͻ��� �ܺο��� ������ �� �ֵ��� static ������Ƽ�� ����
    public static SingletonGameManager1 Instance { get; private set; }

    //  ���� ���� ���ÿ� �ʵ�
    public bool isGameStarted = false;
    public int playerScore = 0;

    //  Awake�� �ν��Ͻ� ���� �� �ߺ� ������ ���
    private void Awake()
    {
        // �̹� �ν��Ͻ��� ������ ��� ���� ������Ʈ ����
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // �ν��Ͻ��� ���ٸ� ���� ������Ʈ�� �Ҵ�
        Instance = this;

        // ���� �ٲ� �ı����� �ʰ� ����
        DontDestroyOnLoad(gameObject);
    }

    //  ���� ���� �Լ�
    public void StartGame()
    {
        isGameStarted = true;
        playerScore = 0;
        Debug.Log("������ ���۵Ǿ����ϴ�!");
    }

    //  ���� �߰� �Լ�
    public void AddScore(int amount)
    {
        playerScore += amount;
        Debug.Log("���� ����: " + playerScore);
    }

    //  ���� ���� �Լ�
    public void EndGame()
    {
        isGameStarted = false;
        Debug.Log("������ ����Ǿ����ϴ�!");
    }
}
