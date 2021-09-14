public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    Undefined
}

public static class DirectionExtension
{
    public static Direction Oposite(this Direction s)
    {
        switch (s)
        {
            case Direction.Up:
                return Direction.Down;
            case Direction.Down:
                return Direction.Up;
            case Direction.Left:
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
            default:
                return Direction.Undefined;
        }
    }
}