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
    [SongContent(AssetName = "teleporter", AssetPath = "music/DANJYON_KIMURA_-_TELEPORTER", Artist = "Danjyon kimura", Name = "Space alone")]
    [SongContent(AssetName = "spacealone", AssetPath = "music/DANJYON_KIMURA_-_Space_Alone", Artist = "Danjyon kimura", Name = "Space alone")]
    public class MusicPlayer
    {

        private static MusicPlayer _instance;
        private float _volume;
        private string current_song;

        public MusicPlayer()
        {
            _instance = this;
            _volume = 0.5f;
            current_song = "";
            MediaPlayer.IsRepeating = true;
        }

        public void PlayMusic(String songname)
        {
            var song = Game1.Instance.magicContentManager.GetMusic(songname);
            if (current_song.CompareTo(songname) == 0) return;
            current_song = songname;
            _instance.PlaySong(song.Song);
        }

        public void StopMusic()
        {
            if (MediaState.Playing == MediaPlayer.State)
            {
                MediaPlayer.Stop();
                current_song = "";
            }

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
