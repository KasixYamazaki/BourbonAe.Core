using System.Collections.Generic;

namespace BourbonAe.Core.Models.Constants
{
    /// <summary>
    /// Provides export data type definitions corresponding to CONST_EXPORT_DATA_KBN
    /// from the original VB project【117833211277825†L21-L33】.  Each field
    /// contains a <see cref="CodeName"/> describing the code and the
    /// human-readable label.  An additional helper method returns the
    /// name based on a supplied code.
    /// </summary>
    public static class ExportDataType
    {
        public static readonly CodeName TokuyakuAll = new("0", "特約・登録店（全て）");
        public static readonly CodeName TokuyakuConfirmed = new("1", "特約・登録店（確定）");
        public static readonly CodeName TokuyakuUnconfirmed = new("2", "特約・登録店（未確定）");
        public static readonly CodeName MannequinExpense = new("3", "マネキン販促費");

        /// <summary>
        /// Returns the export data type name for the given code or <c>null</c>
        /// if the code is not recognised.
        /// </summary>
        /// <param name="code">Code string (e.g. "0", "1", "2" or "3").</param>
        public static string? GetNameByCode(string code)
        {
            return code switch
            {
                "0" => TokuyakuAll.Name,
                "1" => TokuyakuConfirmed.Name,
                "2" => TokuyakuUnconfirmed.Name,
                "3" => MannequinExpense.Name,
                _ => null
            };
        }

        /// <summary>
        /// Enumerates all defined export data types.
        /// </summary>
        public static IEnumerable<CodeName> All => new[]
        {
            TokuyakuAll,
            TokuyakuConfirmed,
            TokuyakuUnconfirmed,
            MannequinExpense
        };
    }
}