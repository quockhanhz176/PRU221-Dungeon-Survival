public interface IState
{
    public void Enter();
    public void Exit();
    public void ExecuteLogic();
    public void ExecutePhysic();
}