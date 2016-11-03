using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.IO;

namespace Hungry_Man
{
    /// <summary>
    /// Controls playback of an Animation.
    /// </summary>

    struct AnimationPlayer
    {
        private Animation animation;
        private Rectangle partAnimarion;
        private bool isPlaying;
        private bool stop;
        private float time;
        private int frameIndex;
        

        #region "Properties"

        public Rectangle PartAnimarion
        {
            get { return partAnimarion; }
        }       

        public Animation Animation
        {
            get { return animation; }
            set { animation = value; }
        }

        /// <summary>
        /// Gets the index of the current frame in the animation.
        /// </summary>
        public int FrameIndex
        {
            get { return frameIndex; }
        }      

        public bool IsPlaying
        {
            get { return isPlaying; }
        }

        #endregion

        /// <summary>
        /// Begins or continues playback of an animation.
        /// </summary>
        public AnimationPlayer(Animation animation)
        {
            this.animation = animation;
            this.partAnimarion = Rectangle.Empty;            
            this.isPlaying = true;
            this.stop = false;
            this.time = 0.0f;
            this.frameIndex = 0;            
        }

        /// <summary>
        /// Advances the time position and draws the current frame of the animation.
        /// </summary>
        /// 
        public void Update(GameTime gameTime)
        {
            if (!stop)
            {
                // Process passing time.
                time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                while (time > animation.FrameTime)
                {
                    time -= animation.FrameTime;

                    // Advance the frame index; looping or clamping as appropriate.
                    if (animation.IsLooping)
                    {
                        frameIndex = (frameIndex + 1) % animation.FrameCount;
                    }
                    else
                    {
                        isPlaying = true;

                        if (frameIndex < animation.FrameCount - 1)
                            frameIndex++;
                        else
                            isPlaying = false;
                    }
                }
            }

            // Calculate the source rectangle of the current frame.
            partAnimarion = new Rectangle(FrameIndex * animation.Texture.Height, 0, animation.Texture.Height, animation.Texture.Height);

        }

        public void Stop()
        {
            stop = true;
        }

        public void Play()
        {
            stop = false;
        }
    }
}
