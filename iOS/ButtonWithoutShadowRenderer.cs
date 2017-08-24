using System;
using Xamarin.Forms;
using WhelpWizard;
using WhelpWizard.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ButtonWithoutShadow), typeof(ButtonWithoutShadowRenderer))]
namespace WhelpWizard.iOS
{
	public class ButtonWithoutShadowRenderer : Xamarin.Forms.Platform.iOS.ButtonRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
			}
		}
	}
}
