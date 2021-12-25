using Microsoft.Diagnostics.DebugServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Diagnostics.ExtensionCommands
{
    [Command(Name = "typesearch", Aliases = new string[] { "mx" }, Help = "Searches for a type.")]
    public class TypeSearchCommand : ExtensionCommandBase
    {

        [Argument(Help = "Name of the type to search.")]
        public string Query { get; set; }

        public override void Invoke()
        {
            if (string.IsNullOrEmpty(Query))
            {
                WriteLine("Missing type name to search for..." + Environment.NewLine);
                return;
            }

            foreach (var t in Helper.FindTypes(Query))
            {
                WriteLine($"{t.module}!{t.name} (MT: 0x{t.mt:X})");
            }
        }

        protected override string GetDetailedHelp()
        {
            return DetailedHelpText;
        }

        readonly string DetailedHelpText =
            "-------------------------------------------------------------------------------" + Environment.NewLine +
            "typesearch [query]" + Environment.NewLine + Environment.NewLine +
            "Typesearch looks for types in all the loaded modules matching a given query. " +
            "If the query starts with '^', it matches types with names starting with a query string, skipping the '^' character. " +
            "If the query ends with '$', it matches types with names ending with a query string without the '$' character. " +
            "Otherwise, it retuns types that contain query string in their names." + Environment.NewLine
            ;
    }
}
