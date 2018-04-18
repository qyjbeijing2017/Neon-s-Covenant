using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAll : MonoBehaviour {

	// Use this for initialization

    public enum type
    {
        player = 0,
        boss = 1,
        enemy = 2

    }

    public type myType;
    public float damage;
    public int colorType;
    public float speed;
    public float destoryTime;
    [SerializeField] private Color white;
    [SerializeField] private Color black;
    [SerializeField] private Color red;
    [SerializeField] private Color cyan;
    [SerializeField] private Material material;
    [SerializeField] private float bulletVelocity;

    void Start () {

        //Debug.Break();
        if (colorType == 0)
        {
            material.color = white;
        }
        else if(colorType == 1)
        {
            material.color = red;
        }
        else if (colorType == 2)
        {
            material.color = cyan;
        }
        else if (colorType == 3)
        {
            material.color = black;
        }
        Destroy(gameObject, destoryTime);
        GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * bulletVelocity;


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //if(other.)
        //Destroy(gameObject);
    }
}
