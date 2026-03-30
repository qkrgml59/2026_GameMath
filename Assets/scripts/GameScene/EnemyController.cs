using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;

    public float viewAngle = 90f;
    public float viewDistance = 8f;
    public float rotateSpeed = 45f;

    public float rushSpeed = 6f;
    public float attackDistance = 2f;

    bool isChasing = false;

    void Update()
    {
        if (!isChasing)
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            Detect();
        }
        else
        {
            Chase();
        }
    }


    void Detect()
    {
        Vector3 dir = (player.position - transform.position).normalized;

        float dot = Vector3.Dot(transform.forward, dir);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        float dist = Vector3.Distance(transform.position, player.position);

        if (angle < viewAngle * 0.5f && dist < viewDistance)
        {
            isChasing = true;
        }
    }

    void Chase()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * rushSpeed * Time.deltaTime;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist < attackDistance)
        {
            CheckParry(dir);
        }
    }

    void CheckParry(Vector3 enemyDir)
    {
        PlayerController pc = player.GetComponent<PlayerController>();

        Vector3 cross = Vector3.Cross(player.forward, enemyDir);
        bool isLeft = cross.y > 0;

        if (isLeft && pc.isParryLeft)
        {
            Destroy(gameObject);
        }
        else if (!isLeft && pc.isParryRight)
        {
            Destroy(gameObject);
        }
        else
        {
            // 실패 → 시작 위치로
            player.position = GameManager.instance.startPoint.position;
        }
    }
}