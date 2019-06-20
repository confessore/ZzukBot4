using Harvester.Engine.Modules;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Harvester.Engine
{
    public class Controller
    {
        Flow Flow { get; }
        Inventory Inventory { get; }
        ObjectManager ObjectManager { get; }
        PathModule PathModule { get; }

        public Controller(
            Flow flow,
            Inventory inventory,
            ObjectManager objectManager, 
            PathModule pathModule)
        {
            Flow = flow;
            Inventory = inventory;
            ObjectManager = objectManager;
            PathModule = pathModule;
        }

        public void Behavior()
        {
            switch (StateLogic())
            {
                case STATUS.ALIVE:
                    Flow.ExecuteFlow();
                    return;
                case STATUS.DEAD:
                    ObjectManager.Player.RepopMe();
                    return;
                case STATUS.GHOST:
                    PathModule.Traverse(ObjectManager.Player.CorpsePosition);
                    if (ObjectManager.Player.CorpsePosition
                        .GetDistanceTo(ObjectManager.Player.Position) < 20)
                            ObjectManager.Player.RetrieveCorpse();
                    return;
            }
        }

        STATUS StateLogic()
        {
            LocalPlayer player = ObjectManager.Player;
            WoWUnit target = ObjectManager.Target;

            if (player.IsDead)
                return STATUS.DEAD;
            else if (player.InGhostForm)
                return STATUS.GHOST;
            return STATUS.ALIVE;
        }

        enum STATUS
        {
            ALIVE,
            DEAD,
            GHOST,
        }
    }
}
