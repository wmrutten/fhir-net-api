﻿/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Source.Summary
{
    /// <summary>Generic read-only interface to access previously harvested artifact summary properties by key.</summary>
    /// <remarks>
    /// Implemented by both <see cref="ArtifactSummaryPropertyBag"/> and <see cref="ArtifactSummary"/>,
    /// so summary harvester implementations can define common extension methods to access specific
    /// property values that operate on both types.
    /// </remarks>
    public interface IArtifactSummaryPropertyBag : IReadOnlyDictionary<string, object>
    {
    }

    /// <summary>A property bag for storing and retrieving harvested artifact summary information by key.</summary>
    /// <remarks>
    /// The <see cref="ArtifactSummaryGenerator"/> creates a new <see cref="ArtifactSummaryPropertyBag"/>
    /// instance for each artifact and calls the <see cref="ArtifactSummaryHarvester"/> delegates to
    /// harvest summary information into the property bag. Finally, the generator creates a new
    /// <see cref="ArtifactSummary"/> instance from the initialized property bag.
    /// </remarks>
    public class ArtifactSummaryPropertyBag : Dictionary<string, object>, IArtifactSummaryPropertyBag
    {
        public const int DefaultCapacity = 8;

        public ArtifactSummaryPropertyBag() : base(DefaultCapacity) { }
    }
}