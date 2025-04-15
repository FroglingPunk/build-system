public interface IInteractionHandler
{
    void Interact<T>(T interactableObject, out bool isContinuousInteraction) where T : IInteractableObject;

    bool UpdateContinuousInteraction();
}