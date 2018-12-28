using System;
using System.Management.Automation;
using SnipeSharp.Models;
using SnipeSharp.Filters;

namespace SnipeSharp.PowerShell.Cmdlets.Find
{
    /// <summary>
    /// <para type="synopsis">Finds a Snipe IT supplier.</para>
    /// <para type="description">The Find-Supplier cmdlet finds supplier objects by filter.</para>
    /// </summary>
    /// <example>
    ///   <code>Find-Supplier</code>
    ///   <para>Finds all suppliers.</para>
    /// </example>
    /// <example>
    ///   <code>Find-Supplier "Potato Supplier Inc"</code>
    ///   <para>Finds suppliers that match the search string "Potato Supplier Inc".</para>
    /// </example>
    [Cmdlet(VerbsCommon.Find, nameof(Supplier), SupportsPaging = true)]
    [OutputType(typeof(Supplier))]
    public class FindSupplier: FindObject<Supplier, string, SearchFilter>
    {
        /// <inheritdoc />
        protected override void PopulateFilter(SearchFilter filter)
        {
            // nop
        }
    }
}
