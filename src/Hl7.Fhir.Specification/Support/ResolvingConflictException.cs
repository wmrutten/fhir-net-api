using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Support
{
    // cf. System.Enumerable.SingleOrDefault, throws Error.MoreThanOneElement = InvalidOperationException

    /// <summary>The exception that is throw to report a resolving conflict caused by multiple conflicting artifacts with the same identifier.</summary>
    /// <remarks>Generic replacement for the obsolete <seealso cref="CanonicalUrlConflictException"/>.</remarks>
    public class ResolvingConflictException : InvalidOperationException
    {
        readonly static string ResourceUriConflictErrorMessage = "Found multiple conflicting resources with the same resource uri identifier.";

        /// <summary>Generate a new <see cref="ResolvingConflictException"/> to report multiple conflicting resources with the same Resource Uri identifier.</summary>
        public static ResolvingConflictException ResourceUriConflict(IEnumerable<ResolvingConflict> conflicts)
            => new ResolvingConflictException(ResourceUriConflictErrorMessage, conflicts);

        readonly static string CanonicalUrlConflictErrorMessage = "Found multiple conflicting conformance resources with the same canonical url identifier.";

        /// <summary>Generate a new <see cref="ResolvingConflictException"/> to report multiple conflicting naming system resources with the same unique identifier.</summary>
        public static ResolvingConflictException CanonicalUrlConflict(IEnumerable<ResolvingConflict> conflicts)
            => new ResolvingConflictException(CanonicalUrlConflictErrorMessage, conflicts);

        readonly static string NamingSystemUniqueIdConflictErrorMessage = "Found multiple conflicting NamingSystem resources with the same unique identifier.";

        /// <summary>Generate a new <see cref="ResolvingConflictException"/> to report multiple conflicting NamingSystem resources with the same unique identifier.</summary>
        public static ResolvingConflictException NamingSystemUniqueIdConflict(IEnumerable<ResolvingConflict> conflicts)
            => new ResolvingConflictException(NamingSystemUniqueIdConflictErrorMessage, conflicts);

        readonly static string CodeSystemConflictErrorMessage = "Found multiple conflicting CodeSystem resources associated with the same ValueSet url.";

        /// <summary>Generate a new <see cref="ResolvingConflictException"/> to report multiple conflicting CodeSystem resources with the same ValueSet url.</summary>
        public static ResolvingConflictException CodeSystemConflict(IEnumerable<ResolvingConflict> conflicts)
            => new ResolvingConflictException(CodeSystemConflictErrorMessage, conflicts);

        readonly static string ConceptMapUrlConflictErrorMessage = "Found multiple conflicting ConceptMap resources with the same source or target url.";

        /// <summary>Generate a new <see cref="ResolvingConflictException"/> to report multiple conflicting ConceptMap resources with the same source or target url.</summary>
        public static ResolvingConflictException ConceptMapUrlConflict(IEnumerable<ResolvingConflict> conflicts)
            => new ResolvingConflictException(ConceptMapUrlConflictErrorMessage, conflicts);

        /// <summary>Provides information about a resolving conflict for a group of resources identified by the same url.</summary>
        public class ResolvingConflict
        {
            public ResolvingConflict(string identifier, IEnumerable<string> origins)
            {
                Identifier = identifier;
                Origins = origins.ToArray();
            }

            /// <summary>The identifier value associated with multiple conflicting resources.</summary>
            public string Identifier { get; }

            /// <summary>File paths of conflicting resources identified by the same value.</summary>
            public string[] Origins { get; }
        }

        /// <summary>
        /// Create a new <see cref="ResolvingConflictException"/> instance.
        /// Use static factory methods to create exceptions for specific types of conflicts.
        /// </summary>
        public ResolvingConflictException(string errorMessage, IEnumerable<ResolvingConflict> conflicts)
             : base(formatMessage(errorMessage, conflicts))
        {
            Conflicts = conflicts.ToArray();
        }

        /// <summary>Returns a list of resolving conflicts.</summary>
        public ResolvingConflict[] Conflicts { get; private set; }

        static string formatMessage(string errorMessage, IEnumerable<ResolvingConflict> conflicts)
        {
            if (conflicts != null && conflicts.Any())
            {
                StringBuilder sb = new StringBuilder(errorMessage);
                sb.AppendLine();
                sb.AppendLine();
                foreach (var conflict in conflicts)
                {
                    sb.Append("Url: ");
                    sb.AppendLine(conflict.Identifier);
                    foreach (var file in conflict.Origins)
                    {
                        sb.Append("   File: ");
                        sb.AppendLine(file);
                    }
                }
                return sb.ToString();
            }
            return null;
        }
    }

}
