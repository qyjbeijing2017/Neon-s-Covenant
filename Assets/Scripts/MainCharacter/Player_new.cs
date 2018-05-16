using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_new : MonoBehaviour
{

    public int HP;
    public int HpMax;
    public float power;
    public float powerMax;
    public float powerColor;
    public int powerType;//红色为1，青色为2，白色为0，黑色为3

    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode down;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode dodgeKey;

    public float moveSpeed;
    [SerializeField] private float dodgeSpeed;

    [SerializeField] private AxeDamage axeDamage;
    [SerializeField] private int axeValue;
    [SerializeField] private float axeStopTime;
    [SerializeField] private SpriteRenderer aimLine;

    [SerializeField] private BulletAll bulletPlayer_red;
    [SerializeField] private BulletAll bulletPlayer_cyan;
    [SerializeField] private BulletAll bulletPlayer_white;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootPower;
    [SerializeField] private Camera cameraControl;
    [SerializeField] private float dieCamerDis;
    [SerializeField] private float MaxCamerDis;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material cyanMaterial;
    [SerializeField] private Material normalMaterial;

    [SerializeField] private float whitePowerToHp;
    [SerializeField] private KeyCode whitePowerToHPKey;
    [SerializeField] private bool combo;
    [SerializeField] private float m_nearattackMove;



    private bool comboL;
    private Animator animatorPlayer;
    private Vector3 cameraPoint;

    public bool weakBoss;




    public bool inJured(int HpDamage, float powerDamage, int type, float stopTime)
    {
        if (!animatorPlayer.GetBool("rolling"))
        {
            HP -= HpDamage;
            if (powerType == 0)
            {
                power -= powerDamage;

                if (type == 1 || type == 2)
                {
                    powerType = type;
                    powerColor += powerDamage;
                }

            }
            else
            {

                if (type == powerType)
                {
                    power -= powerDamage;
                    powerColor += powerDamage;
                    powerType = type;

                }
                else if (type == 3)
                {
                    if (powerColor < powerDamage)
                    {
                        powerColor = 0;
                        power -= powerDamage - powerColor;

                    }
                    else
                    {
                        powerColor -= powerDamage;
                    }
                }
                else
                {
                    if (powerColor < powerDamage)
                    {
                        power += powerColor;
                        powerColor = powerDamage - powerColor;
                        power -= powerColor;
                        if (powerType == 1)
                            powerType = 2;
                        else if (powerType == 2)
                            powerType = 1;

                    }
                    else
                    {
                        powerColor -= powerDamage;
                        power += powerDamage;
                    }

                }

            }



            if (stopTime > 0)
            {

                player_stopImmediately();
                StartCoroutine(stop(stopTime));

            }

        }
        else
        {
            return false;
        }



        //规范血量与蓝量
        if (power < 0)
        {
            powerColor += power;
            power = 0;

        }
        if (powerColor > powerMax)
        {
            powerColor = powerMax;
        }
        if (powerColor <= 0)
        {
            powerType = 0;
            powerColor = 0;
        }

        return true;

    }

    // Use this for initialization
    void Start()
    {
        weakBoss = false;


        animatorPlayer = GetComponent<Animator>();
        powerMax = power;
        HpMax = HP;
        aimLine.enabled = false;
        cameraPoint = cameraControl.transform.position - transform.position;
        StartCoroutine("player_move");
        axeDamage.axeDamege = axeValue;
        axeDamage.stopTime = axeStopTime;
    }

    // Update is called once per frame
    void Update()
    {
        axeDamage.axeType = powerType;

        if (HP < 0)
        {
            player_stopImmediately();
        }


        if (powerType == 1)
        {
            meshRenderer.material = redMaterial;
        }
        else if (powerType == 2)
        {
            meshRenderer.material = cyanMaterial;
        }
        else
        {
            meshRenderer.material = normalMaterial;
        }

        if (powerMax < (power + powerColor))
        {
            if (powerColor > powerMax)
                powerColor = powerMax;
            else
                power = powerMax - powerColor;

        }

        if (HP > HpMax)
        {
            HP = HpMax;
        }

        if (Input.GetKeyDown(dodgeKey) && !animatorPlayer.GetBool("rolling") && !animatorPlayer.GetBool("stop"))
        {
            player_stopImmediately();
            Vector3 movingSpeed = Vector3.zero;
            if (Input.GetKey(up))
            {
                movingSpeed += new Vector3(0, 0, 1);
            }
            if (Input.GetKey(down))
            {
                movingSpeed += new Vector3(0, 0, -1);
            }
            if (Input.GetKey(left))
            {
                movingSpeed += new Vector3(-1, 0, 0);
            }
            if (Input.GetKey(right))
            {
                movingSpeed += new Vector3(1, 0, 0);
            }

            if (movingSpeed == Vector3.zero)
            {
            }
            else
            {
                this.transform.forward = movingSpeed.normalized;
            }

            StartCoroutine("player_Dodge");

        }


    }

    public void player_stopImmediately()
    {
        weakBoss = false;
        StopAllCoroutines();
        animatorPlayer.SetBool("moving", false);
        animatorPlayer.SetBool("nearAttack", false);
        animatorPlayer.SetBool("rangeAttack", false);
        animatorPlayer.SetBool("stop", false);
        animatorPlayer.Play("Idle");
    }
    public void player_start()
    {

        StartCoroutine("player_move");
    }

    IEnumerator player_move()
    {

        while (true)
        {
            cameraMove();
            //闪避



            //移动
            Vector3 movingSpeed = Vector3.zero;
            if (Input.GetKey(up))
            {
                movingSpeed += new Vector3(0, 0, 1);
            }
            if (Input.GetKey(down))
            {
                movingSpeed += new Vector3(0, 0, -1);
            }
            if (Input.GetKey(left))
            {
                movingSpeed += new Vector3(-1, 0, 0);
            }
            if (Input.GetKey(right))
            {
                movingSpeed += new Vector3(1, 0, 0);
            }
            if (movingSpeed != Vector3.zero)
            {
                transform.position += movingSpeed.normalized * moveSpeed * Time.deltaTime;
            }

            if (movingSpeed == Vector3.zero)
            {
                animatorPlayer.SetBool("moving", false);
            }
            else
            {
                //this.transform.forward = new Vector3(transform.position.x - startPosition.x, 0, transform.position.z - startPosition.z);
                this.transform.forward = movingSpeed.normalized;
                animatorPlayer.SetBool("moving", true);
            }

            //攻击
            if (Input.GetMouseButton(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                LayerMask mouseMask = 1 << 9;
                if (Physics.Raycast(ray, out hit, 100f, mouseMask))
                {
                    Vector3 offset = new Vector3((hit.point - transform.position).x, 0, (hit.point - transform.position).z);
                    if (offset.magnitude > 0.7)
                    {
                        transform.forward = new Vector3(offset.x, transform.forward.y, offset.z).normalized;
                    }
                    aimLine.size = new Vector2((hit.point - this.transform.position).magnitude, aimLine.size.y);
                    aimLine.enabled = true;
                }
                if (Input.GetMouseButtonDown(0))
                {

                    yield return StartCoroutine(player_attackRange());
                }
            }
            else
            {


                aimLine.enabled = false;
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    LayerMask mouseMask = 1 << 9;
                    if (Physics.Raycast(ray, out hit, 100f, mouseMask))
                    {
                        Vector3 offset = new Vector3((hit.point - transform.position).x, 0, (hit.point - transform.position).z);
                        if (offset.magnitude > 0.7)
                        {
                            transform.forward = new Vector3(offset.x, transform.forward.y, offset.z).normalized;
                        }
                    }
                    StopCoroutine("player_move");
                    StopAllCoroutines();
                    player_attackNear();

                }
            }

            if (Input.GetKeyDown(whitePowerToHPKey))
            {
                if (whitePowerToHp < power && HP < HpMax)
                {
                    HP++;
                    power -= whitePowerToHp;

                }
                else
                {
                    print("无法转换血量");
                }
            }

            yield return 0;
        }

    }

    IEnumerator player_Dodge()
    {
        animatorPlayer.SetBool("rolling", true);
        animatorPlayer.Play("Roll");
        while (animatorPlayer.GetBool("rolling"))
        {
            cameraMove();
            transform.position += transform.forward * dodgeSpeed * Time.deltaTime;
            yield return 0;
        }
        player_start();

    }
    public void DodgeEnd()
    {
        animatorPlayer.SetBool("rolling", false);

    }



    void player_attackNear()
    {
        animatorPlayer.SetBool("nearAttack", true);
        animatorPlayer.Play("AttackNear");

    }
    public void startNearDamage()
    {
        axeDamage.GetComponent<Collider>().enabled = true;

    }
    public void endNearDamage()
    {
        axeDamage.GetComponent<Collider>().enabled = false;
    }
    public void endNearAttack()
    {
        animatorPlayer.SetBool("nearAttack", false);
        StartCoroutine("player_move");
    }


    IEnumerator player_attackRange()
    {

        animatorPlayer.SetBool("rangeAttack", true);
        animatorPlayer.Play("AttackDistant");
        while (animatorPlayer.GetBool("rangeAttack"))
        {
            cameraMove();
            yield return 0;
        }
    }
    public void shoot()
    {

        GameObject bullet;

        if (powerType == 0)
        {
            bullet = Instantiate(bulletPlayer_white.gameObject);
            bullet.transform.localEulerAngles = shootPoint.localEulerAngles;
            bullet.transform.position = shootPoint.position;
            bullet.GetComponent<Rigidbody>().velocity = shootPoint.forward;
            bullet.GetComponent<BulletAll>().colorType = powerType;
            inJured(0, shootPower, 3, 0);
        }
        else if (powerType == 1)
        {
            bullet = Instantiate(bulletPlayer_red.gameObject);
            bullet.transform.localEulerAngles = shootPoint.localEulerAngles;
            bullet.transform.position = shootPoint.position;
            bullet.GetComponent<Rigidbody>().velocity = shootPoint.forward;
            bullet.GetComponent<BulletAll>().colorType = powerType;
            inJured(0, shootPower, 3, 0);
        }
        else if (powerType == 2)
        {
            bullet = Instantiate(bulletPlayer_cyan.gameObject);
            bullet.transform.localEulerAngles = shootPoint.localEulerAngles;
            bullet.transform.position = shootPoint.position;
            bullet.GetComponent<Rigidbody>().velocity = shootPoint.forward;
            bullet.GetComponent<BulletAll>().colorType = powerType;
            inJured(0, shootPower, 3, 0);
        }



    }
    public void shootEnd()
    {
        animatorPlayer.SetBool("rangeAttack", false);
        StopCoroutine(player_attackRange());
    }


    IEnumerator stop(float stopTime)
    {
        weakBoss = false;
        cameraMove();
        animatorPlayer.SetBool("stop", true);
        animatorPlayer.SetBool("rangeAttack", false);
        animatorPlayer.SetBool("rolling", false);
        animatorPlayer.SetBool("nearAttack", false);
        animatorPlayer.SetBool("moving", false);
        animatorPlayer.Play("injured");


        yield return new WaitForSeconds(stopTime);


        animatorPlayer.SetBool("stop", false);
        StartCoroutine(player_move());
    }

    void cameraMove()
    {
        Vector3 targetPoint = transform.position + cameraPoint;
        if (Vector3.Magnitude(cameraControl.transform.position - targetPoint) > dieCamerDis)
        {
            Vector3 cameraSpeed = (Vector3.Magnitude(cameraControl.transform.position - targetPoint) - dieCamerDis) / (MaxCamerDis - dieCamerDis) * moveSpeed * (targetPoint - cameraControl.transform.position);
            cameraControl.transform.position += cameraSpeed * Time.deltaTime;

        }



    }

    void player_dead()
    {
        player_stopImmediately();
        GetComponent<AudioStart>().audio_start();
    }

    void player_pasue()
    {
        Debug.Break();
    }

    public void comboStart()
    {
        if (combo)
        {
            comboL = true;
            
            animatorPlayer.SetBool("combo", false);
            StartCoroutine(comboLi());
        }
        else
        {
            comboL = false;
        }

    }

    public void comboNex()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask mouseMask = 1 << 9;
        if (Physics.Raycast(ray, out hit, 100f, mouseMask))
        {
            Vector3 offset = new Vector3((hit.point - transform.position).x, 0, (hit.point - transform.position).z);
            if (offset.magnitude > 0.7)
            {
                transform.forward = new Vector3(offset.x, transform.forward.y, offset.z).normalized;
            }
            aimLine.size = new Vector2((hit.point - this.transform.position).magnitude, aimLine.size.y);
        }
        animatorPlayer.SetBool("combo", false);
    }

    public void nearAttackMove()
    {
        this.transform.position += transform.forward * m_nearattackMove;
    }

    public void comboEnd()
    {
        //if (!animatorPlayer.GetBool("combo"))
        //{
        //    endNearAttack();
        //    return;
        //}

        if (comboL && !animatorPlayer.GetBool("combo"))
        {
            comboL = false;
            endNearAttack();
            return;
        }

    }

    IEnumerator comboLi()
    {
        while (comboL)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animatorPlayer.SetBool("combo", true);
                comboL = false;

            }
            yield return 0;
        }

    }


    public void startWeakBoss()
    {
        weakBoss = true;
    }
    public void endWeakBoss()
    {
        weakBoss = false;
    }


}
