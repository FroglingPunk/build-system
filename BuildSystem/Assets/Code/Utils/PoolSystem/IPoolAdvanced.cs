using System.Collections.Generic;
using System.Collections.ObjectModel;

public interface IPoolAdvanced<T> : IPool<T>
{
    int CountActive { get; }

    int CountCache { get; }

    int Count { get; }

    ReadOnlyCollection<T> ObjectsActive { get; }

    ReadOnlyCollection<T> ObjectsCache { get; }

    void PushAllActiveObjects();

    void Add(T obj);

    void Remove(T obj);

    IEnumerable<T> ObjectsActiveAsReverseEnumerable();
}