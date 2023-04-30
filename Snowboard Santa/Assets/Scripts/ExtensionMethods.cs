using Unity.VisualScripting;
using UnityEngine;

public static class ExtensionMethods
{
    #region SPRITE_STUFF
    public static float Top(this SpriteRenderer sprite)
    {
        return sprite.bounds.center.y + sprite.bounds.extents.y;
    }

    public static float Bottom(this SpriteRenderer sprite)
    {
        return sprite.bounds.center.y - sprite.bounds.extents.y;
    }

    public static float Left(this SpriteRenderer sprite)
    {
        return sprite.bounds.center.x - sprite.bounds.extents.x;
    }

    public static float Right(this SpriteRenderer sprite)
    {
        return sprite.bounds.center.x + sprite.bounds.extents.x;
    }

    public static float MidX(this SpriteRenderer sprite)
    {
        return sprite.bounds.center.x;
    }

    public static float MidY(this SpriteRenderer sprite)
    {
        return sprite.bounds.center.y;
    }

    public static Vector2 Center(this SpriteRenderer sprite)
    {
        return sprite.bounds.center;
    }

    public static Vector2 TopLeft(this SpriteRenderer sprite)
    {
        return new Vector2(sprite.Left(), sprite.Top());
    }
    public static Vector2 TopMid(this SpriteRenderer sprite)
    {
        return new Vector2(sprite.MidX(), sprite.Top());
    }
    public static Vector2 TopRight(this SpriteRenderer sprite)
    {
        return new Vector2(sprite.Right(), sprite.Top());
    }

    public static Vector2 MidLeft(this SpriteRenderer sprite)
    {
        return new Vector2(sprite.Left(), sprite.MidY());
    }
    public static Vector2 MidRight(this SpriteRenderer sprite)
    {
        return new Vector2(sprite.Right(), sprite.MidY());
    }

    public static Vector2 BotMid(this SpriteRenderer sprite)
    {
        return new Vector2(sprite.MidX(), sprite.Bottom());
    }
    public static Vector2 BotRight(this SpriteRenderer sprite)
    {
        return new Vector2(sprite.Right(), sprite.Bottom());
    }
    public static Vector2 BotLeft(this SpriteRenderer sprite)
    {
        return new Vector2(sprite.Left(), sprite.Bottom());
    }

    public static float Height(this SpriteRenderer sprite)
    {
        return sprite.bounds.extents.y * 2;
    }

    public static float Width(this SpriteRenderer sprite)
    {
        return sprite.bounds.extents.x * 2;
    }

    public static float HalfHeight(this SpriteRenderer sprite)
    {
        return sprite.bounds.extents.y;
    }

    public static float HalfWidth(this SpriteRenderer sprite)
    {
        return sprite.bounds.extents.x;
    }
    #endregion
}
