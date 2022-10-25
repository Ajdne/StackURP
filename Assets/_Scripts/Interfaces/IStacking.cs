using UnityEngine;

public interface IStacking
{
    void AddMoneyToStack(GameObject moneyObj);
    void InstantiateToStack(int addNumberOfStacks);
    void MultiplyStack(int multiplierCoefficient);
    void RemoveFromStack(int subtractNumberOfStacks);
    void DivideStack(int divideTheStackBy);
    void RemoveAllStacks();
    void LoseStacks();
    void RemoveMoneyToProperty(Vector3 objPos, bool destroy);
    void RemoveStackToShortcut(Vector3 objPos);
    public int GetStackCount();
}