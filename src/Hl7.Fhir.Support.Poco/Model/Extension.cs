﻿/*
  Copyright (c) 2011+, HL7, Inc.
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  

*/


using Hl7.Fhir.Introspection;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SystemPrimitive = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Optional Extensions Element
    /// </summary>
    [Serializable]
    [System.Diagnostics.DebuggerDisplay(@"\{Value={Value} Url={_Url}}")]
    [FhirType("Extension", "http://hl7.org/fhir/StructureDefinition/Extension")]
    [DataContract]
    public class Extension : DataType
    {
        public Extension()
        {
        }

        public Extension(string url, DataType value)
        {
            this.Url = url;
            this.Value = value;
        }

        public override string TypeName { get { return "Extension"; } }

        /// <summary>
        /// identifies the meaning of the extension
        /// </summary>
        [FhirElement("url", XmlSerialization = XmlRepresentation.XmlAttr, InSummary = true, Order = 30)]
        [DeclaredType(Type = typeof(SystemPrimitive.String))]
        [Cardinality(Min = 1, Max = 1)]
        [UriPattern]
        [DataMember]
        public string Url
        {
            get { return _Url; }
            set { _Url = value; OnPropertyChanged("Url"); }
        }

        private string _Url;

        /// <summary>
        /// Value of extension
        /// </summary>
        [FhirElement("value", InSummary = true, Order = 40, Choice = ChoiceType.DatatypeChoice)]
        [DataMember]
        public Hl7.Fhir.Model.DataType Value
        {
            get { return _Value; }
            set { _Value = value; OnPropertyChanged("Value"); }
        }

        private Hl7.Fhir.Model.DataType _Value;

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Extension;

            if (dest != null)
            {
                base.CopyTo(dest);
                if (Url != null) dest.Url = Url;
                if (Value != null) dest.Value = (Hl7.Fhir.Model.DataType)Value.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Extension());
        }

        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Extension;
            if (otherT == null) return false;

            if (!base.Matches(otherT)) return false;
            if (Url != otherT.Url) return false;
            if (!DeepComparable.Matches(Value, otherT.Value)) return false;

            return true;
        }

        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Extension;
            if (otherT == null) return false;

            if (!base.IsExactly(otherT)) return false;
            if (Url != otherT.Url) return false;
            if (!DeepComparable.IsExactly(Value, otherT.Value)) return false;

            return true;
        }

        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (Url != null) yield return new FhirUri(Url);
                if (Value != null) yield return Value;
            }
        }

        public override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                // Extension elements 
                foreach (var item in base.NamedChildren) yield return item;
                if (Url != null) yield return new ElementValue("url", new FhirUri(Url));
                if (Value != null) yield return new ElementValue("value", Value);
            }
        }

        protected override bool TryGetValue(string key, out object value)
        {
            switch (key)
            {
                case "url":
                    value = Url;
                    return Url is not null;
                case "value":
                    value = Value;
                    return Value is not null;
                default:
                    return base.TryGetValue(key, out value);
            };
        }

        protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
        {
            foreach (var kvp in base.GetElementPairs()) yield return kvp;
            if (Url is not null) yield return new KeyValuePair<string, object>("url", Url);
            if (Value is not null) yield return new KeyValuePair<string, object>("value", Value);
        }
    }
}
