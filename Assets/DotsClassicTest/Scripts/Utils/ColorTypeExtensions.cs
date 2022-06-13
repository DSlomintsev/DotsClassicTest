using UnityEngine;

namespace DotsClassicTest.Utils
{
    public static class ColorTypeUtils
    {
        public static Color ToColor(this ColorType colorType)
        {
            return colorType switch
            {
                ColorType.RED => Color.red,
                ColorType.GREEN => Color.green,
                ColorType.BLUE => Color.blue,
                ColorType.PURPLE => Color.magenta,
                ColorType.YELLOW => Color.yellow,
                _ => Color.white
            };
        }
    }
}