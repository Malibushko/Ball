using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Common.UI
{
    public class TiledImage : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<TiledImage, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _imagePath = new()
            {
                name = "image-path",
                defaultValue = ""
            };

            private readonly UxmlFloatAttributeDescription _tileWidth = new()
            {
                name = "tile-width",
                defaultValue = 32f
            };

            private readonly UxmlFloatAttributeDescription _tileHeight = new()
            {
                name = "tile-height",
                defaultValue = 32f
            };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                var tiledBackground = ve as TiledImage;
                if (tiledBackground != null)
                {
                    tiledBackground.ImagePath = _imagePath.GetValueFromBag(bag, cc);
                    tiledBackground.TileWidth = _tileWidth.GetValueFromBag(bag, cc);
                    tiledBackground.TileHeight = _tileHeight.GetValueFromBag(bag, cc);
                }
            }
        }

        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                UpdateBackground();
            }
        }

        private float _tileWidth;
        public float TileWidth
        {
            get => _tileWidth;
            set
            {
                _tileWidth = value;
                UpdateBackground();
            }
        }

        private float _tileHeight;
        public float TileHeight
        {
            get => _tileHeight;
            set
            {
                _tileHeight = value;
                UpdateBackground();
            }
        }

        private void UpdateBackground()
        {
            if (string.IsNullOrEmpty(_imagePath))
            {
                style.backgroundImage = null;
                return;
            }

            var texture = Resources.Load<Texture2D>(_imagePath);
            if (texture == null)
            {
                Debug.LogError($"Texture not found at path: {_imagePath}");
                return;
            }

            style.backgroundImage = new StyleBackground(texture);
            style.backgroundSize = new StyleBackgroundSize(new BackgroundSize(new Length(_tileWidth, LengthUnit.Pixel), new Length(_tileHeight, LengthUnit.Pixel)));
            style.backgroundRepeat = new StyleBackgroundRepeat(new BackgroundRepeat(Repeat.Repeat, Repeat.Repeat));
        }
    }
}