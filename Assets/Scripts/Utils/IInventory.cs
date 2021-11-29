public interface IInventory
{
    void Attach(IObserver observer);
    
    void Detach(IObserver observer);
    
    void Notify();
}
