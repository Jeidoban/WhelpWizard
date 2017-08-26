using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using WhelpWizard;
using WhelpWizard.Droid;

[assembly: ExportRenderer(typeof(ButtonWithoutShadow), typeof(ButtonWithoutShadowRenderer))]
namespace WhelpWizard.Droid
{
    public class ButtonWithoutShadowRenderer : ButtonRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.Elevation = 0;
			}
		}
	}
}
