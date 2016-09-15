
using Android.Graphics;


namespace XamProjectTest.utils
{
    public class FontUtilityHelper
    {
        private static Typeface font;
        static FontUtilityHelper()
        {
            // Initialize font.
            font = Typeface.CreateFromAsset(Android.App.Application.Context.Assets, "fontawesome-webfont.ttf");
        }

        private FontUtilityHelper() { }

        public static Typeface getFont()
        {
            return font;
        }

    }
}