using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using PrecisionTiming;
using FCalcACC.SharedMemory.Types.Enums;

namespace FCalcACC.SharedMemory
{
    public class TelemetryReader : IDisposable
    {
        public event Action<Graphics>? GraphicUpdated;
        public event Action<Physics>? PhysicsUpdated;
        public event Action<StaticInfos>? StaticInfosUpdated;
        public event Action<GameStatus>? GameStatusChanged;

        private const string PhysicPath = "Local\\acpmf_physics";
        private const string GraphicPath = "Local\\acpmf_graphics";
        private const string StaticInfoPath = "Local\\acpmf_static";

        private readonly MemoryMappedFile _physicsMap;
        private readonly MemoryMappedFile _graphicsMap;
        private readonly MemoryMappedFile _staticInfosMap;

        public static PrecisionTimer MyTimer = new();
        private readonly bool _newDataOnly;

        private Physics? _oldPhysics;
        private Graphics? _oldGraphics;
        private StaticInfos? _oldStaticInfos;

        /// <summary>
        /// Assetto Corsa (including Assetto Corsa Competizione) shared memory reader, this object will trigger events at a set rates
        /// </summary>
        /// <param name="physicsInterval">Time in milliseconds between physics update</param>
        /// <param name="graphicsInterval">Time in milliseconds between graphics update</param>
        /// <param name="staticInterval">Time in milliseconds between statics info update</param>
        /// <param name="newDataOnly">If set to true, event will only be triggered if the data read is new</param>
        public TelemetryReader(int physicsInterval = 2, int graphicsInterval = 10, int staticInterval = 1000,
            bool newDataOnly = false)
        {
            _newDataOnly = newDataOnly;

            _physicsMap = MemoryMappedFile.CreateOrOpen(PhysicPath, Marshal.SizeOf<Physics>());
            _graphicsMap = MemoryMappedFile.CreateOrOpen(GraphicPath, Marshal.SizeOf<Graphics>());
            _staticInfosMap = MemoryMappedFile.CreateOrOpen(StaticInfoPath, Marshal.SizeOf<StaticInfos>());

            //MyTimer.SetInterval(ReadPhysics, physicsInterval);
            MyTimer.SetInterval(ReadGraphics, graphicsInterval);
            //MyTimer.SetInterval(ReadStaticInfos, staticInterval);
        }

        ~TelemetryReader()
        {
            Dispose(false);
        }

        public void Start()
        {
            MyTimer.Start();
        }

        public void Stop()
        {
            MyTimer.Stop();
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
            var data = ReadMap<Graphics>(_graphicsMap);

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

            MyTimer.Stop();

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