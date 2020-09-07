using BMAPI.v1;
using System;
using System.IO;

namespace OsuDbAPI
{
    /*
        This class will hold the data obtained by the beatmap from osu!.db
        It it does not store star difficulty or timing points
    */
    public class Beatmap
    {
        public string ArtistName
        {
            get; set;
        }
        public string ArtistNameUnicode
        {
            get; set;
        }
        public string SongTitle
        {
            get; set;
        }
        public string SongTitleUnicode
        {
            get; set;
        }
        public string Creator
        {
            get; set;
        }
        public string Difficulty
        {
            get; set;
        }
        public string AudioFile
        {
            get; set;
        }
        public string Hash
        {
            get; set;
        }
        public string OsuFile
        {
            get; set;
        }
        public byte RankedStatus
        {
            get; set;
        }
        public ushort NumHitcircles
        {
            get; set;
        }
        public ushort NumSliders
        {
            get; set;
        }
        public ushort NumSpinners
        {
            get; set;
        }
        public DateTime LastModificationTime
        {
            get; set;
        }
        public float ApproachRate
        {
            get; set;
        }
        public float CircleSize
        {
            get; set;
        }
        public float HPDrain
        {
            get; set;
        }
        public float OverallDifficulty
        {
            get; set;
        }
        public double SliderVelocity
        {
            get; set;
        }
        public int DrainTimeSeconds
        {
            get; set;
        }
        public int DrainTimeMilliseconds
        {
            get; set;
        }
        public int PreviewPoint
        {
            get; set;
        }
        public int ID
        {
            get; set;
        }
        public int SetID
        {
            get; set;
        }
        public int ThreadID
        {
            get; set;
        }
        public byte GradeOsu
        {
            get; set;
        }
        public byte GradeTaiko
        {
            get; set;
        }
        public byte GradeCTB
        {
            get; set;
        }
        public byte GradeMania
        {
            get; set;
        }
        public ushort LocalOffset
        {
            get; set;
        }
        public float StackLeniency
        {
            get; set;
        }
        public byte GameMode
        {
            get; set;
        }
        public string Source
        {
            get; set;
        }
        public string Tags
        {
            get; set;
        }
        public ushort OnlineOffset
        {
            get; set;
        }
        public string Font
        {
            get; set;
        }
        public bool Unplayed
        {
            get; set;
        }
        public DateTime LastPlayed
        {
            get; set;
        }
        public bool Osz2
        {
            get; set;
        }
        public string FolderName
        {
            get; set;
        }
        public DateTime LastCheck
        {
            get; set;
        }
        public bool IgnoreHitSounds
        {
            get; set;
        }
        public bool IgnoreSkin
        {
            get; set;
        }
        public bool DisableStoryboard
        {
            get; set;
        }
        public bool DisableVideo
        {
            get; set;
        }
        public bool VisualOverride
        {
            get; set;
        }
        public int LastModTime
        {
            get; set;
        }
        public byte ManiaScrollSpeed
        {
            get; set;
        }
        public string[] SearchableTerms => new[]
        {
            Creator,
            ArtistName,
            ArtistNameUnicode,
            SongTitle,
            SongTitleUnicode,
            //Difficulty,
            Source,
            Tags
        };

        public Beatmap()
        {
        }

        public BMAPI.v1.Beatmap Load(string songsFolder)
        {
            return new BMAPI.v1.Beatmap(Path.Combine(songsFolder, this.FolderName, this.OsuFile));
        }

        public override string ToString()
        {
            return $"{ArtistName} - {SongTitle} [{Difficulty}] ({Creator})";
        }
    }
}
