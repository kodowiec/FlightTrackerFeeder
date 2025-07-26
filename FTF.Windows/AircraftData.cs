namespace FTF.Windows
{
    public class AircraftData
    {
        // submission metadata
        public string callsign { get; set; }
        public string generatedDate { get; set; } // YYYY/MM/DD
        public string generatedTime { get; set; } // HH:MM:SS

        public double latitude { get; set; }
        public double longitude { get; set; }
        public double altitude { get; set; }

        public double verticalRate { get; set; }
        public string squawk { get; set; }
        public double groundSpeed { get; set; }
        public double track {  get; set; }
        public bool alert { get; set; } = false; //always false
        public bool emergency { get; set; } = false; //defaults to false unless squawk { 7500, 7600, 7700 }
        public bool spi { get; set; } = false; //always false
        public bool isOnGround { get; set; }
    }
}
