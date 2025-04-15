using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _image;

    public string Text
    {
        get => _text.text;
        set => _text.text = value;
    }

    public Sprite Sprite
    {
        get => _image.sprite;
        set => _image.sprite = value;
    }

    public Button UnityButton => _button;
    public IObservable<UIButton> OnClick => _onClick;
    
    private readonly ReactiveCommand<UIButton> _onClick = new();


    private void Reset()
    {
        TryGetComponent(out _button);
    }

    protected virtual void Awake()
    {
        _button.onClick.AddListener(() => { _onClick.Execute(this); });
    }

    protected virtual void OnDestroy()
    {
        _onClick.Dispose();
    }
}