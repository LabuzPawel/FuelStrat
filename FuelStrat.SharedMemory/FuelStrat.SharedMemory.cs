using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using PrecisionTiming;
using FuelStrat.SharedMemory.Types.Enums;

namespace FuelStrat.SharedMemory
{
    public class TelemetryReader : IDisposable
    {
        public event Action<GraphicInfos>? GraphicUpdated;
        public event Action<Physics>? PhysicsUpdated;
        public event Action<StaticInfos>? StaticInfosUpdated;
        public event Action<GameStatus>? GameStatusChanged;

        private const string PhysicPath = "Local\\acpmf_physics";
        private const string GraphicPath = "Local\\acpmf_graphics";
        private const string StaticInfoPath = "Local\\acpmf_static";

        private readonly MemoryMappedFile _physicsMap;
        private readonly MemoryMappedFile _graphicsMap;
        private readonly MemoryMappedFile _staticInfosMap;

        public static PrecisionTimer GraphicsTimer = new();
        public static PrecisionTimer StaticInfosTimer = new();
        public static PrecisionTimer PhysicsTimer = new();
        private readonly bool _newDataOnly;

        private Physics? _oldPhysics;
        private GraphicInfos? _oldGraphics;
        private StaticInfos? _oldStaticInfos;

        /// <summary>
        /// Assetto Corsa (including Assetto Corsa Competizione) shared memory reader, this object will trigger events at a set rates
        /// </summary>
        /// <param name="physicsInterval">Time in milliseconds between physics update</param>
        /// <param name="graphicsInterval">Time in milliseconds between graphics update</param>
        /// <param name="staticInterval">Time in milliseconds between statics info update</param>
        /// <param name="newDataOnly">If set to true, event will only be triggered if the data read is new</param>
        public TelemetryReader(int physicsInterval = 1, int graphicsInterval = 3, int staticInterval = 3,
            bool newDataOnly = false)
        {
            _newDataOnly = newDataOnly;

            _physicsMap = MemoryMappedFile.CreateOrOpen(PhysicPath, Marshal.SizeOf<Physics>());
            _graphicsMap = MemoryMappedFile.CreateOrOpen(GraphicPath, Marshal.SizeOf<GraphicInfos>());
            _staticInfosMap = MemoryMappedFile.CreateOrOpen(StaticInfoPath, Marshal.SizeOf<StaticInfos>());

            PhysicsTimer.SetInterval(ReadPhysics, physicsInterval);
            GraphicsTimer.SetInterval(ReadGraphics, graphicsInterval);
            StaticInfosTimer.SetInterval(ReadStaticInfos, staticInterval);
        }

        ~TelemetryReader()
        {
            Dispose(false);
        }

        public void Start()
        {
            GraphicsTimer.Start();
            StaticInfosTimer.Start();
            PhysicsTimer.Start();
        }

        public void Stop()
        {
            GraphicsTimer.Stop();
            StaticInfosTimer.Stop();
            PhysicsTimer.Stop();
        }

        private static T? ReadMap<T>(MemoryMappedFile file)
        {
            using var stream = file.CreateViewStream();
            using var reader = new BinaryReader(stream);

            var bytes = reader.ReadBytes(Marshal.SizeOf<T>());
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var data = Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            handle.Free();

            return data;
        }

        private void ReadPhysics()
        {
            var data = ReadMap<Physics>(_physicsMap);
            if (data is null || (_newDataOnly && data == _oldPhysics)) return;

            _oldPhysics = data;
            PhysicsUpdated?.Invoke(data);
        }

        private void ReadGraphics()
        {
            var data = ReadMap<GraphicInfos>(_graphicsMap);

            if (data is null || (_newDataOnly && data == _oldGraphics)) return;

            if (_oldGraphics is null || _oldGraphics.Status != data.Status)
                GameStatusChanged?.Invoke(data.Status);

            _oldGraphics = data;
            GraphicUpdated?.Invoke(data);
        }

        private void ReadStaticInfos()
        {
            var data = ReadMap<StaticInfos>(_staticInfosMap);
            if (data is null || (_newDataOnly && data == _oldStaticInfos)) return;

            _oldStaticInfos = data;
            StaticInfosUpdated?.Invoke(data);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _isDisposed;

        private void Dispose(bool isDisposing)
        {
            if (_isDisposed) return;

            GraphicsTimer.Stop();
            StaticInfosTimer.Stop();
            PhysicsTimer.Stop();

            if (isDisposing)
            {
                _physicsMap.Dispose();
                _graphicsMap.Dispose();
                _staticInfosMap.Dispose();
            }

            _isDisposed = true;
        }
    }
}