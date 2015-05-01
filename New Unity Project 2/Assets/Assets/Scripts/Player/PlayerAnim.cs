using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour
{

    Animator anim;
    PlayerController player;
    PlayerCondition stat;
    
    

    void Awake()
    {
        
        player = GetComponentInParent<PlayerController>();
        anim = GetComponent<Animator>();
        stat = GetComponentInParent<PlayerCondition>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player.walk()&&stat.isDead())
        {
            anim.SetBool("Iswalking", true);
        }
        else
            anim.SetBool("Iswalking", false);
        if (player.IsAttacking()&&stat.isDead())
        {
            Debug.Log("Anim In Coming");
            anim.SetTrigger("Ishitting");
        }
        else if(stat.isDead()==false)
        {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            Debug.Log("dead");
            
        }


    }
}
