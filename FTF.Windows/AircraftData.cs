namespace FTF.Windows
{
    public class AircraftData
    {
        // submission metadata
        [ADA(false)]
        public string callsign { get; set; }
        [ADA(false)]
        public string generatedDate { get; set; } // YYYY/MM/DD
        [ADA(false)]
        public string generatedTime { get; set; } // HH:MM:SS

        [ADA(true, "PLANE LATITUDE")]
        public double latitude { get; set; }

        [ADA(true, "PLANE LONGITUDE")]
        public double longitude { get; set; }

        [ADA(true, "INDICATED ALTITUDE")]
        public double altitude { get; set; }

        [ADA(true, "VERTICAL SPEED")]
        public double verticalRate { get; set; }

        [ADA(true, "TRANSPONDER CODE:1")]
        public string squawk { get; set; }

        [ADA(true, "GPS GROUND SPEED")]
        public double groundSpeed { get; set; }

        [ADA(true, "MAGNETIC COMPASS")]
        public double track { get; set; }

        [ADA(false)]
        public bool alert { get; set; } = false; //always false

        [ADA(false)]
        public bool emergency { get; set; } = false; //defaults to false unless squawk { 7500, 7600, 7700 }

        [ADA(false)]
        public bool spi { get; set; } = false; //always false

        [ADA(true, "CONTACT POINT IS ON GROUND")]
        public bool isOnGround { get; set; }
    }

    [System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.GenericParameter | System.AttributeTargets.Field)]
    public class ADA : System.Attribute //AircraftDataAttribute
    {
        public bool IsMappable;
        public string DefSimvar = null;
        public string Simvar = null; 
        public ADA(bool mappable, string defsimvar = null)
        {
            IsMappable = mappable;
            Simvar = DefSimvar = defsimvar;
        }
    }
}
