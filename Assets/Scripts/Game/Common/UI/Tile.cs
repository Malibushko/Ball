using UnityEngine.UIElements;

namespace Game.Common.UI
{
    public class Tile : Image
    {
        public new class UxmlFactory : UxmlFactory<Tile, UxmlTraits>{}
    }
}