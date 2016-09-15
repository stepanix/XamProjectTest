
using Android.Graphics;


namespace XamProjectTest.utils
{
    public static class FontUtilityHelper
    {
        private static Typeface font;

        public static Typeface getFont()
        {
            font = Typeface.CreateFromAsset(Android.App.Application.Context.Assets, "fonts/fontawesome-webfont.ttf");
            return font;
        }

    }
}