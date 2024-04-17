using System.Runtime.InteropServices;
using FCalcACC.SharedMemory.Types;

namespace FCalcACC.SharedMemory;

[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode)]
[Serializable]
public record Physics
{
    public int PacketId { get; init; }
    public float Gas { get; init; }
    public float Brake { get; init; }
    public float Fuel { get; init; }
    public int Gear { get; init; }
    public int Rpm { get; init; }
    public float SteerAngle { get; init; }
    public float SpeedKmh { get; init; }
    public Vec3 Velocity { get; init; }
    public Vec3 AccG { get; init; }
    public Wheels<float> WheelSlip { get; init; }
    public Wheels<float> WheelLoad { get; init; }
    public Wheels<float> WheelsPressure { get; init; }
    public Wheels<float> WheelAngularSpeed { get; init; }
    public Wheels<float> TyreWear { get; init; }
    public Wheels<float> TyreDirtyLevel { get; init; }
    public Wheels<float> TyreCoreTemperature { get; init; }
    public Wheels<float> CamberRad { get; init; }
    public Wheels<float> SuspensionTravel { get; init; }
    public float Drs { get; init; }
    public float Tc { get; init; }
    public float Heading { get; init; }
    public float Pitch { get; init; }
    public float Roll { get; init; }
    public float CgHeight { get; init; }
    public BodywordDamage CarDamage { get; init; }
    public int NumberOfTyresOut { get; init; }
    public int PitLimiterOn { get; init; }
    public float Abs { get; init; }
    public float KersCharge { get; init; }
    public float KersInput { get; init; }
    public int AutoShifterOn { get; init; }
    public RideHeight RideHeight { get; init; }
    public float TurboBoost { get; init; }
    public float Ballast { get; init; }
    public float AirDensity { get; init; }
    public float AirTemp { get; init; }
    public float RoadTemp { get; init; }
    public Vec3 LocalAngularVelocity { get; init; }
    public float FinalFf { get; init; }
    public float PerformanceMeter { get; init; }
    public int EngineBrake { get; init; }
    public int ErsRecoveryLevel { get; init; }
    public int ErsPowerLevel { get; init; }
    public int ErsHeatCharging { get; init; }
    public int ErsIsCharging { get; init; }
    public float KersCurrentKj { get; init; }
    public int DrsAvailable { get; init; }
    public int DrsEnabled { get; init; }
    public Wheels<float> BrakeTemp { get; init; }
    public float Clutch { get; init; }
    public Wheels<float> TyreTempI { get; init; }
    public Wheels<float> TyreTempM { get; init; }
    public Wheels<float> TyreTempO { get; init; }
    public int IsAiControlled { get; init; }
    public Wheels<Vec3> TyreContactPoint { get; init; }
    public Wheels<Vec3> TyreContactNormal { get; init; }
    public Wheels<Vec3> TyreContactHeading { get; init; }
    public float BrakeBias { get; init; }
    public Vec3 LocalVelocity { get; init; }
    public int P2PActivation { get; init; }
    public int P2PStatus { get; init; }
    public float CurrentMaxRpm { get; init; }
    public Vec4 Mz { get; init; }
    public Vec4 Fx { get; init; }
    public Vec4 Fy { get; init; }
    public Wheels<float> SlipRatio { get; init; }
    public Wheels<float> SlipAngle { get; init; }
    public int TcInAction { get; init; }
    public int AbsInAction { get; init; }
    public Wheels<float> SuspensionDamage { get; init; }
    public Wheels<float> TyreTemp { get; init; }
    public float WaterTemp { get; init; }
    public Wheels<float> BrakePressure { get; init; }
    public int FrontBrakeCompound { get; init; }
    public int RearBrakeCompound { get; init; }
    public Wheels<float> PadLife { get; init; }
    public Wheels<float> DiscLife { get; init; }
    public int IgnitionOn { get; init; }
    public int StarterEngineOn { get; init; }
    public int IsEngineRunning { get; init; }
    public float KerbVibration { get; init; }
    public float SlipVibrations { get; init; }
    public float GVibrations { get; init; }
    public float AbsVibrations { get; init; }
}