using System;

[Flags]
public enum OutfitTag
{
    None = 0,
    Cool = 1 << 0,
    Dark = 1 << 1,
    Y2K = 1 << 2,
    Gothic = 1 << 3,
    Cute = 1 << 4,
    Sweet = 1 << 5,
    Waterproof = 1 << 6,
    Passionate = 1 << 7
}