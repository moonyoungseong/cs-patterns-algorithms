using System.Collections.Generic; // List<T>�� ����Ϸ��� �ʼ�
using UnityEngine;

/// <summary>
/// ��(Enemy)���� ����Ʈ�� �����ϴ� ���� Ŭ����
/// </summary>
public class EnemyList : MonoBehaviour
{
    // ���� ���� �����ϴ� ��� ���� ��� ����Ʈ
    private List<Enemy> enemies = new List<Enemy>();

    void Start()
    {
        // ���÷� 3���� �� ���� (Instantiate ��� ���� ��ü ����)
        enemies.Add(new Enemy("������", 10));
        enemies.Add(new Enemy("���", 20));
        enemies.Add(new Enemy("��ũ", 30));

        Debug.Log("�� ����Ʈ �ʱ�ȭ �Ϸ�");
    }

    void Update()
    {
        // �� �����Ӹ��� �� ����Ʈ ������Ʈ (���� �� ���� ��)
        UpdateEnemies();
    }

    /// <summary>
    /// ����Ʈ�� ��� ���� ��ȸ�ϸ� ���� ����
    /// </summary>
    void UpdateEnemies()
    {
        // ���� �׾����� �˻� �� ���� (�ڿ������� ��ȸ!)
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            enemies[i].UpdateEnemy();

            if (enemies[i].IsDead)
            {
                Debug.Log($"{enemies[i].name}��(��) �׾����ϴ�.");
                enemies.RemoveAt(i); // ����Ʈ���� ����
            }
        }
    }

    /// <summary>
    /// ���ο� ���� ����Ʈ�� �߰�
    /// </summary>
    public void AddEnemy(Enemy newEnemy)
    {
        enemies.Add(newEnemy);
        Debug.Log($"{newEnemy.name}��(��) ������ �շ��߽��ϴ�!");
    }

    /// <summary>
    /// ����Ʈ �� ���� ���� Ȯ��
    /// </summary>
    public int GetEnemyCount()
    {
        return enemies.Count;
    }
}

/// <summary>
/// ������ Enemy Ŭ���� (���� ���ӿ��� ���� ��ũ��Ʈ�� ����)
/// </summary>
public class Enemy
{
    public string name;   // �� �̸�
    public int hp;        // ü��
    public bool IsDead => hp <= 0; // ���� ���� (������Ƽ)

    public Enemy(string name, int hp)
    {
        this.name = name;
        this.hp = hp;
    }

    /// <summary>
    /// ���� ���¸� �����ϴ� �޼��� (���⼱ �ܼ��� ü�� ���� ����)
    /// </summary>
    public void UpdateEnemy()
    {
        hp -= 1; // �� �����Ӹ��� 1�� ü�� ���� (���ÿ�)
        Debug.Log($"{name}�� ���� ü��: {hp}");
    }
}
