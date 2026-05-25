using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MouseRaycastTest : MonoBehaviour
{
    public Rigidbody player1Ball;
    public Rigidbody player2Ball;

    public bool isPlayerturn = true;
    public bool canShoot = true;

    public float rayDistance = 100f;
    public float hitPower = 2f;

    public int player1Score;
    public int player2Score;

    public TextMeshProUGUI turnText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    float moveInput;

    public CameraOrbit cam;

    public void OnMouse(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        moveInput = input.x;

        cam.moveInput = moveInput;
    }

    void Start()
    {
        cam.target = player1Ball.transform;

        UpdateUI();

        gameOverText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!canShoot)
        {
            if (player1Ball.linearVelocity.magnitude < 0.5f &&
                player2Ball.linearVelocity.magnitude < 0.5f)
            {
                canShoot = true;

                isPlayerturn = !isPlayerturn;

                if (isPlayerturn)
                {
                    cam.target = player1Ball.transform;
                }
                else
                {
                    cam.target = player2Ball.transform;
                }

                UpdateUI();
            }
        }
    }

    public void OnClick(InputValue value)
    {
        if (!value.isPressed || !canShoot)
            return;

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            Rigidbody rb = hit.collider.attachedRigidbody;

            if (rb == null)
                return;

            // 1P 턴이면 1P 공만
            if (isPlayerturn && rb != player1Ball)
                return;

            // 2P 턴이면 2P 공만
            if (!isPlayerturn && rb != player2Ball)
                return;

            Vector3 hitPoint = hit.point;

            Vector3 center = rb.gameObject.transform.position;

            Vector3 forceDirection = center - hitPoint;

            forceDirection.y = 0f;

            forceDirection.Normalize();

            rb.AddForce(forceDirection * hitPower, ForceMode.Impulse);

            canShoot = false;
        }
    }

    public void AddScore(bool isPlayer1)
    {
        if (isPlayer1)
        {
            player1Score++;
        }
        else
        {
            player2Score++;
        }

        UpdateUI();

        CheckGameOver();
    }

    public void MinusScore(bool isPlayer1)
    {
        if (isPlayer1)
        {
            if (player1Score > 0)
            {
                player1Score--;
            }
        }
        else
        {
            if (player2Score > 0)
            {
                player2Score--;
            }
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (isPlayerturn)
        {
            turnText.text = "1P TURN";
        }
        else
        {
            turnText.text = "2P TURN";
        }

        scoreText.text =
            "1P : " + player1Score +
            " | " +
            "2P : " + player2Score;
    }

    void CheckGameOver()
    {
        if (player1Score >= 5)
        {
            gameOverText.gameObject.SetActive(true);

            gameOverText.text = "1P WIN";

            canShoot = false;
        }

        if (player2Score >= 5)
        {
            gameOverText.gameObject.SetActive(true);

            gameOverText.text = "2P WIN";

            canShoot = false;
        }
    }
}