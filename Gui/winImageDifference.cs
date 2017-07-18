using System;
using System.Drawing;
using System.Windows.Forms;
using PaintDotNet.Effects;

namespace ImageDifference.Gui
{
    /// <summary>
    /// The dialog used for working with the effect.
    /// </summary>
    public partial class winImageDifference : EffectConfigDialog
    {
        #region Fields
        /// <summary>
        /// The bitmap to be replaced.
        /// </summary>
        public Bitmap storedImg;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes components.
        /// </summary>
        public winImageDifference()
        {
            InitializeComponent();
            storedImg = new Bitmap(1, 1);
        }
        #endregion

        #region Methods (overridden)
        /// <summary>
        /// Configures settings so they can be stored between consecutive
        /// calls of the effect.
        /// </summary>
        protected override void InitialInitToken()
        {
            theEffectToken = new PersistentSettings(new Bitmap(1, 1), false, false);
        }

        /// <summary>
        /// Sets up the GUI to reflect the previously-used settings; i.e. this
        /// loads the settings. Called twice by a quirk of Paint.NET.
        /// </summary>
        protected override void InitDialogFromToken(EffectConfigToken effectToken)
        {
            //Copies GUI values from the settings.
            PersistentSettings token = (PersistentSettings)effectToken;
            storedImg = token.StoredImg;
            chkbxInvertArea.Checked = token.DoInvertArea;
            chkbxInvertPicture.Checked = token.DoInvertPicture;
        }

        /// <summary>
        /// Overwrites the settings with the dialog's current settings so they
        /// can be reused later; i.e. this saves the settings.
        /// </summary>
        protected override void InitTokenFromDialog()
        {
            ((PersistentSettings)EffectToken).StoredImg = storedImg;
        }
        #endregion

        #region Methods (not event handlers)
        /// <summary>
        /// Sets/resets all persistent settings in the dialog to their default
        /// values.
        /// </summary>
        public void InitSettings()
        {
            InitialInitToken();
            InitDialogFromToken();
        }
        #endregion

        #region Methods (event handlers)
        /// <summary>
        /// Stores the image selected by the user.
        /// </summary>
        private void bttnStoreImage_Click(object sender, EventArgs e)
        {
            StaticSettings.dialogResult = StaticSettings.DialogResult.StoringImage;
            DialogResult = DialogResult.OK;
            FinishTokenUpdate();
            Close();
        }

        /// <summary>
        /// Finds the difference between the current and stored image.
        /// </summary>
        private void bttnFindDifference_Click(object sender, EventArgs e)
        {
            StaticSettings.dialogResult = StaticSettings.DialogResult.Replacing;
            DialogResult = DialogResult.OK;
            FinishTokenUpdate();
            Close();
        }

        /// <summary>
        /// Toggles whether inverted area should be used.
        /// </summary>
        private void chkbxInvertArea_CheckedChanged(object sender, EventArgs e)
        {
            ((PersistentSettings)theEffectToken).DoInvertArea = chkbxInvertArea.Checked;
        }

        /// <summary>
        /// Toggles whether the resulting picture should highlight differences
        /// from the 1st picture instead of from the 2nd.
        /// </summary>
        private void chkbxInvertResultingPicture_CheckedChanged(object sender, EventArgs e)
        {
            ((PersistentSettings)theEffectToken).DoInvertPicture = chkbxInvertPicture.Checked;
        }
        #endregion
    }
}