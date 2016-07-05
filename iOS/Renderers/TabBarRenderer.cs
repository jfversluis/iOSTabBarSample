using System;
using iOSTabBarSample.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer (typeof (TabbedPage), typeof (TabBarRenderer))]

namespace iOSTabBarSample.iOS
{
    public class TabBarRenderer : TabbedRenderer
    {
        private bool _initialized;

        public override void ViewWillAppear (bool animated)
        {
            if (!_initialized) {
                if (TabBar?.Items == null)
                    return;

                var tabs = Element as TabbedPage;

                if (tabs != null) {
                    for (int i = 0; i < TabBar.Items.Length; i++) {
                        UpdateItem (TabBar.Items [i], tabs.Children [i].Icon, tabs.Children [i].StyleId);
                    }
                }

                _initialized = true;
            }

            base.ViewWillAppear (animated);
        }

        private void UpdateItem (UITabBarItem item, string icon, string badgeValue)
        {
            if (item == null)
                return;

            try {
                if (icon.EndsWith (".png"))
                    icon = icon.Replace (".png", "_selected.png");
                else
                    icon += "_selected";

                item.SelectedImage = UIImage.FromBundle (icon);
                item.SelectedImage.AccessibilityIdentifier = icon;
            } catch (Exception ex) {
                Console.WriteLine ("Unable to set selected icon: " + ex);
            }
        }
    }
}