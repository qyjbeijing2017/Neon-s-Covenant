using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSC_CameraFollow : MonoBehaviour
{
    [Tooltip("主角色")]
    [SerializeField] Player player;
    [Tooltip("摄像机开始跟随的最小距离")]
    [SerializeField] float dieRange;
    [Tooltip("摄像机跟随速度")]
    [SerializeField] float cameraSpeed;
    Vector3 startVectorL;

    // Use this for initialization
    void Start()
    {
        startVectorL = new Vector3(transform.position.x - player.transform.position.x, 0, transform.position.z - player.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!player.dead)
        {
            cameraMove();
        }

    }

    void cameraMove()
    {
        Vector3 followPoint = new Vector3((player.transform.position + startVectorL).x, transform.position.y, (player.transform.position + startVectorL).z);
        if((transform.position - followPoint).magnitude > dieRange)
        {
            transform.position += ((transform.position - followPoint).magnitude - dieRange) * cameraSpeed * (followPoint - transform.position) * Time.deltaTime;
        }
    }
}
