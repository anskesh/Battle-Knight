public class PlayerDeathTransition : Transition
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (_player.Health <= 0) 
            NeedTransit = true;
    }
}
