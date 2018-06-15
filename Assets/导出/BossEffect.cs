using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffect : MonoBehaviour
{
    [SerializeField] Boss boss;
    [SerializeField]
    GameObject red_Right;
    [SerializeField]
    GameObject cyan_Right;

    [SerializeField]
    GameObject red_Left;
    [SerializeField]
    GameObject cyan_Left;

    // Use this for initialization
    void Start()
    {
        OffEffect();
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.animator.GetBool("weak")||boss.dead)
        {
            OffEffect();
        }
        
    }


    public void ONEffect(int Left)
    {
        if (boss.power.m_colorType == NSC_Color.colorType.red)
        {
            if (Left==0)
            {
                red_Right.SetActive(false);
                cyan_Right.SetActive(false);
                red_Left.SetActive(true);
                cyan_Left.SetActive(false);
            }
            else if(Left ==1)
            {
                red_Right.SetActive(true);
                cyan_Right.SetActive(false);
                red_Left.SetActive(false);
                cyan_Left.SetActive(false);
            }
            else
            {
                red_Right.SetActive(true);
                cyan_Right.SetActive(false);
                red_Left.SetActive(true);
                cyan_Left.SetActive(false);
            }

        }
        else if (boss.power.m_colorType == NSC_Color.colorType.cyan)
        {
            if (Left == 0)
            {
                red_Right.SetActive(false);
                cyan_Right.SetActive(false);
                red_Left.SetActive(false);
                cyan_Left.SetActive(true);
            }
            else if (Left == 1)
            {
                red_Right.SetActive(false);
                cyan_Right.SetActive(true);
                red_Left.SetActive(false);
                cyan_Left.SetActive(false);
            }
            else
            {
                red_Right.SetActive(false);
                cyan_Right.SetActive(true);
                red_Left.SetActive(false);
                cyan_Left.SetActive(true);
            }
        }


    }

    public void OffEffect()
    {

        red_Right.SetActive(false);
        cyan_Right.SetActive(false);
        red_Left.SetActive(false);
        cyan_Left.SetActive(false);

    }

}
