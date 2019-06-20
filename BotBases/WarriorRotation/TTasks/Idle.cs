using TreeTaskCore;

namespace WarriorRotation.TTasks
{
    public class Idle : TTask
    {
        public override int Priority => 0;

        public override bool Activate()
        {
            return true;
        }

        public override void Execute()
        {
            
        }
    }
}
