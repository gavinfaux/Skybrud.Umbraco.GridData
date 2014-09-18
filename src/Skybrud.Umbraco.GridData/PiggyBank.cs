﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Values;

namespace Skybrud.Umbraco.GridData {
    
    public class PiggyBank {
        
        readonly Dictionary<string, Func<JToken, IGridControlValue>> _oink = new Dictionary<string, Func<JToken, IGridControlValue>>();
 
        private static readonly PiggyBank NomNom = new PiggyBank();

        public static PiggyBank OneLittlePiggy {
            get { return NomNom; }
        }

        public Func<JToken, IGridControlValue> this[string comeHereLittlePiggy] {
            get { return _oink[comeHereLittlePiggy]; }
            set { _oink[comeHereLittlePiggy] = value; }
        }

        private PiggyBank() {

            _oink["media"] = ConvertMediaValue;
            _oink["embed"] = ConvertEmbedValue;
            _oink["rte"] = ConvertRichTextValue;
            _oink["macro"] = ConvertMacroValue;
            
        }

        public bool TryGetValue(string alias, out Func<JToken, IGridControlValue> func) {
            return _oink.TryGetValue(alias, out func);
        }

        public static IGridControlValue ConvertMediaValue(JToken token) {

            // At this point the token should be a JObject, but we cast it safely to be sure
            JObject obj = token as JObject;

            // Return the converted media value (or NULL if the object is already NULL)
            return obj == null ? null : obj.ToObject<GridControlMediaValue>();

        }

        public static IGridControlValue ConvertEmbedValue(JToken token) {
            return new GridControlEmbedValue {
                Value = token.Value<string>()
            };
        }

        public static IGridControlValue ConvertRichTextValue(JToken token) {
            return new GridControlRichTextValue {
                Value = token.Value<string>()
            };
        }

        public static IGridControlValue ConvertMacroValue(JToken token) {
            return new GridControlMacroValue();
        }
    
    }

}
