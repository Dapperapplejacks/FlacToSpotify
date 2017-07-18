using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FlacToSpot
{
    class CommentFields : IEnumerable<string>
    {
        #region Properties

        public string title
        {
            get
            {
                return title;
            }
            private set
            {
                title = value;
            }
        }
        public string version
        {
            get
            {
                return version;
            }
            private set
            {
                version = value;
            }
        }
        public string album
        {
            get
            {
                return album;
            }
            private set
            {
                album = value;
            }
        }
        public string trackNumber
        {
            get
            {
                return trackNumber;
            }
            private set
            {
                trackNumber = value;
            }
        }
        public string artist
        {
            get
            {
                return artist;
            }
            private set
            {
                artist = value;
            }
        }
        public string performer
        {
            get
            {
                return performer;
            }
            private set
            {
                performer = value;
            }
        }
        public string copyright
        {
            get
            {
                return copyright;
            }
            private set
            {
                copyright = value;
            }
        }
        public string license
        {
            get
            {
                return license;
            }
            private set
            {
                license = value;
            }
        }
        public string organization
        {
            get
            {
                return organization;
            }
            private set
            {
                organization = value;
            }
        }
        public string description
        {
            get
            {
                return description;
            }
            private set
            {
                description = value;
            }
        }
        public string genre
        {
            get
            {
                return genre;
            }
            private set
            {
                genre = value;
            }
        }
        public string date
        {
            get
            {
                return date;
            }
            private set
            {
                date = value;
            }
        }
        public string location
        {
            get
            {
                return location;
            }
            private set
            {
                location = value;
            }
        }
        public string contact
        {
            get
            {
                return contact;
            }
            private set
            {
                contact = value;
            }
        }
        public string ISRC
        {
            get
            {
                return ISRC;
            }
            private set
            {
                ISRC = value;
            }
        }

        #endregion

        public CommentFields(string title = "Untitled", string version = "", string album = "", string trackNumber = "0",
                   string artist = "", string performer = "", string copyright = "", string license = "",
                   string organization = "", string description = "", string genre = "", string date = "",
                   string location = "", string contact = "", string ISRC = "")
        {
            this.title = title;
            this.version = version;
            this.album = album;
            this.trackNumber = trackNumber;
            this.artist = artist;
            this.performer = performer;
            this.copyright = copyright;
            this.license = license;
            this.organization = organization;
            this.description = description;
            this.genre = genre;
            this.date = date;
            this.location = location;
            this.contact = contact;
            this.ISRC = ISRC;
        }




        public IEnumerator<string> GetEnumerator()
        {
            yield return title;
            yield return version;
            yield return album;
            yield return trackNumber;
            yield return artist;
            yield return performer;
            yield return copyright;
            yield return license;
            yield return organization;
            yield return description;
            yield return genre;
            yield return date;
            yield return location;
            yield return contact;
            yield return ISRC;

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
