using CrooksAndCastles.Characters;

namespace CrooksAndCastles.Interfaces
{
    public interface ISkills
    {
        int Health { get; set; }
        int Damage { get; set; }
        bool IsAlive { get; set; }
    }
}