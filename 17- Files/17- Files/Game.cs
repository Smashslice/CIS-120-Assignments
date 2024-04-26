using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17__Files
{
    internal class Game
    {
        private string Name;
        private string Genre;
        private string Developer;
        private int ReleaseDate;
        private bool ActiveDevelopment;
        private string GameProviders;

        public string name
        {
            get { return Name; }
            set { Name = value; }
        }
        public string genre
        {
            get { return Genre; }
            set { Genre = value; }
        }
        public string developer
        {
            get { return Developer; }
            set { Developer = value; }
        }
        public int releaseDate
        {
            get { return ReleaseDate; }
            set { ReleaseDate = value; }
        }
        public bool activeDevelopment
        {
            get { return ActiveDevelopment; }
            set { ActiveDevelopment = value; }
        }
        public string gameProviders
        {
            get { return GameProviders; }
            set { GameProviders = value; }
        }

        public override string ToString()
        {
            return $"--------------------\n\nGame Name: {name}\nGame Genre: {genre}\nGame Developer: {developer}\nGame Release Date: {releaseDate}\nIs the game still in development: {activeDevelopment}\nThe game is available on: {gameProviders}\n";
        }
    }
}
