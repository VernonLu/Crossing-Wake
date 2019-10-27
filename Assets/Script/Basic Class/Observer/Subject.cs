public interface Subject
{
    void NotifyObserver();
    void AddObserver(Observer observer);
    void RemoveObserver(Observer observer);
}
