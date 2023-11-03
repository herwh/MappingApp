using System.Collections.Generic;

namespace DefaultNamespace
{
    [System.Serializable]
    public class PinData
    {
        public int id;
        public PinPosition pinPosition;
        public string title;
        public string description;
        public string imagePath;
    }

    [System.Serializable]
    public struct PinPosition
    {
        public float x;
        public float y;

        public PinPosition(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    [System.Serializable]
    public class GameData
    {
        public List<PinData> pins;
    }
}