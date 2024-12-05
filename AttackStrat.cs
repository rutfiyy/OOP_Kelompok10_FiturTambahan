using System;

namespace OOP_Kelompok2
{
    public interface IAttackStrategy
    {
        void Execute(Player player, Enemy enemy);
    }

    public class NormalAttack : IAttackStrategy
    {
        public void Execute(Player player, Enemy enemy)
        {
            Console.WriteLine($"{player.Name} uses Normal Attack!");
            int damage = player.Attack;
            enemy.Heart = Math.Max(enemy.Heart - damage, 0);
            Console.WriteLine($"{enemy.Name} takes {damage} damage. Remaining Heart: {enemy.Heart}");
        }
    }
}
