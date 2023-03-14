public class StateMachine
{
    private IState currentState;

    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState.Enter();
    }

    public void Update()
    {
        currentState?.ExecuteLogic();
    }

    public void FixedUpdate()
    {
        currentState?.ExecutePhysic();
    }
}