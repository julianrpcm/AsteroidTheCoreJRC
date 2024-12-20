using UnityEngine;

public class BulletMovement : ForwardContinuousMovement
{
    protected override void OnOffScreen()
    {
        Destroy(gameObject);
    }
}
