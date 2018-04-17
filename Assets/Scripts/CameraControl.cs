using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{

    public Player_new player;
    Vector3 targetPoint;
    public float deadR;
    public float maxR;
    public Vector3 targetPointOffset;
    Vector3 starVector;
  

    void Start()
    {
        
        starVector = transform.position - player.transform.position;
        targetPoint = player.transform.position + starVector + targetPointOffset;


    }

    private void Update()
    {

    }

    public void cameraMove()
    {
        if (Vector3.Magnitude(transform.position - targetPoint) > deadR)
        {


            float speedCamera = (Vector3.Magnitude(transform.position - targetPoint) - deadR) / (maxR - deadR) * Mathf.Cos(45 * Mathf.PI / 180) * player.moveSpeed;


            this.transform.position += (targetPoint - transform.position).normalized * speedCamera;


        }

        targetPoint = player.transform.position + starVector + targetPointOffset;
    }
}