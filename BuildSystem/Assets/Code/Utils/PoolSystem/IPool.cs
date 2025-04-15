using System.Collections.Generic;

public interface IPool<T>
{
    T GetObject();

    void Push(T obj);

    void PushRange(IEnumerable<T> objs);

    void PushRange(T[] objs);
}