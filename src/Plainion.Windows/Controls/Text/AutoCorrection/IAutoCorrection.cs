﻿using System.Windows.Documents;

namespace Plainion.Windows.Controls.Text.AutoCorrection
{
    public interface IAutoCorrection
    {
        AutoCorrectionResult TryApply(AutoCorrectionInput input);
        AutoCorrectionResult TryUndo(TextPointer pos);
    }

    public enum AutoCorrectionTrigger
    {
        Return,
        Space,
        CopyAndPaste
    }

    public class AutoCorrectionInput
    {
        public AutoCorrectionInput(TextRange range, AutoCorrectionTrigger trigger)
        {
            Contract.RequiresNotNull(range, "range");

            Range = range;
            Trigger = trigger;
        }

        public TextRange Range { get; private set; }

        public AutoCorrectionTrigger Trigger { get; private set; }
    }

    public class AutoCorrectionResult
    {
        public AutoCorrectionResult(bool success)
        : this(success, null)
        {
        }

        public AutoCorrectionResult(bool success, TextPointer caretPosition)
        {
            Success = success;
            CaretPosition = caretPosition;
        }

        public bool Success { get; private set; }

        public TextPointer CaretPosition { get; private set; }

        internal AutoCorrectionResult Merge(AutoCorrectionResult other)
        {
            Contract.RequiresNotNull(other, "other");

            return new AutoCorrectionResult(
                Success || other.Success,
                CaretPosition != null && CaretPosition.CompareTo(other.CaretPosition) > 0 ? CaretPosition : other.CaretPosition
                );
        }
    }
}
