using CustomProgram.Zombies;

namespace CustomProgram.Factory
{
    public class FactoryZombieFootball : ZombieFactory //concretecreator 
    {
        public Zombie GetZombie()
        {
            Zombie zombie = new ZombieFootball();
            return zombie;
        }
    }
}
