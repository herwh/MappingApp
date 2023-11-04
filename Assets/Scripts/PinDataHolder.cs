
namespace DefaultNamespace
{
    public class PinDataHolder
    {
        public readonly Pin Pin;
        public readonly PinData Data;

        public PinDataHolder(Pin pin, PinData data)
        {
            Pin = pin;
            Data = data;
        }
    }
}