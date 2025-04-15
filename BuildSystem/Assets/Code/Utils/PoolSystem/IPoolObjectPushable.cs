public interface IPoolObjectPushable<T>
{
    IPool<T> Pool { set; }

    void Push();
}