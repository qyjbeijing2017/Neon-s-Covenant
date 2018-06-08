using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Attack))]
public class LightPower : MonoBehaviour {
    [Tooltip("初始爆光能时的y轴长度，y轴与2d平面向量组成初始角度，height越大角度越接近地面法线"),SerializeField]
    float height;
    [Tooltip("发射速度"),SerializeField]
    float speed;
    [Tooltip("冲向玩家速度")]
    float speedFly;
    [Tooltip("玩家吸收距离")]
    float dis;

    bool startToPlayer;
    Player player;

	// Use this for initialization
	void Start () {
        startToPlayer = false;
        player = FindObjectOfType<Player>();
        Vector2 ground = new Vector2(Random.Range(0, 1), Random.Range(0, 1)).normalized;
        GetComponent<Rigidbody>().velocity = speed * new Vector3(ground.x, height, ground.y).normalized;
        GetComponent<Rigidbody>().useGravity = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void FixedUpdate()
    {
        if((player.transform.position - transform.position).magnitude < dis)
        {
            startToPlayer = true;
            GetComponent<Rigidbody>().useGravity = false;
            
        }

        if (startToPlayer)
        {
            GetComponent<Rigidbody>().velocity = (player.transform.position - transform.position) * speedFly * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().injured(GetComponent<Attack>());
            Destroy(gameObject);
        }
    }


}
