﻿using XamForms.Controls;
using XamForms.Controls.iOS;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;
#if __UNIFIED__
using Foundation;
#else
using MonoTouch.Foundation;
#endif

[assembly: Xamarin.Forms.ExportRenderer(typeof(CalendarButton), typeof(CalendarButtonRenderer))]
namespace XamForms.Controls.iOS
{
	[Preserve(AllMembers = true)]
    public class CalendarButtonRenderer : ButtonRenderer
    {
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var element = Element as CalendarButton;
            if (e.PropertyName == nameof(element.TextWithoutMeasure) || e.PropertyName == "Renderer")
            {
                Control.SetTitle(element.TextWithoutMeasure, UIControlState.Normal);
				Control.SetTitle(element.TextWithoutMeasure, UIControlState.Disabled);
            }
			if (e.PropertyName == nameof(element.TextColor) || e.PropertyName == "Renderer")
			{
				Control.SetTitleColor(element.TextColor.ToUIColor(), UIControlState.Disabled);
				Control.SetTitleColor(element.TextColor.ToUIColor(), UIControlState.Normal);
			}
			if (e.PropertyName == nameof(element.BackgroundPattern))
			{ 
				DrawBackgroundPattern();
			}
        }

		public override void Draw(CGRect rect)
		{
			base.Draw(rect);
			DrawBackgroundPattern();
		}

		protected void DrawBackgroundPattern()
		{
			var element = Element as CalendarButton;
			Control.SetBackgroundImage(null, UIControlState.Normal);
			Control.SetBackgroundImage(null, UIControlState.Disabled);
			if (element == null || element.BackgroundPattern == null || Control.Frame.Width == 0) return;

			UIImage image;
			UIGraphics.BeginImageContext(Control.Frame.Size);
			using (CGContext g = UIGraphics.GetCurrentContext())
			{
				for (var i = 0; i < element.BackgroundPattern.Pattern.Count; i++)
				{
					var p = element.BackgroundPattern.Pattern[i];
					g.SetFillColor(p.Color.ToCGColor());
					var l = (int)(Control.Frame.Width * element.BackgroundPattern.GetLeft(i));
					var t = (int)(Control.Frame.Height * element.BackgroundPattern.GetTop(i));
					var w = (int)(Control.Frame.Width * element.BackgroundPattern.Pattern[i].WidthPercent);
					var h = (int)(Control.Frame.Height * element.BackgroundPattern.Pattern[i].HightPercent);
					g.FillRect(new CGRect { X = l, Y = t, Width = w, Height = h });
				}

				image = UIGraphics.GetImageFromCurrentImageContext();
			}
			UIGraphics.EndImageContext();
			Control.SetBackgroundImage(image, UIControlState.Normal);
			Control.SetBackgroundImage(image, UIControlState.Disabled);
		}
    }

	public static class Calendar
	{
		public static void Init()
		{
			var d = "";
		}
	}
}

