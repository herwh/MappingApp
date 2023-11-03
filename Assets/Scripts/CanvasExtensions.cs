using UnityEngine;

namespace DefaultNamespace
{
    public static class CanvasExtensions
    {
        public static (float, float) GetCanvasSize(this Canvas canvas)
        {
            var width = Screen.width / canvas.scaleFactor;
            var height = Screen.height / canvas.scaleFactor;

            return (width, height);
        }
    }
}