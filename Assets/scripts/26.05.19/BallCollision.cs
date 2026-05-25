using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public bool isPlayer1Ball;

    void OnCollisionEnter(Collision collision)
    {
        BallCollision other =
            collision.gameObject.GetComponent<BallCollision>();

        if (other == null)
            return;

        if (other.isPlayer1Ball != isPlayer1Ball)
        {
            MouseRaycastTest gameManager =
                FindFirstObjectByType<MouseRaycastTest>();

            gameManager.MinusScore(isPlayer1Ball);
        }
    }
}