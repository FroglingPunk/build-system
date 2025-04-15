using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolComponents<T> : PoolAdvanced<T> where T : Component
{
    public T ObjectTemplate => _objectTemplate;

    protected T _objectTemplate;
    protected Transform _parent;


    public PoolComponents(T objectTemplate, bool pushTemplate = true, Transform parent = null, int preload = 0)
    {
        _objectTemplate = objectTemplate;
        _parent = parent;

        if (pushTemplate)
        {
            Push(objectTemplate);
            preload--;
        }

        while (preload-- > 0)
        {
            var obj = _parent != null
                ? Object.Instantiate(_objectTemplate.gameObject, _parent).GetComponent<T>()
                : Object.Instantiate(_objectTemplate.gameObject).GetComponent<T>();

            if (obj is IPoolObjectPushable<T> objPushable)
            {
                objPushable.Pool = this;
            }

            _cache.Add(obj);
            obj.gameObject.SetActive(false);
        }
    }


    public override T GetObject()
    {
        return GetObject(true);
    }

    public virtual T GetObject(bool setActive)
    {
        if (_cache.Count > 0)
        {
            var obj = _cache[0];
            _cache.RemoveAt(0);
            obj.transform.SetAsLastSibling();
            obj.gameObject.SetActive(setActive);
            _objects.Add(obj);
            return obj;
        }
        else
        {
            var obj = _parent != null
                ? Object.Instantiate(_objectTemplate.gameObject, _parent).GetComponent<T>()
                : Object.Instantiate(_objectTemplate.gameObject).GetComponent<T>();

            if (obj is IPoolObjectPushable<T> objPushable)
            {
                objPushable.Pool = this;
            }

            obj.gameObject.SetActive(setActive);
            _objects.Add(obj);
            return obj;
        }
    }

    public override void Push(T obj)
    {
        if (_objects.Contains(obj))
        {
            _objects.Remove(obj);
        }

        _cache.Add(obj);
        obj.gameObject.SetActive(false);

        if (obj is IPoolObject poolObj)
        {
            poolObj.OnGoingToPool();
        }
    }

    public override void PushRange(IEnumerable<T> objs)
    {
        foreach (var obj in objs)
        {
            if (_objects.Contains(obj))
            {
                _objects.Remove(obj);
            }

            _cache.Add(obj);
            obj.gameObject.SetActive(false);

            if (obj is IPoolObject poolObj)
            {
                poolObj.OnGoingToPool();
            }
        }
    }

    public override void PushRange(T[] objs)
    {
        foreach (var obj in objs)
        {
            if (_objects.Contains(obj))
            {
                _objects.Remove(obj);
            }

            _cache.Add(obj);
            obj.gameObject.SetActive(false);

            if (obj is IPoolObject poolObj)
            {
                poolObj.OnGoingToPool();
            }
        }
    }

    public override void PushAllActiveObjects()
    {
        if (_objects.Any())
        {
            var count = _objects.Count;

            for (var i = 0; i < count; i++)
            {
                if (!_objects[i])
                {
                    continue;
                }

                _objects[i].gameObject.SetActive(false);

                if (_objects[i] is IPoolObject poolObj)
                {
                    poolObj.OnGoingToPool();
                }
            }

            _cache.AddRange(_objects);
            _objects.Clear();
        }
    }

    public void Destroy()
    {
        foreach (var obj in _objects)
        {
            if (obj)
            {
                Object.Destroy(obj.gameObject);
            }
        }

        _objects.Clear();

        foreach (var obj in _cache)
        {
            if (obj)
            {
                Object.Destroy(obj.gameObject);
            }
        }

        _cache.Clear();
    }
}