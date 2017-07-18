using System.Collections.Generic;
using System.Drawing;

namespace ImageDifference
{
    /// <summary>
    /// Represents the settings used in the dialog so they can be stored and
    /// loaded when applying the effect consecutively for convenience.
    /// </summary>
    public class PersistentSettings : PaintDotNet.Effects.EffectConfigToken
    {
        #region Fields
        /// <summary>
        /// Contains the stored bitmap data.
        /// </summary>
        public Bitmap StoredImg
        {
            get;
            set;
        }

        /// <summary>
        /// Whether the area should be inverted.
        /// </summary>
        public bool DoInvertArea;

        /// <summary>
        /// If true, the 2nd image is treated as the original and the changes
        /// from the 1st are preserved instead of the other way around.
        /// </summary>
        public bool DoInvertPicture;
        #endregion

        #region Constructors
        /// <summary>
        /// Calls and sets up dialog settings to be stored.
        /// </summary>
        public PersistentSettings(
            Bitmap bmpStored, bool doInvertArea, bool doInvertPicture)
            : base()
        {
            StoredImg = bmpStored;
            DoInvertArea = doInvertArea;
            DoInvertPicture = doInvertPicture;
        }

        /// <summary>
        /// Copies all settings to another token.
        /// </summary>
        protected PersistentSettings(PersistentSettings other)
            : base(other)
        {
            StoredImg = other.StoredImg;
            DoInvertPicture = other.DoInvertPicture;
            DoInvertArea = other.DoInvertArea;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Copies all settings to another token.
        /// </summary>
        public override object Clone()
        {
            return new PersistentSettings(this);
        }
        #endregion
    }
}