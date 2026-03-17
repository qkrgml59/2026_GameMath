using TMPro;
using UnityEngine;

public class DotExample : MonoBehaviour
{
    public Transform player;

    public float viewAngle = 60f;      //시야각
    public float MaxViewdistance = 15;

    float DotProduct(Vector3 toPlayer, Vector3 forward)
    {
        return toPlayer.x * forward.x + toPlayer.y * forward.y + toPlayer.z * forward.z;
    }



    // Update is called once per frame
    void Update()
    {
        


        Vector3 toPlayer = (player.position - transform.position).normalized;
        Vector3 forward = transform.forward;

        DotProduct(toPlayer, forward);

        if (DotProduct(toPlayer,forward) < viewAngle /2)
        {
            Debug.Log("플레이어가 시야 안에 있음");
        }


    }


}
