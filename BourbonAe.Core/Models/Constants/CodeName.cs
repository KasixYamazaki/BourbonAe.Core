namespace BourbonAe.Core.Models.Constants
{
    /// <summary>
    /// Represents a pair of code and name values.  This class mirrors the
    /// behaviour of the VB ClsCodeName type【117833211277825†L0-L8】.  It is
    /// implemented as a record to provide read-only properties and value
    /// equality semantics in C#.
    /// </summary>
    public readonly record struct CodeName(string Code, string Name);
}