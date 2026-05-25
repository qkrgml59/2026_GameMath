using UnityEngine;

public class TargetBall : MonoBehaviour
{
    public bool isPlayer1Target;

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;

        if (rb == null)
            return;

        MouseRaycastTest gameManager =
            FindFirstObjectByType<MouseRaycastTest>();

        // 1P 공이 맞춘 경우
        if (rb == gameManager.player1Ball)
        {
            // 2P 타겟 맞추기
            if (!isPlayer1Target)
            {
                gameManager.AddScore(true);

                Destroy(gameObject);
            }
        }

        // 2P 공이 맞춘 경우
        if (rb == gameManager.player2Ball)
        {
            // 1P 타겟 맞추기
            if (isPlayer1Target)
            {
                gameManager.AddScore(false);

                Destroy(gameObject);
            }
        }
    }
}