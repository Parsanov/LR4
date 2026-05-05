namespace LR4.Core.Memento
{
    /// <summary>
    /// Memento — незмінний знімок стану TableSettings.
    /// Зберігає всі параметри відображення таблиці на певний момент часу.
    /// </summary>
    public class TableSettingsMemento
    {
        public string FontFamily { get; }
        public int    FontSize   { get; }
        public string FontColor  { get; }
        public bool   IsBold     { get; }
        public bool   IsItalic   { get; }

        public TableSettingsMemento(
            string fontFamily,
            int    fontSize,
            string fontColor,
            bool   isBold,
            bool   isItalic)
        {
            FontFamily = fontFamily;
            FontSize   = fontSize;
            FontColor  = fontColor;
            IsBold     = isBold;
            IsItalic   = isItalic;
        }
    }
}
