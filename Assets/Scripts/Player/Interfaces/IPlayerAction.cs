using Player.Game;

namespace Player.Interfaces
{
    public interface IPlayerAction
    {
        public PlayerActionType Type { get; }
        public void Initialize(in IPlayerController playerController);
    }
}