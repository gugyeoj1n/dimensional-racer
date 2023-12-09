using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car1move : MonoBehaviour
{
    private float moveSpeed; // �̵� �ӵ�
    public Vector3 moveDirection = Vector3.forward; // ������ ���� (�⺻������ ������)

    void Start()
    {
        // ������ �ӵ� ����
        moveSpeed = Random.Range(10.0f, 40.0f);
    }

    void Update()
    {
        // �̵� ����� �ӵ��� ���Ͽ� �̵� ���� ���
        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;

        // �̵� ���͸� ���� ��ġ�� �����־� ������Ʈ �̵�
        transform.Translate(movement);
    }
}

