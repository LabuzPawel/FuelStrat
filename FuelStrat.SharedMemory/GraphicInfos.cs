using System.Runtime.InteropServices;
using FuelStrat.SharedMemory.Types;
using FuelStrat.SharedMemory.Types.Enums;

namespace FuelStrat.SharedMemory;

[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode)]
[Serializable]
public record GraphicInfos
{
    public int PacketId;
    public GameStatus Status;
    public AcSessionType Session;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string? CurrentTime;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string? LastTime;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string? BestTime;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string? Split;

    public int CompletedLaps;
    public int Position;
    public int ICurrentTime;
    public int ILastTime;
    public int IBestTime;
    public float SessionTimeLeft;
    public float DistanceTraveled;

    [MarshalAs(UnmanagedType.Bool)] 
    public bool IsInPits;

    public int CurrentSectorIndex;
    public int LastSectorTime;
    public int NumberOfLaps;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string? TyreCompound;

    public float ReplayTimeMultiplier;
    public float NormalizedCarPosition;
    public int ActiveCars;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
    public Vec3[]? CarCoordinates;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
    public int[]? CarIDs;

    public int PlayerCarID;
    public float PenaltyTime;
    public FlagType Flag;
    public PenaltyType Penalty;
    public int IdealLineOn;

    [MarshalAs(UnmanagedType.Bool)] 
    public bool IsInPitLane;

    public float SurfaceGrip;
    public int MandatoryPitDone;
    public float WindSpeed;
    public float WindDirection;
    public int IsSetupMenuVisible;
    public int MainDisplayIndex;
    public int SecondaryDisplayIndex;
    public int Tc;
    public int TcCut;
    public int EngineMap;
    public int Abs;
    public float FuelXLap;
    public int RainLights;
    public int FlashingLights;
    public int LightsStage;
    public float ExhaustTemperature;
    public int WiperLV;
    public int DriverStintTotalTimeLeft;
    public int DriverStintTimeLeft;
    public int RainTyres;
    public int SessionIndex;
    public float UsedFuel;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string? DeltaLapTime;

    public int IDeltaLapTime;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string? EstimatedLapTime;

    public int IEstimatedLapTime;
    public int IsDeltaPositive;
    public int ISplit;
    public int IsValidLap;
    public float FuelEstimatedLaps;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string? TrackStatus;

    public int MissingMandatoryPits;
    public float Clock;
    public int DirectionLightsLeft;
    public int DirectionLightsRight;

    public int GlobalYellow;
    public int GlobalYellow1;
    public int GlobalYellow2;
    public int GlobalYellow3;
    public int GlobalWhite;
    public int GlobalGreen;
    public int GlobalChequered;
    public int GlobalRed;
    public int MfdTyreSet;
    public float MfdFuelToAdd;
    public Wheels<float> MfdTyrePressure;
    public TrackGripStatus trackGripStatus;
    public RainIntensity RainIntensity;
    public RainIntensity RainIntensityIn10min;
    public RainIntensity RainIntensityIn30min;
    public int CurrentTyreSet;
    public int StrategyTyreSet;
    public int GapAhead;
    public int GapBehind;
}