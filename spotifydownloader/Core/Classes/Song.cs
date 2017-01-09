using System.Collections.Generic;
using System.ComponentModel;

namespace spotifydownloader.Core.Classes {
    public class Song : INotifyPropertyChanged {
        private string _albumname;
        private List<string> _artist;
        private string _doing;
        private string _ico;
        private string _id;
        private string _image;
        private double _length;
        private int _progress;
        private string _songname;
        private string _youtubeid;


        public string Id {
            get { return _id; }
            set {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        public string AlbumName {
            get { return _albumname; }
            set {
                _albumname = value;
                NotifyPropertyChanged("AlbumName");
            }
        }

        public string SongName {
            get { return _songname; }
            set {
                _songname = value;
                NotifyPropertyChanged("SongName");
            }
        }

        public string Image {
            get { return _image; }
            set {
                _image = value;
                NotifyPropertyChanged("Image");
            }
        }

        public string Ico {
            get { return _ico; }
            set {
                _ico = value;
                NotifyPropertyChanged("Ico");
            }
        }

        public string Doing {
            get { return _doing; }
            set {
                _doing = value;
                NotifyPropertyChanged("Doing");
            }
        }

        public int Progress {
            get { return _progress; }
            set {
                _progress = value;
                NotifyPropertyChanged("Progress");
            }
        }

        public List<string> Artists { get; set; }
        public double Length { get; set; }
        public string YoutubeId { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}