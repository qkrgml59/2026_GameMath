using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class PredictionLineRender : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public Transform enemyPos;
    public CameraSlerp cam;


    [Range(1f, 5f)] public float extend = 1.5f;

    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.widthMultiplier = 0.05f;
        lr.material = new Material(Shader.Find("Unlit/Color"))
        {
            color = Color.red
        };

    }

    public void Update()
    {
        if (!startPos || !endPos) return;
        

        Vector3 a = startPos.position;

        if (enemyPos !=null)
        {
            Vector3 b = enemyPos.position;
            Vector3 pred = Vector3.LerpUnclamped(a, b, extend);
            lr.SetPosition(0, a);
            lr.SetPosition(1, pred);
        }
        else if (endPos !=null)
        { 
            Vector3 b = endPos.position;
            Vector3 pred = Vector3.LerpUnclamped(a, b, extend);
            lr.SetPosition(0, a);
            lr.SetPosition(1, pred);
        }
     
    }

    public void OnRightClick(InputValue value)
    {
        if (!value.isPressed) return;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                enemyPos = hit.collider.transform;
               if(cam !=null)
               {
                    cam.target = enemyPos;
               }
            }
 
        }
        else
        {
            enemyPos = null;
            if (cam != null)
            {
                cam.target = null;
            }

        }
    }

}
