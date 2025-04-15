using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Highlighter : MonoBehaviour
{
    [SerializeField] private Material _highlightMaterialOrigin;

    private Renderer _renderer;
    private Material _defaultMaterial;
    private Material _hlMaterial;


    private void Awake()
    {
        TryGetComponent(out _renderer);
        _defaultMaterial = _renderer?.sharedMaterial;
        _hlMaterial = new Material(_highlightMaterialOrigin);
    }

    public void Enable()
    {
        _renderer.sharedMaterial = _hlMaterial;
    }

    public void SetColor(Color color)
    {
        _hlMaterial.color = color;
    }

    public void Disable()
    {
        _renderer.sharedMaterial = _defaultMaterial;
    }
}