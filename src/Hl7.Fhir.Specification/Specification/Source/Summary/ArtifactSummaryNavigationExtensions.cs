﻿using Hl7.Fhir.ElementModel;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Specification.Source.Summary
{
    // Q: Move to separate namespace in order to avoid pollution?

    /// <summary>Extension methods on <see cref="IElementNavigator"/> to allow easy extraction of summary details from an artifact.</summary>
    public static class ArtifactSummaryNavigationExtensions
    {
        /// <summary>
        /// Try to position the navigator on the current or sibling element with the specified name.
        /// If the current element name matches, then maintain the current position.
        /// Otherwise, navigate to the next matching sibling element (if it exists).
        /// </summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="element">An element name.</param>
        /// <returns><c>true</c> if, upon return, the navigator is positioned on a matching element, or <c>false</c> otherwise.</returns>
        public static bool Find(this IElementNavigator nav, string element)
        {
            return nav.Name == element || nav.MoveToNext(element);
        }

        /// <summary>Extract the value of the current element into an <see cref="ArtifactSummaryDetailsCollection"/> using the specified key.</summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="details">An <see cref="ArtifactSummaryDetailsCollection"/> instance.</param>
        /// <param name="key">A collection key.</param>
        public static bool TryExtractValue(this IElementNavigator nav, ArtifactSummaryDetailsCollection details, string key)
        {
            var value = nav.Value;
            if (value != null)
            {
                var s = PrimitiveTypeConverter.ConvertTo<string>(value);
                details[key] = s;
                return true;
            }
            return false;
        }

        /// <summary>Extract the value of the (current or sibling) element with the specified name into an <see cref="ArtifactSummaryDetailsCollection"/> using the specified key.</summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="details">An <see cref="ArtifactSummaryDetailsCollection"/> instance.</param>
        /// <param name="key">A collection key.</param>
        /// <param name="element">An element name.</param>
        public static bool TryExtractValue(this IElementNavigator nav, ArtifactSummaryDetailsCollection details, string key, string element)
        {
            return nav.Find(element) && nav.TryExtractValue(details, key);
        }

        // TODO: Use nav.Children("name")

        /// <summary>Extract the value of a child element into an <see cref="ArtifactSummaryDetailsCollection"/> using the specified key.</summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="details">An <see cref="ArtifactSummaryDetailsCollection"/> instance.</param>
        /// <param name="key">A collection key.</param>
        /// <param name="element">An element name.</param>
        /// <param name="childElement">A child element name.</param>
        public static bool TryExtractValue(this IElementNavigator nav, ArtifactSummaryDetailsCollection details, string key, string element, string childElement)
        {
            if (nav.Find(element))
            {
                var childNav = nav.Clone();
                return childNav.MoveToFirstChild(childElement) && childNav.TryExtractValue(details, key);
            }
            return false;
        }

        /// <summary>Add the value of the current element to a list, if not missing or empty.</summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="values">A list of values.</param>
        public static bool TryExtractValue(this IElementNavigator nav, List<string> values)
        {
            var value = nav.Value;
            if (value != null)
            {
                var s = PrimitiveTypeConverter.ConvertTo<string>(value);
                values.Add(s);
                return true;
            }
            return false;
        }

        /// <summary>Extract an array of child element values into an <see cref="ArtifactSummaryDetailsCollection"/> using the specified key.</summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="details">An <see cref="ArtifactSummaryDetailsCollection"/> instance.</param>
        /// <param name="key">A collection key.</param>
        /// <param name="element">An element name.</param>
        /// <param name="childElement">A child element name.</param>
        public static bool TryExtractValues(this IElementNavigator nav, ArtifactSummaryDetailsCollection details, string key, string element, string childElement)
        {
            if (nav.Find(element))
            {
                var values = new List<string>();
                do
                {
                    var childNav = nav.Clone();
                    if (childNav.MoveToFirstChild(childElement))
                    {
                        TryExtractValue(childNav, values);
                    }
                } while (nav.MoveToNext(element));
                if (values.Count > 0)
                {
                    details[key] = values.ToArray();
                    return true;
                }
            }
            return false;
        }

    }

}
