using System.Collections.Generic;
using System.Collections.ObjectModel;

public abstract class PoolAdvanced<T> : IPoolAdvanced<T>
{
    public int CountActive => _objects.Count;

    public int CountCache => _cache.Count;

    public int Count => _cache.Count + _objects.Count;

    public ReadOnlyCollection<T> ObjectsActive => _objectsReadOnlyCollection;

    public ReadOnlyCollection<T> ObjectsCache => _cacheReadOnlyCollection;

    protected readonly List<T> _objects;
    protected readonly List<T> _cache;

    protected readonly ReadOnlyCollection<T> _objectsReadOnlyCollection;
    protected readonly ReadOnlyCollection<T> _cacheReadOnlyCollection;

    protected PoolAdvanced()
    {
        _objects = new List<T>();
        _cache = new List<T>();

        _objectsReadOnlyCollection = _objects.AsReadOnly();
        _cacheReadOnlyCollection = _cache.AsReadOnly();
    }

    public abstract T GetObject();

    public virtual void Push(T obj)
    {
        _objects.Remove(obj);
        _cache.Add(obj);

        if (obj is IPoolObject poolObj)
        {
            poolObj.OnGoingToPool();
        }
    }

    public virtual void PushRange(IEnumerable<T> objs)
    {
        foreach (var obj in objs)
        {
            Push(obj);
        }
    }

    public virtual void PushRange(T[] objs)
    {
        foreach (var obj in objs)
        {
            Push(obj);
        }
    }

    public virtual void PushAllActiveObjects()
    {
        if (_objects.Count > 0)
        {
            for (var i = _objects.Count - 1; i >= 0; i--)
            {
                if (_objects[i] is IPoolObject poolObj)
                {
                    poolObj.OnGoingToPool();
                }
            }

            _cache.AddRange(_objects);
            _objects.Clear();
        }
    }

    public void Add(T obj)
    {
        if (!_cache.Contains(obj))
        {
            _cache.Add(obj);
        }
    }

    public void Remove(T obj)
    {
        _objects.Remove(obj);
        _cache.Remove(obj);
    }

    public IEnumerable<T> ObjectsActiveAsReverseEnumerable()
    {
        for (var index = _objects.Count - 1; index >= 0; index--)
        {
            yield return _objects[index];
        }
    }
}