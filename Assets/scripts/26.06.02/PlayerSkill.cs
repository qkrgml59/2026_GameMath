using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkill : MonoBehaviour
{
    public GameObject bombPrefab;
    public GameObject minePrefab;

    public float throwPower = 10f;
    public float mineDistance = 3f;

    private void OnBomb()
    {
        FireBomb();
    }

    private void OnMine()
    {
        SpawnMine();
    }

    void FireBomb()
    {
        GameObject bomb = Instantiate(
            bombPrefab,
            transform.position + transform.forward,
            Quaternion.identity);

        ReflectTest bombScript = bomb.GetComponent<ReflectTest>();

        bombScript.velocity =
            transform.forward * throwPower +
            Vector3.up * 5f;
    }

    void SpawnMine()
    {
        Vector3 spawnPos =
            transform.position +
            transform.forward * mineDistance;

        Instantiate(minePrefab, spawnPos, Quaternion.identity);
    }
}