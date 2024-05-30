using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshDenSc : MonoBehaviour
{
    
    NavMeshAgent agent;
    //private Animator warriorAnimator;
    public Transform player;
    [SerializeField] private float attackRange=10f;
    [SerializeField] private float detectRange = 25f;
    [SerializeField] private GameObject damageScreen;
    //sonra seslere ekleme yap þuan sadece 1
    [SerializeField] AudioSource[] hitSounds;
    [SerializeField] private bool isAttacking = false;
    private float attackCoolDown = 1.5f;
    private float currentCoolDown=0f;


    private void Start()
    {
        //warriorAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        
        if(player != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToTarget<=attackRange)
            {
                agent.enabled = false;
                EnemyAttack();
            }
            else if(distanceToTarget<=detectRange) 
            {
                agent.enabled=true;
                agent.SetDestination(player.transform.position);
                
            }
        }
        
    }

    public void EnemyAttack()
    {
        print("Saldýrý");
        //warriorAnimator.SetBool("Attacking", true);
        if (!isAttacking && KalanCan.oyuncuCan > 0)
        {
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
    }

    IEnumerator ActiveAndDeactiveFlash()
    {
        damageScreen.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        damageScreen.SetActive(false);
    }


}
