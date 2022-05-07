using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : StateMachine
{
    public void OnPlayerDied()
    {
        Transit(_celebration);
    }
}
