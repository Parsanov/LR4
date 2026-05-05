using System.Text.Json;
using LR4.Core.Memento;
using Microsoft.AspNetCore.Http;

namespace LR4.Application.Session
{
    /// <summary>DTO для серіалізації стану в Session.</summary>
    public class TableSettingsDto
    {
        public string FontFamily { get; set; } = "Arial";
        public int    FontSize   { get; set; } = 14;
        public string FontColor  { get; set; } = "#212529";
        public bool   IsBold     { get; set; } = false;
        public bool   IsItalic   { get; set; } = false;

        public static TableSettingsDto FromSettings(TableSettings s)
            => new() { FontFamily = s.FontFamily, FontSize = s.FontSize,
                       FontColor = s.FontColor, IsBold = s.IsBold, IsItalic = s.IsItalic };

        public TableSettings ToSettings() => new()
            { FontFamily = FontFamily, FontSize = FontSize,
              FontColor = FontColor, IsBold = IsBold, IsItalic = IsItalic };

        public TableSettingsMemento ToMemento()
            => new(FontFamily, FontSize, FontColor, IsBold, IsItalic);

        public static TableSettingsDto FromMemento(TableSettingsMemento m)
            => new() { FontFamily = m.FontFamily, FontSize = m.FontSize,
                       FontColor = m.FontColor, IsBold = m.IsBold, IsItalic = m.IsItalic };
    }

    /// <summary>
    /// Хелпер для збереження/читання TableSettings та SettingsHistory із Session.
    /// </summary>
    public static class TableSettingsSession
    {
        private const string KeySettings = "TableSettings";
        private const string KeyUndo     = "TableSettings_Undo";
        private const string KeyRedo     = "TableSettings_Redo";

        private static readonly JsonSerializerOptions _json = new()
            { PropertyNameCaseInsensitive = true };

        // ── Settings ─────────────────────────────────────────────────────────

        public static TableSettings LoadSettings(ISession session)
        {
            var json = session.GetString(KeySettings);
            if (string.IsNullOrEmpty(json)) return new TableSettings();
            var dto = JsonSerializer.Deserialize<TableSettingsDto>(json, _json);
            return dto?.ToSettings() ?? new TableSettings();
        }

        public static void SaveSettings(ISession session, TableSettings settings)
            => session.SetString(KeySettings,
                JsonSerializer.Serialize(TableSettingsDto.FromSettings(settings)));

        // ── History ──────────────────────────────────────────────────────────

        public static SettingsHistory LoadHistory(ISession session)
        {
            var history = new SettingsHistory();

            var undoJson = session.GetString(KeyUndo);
            var redoJson = session.GetString(KeyRedo);

            var undoList = string.IsNullOrEmpty(undoJson)
                ? new List<TableSettingsDto>()
                : JsonSerializer.Deserialize<List<TableSettingsDto>>(undoJson, _json) ?? new();

            var redoList = string.IsNullOrEmpty(redoJson)
                ? new List<TableSettingsDto>()
                : JsonSerializer.Deserialize<List<TableSettingsDto>>(redoJson, _json) ?? new();

            history.LoadFromLists(
                undoList.Select(d => d.ToMemento()),
                redoList.Select(d => d.ToMemento()));

            return history;
        }

        public static void SaveHistory(ISession session, SettingsHistory history)
        {
            var undoDtos = history.GetUndoList().Select(TableSettingsDto.FromMemento).ToList();
            var redoDtos = history.GetRedoList().Select(TableSettingsDto.FromMemento).ToList();

            session.SetString(KeyUndo, JsonSerializer.Serialize(undoDtos));
            session.SetString(KeyRedo, JsonSerializer.Serialize(redoDtos));
        }
    }
}
