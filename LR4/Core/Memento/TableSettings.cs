namespace LR4.Core.Memento
{
    /// <summary>
    /// Originator — зберігає поточний стан налаштувань таблиці.
    /// Патерн Memento: може створювати знімки свого стану та відновлюватись із них.
    /// </summary>
    public class TableSettings
    {
        public string FontFamily { get; set; } = "Arial";
        public int FontSize { get; set; } = 14;
        public string FontColor { get; set; } = "#212529";
        public bool IsBold { get; set; } = false;
        public bool IsItalic { get; set; } = false;

        /// <summary>Створити знімок поточного стану (Memento).</summary>
        public TableSettingsMemento Save()
            => new TableSettingsMemento(FontFamily, FontSize, FontColor, IsBold, IsItalic);

        /// <summary>Відновити стан зі знімка (Memento).</summary>
        public void Restore(TableSettingsMemento memento)
        {
            FontFamily = memento.FontFamily;
            FontSize   = memento.FontSize;
            FontColor  = memento.FontColor;
            IsBold     = memento.IsBold;
            IsItalic   = memento.IsItalic;
        }

        /// <summary>Повернути рядок CSS-стилю для застосування до таблиці.</summary>
        public string ToCssStyle()
        {
            var weight = IsBold   ? "bold"   : "normal";
            var style  = IsItalic ? "italic" : "normal";
            return $"font-family:{FontFamily}; font-size:{FontSize}px; color:{FontColor}; font-weight:{weight}; font-style:{style};";
        }
    }
}
