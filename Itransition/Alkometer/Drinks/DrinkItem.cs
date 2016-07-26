using System;

namespace Alkometer.Shared
{
    /// <summary>
    /// Todo Item business object
    /// </summary>
    public class DrinkItem
    {
        int id;
        string name;
        double volume;
        double alcoholByVolume;
        string about;
        int iconID;

        public DrinkItem()
        {
            id = 0;
            name = "";
            volume = 0;
            alcoholByVolume = 0;
            about = "";
            iconID = 0;
        }

        public DrinkItem(string name, double volume, double alcoholByVolume, string about, int iconID)
        {
            id = 0;
            this.name = name;
            this.volume = volume;
            this.alcoholByVolume = alcoholByVolume;
            this.about = about;
            this.iconID = iconID;
        }

        public int ID { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public double Volume { get { return volume;} set { volume = value; }}
        public double AlcoholByVolume { get { return alcoholByVolume; } set { alcoholByVolume = value; } }
        public string About { get { return about; } set { about = value; } }
        public int IconId { get { return iconID; } set { iconID = value; } }
	}
}