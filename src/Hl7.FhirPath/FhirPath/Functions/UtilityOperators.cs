﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.ElementModel;
using System.Diagnostics;
using Hl7.Fhir.Utility;

namespace Hl7.FhirPath.Functions
{
    internal static class UtilityOperators
    {
#if WAY_TOO_MUCH_OUTPUT
        static Action<string> WriteLine = (string s) => Debug.WriteLine(s);
#else
        static Action<string> WriteLine = (string s) => { };
#endif

        public static IEnumerable<IElementNavigator> Extension(this IEnumerable<IElementNavigator> focus, string url)
        {
            return focus.Navigate("extension").Where(es => es.Navigate("url").Single().IsEqualTo(new ConstantValue(url)));
        }

        public static IEnumerable<IElementNavigator> Trace(this IEnumerable<IElementNavigator> focus, string name, EvaluationContext ctx)
        {
            if (ctx.Tracer != null)
                ctx.Tracer(name, focus);
            return focus;
        }
    }
}
