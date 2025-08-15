using System.Collections.Generic;

namespace BourbonAe.Core.Models.Constants
{
    /// <summary>
    /// Provides export type definitions corresponding to CONST_EXPORT_TYPE
    /// from the original VB project【117833211277825†L11-L18】.  Each field
    /// contains a <see cref="CodeName"/> describing the code and the
    /// human-readable label.  The helper method <see cref="GetNameByCode"/>
    /// can be used to retrieve a name based on its code.
    /// </summary>
    public static class ExportType
    {
        public static readonly CodeName Pdf = new("1", "帳票（PDF）");
        public static readonly CodeName Excel = new("2", "データ（EXCEL）");

        /// <summary>
        /// Returns the export type name for the given code or <c>null</c> if
        /// the code is not recognised.
        /// </summary>
        /// <param name="code">Code string (e.g. "1" or "2").</param>
        public static string? GetNameByCode(string code)
        {
            return code switch
            {
                "1" => Pdf.Name,
                "2" => Excel.Name,
                _ => null
            };
        }

        /// <summary>
        /// Enumerates all defined export types.  This can be useful when
        /// binding values to a dropdown list or validating input.
        /// </summary>
        public static IEnumerable<CodeName> All => new[] { Pdf, Excel };
    }
}