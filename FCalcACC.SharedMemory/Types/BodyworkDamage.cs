namespace FCalcACC.SharedMemory.Types;

public record struct BodywordDamage
{
    public float Right { get; init; }
    public float Left { get; init; }
    public float Front { get; init; }
    public float Rear { get; init; }
    public float Center { get; init; }

    public float TotalDamageSeconds()
    {
        return (Right + Left + Rear + Front) * 0.282f;
    }
}