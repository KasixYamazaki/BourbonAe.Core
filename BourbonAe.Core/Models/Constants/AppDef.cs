namespace BourbonAe.Core.Models.Constants
{
    /// <summary>
    /// Defines system-wide constants corresponding to CONST_APP_DEF in the
    /// original VB code【117833211277825†L38-L45】.  These values represent
    /// default company identifiers and system settings.
    /// </summary>
    public static class AppDef
    {
        /// <summary>
        /// Company code (会社CD).
        /// </summary>
        public const string CompanyCode = "010";

        /// <summary>
        /// Company name (会社NM).
        /// </summary>
        public const string CompanyName = "ブルボン";

        /// <summary>
        /// Number of months that can be carried over when calculating budgets
        /// (繰越可能月数).  A negative value indicates months prior to the
        /// target month.  In VB this was defined as -2【117833211277825†L42-L43】.
        /// </summary>
        public const int CarryoverMonths = -2;

        /// <summary>
        /// Display text for unspecified selections (指定なし).
        /// </summary>
        public const string NotSpecified = "**** 指定無 ****";
    }
}