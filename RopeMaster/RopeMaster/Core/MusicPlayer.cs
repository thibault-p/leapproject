using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Glitch.Engine.Content;
using System.Threading;



namespace RopeMaster.Core
{

    [SongContent(AssetName = "spacealone", AssetPath = "music/DANJYON_KIMURA_-_Space_Alone", Artist = "Danjyon kimura", Name = "Space alone")]
    public class MusicPlayer
    {

        private static MusicPlayer _instance;
        private float _volume;

        public MusicPlayer()
        {
            _instance = this;
            _volume = 0.5f;
            MediaPlayer.IsRepeating = true;
        }

        public void PlayMusic(String songname)
        {
             var song =Game1.Instance.magicContentManager.GetMusic(songname);
             _instance.PlaySong(song.Song);
        }


        private void PlaySong(Song song)
        {
    
            new Thread(new ThreadStart(() =>
            {
                MediaPlayer.Volume = _volume;
                MediaPlayer.Play(song);
            })).Start();
        }
    }
}
