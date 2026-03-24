using TMPro;
using UnityEngine;

public class DotExample : MonoBehaviour
{
    public GameObject enemy;
    public Vector3 enemyScale;

    public Transform player;
    public float viewAngle = 60f; //시야각

    void Update()
    {
        FindPlayer();
    }
    void FindPlayer()
    {
        enemyScale = enemy.transform.localScale;
        float distance = (player.position - transform.position).magnitude;

        Vector3 toPlayer = (player.position - transform.position).normalized;
        Vector3 forward = transform.forward;

        //float dot = Vector3.Dot(forward, toPlayer);
        float DotProduct(Vector3 forward, Vector3 toPlayer)
        {
            return forward.x * toPlayer.x + forward.y * toPlayer.y + forward.z * toPlayer.z;
        }
        float angle = Mathf.Acos(DotProduct(forward, toPlayer)) * Mathf.Rad2Deg; //내적을 각도로 변환

        if (angle < viewAngle / 2 && distance < 3f)
        {
            enemyScale = enemyScale * 2f;
            Debug.Log("플레이어가 시야 안에 있음.");
        }
    }


}
