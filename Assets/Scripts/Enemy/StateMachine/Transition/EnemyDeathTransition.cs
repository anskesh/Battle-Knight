public class EnemyDeathTransition : Transition
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (_enemy.Health <= 0) 
            NeedTransit = true;
    }
}
