using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewTask : MonoBehaviour
{
    public Text legacyText; // ������ �� ��������� ����, ������� ����� ��������
    public string newText; // �������� �����
    public float delay = 1f; // �������� ����� ����������� �������� �������

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ��� �������� ������ ����� ��� "Player"
        if (other.CompareTag("Player"))
        {
            // ������������� ����� �����
            legacyText.text = newText;

            // ��������� �������� ��� ���������� ������� � ���������
            StartCoroutine(DisableAfterDelay());
        }
    }

    // �������� ��� ���������� ������� � ���������
    private IEnumerator DisableAfterDelay()
    {
        // ���� �������� ���������� ������
        yield return new WaitForSeconds(delay);

        // ��������� ������� ������
        gameObject.SetActive(false);
    }
}
