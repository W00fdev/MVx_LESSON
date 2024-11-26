using System.Collections.Generic;
using UnityEngine;

public abstract class ListView<T> : MonoBehaviour
    where T : Component
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _container;

    private readonly Queue<T> _freeItems = new();
    private readonly List<T> _showedItems = new();

    public T SpawnView()
    {
        if (_freeItems.TryDequeue(out T item))
            item.gameObject.SetActive(true);
        else
            item = Instantiate(_prefab, _container);

        _showedItems.Add(item);
        return item;
    }

    public void UnspawnView(T view)
    {
        if (view && _showedItems.Remove(view))
        {
            view.gameObject.SetActive(false);
            _freeItems.Enqueue(view);
        }
    }

    public void Clear()
    {
        for (var i = 0; i < _showedItems.Count; i++)
        {
            T item = _showedItems[i];
            item.gameObject.SetActive(false);
            _freeItems.Enqueue(item);
        }

        _showedItems.Clear();
    }
}