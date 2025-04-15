using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UniRx;
using UnityEngine;

public class Player : IDisposable
{
    private readonly Dictionary<Type, IInteractionHandler> _interactHandlers = new();

    private readonly Dictionary<EPlayerControlState, IPlayerControlState> _controlStates;

    public readonly PlayerView view;

    private EPlayerControlState _activeState;
    private IInteractionHandler _activeContinuousInteractionHandler;

    private readonly CompositeDisposable _disposables = new();


    public Player(PlayerView view)
    {
        this.view = view;

        CreateInteractionHandlers();

        _controlStates = new()
        {
            { EPlayerControlState.Default, new DefaultPlayerControlState(this) },
            { EPlayerControlState.Builder, new BuildingPlayerControlState(this) }
        };

        SetControlState(EPlayerControlState.Default);

        Observable
            .EveryUpdate()
            .Subscribe(_ => { _controlStates[_activeState].Update(); })
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables?.Dispose();
    }


    private void CreateInteractionHandlers()
    {
        var baseType = typeof(InteractionHandler<>);

        var types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(t => typeof(IInteractionHandler).IsAssignableFrom(t))
            .Where(t => GetBaseGenericType(t, baseType) != null);

        foreach (var type in types)
        {
            try
            {
                var baseGeneric = GetBaseGenericType(type, baseType);
                var genericArg = baseGeneric.GetGenericArguments()[0];

                var instance = (IInteractionHandler)Activator.CreateInstance(type, this);
                _interactHandlers.Add(genericArg, instance);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Не удалось создать экземпляр {type.Name}: {ex.Message}");
            }
        }

        Type GetBaseGenericType(Type type, Type openGeneric)
        {
            while (type != null && type != typeof(object))
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == openGeneric)
                    return type;

                type = type.BaseType;
            }

            return null;
        }
    }


    public void SetControlState(EPlayerControlState state)
    {
        if (_activeState == state)
        {
            return;
        }
        
        _controlStates[_activeState].Disable();
        _activeState = state;
        _controlStates[_activeState].Enable();

        // to do
        // планировалось что будет переход к другому контроллеру игрока для режима строительства
        // при котором появлялся бы UI для просмотра всех доступных построек и выбора
        // а также возможность строительства не только от первого лица, но и в режиме полёта (как в Fallout 76)
    }

    public IInteractionHandler GetInteractionHandler(IInteractableObject interactableObject)
    {
        return _interactHandlers[interactableObject.GetType()];
    }
}