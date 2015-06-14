using CrooksAndCastles.Characters;

namespace CrooksAndCastles.Interfaces
{
    public interface IAttack
    {
        int Health { get; set; }
        int Damage { get; set; }
        bool IsAlive { get; set; }
    }
}