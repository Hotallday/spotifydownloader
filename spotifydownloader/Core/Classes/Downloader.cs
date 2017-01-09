using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace spotifydownloader.Core.Classes {
    public class Downloader {
        private readonly BlockingCollection<Song> _songs = new BlockingCollection<Song>();
        private readonly Semaphore _stopper = new Semaphore(1, 2);
        //public readonly Youtube Youtube;

        public Downloader() {
           // Youtube = Youtube.Instance;
            Debug.AutoFlush = true;
            ThreadPool.QueueUserWorkItem(a => {
                while (true) {
                    try {
                        _stopper.WaitOne();
                        ThreadPool.QueueUserWorkItem(aa => Download(_songs.Take()));
                    } catch (Exception) {
                        MessageBox.Show(@"An error has occurred please reload the application.");
                        break;
                    }
                }
            });
        }

        public void Add(Song song) {
            if (song == default(Song))
                throw new ArgumentNullException(nameof(song), @"Please enter a song.");
            Debug.WriteLine($"[Waiting] - Downloading song {song.SongName}");
            _songs.Add(song);
        }


        public void Download(object o) {
            var song = (Song) o;
            try {
                song.Doing = "Downloading...";
                var i = 0;
                while (i <= 1000000000) {
                    i++;
                    //Increment(song);
                }
                song.Doing = "Finished.";
            } catch
                (Exception
                    ex) {
                song.Doing = "Error has ocurred.";
                Debug.WriteLine(ex);
            } finally {
                _stopper.Release();
            }
        }
    }
}