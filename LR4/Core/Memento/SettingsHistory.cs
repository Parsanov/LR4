namespace LR4.Core.Memento
{
    /// <summary>
    /// Caretaker — керує списком знімків (Memento) для підтримки Undo/Redo.
    /// Не має доступу до внутрішнього стану TableSettings — тільки зберігає знімки.
    /// </summary>
    public class SettingsHistory
    {
        private readonly Stack<TableSettingsMemento> _undoStack = new();
        private readonly Stack<TableSettingsMemento> _redoStack = new();

        public bool CanUndo => _undoStack.Count > 0;
        public bool CanRedo => _redoStack.Count > 0;

        /// <summary>
        /// Зберегти поточний знімок перед застосуванням нових налаштувань.
        /// Очищує Redo-стек, бо нова гілка змін перезаписує майбутнє.
        /// </summary>
        public void Push(TableSettingsMemento memento)
        {
            _undoStack.Push(memento);
            _redoStack.Clear();
        }

        /// <summary>Відмінити: повернути попередній знімок, зберегти поточний у Redo.</summary>
        public TableSettingsMemento Undo(TableSettingsMemento current)
        {
            if (!CanUndo) throw new InvalidOperationException("Немає станів для скасування.");
            _redoStack.Push(current);
            return _undoStack.Pop();
        }

        /// <summary>Повторити: відновити знімок із Redo, зберегти поточний у Undo.</summary>
        public TableSettingsMemento Redo(TableSettingsMemento current)
        {
            if (!CanRedo) throw new InvalidOperationException("Немає станів для повторення.");
            _undoStack.Push(current);
            return _redoStack.Pop();
        }

        /// <summary>Скинути всю історію.</summary>
        public void Reset()
        {
            _undoStack.Clear();
            _redoStack.Clear();
        }

        // ── Серіалізація для Session ──────────────────────────────────────────

        public List<TableSettingsMemento> GetUndoList() => _undoStack.ToList();
        public List<TableSettingsMemento> GetRedoList() => _redoStack.ToList();

        public void LoadFromLists(
            IEnumerable<TableSettingsMemento> undoItems,
            IEnumerable<TableSettingsMemento> redoItems)
        {
            _undoStack.Clear();
            _redoStack.Clear();

            // Stack додає останній зверху — завантажуємо в зворотньому порядку
            foreach (var m in undoItems.Reverse()) _undoStack.Push(m);
            foreach (var m in redoItems.Reverse()) _redoStack.Push(m);
        }
    }
}
