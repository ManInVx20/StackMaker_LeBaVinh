using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeStack : Stack
{
    [SerializeField]
    private MeshRenderer _meshRenderer;

    public override void Setup(Player player)
    {
        _meshRenderer.enabled = false;

        base.Setup(player);
    }

    public override void Activate(Player player)
    {
        _meshRenderer.enabled = true;

        base.Activate(player);
    }
}
