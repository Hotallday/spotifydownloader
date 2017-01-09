using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using spotifydownloader.Core;
using spotifydownloader.Core.Classes;

namespace spotifydownloader
{
    public partial class MainWindow : Window
    {
        private readonly Downloader _downloader;

        private readonly object _firstlock = new object();
        private readonly Spotify _spotify = new Spotify();
        private readonly Utilities _utilities = new Utilities();
       // private readonly Youtube _youtube = Youtube.Instance;
        public ObservableCollection<Song> Songs { get; }
        public MainWindow()
        {
            Songs = new ObservableCollection<Song>();
            _downloader = new Downloader();
            InitializeComponent();
            Downloads.ItemsSource = Songs;
            Debug.AutoFlush = true;
            BindingOperations.EnableCollectionSynchronization(Songs, _firstlock);
        }

        private void DropList_DragEnter(object sender, DragEventArgs e)
        {
            if (sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void DropList_Drop(object sender, DragEventArgs e)
        {     
                ThreadPool.QueueUserWorkItem(a => {
                    var stringData = e.Data.GetData(typeof(string)) as string;
                    var songs = stringData?.Split('\n');
                    if (songs == null) return;
                    try {
                        foreach (var result in
                            songs.Select(songin => songin?.Split('/'))
                                .Select(splitted => splitted?[splitted.Length - 1])) {
                            ThreadPool.QueueUserWorkItem(async aa => {
                                var song = await _spotify.DownloadSongInformationAsync(result);
                                Songs.Add(song);
                                _downloader.Add(song);
                            });
                        }
                    } catch (Exception ex) {
                        Debug.WriteLine(ex);
                    }
                });        
        }
    }
}