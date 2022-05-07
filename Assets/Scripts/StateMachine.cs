using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
	[SerializeField] protected State _firstState;
	[SerializeField] protected State _celebration;
    
	private Player _target;
	private State _currentState;

	protected virtual void Start()
	{
		_target = FindObjectOfType<Player>();
		Reset();
	}

	protected virtual void Update()
	{
		if (_currentState == null)
			return;
		var nextState = _currentState.GetNextState();
		if (nextState != null) Transit(nextState);
	}

	protected void Reset()
	{
		_currentState = _firstState;
		if (_currentState != null)
			_currentState.Enter(_target);
	}

	protected void Transit(State nextState)
	{
		_currentState.Exit();
		_currentState = nextState;
		_currentState.Enter(_target);
	}
}
