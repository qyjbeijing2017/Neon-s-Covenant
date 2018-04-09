using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{

    MainCharacterBehaviour mainCharacterBehaviour;
    Vector3 targetPoint;
    public float deadR;
    public float maxR;
    public Vector3 targetPointOffset;
    Vector3 starVector;
  

    void Start()
    {
        mainCharacterBehaviour = FindObjectOfType<MainCharacterBehaviour>();
        starVector = transform.position - mainCharacterBehaviour.transform.position;
        targetPoint = mainCharacterBehaviour.transform.position + starVector + targetPointOffset;

    }

    private void Update()
    {
        if (Vector3.Magnitude(transform.position - targetPoint) > deadR)
        {
            

            float speedCamera = (Vector3.Magnitude(transform.position - targetPoint) - deadR) / (maxR - deadR) * Mathf.Cos(45 * Mathf.PI / 180) * mainCharacterBehaviour.moveSpeed;


            this.transform.position += (targetPoint - transform.position).normalized * speedCamera;


        }

        targetPoint = mainCharacterBehaviour.transform.position + starVector + targetPointOffset;
    }
}