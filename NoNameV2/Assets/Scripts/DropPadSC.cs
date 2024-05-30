using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPadSC : MonoBehaviour
{
    public float moveSpeed = 1f, detectRange = 30f, attackRange = 10f;
    public Transform target;
    public GameObject warrior;
    private Animator animatorWarrior;
    public GameObject ekranFlash;

    public AudioSource[] hitSounds;
    public int saldiri, vurmaSesi;
    private bool isAttacking = false;
    private bool isChasing = false;


    private float attackCoolDown = 5f;
    private float currentCoolDown = 0f;


    private void Start()
    {
        animatorWarrior = GetComponent<Animator>();
        
    }

    private void FixedUpdate()
    {
        transform.LookAt(target);
    }
    
    private void Update()
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= attackRange)
            {
                
                Attack();
            }
            else if (distanceToTarget <= detectRange)
            {
                
                MoveTowardTarget();
                isChasing = true;
            }
            else
            {
                isAttacking = false;
                isChasing = false;
                //animatorWarrior.SetBool("Walking", true);
                //animatorWarrior.SetBool("Attacking", false);
            }

            if (currentCoolDown > 0)
            {
                currentCoolDown -= Time.deltaTime;
            }
            else
            {
                isAttacking = false;
            }

        }
    }

    

    void MoveTowardTarget()
    {
        if (target.position.y < 3f)
        {
            transform.position = new Vector3(transform.position.x, 15f, transform.position.z);
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed*Time.fixedDeltaTime );
        //animatorWarrior.SetBool("Walking", true);
        //animatorWarrior.SetBool("Attacking", false);
    }

    void Attack()
    {
        //animatorWarrior.SetBool("Walking", false);
        //animatorWarrior.SetBool("Attacking", true);
        if (!isAttacking && KalanCan.oyuncuCan > 0)
        {
            //animatorWarrior.SetBool("Walking", false);
            //animatorWarrior.SetBool("Attacking", true);
            if (!isAttacking && KalanCan.oyuncuCan > 0)
            {
                // Raycast iþlemi
                RaycastHit hit;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Physics.Raycast(transform.position, directionToTarget, out hit, attackRange))
                {
                    if (hit.transform == target)
                    {
                        // Hedef vuruldu, oyuncunun canýný azalt
                        isAttacking = true;
                        KalanCan.oyuncuCan -= 1;
                        currentCoolDown = attackCoolDown;
                        if (KalanCan.oyuncuCan > 0)
                        {
                            int randomSoundIndex = Random.Range(0, hitSounds.Length);
                            hitSounds[randomSoundIndex].Play();
                            StartCoroutine(ActiveAndDeactiveFlash());
                        }
                    }
                    else
                    {
                        // Hedefin önünde bir engel var, can azaltma iþlemi yapýlmýyor
                        Debug.Log("Hedefin önünde bir engel var: " + hit.transform.name);
                    }
                }
            }
        }
    }

    IEnumerator ActiveAndDeactiveFlash()
    {
        ekranFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        ekranFlash.SetActive(false);
    }


}