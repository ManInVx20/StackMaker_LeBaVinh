using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalWallDetector : MonoBehaviour
{
    [SerializeField]
    private LayerMask _finalWallLayer;

    private void Update()
    {
        CheckFinalWall(Vector3.left);
        CheckFinalWall(Vector3.right);
    }

    private void CheckFinalWall(Vector3 direction)
    {
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 2.0f, _finalWallLayer))
        {
            FinalWall finalWall = hit.collider.GetComponent<FinalWall>();

            if (!finalWall.IsActivated)
            {
                finalWall.Activate();
            }
        }
    }
}
