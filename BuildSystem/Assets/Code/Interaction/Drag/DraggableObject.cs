using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DraggableObject : InteractableObject
{
    private Rigidbody _rigidbody;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    public void Drag(Transform holder)
    {
        transform.SetParent(holder);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        _rigidbody.isKinematic = true;
    }

    public void Release()
    {
        transform.SetParent(null);
        
        _rigidbody.isKinematic = false;
    }

    public void Throw()
    {
        Release();
        _rigidbody.AddForce(transform.forward * 15f, ForceMode.Impulse);
    }
}