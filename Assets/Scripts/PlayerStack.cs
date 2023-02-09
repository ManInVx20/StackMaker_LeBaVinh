using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStack : Stack
{
    public override void Setup(Player player)
    {
        if (player != null)
        {
            transform.localPosition = new Vector3(0.0f, _stackHeight * player.StacksCount, 0.0f);
        }

        base.Setup(player);
    }

    public override void Activate(Player player)
    {
        Destroy(gameObject);

        base.Activate(player);
    }
}
