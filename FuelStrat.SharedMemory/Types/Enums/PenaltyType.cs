namespace FuelStrat.SharedMemory.Types.Enums;

public enum PenaltyType
{
    None = 0,
    DriveThroughCutting = 1,
    StopAndGo10Cutting = 2,
    StopAndGo20Cutting = 3,
    StopAndGo30Cutting = 4,
    DisqualifiedCutting = 5,
    RemoveBestLapTimeCutting = 6,
    DriveThroughPitSpeeding = 7,
    StopAndGo10PitSpeeding = 8,
    StopAndGo20PitSpeeding = 9,
    StopAndGo30PitSpeeding = 10,
    DisqualifiedPitSpeeding = 11,
    RemoveBestLapTimePitSpeeding = 12,
    DisqualifiedIgnoredMandatoryPit = 13,
    PostRaceTime = 14,
    DisqualifiedTrolling = 15,
    DisqualifiedPitEntry = 16,
    DisqualifiedPitExit = 17,
    DisqualifiedWrongWay = 18,
    DriveThroughIgnoredDriverStint = 19,
    DisqualifiedIgnoredDriverStint = 20,
    DisqualifiedExceededDriverStintLimit = 21
}