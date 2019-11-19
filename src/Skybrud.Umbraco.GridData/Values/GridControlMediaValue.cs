﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Config;
using Skybrud.Umbraco.GridData.Converters;

namespace Skybrud.Umbraco.GridData.Values {

    /// <summary>
    /// Class representing the media value of a control.
    /// </summary>
    [GridConverter("media", typeof(GridEditorMediaConfig))]
    public class GridControlMediaValue : GridControlObjectBase {

        #region Properties

        /// <summary>
        /// Gets the focal point with information on how the iamge should be cropped.
        /// </summary>
        [JsonProperty("focalPoint")]
        public GridControlMediaFocalPoint FocalPoint { get; protected set; }

        /// <summary>
        /// Gets the ID of the image.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; protected set; }

        /// <summary>
        /// Gets the URL of the media.
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; protected set; }

        /// <summary>
        /// Gets the alt text of the media.
        /// </summary>
        [JsonProperty("altText", NullValueHandling = NullValueHandling.Ignore)]
        public string AlternativeText { get; protected set; }

        /// <summary>
        /// Gets whether the <see cref="AlternativeText"/> property has a value.
        /// </summary>
        [JsonIgnore]
        public bool HasAlternativeText => !String.IsNullOrWhiteSpace(AlternativeText);

        /// <summary>
        /// Gets the caption of the media.
        /// </summary>
        [JsonProperty("caption", NullValueHandling = NullValueHandling.Ignore)]
        public string Caption { get; protected set; }

        /// <summary>
        /// Gets whether the <see cref="Caption"/> property has a value.
        /// </summary>
        public bool HasCaption => !String.IsNullOrWhiteSpace(Caption);

        /// <summary>
        /// Gets whether the value is valid. For an instance of <see cref="GridControlMediaValue"/>, this means
        /// checking whether an image has been selected. The property will however not validate the image against the
        /// media cache.
        /// </summary>
        [JsonIgnore]
        public override bool IsValid => Id > 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="control"/> and <paramref name="token"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        /// <param name="token">An instance of <see cref="JToken"/> representing the value of the control.</param>
        public GridControlMediaValue(GridControl control, JToken token) : base(control, token) {
            FocalPoint = JObject.GetObject("focalPoint", GridControlMediaFocalPoint.Parse);
            Id = JObject.GetInt32("id");
            Image = JObject.GetString("image");
            AlternativeText = JObject.GetString("altText");
            Caption = JObject.GetString("caption");
        }

        #endregion

        

    }

}