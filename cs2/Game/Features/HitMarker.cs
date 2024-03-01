using cs2.Game.Objects;
using cs2.GameOverlay;
using cs2.Offsets;
using GameOverlay.Drawing;
using SharpDX.Multimedia;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Game.Features
{
    internal class HitMarker
    {
        public static void Initialize()
        {
            _device = new XAudio2();
            _masteringVoice = new MasteringVoice(_device);
            _voices = new List<SourceVoice>();

            foreach (FileInfo file in new DirectoryInfo(folder).GetFiles())
            {
                InitBuffer(file);
                return;
            }
            Program.Log($"Features.HitMarker.{nameof(_buffer)} is empty", ConsoleColor.Yellow);
        }

        private static void InitBuffer(FileInfo file)
        {
            FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);

            var stream = new SoundStream(fs);
            var waveFormat = stream.Format;
            var buffer = new AudioBuffer
            {
                Stream = stream.ToDataStream(),
                AudioBytes = (int)stream.Length,
                Flags = BufferFlags.EndOfStream
            };
            stream.Close();

            _buffer = buffer;

            _voices = new List<SourceVoice>();

            for (int i = 0; i < 6; i++)
            {
                SourceVoice voice = new SourceVoice(_device, waveFormat, true);
                voice.SetVolume(0.6f);
                voice.SubmitSourceBuffer(_buffer, stream.DecodedPacketsInfo);
                voice.BufferEnd += (context) =>
                {
                    voice.Stop();
                    voice.SubmitSourceBuffer(_buffer, stream.DecodedPacketsInfo);
                };
                _voices.Add(voice);
            }
        }

        private static int hit = 0;
        public static void Update()
        {
            IntPtr CCSPlayer_BulletServices = Memory.Read<IntPtr>(LocalPlayer.Current.AddressBase + OffsetsLoader.C_CSPlayerPawn.m_pBulletServices);
            int hits = Memory.Read<int>(CCSPlayer_BulletServices + 0x40);
            if (hits != hit)
            {
                if (hits != 0)
                {
                    if (Config.Configuration.Current.HitMarker)
                    {
                        PlayHit();
                        _hitMarkerOpacity = 1f;
                    }
                }
                hit = hits;
            }
        }

        public static void Draw(Graphics g)
        {
            if (!Config.Configuration.Current.HitMarker)
                return;

            Brushes.Share.Color = new Color(1f, 1f, 1f, _hitMarkerOpacity);

            g.DrawCrosshair(Brushes.Share, new Point(g.Width / 2, g.Height / 2), 8, 2, CrosshairStyle.Diagonal);

            _hitMarkerOpacity -= 0.04f;

            if (_hitMarkerOpacity < 0)
                _hitMarkerOpacity = 0;
        }

        public static void PlayHit()
        {
            if (_buffer == null)
            {
                return;
            }
            for (int i = 0; i < _voices.Count; i++)
            {
                var voice = _voices[i];
                if (voice.State.SamplesPlayed > 800)
                    continue;
                voice.Start();
                return;
            }

        }

        // lasthit po perhoti
        private static readonly string folder = Path.Combine(Directory.GetCurrentDirectory(), "sounds");

        private static AudioBuffer _buffer = null!;
        private static XAudio2 _device = null!;
        private static MasteringVoice _masteringVoice = null!;

        private static List<SourceVoice> _voices = new List<SourceVoice>();

        private static float _hitMarkerOpacity = 0f;

    }
}
