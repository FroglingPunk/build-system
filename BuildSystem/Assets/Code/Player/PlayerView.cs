using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [field: SerializeField] public CharacterController CharController { get; private set; }
    [field: SerializeField] public Camera Camera { get; private set; }
    [field: SerializeField] public Transform Root { get; private set; }
    [field: SerializeField] public Transform Hands { get; private set; }
}