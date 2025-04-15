using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Build/Category", fileName = "BuildCategory", order = 0)]
public class BuildCategoryData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private List<BuildObjectData> _objects;

    public string Name => _name;
    public Sprite Icon => _icon;
    public IReadOnlyList<BuildObjectData> Objects => _objects;
}