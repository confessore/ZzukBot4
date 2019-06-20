namespace Grinderv2.Models
{
    public class Creature
    {
        public int Guid { get; set; }
        public int Id { get; set; }
        public int Map { get; set; }
        public float Position_X { get; set; }
        public float Position_Y { get; set; }
        public float Position_Z { get; set; }
        public int SpawnTimeSecsMax { get; set; }
    }
}
