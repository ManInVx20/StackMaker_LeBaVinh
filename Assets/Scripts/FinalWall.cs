using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalWall : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private Color _color;

    public bool IsActivated { get; private set; }

    public void Activate()
    {
        _meshRenderer.material.SetColor("_Color", _color);

        IsActivated = true;
    }

    public void Deactivate()
    {
        _meshRenderer.material.SetColor("_Color", Color.black);

        IsActivated = false;
    }
}
