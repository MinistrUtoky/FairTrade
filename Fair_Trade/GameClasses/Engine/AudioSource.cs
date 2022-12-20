using Fair_Trade.GameClasses.GameBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Windows.Media;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;

namespace Fair_Trade.GameClasses.Engine
{
    public class AudioSource
    {
        public enum AudioType
        {
            Loop,
            OneTime
        }
        private AudioType _audioType;
        private MediaPlayer _mediaPlayer;
        public bool isPlaying;

        public AudioSource(AudioType audioType) { _audioType = audioType; }

        //public int Length { get { audioUri.} }
        public MediaPlayer MediaPlayer { get { return _mediaPlayer; } }

        public void SetPlayer(MediaPlayer player) { 
            _mediaPlayer = player;
            _mediaPlayer.MediaEnded += Media_Ended;
        }
        public void Play() { isPlaying = true; }
        public void Stop() { isPlaying = false; }

        public void Media_Ended(object sender, EventArgs e)
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Position = TimeSpan.Zero;
            if (_audioType == AudioType.Loop)
            {
                _mediaPlayer.Play();
            }
        }        
    }
}
