using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField]
    protected BoxCollider _collider;
    [SerializeField]
    protected Transform _modelTransform;
    [SerializeField]
    protected float _stackHeight;

    public bool IsActive { get; private set; }

    public float StackHeight => _stackHeight;

    public virtual void Setup(Player player)
    {
        IsActive = false;
    }

    public virtual void Activate(Player player)
    {
        IsActive = true;
    }
}
