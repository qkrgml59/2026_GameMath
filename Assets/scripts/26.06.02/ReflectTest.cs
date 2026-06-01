using UnityEngine;

public class ReflectTest : MonoBehaviour
{
    public Vector3 velocity = new Vector3(2f, -3f, 0f);

    public Vector3 gravity = new Vector3(0, -9.81f, 0);

    float damping = .9f;

    int bounceCount = 0;


    private void Update()
    {
        velocity += gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Explode();
            return;
        }

        if (col.gameObject.CompareTag("Ground"))
        {
            bounceCount++;

            if (bounceCount >= 3)
            {
                Explode();
                return;
            }

        }

        if (col.gameObject.CompareTag("Ground"))
        {
            bounceCount++;

            if (bounceCount >= 3)
            {
                Explode();
                return;
            }

            Vector3 normal = col.contacts[0].normal.normalized;

            float dot = Vector3.Dot(velocity, normal);

            Vector3 reflect =
                velocity - 2f * dot * normal;

            velocity = reflect * damping;
        }
    }

    void Explode()
    {
        Debug.Log("Æø¹ß");

        Destroy(gameObject);
    }
}
