using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Fair_Trade.GameClasses.Engine
{
    public class Animation
    {
        public enum AnimationType {
            Loop,
            OneTime
        }
        private AnimationType _animationType;
        private List<Image> _frames;
        private int _animationFrameRate;
        private Stopwatch _animationFrameRateCap;
        private int _currentFramePos;
        private bool _canBePlayed = true;
        public Animation(AnimationType type, List<Image> frameList, int animationFrameRate = GameMode.maxFrameRate) { 
            _animationType = type; _frames = frameList; _currentFramePos = 0;
            _animationFrameRate = animationFrameRate > GameMode.maxFrameRate ? GameMode.maxFrameRate: animationFrameRate ; 
            _animationFrameRateCap = new Stopwatch();
        }

        public void LoopAnimation() => _animationType = AnimationType.Loop;
        public void UnloopAnimation() => _animationType = AnimationType.OneTime;
        public void Clear() => _frames = new List<Image>();
        public void AddFrame(Image frame) => _frames.Add(frame);
        public void RemoveFrame(int framePosition) => _frames.RemoveAt(framePosition);
        public void AssignFrameList(List<Image> frames) => _frames = frames;
        public void Restart() => _currentFramePos = 0;
        public void GoToFrame(int frame) => _currentFramePos = frame % _frames.Count;
        public Image GetNextFrame()
        {
            if (_canBePlayed )
            {
                if (_currentFramePos > _frames.Count - 1)
                {
                    if (_animationType == AnimationType.OneTime) Pause();
                    else Restart();
                }
                if (_animationFrameRateCap.ElapsedMilliseconds < 1000 / _animationFrameRate) return _frames[_currentFramePos];
                _animationFrameRateCap.Restart();
                return _frames[_currentFramePos++];
            }
            return _frames[_currentFramePos];
        }
        public void Play() { _canBePlayed = true; _animationFrameRateCap.Start(); }
        public void Pause() { _canBePlayed = false; _animationFrameRateCap.Stop(); }
        public void SetFrameRate(int frameRate) => _animationFrameRate = frameRate > GameMode.maxFrameRate ? GameMode.maxFrameRate : frameRate;
        public int FrameRate { get { return _animationFrameRate; } }
        public int FrameCount { get { return _frames.Count; } }
        public int CurrentFramePosition { get { return _currentFramePos; } }
    }
}
