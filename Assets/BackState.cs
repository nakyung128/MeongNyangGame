using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;

    // 상태 진입
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();
    }

    // 상태 진행
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector2.Distance(enemy.home, enemyTransform.position) < 0.1f || Vector2.Distance(enemyTransform.position, enemy.player.position)<=400) 
        {
            animator.SetBool("isBack", false);
        }
        else
        {
            enemy.DirectionEnemy(enemy.home.x, enemyTransform.position.x);
            enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, enemy.home, Time.deltaTime * enemy.speed);
        }
    }

    // 상태 종료
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
