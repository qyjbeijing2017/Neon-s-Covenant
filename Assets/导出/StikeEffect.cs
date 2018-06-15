using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StikeEffect : MonoBehaviour {

	[SerializeField] GameObject[] Cube;
	[SerializeField] GameObject[] Stickeffect;

	Player player;



	public void Reflash(int Num){
		
		if(player.power.m_colorType == NSC_Color.colorType.white){
			
			Instantiate(Stickeffect[0], Cube[Num].transform.position, Cube[Num].transform.rotation);
			//Debug.Break(); 
		}

		else if(player.power.m_colorType == NSC_Color.colorType.red){
			Instantiate(Stickeffect[1], Cube[Num].transform.position, Cube[Num].transform.rotation);
		}

		else if(player.power.m_colorType == NSC_Color.colorType.cyan){
			Instantiate(Stickeffect[2], Cube[Num].transform.position, Cube[Num].transform.rotation);
		}

		//Debug.Break();
	}
	

	// Use this for initialization
	void Start () {
		player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
