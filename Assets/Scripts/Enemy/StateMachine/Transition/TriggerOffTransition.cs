using System;

public class TriggerOffTransition : Transition
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }
    
    private void Update()
    {
        if (_enemy.Target == null) NeedTransit = true;
    }
}
