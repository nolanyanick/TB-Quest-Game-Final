using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class IslandLocation
    {
        #region FIELDS

        private string _commonName;
        private int _islandLocationID; // must be a unique value for each object
        private int _date;
        private string _coordinates;
        private string _description;
        private string _generalContents;
        private bool _accessible;
        private int _experiencePoints;

        #endregion


        #region PROPERTIES

        public string CommonName
        {
            get { return _commonName; }
            set { _commonName = value; }
        }

        public int IslandLocationID
        {
            get { return _islandLocationID; }
            set { _islandLocationID = value; }
        }

        public int Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Coordinates
        {
            get { return _coordinates; }
            set { _coordinates = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string GeneralContents
        {
            get { return _generalContents; }
            set { _generalContents = value; }
        }

        public bool Accessible
        {
            get { return _accessible; }
            set { _accessible = value; }
        }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set { _experiencePoints = value; }
        }

        #endregion


        #region CONSTRUCTORS

        

        #endregion


        #region METHODS


        #endregion
    }
}
