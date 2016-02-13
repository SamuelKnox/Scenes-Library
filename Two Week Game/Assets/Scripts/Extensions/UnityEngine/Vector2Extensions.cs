using UnityEngine;

public static class Vector2Extensions
{
    /// <summary>
    /// Gets a random position on a circle given its radius and that the center is the Vector2
    /// </summary>
    public static Vector2 GetRandomPositionOnCircle(this Vector2 vector2, float radius)
    {
        float angle = Random.Range(0, 2 * Mathf.PI);
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        return new Vector2(x, y);
    }
}
