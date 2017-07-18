using PaintDotNet;
using PaintDotNet.Effects;
using ImageDifference.Gui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ImageDifference
{
    /// <summary>
    /// Contains assembly information, accessible through variables.
    /// </summary>
    public class PluginSupportInfo : IPluginSupportInfo
    {
        #region Properties
        /// <summary>
        /// Gets the author.
        /// </summary>
        public string Author
        {
            get
            {
                return ((AssemblyCompanyAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)[0]).Company;
            }
        }

        /// <summary>
        /// Gets the copyright information.
        /// </summary>
        public string Copyright
        {
            get
            {
                return ((AssemblyCopyrightAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
            }
        }

        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return ((AssemblyProductAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0]).Product;
            }
        }

        /// <summary>
        /// Gets the version number.
        /// </summary>
        public Version Version
        {
            get
            {
                return base.GetType().Assembly.GetName().Version;
            }
        }

        /// <summary>
        /// Gets the URL where the plugin is released to the public.
        /// </summary>
        public Uri WebsiteUri
        {
            get
            {
                return new Uri("http://forums.getpaint.net/index.php?/forum/7-plugins-publishing-only/");
            }
        }
        #endregion
    }

    /// <summary>
    /// Controls the effect. In short, a GUI is instantiated by
    /// CreateConfigDialog and when the dialog signals OK, Render is called,
    /// passing OnSetRenderInfo to it. The dialog stores its result in an
    /// intermediate class called RenderSettings, which is then accessed to
    /// draw the final result in Render.
    /// </summary>
    [PluginSupportInfo(typeof(PluginSupportInfo), DisplayName = "Image Difference")]
    public class EffectPlugin : Effect
    {
        #region Fields
        /// <summary>
        /// The dialog to be constructed.
        /// </summary>
        private winImageDifference dlg;

        /// <summary>
        /// Paint.net wants to call Render() a million times. This prevents it.
        /// </summary>
        private static bool renderReady = false;
        #endregion

        #region Properties
        /// <summary>
        /// The icon of the plugin to be displayed next to its menu entry.
        /// </summary>
        public static Bitmap StaticImage
        {
            get
            {
                return new Bitmap(Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("ImageDifference.EffectPluginIcon.png"));
            }
        }

        /// <summary>
        /// The name of the plugin as it appears in Paint.NET.
        /// </summary>
        public static string StaticName
        {
            get
            {
                return Globalization.GlobalStrings.Title;
            }
        }

        /// <summary>
        /// The name of the menu category the plugin appears under.
        /// </summary>
        public static string StaticSubMenuName
        {
            get
            {
                return "Tools";
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public EffectPlugin()
            : base(EffectPlugin.StaticName,
            EffectPlugin.StaticImage,
            EffectPlugin.StaticSubMenuName,
            EffectFlags.Configurable)
        {
            dlg = null;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tells Paint.NET which form to instantiate as the plugin's GUI.
        /// Called remotely by Paint.NET.
        /// </summary>
        public override EffectConfigDialog CreateConfigDialog()
        {
            dlg = new winImageDifference();
            StaticSettings.dialogResult = StaticSettings.DialogResult.Default;
            renderReady = false;
            return dlg;
        }

        /// <summary>
        /// Gets the render information.
        /// </summary>
        /// <param name="parameters">
        /// Saved settings used to restore the GUI to the same settings it was
        /// saved with last time the effect was applied.
        /// </param>
        /// <param name="dstArgs">The destination canvas.</param>
        /// <param name="srcArgs">The source canvas.</param>
        protected override void OnSetRenderInfo(
            EffectConfigToken parameters,
            RenderArgs dstArgs,
            RenderArgs srcArgs)
        {
            base.OnSetRenderInfo(parameters, dstArgs, srcArgs);
        }

        /// <summary>
        /// Renders the effect over rectangular regions automatically
        /// determined and handled by Paint.NET for multithreading support.
        /// </summary>
        /// <param name="parameters">
        /// Saved settings used to restore the GUI to the same settings it was
        /// saved with last time the effect was applied.
        /// </param>
        /// <param name="dstArgs">The destination canvas.</param>
        /// <param name="srcArgs">The source canvas.</param>
        /// <param name="rois">
        /// A list of rectangular regions to split this effect into so it can
        /// be optimized by worker threads. Determined and managed by
        /// Paint.NET.
        /// </param>
        /// <param name="startIndex">
        /// The rectangle to begin rendering with. Used in Paint.NET's effect
        /// multithreading process.
        /// </param>
        /// <param name="length">
        /// The number of rectangles to render at once. Used in Paint.NET's
        /// effect multithreading process.
        /// </param>
        public override void Render(
            EffectConfigToken parameters,
            RenderArgs dstArgs,
            RenderArgs srcArgs,
            Rectangle[] rois,
            int startIndex,
            int length)
        {
            //Renders the effect if the dialog is closed and accepted.
            if (!IsCancelRequested && !renderReady)
            {
                var src = srcArgs.Surface;
                var dst = dstArgs.Surface;
                PersistentSettings token = (PersistentSettings)dlg.EffectToken;
                Rectangle selection = EnvironmentParameters.GetSelection(srcArgs.Bounds).GetBoundsInt();

                //Something happened, so this must be the final render.
                if (StaticSettings.dialogResult != StaticSettings.DialogResult.Default)
                {
                    renderReady = true;
                }

                if (StaticSettings.dialogResult ==
                    StaticSettings.DialogResult.StoringImage)
                {
                    if (selection.Width < 1 || selection.Height < 1)
                    {
                        MessageBox.Show("The size of the current selection " +
                            "must match the size of the stored image to work.");
                        return;
                    }

                    //Copies the data into the token.
                    token.StoredImg = new Bitmap(selection.Width, selection.Height);
                    for (int i = 0; i < selection.Width; i++)
                    {
                        for (int j = 0; j < selection.Height; j++)
                        {
                            token.StoredImg.SetPixel(i, j, src[selection.X + i, selection.Y + j]);
                        }
                    }
                }
                else if (StaticSettings.dialogResult ==
                    StaticSettings.DialogResult.Replacing)
                {
                    if (token.StoredImg.Width != selection.Width ||
                        token.StoredImg.Height != selection.Height)
                    {
                        MessageBox.Show("The unedited image needs to be " +
                            "stored, and the edited version selected " +
                            "(with the same size).");
                        return;
                    }

                    for (int y = 0; y < src.Height; y++)
                    {
                        for (int x = 0; x < src.Width; x++)
                        {
                            Color pix = token.StoredImg.GetPixel(x, y);
                            if (!src[x, y].Equals(ColorBgra.FromColor(pix)))
                            {
                                if (!token.DoInvertArea)
                                {
                                    if (token.DoInvertPicture)
                                    {
                                        dst[x, y] = pix;
                                    }
                                    else
                                    {
                                        dst[x, y] = src[x, y];
                                    }
                                }
                                else
                                {
                                    dst[x, y] = ColorBgra.Transparent;
                                }
                            }
                            else
                            {
                                if (!token.DoInvertArea)
                                {
                                    dst[x, y] = ColorBgra.Transparent;
                                }
                                else
                                {
                                    if (token.DoInvertPicture)
                                    {
                                        dst[x, y] = pix;
                                    }
                                    else
                                    {
                                        dst[x, y] = src[x, y];
                                    }
                                }
                            }
                        }
                    }
                }
            }

            StaticSettings.dialogResult = StaticSettings.DialogResult.Default;
        }
        #endregion
    }
}