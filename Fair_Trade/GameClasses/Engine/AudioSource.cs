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

namespace Fair_Trade.GameClasses.Engine
{
    public class AudioSource
    {
        public enum AudioType {
            Loop,
            OneTime
        }
        private AudioType _audioType;
        private MediaPlayer _mediaPlayer;
    
        public AudioSource(AudioType audioType) { _audioType = audioType; }

        //public int Length { get { audioUri.} }
        public MediaPlayer MediaPlayer { get { return _mediaPlayer; } }

        public void SetPlayer(MediaPlayer player) => _mediaPlayer = player;        
        public void Play() {
            _mediaPlayer.Play();
        }
    }
}
