using System;

[Flags]
public enum EBuildRule : byte
{
    Floor = 1,
    Wall = 2,
    ThisTypeObject = 4,
    OtherTypeObject = 8
}