public abstract class InteractionHandler<T> : IInteractionHandler where T : IInteractableObject
{
    protected readonly Player _player;
    protected T _interactableObject;
    
    public InteractionHandler(Player player)
    {
        _player = player;
    }


    public void Interact<Y>(Y interactableObject, out bool isContinuousInteraction) where Y : IInteractableObject
    {
        if (interactableObject is T typedObject)
        {
            _interactableObject = typedObject;
            InternalInteract(typedObject, out isContinuousInteraction);
        }
        else
        {
            isContinuousInteraction = false;
        }
    }

    public abstract bool UpdateContinuousInteraction();

    protected abstract void InternalInteract(T interactableObject, out bool isContinuousInteraction);
}