namespace Core.Maze.Traps
{
    public interface ITrap
    {
        float DealDamage();

        void ChallengePassed();

        void ChallengeFailed();
    }
}

