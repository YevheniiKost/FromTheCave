using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventHandler : MonoBehaviour
{
    private EnemyMovement _enemyController;

    private void Awake()
    {
        _enemyController = transform.parent.GetComponent<EnemyMovement>();
    }

    public void HandleEnemyAttack()
    {
        _enemyController.Attack();
    }
}
