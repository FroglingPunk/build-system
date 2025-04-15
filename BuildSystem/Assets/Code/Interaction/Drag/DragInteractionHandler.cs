using UnityEngine;

public class DragInteractionHandler : InteractionHandler<DraggableObject>
{
    public DragInteractionHandler(Player player) : base(player)
    {
    }


    protected override void InternalInteract(DraggableObject interactableObject, out bool isContinuousInteraction)
    {
        _interactableObject.Drag(_player.view.Hands);

        isContinuousInteraction = true;
    }

    public override bool UpdateContinuousInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _interactableObject.Throw();
            return false;
        }

        return true;
    }
}