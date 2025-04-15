using UnityEngine;

[CreateAssetMenu(menuName = "Build/Object", fileName = "BuildObject", order = 0)]
public class BuildObjectData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private EBuildRule _buildRule;
    [SerializeField] private BuildObject _template;
    
    public string Name => _name;
    public Sprite Icon => _icon;
    public EBuildRule BuildRule => _buildRule;
    public BuildObject Template => _template;
}