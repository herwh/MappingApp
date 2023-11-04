using UnityEngine;

namespace DefaultNamespace
{
    public static class CanvasExtensions
    {
        public static (float, float) GetCanvasSize(this Canvas canvas)
        {
            var scaleFactor = canvas.scaleFactor;
            var width = Screen.width / scaleFactor;
            var height = Screen.height / scaleFactor;

            return (width, height);
        }
    }
}