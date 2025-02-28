using System.ComponentModel;
using System.Runtime.CompilerServices;
using Android.Views;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using Plugin.Badge.Abstractions;
using Font = Microsoft.Maui.Font;
using View = Android.Views.View;

namespace Plugin.Badge.Droid
{
    internal static class BadgeViewExtensions
    {
        public static void UpdateFromElement(this BadgeView badgeView, Page element, Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.ToolbarPlacement toolbarPlacement)
        {
            //get text
            var badgeText = TabBadge.GetBadgeText(element);
            badgeView.Text = badgeText;
            if (badgeView.BottomBadge != null)
            {
                if (Int32.TryParse(TabBadge.GetBadgeText(element), out int number) && number != 0)
                {
                    badgeView.BottomBadge.SetVisible(true);
                    badgeView.BottomBadge.Number = number;
                }
                else
                {
                    badgeView.BottomBadge.SetVisible(false);
                    badgeView.BottomBadge.ClearNumber();
                }
            }

            // set color if not default
            var tabColor = TabBadge.GetBadgeColor(element);
            if (tabColor.IsNotDefault())
            {
                badgeView.BadgeColor = tabColor.ToAndroid();

                if (badgeView.BottomBadge != null)
                {
                    badgeView.BottomBadge.BackgroundColor = tabColor.ToAndroid().ToArgb();
                }
            }

            // set text color if not default
            var tabTextColor = TabBadge.GetBadgeTextColor(element);
            if (tabTextColor.IsNotDefault())
            {
                badgeView.TextColor = tabTextColor.ToAndroid();

                if (badgeView.BottomBadge != null)
                {
                    badgeView.BottomBadge.BadgeTextColor = tabTextColor.ToAndroid().ToArgb();
                }
            }

            // set font if not default
            var font = TabBadge.GetBadgeFont(element);
            if (font != Font.Default)
            {
                badgeView.Typeface = font.ToTypeface((element.Handler ?? Application.Current.Handler).MauiContext.Services.GetRequiredService<IFontManager>());
            }

            var margin = TabBadge.GetBadgeMargin(element);
            if (toolbarPlacement == Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom)
            {
                //var left = Math.Abs((float)margin.Left);
                //var top = Math.Abs((float)margin.Top);
                //var right = Math.Abs((float)margin.Right);
                //var bottom = Math.Abs((float)margin.Bottom);
                //badgeView.SetMargins(left, top, right, bottom);
            }
            else
            {
                badgeView.SetMargins((float)margin.Left, (float)margin.Top, (float)margin.Right, (float)margin.Bottom);
            }

            // set position
            badgeView.Postion = TabBadge.GetBadgePosition(element);
        }

        public static void UpdateFromPropertyChangedEvent(this BadgeView badgeView, Element element, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == TabBadge.BadgeTextProperty.PropertyName)
            {
                badgeView.Text = TabBadge.GetBadgeText(element);
                if (badgeView.BottomBadge != null)
                {
                    if (Int32.TryParse(TabBadge.GetBadgeText(element), out int number) && number != 0)
                    {
                        badgeView.BottomBadge.SetVisible(true);
                        badgeView.BottomBadge.Number = number;
                    }
                    else
                    {
                        badgeView.BottomBadge.SetVisible(false);
                        badgeView.BottomBadge.ClearNumber();
                    }
                }
                return;
            }

            if (e.PropertyName == TabBadge.BadgeColorProperty.PropertyName)
            {
                badgeView.BadgeColor = TabBadge.GetBadgeColor(element).ToAndroid();
                if (TabBadge.GetBadgeColor(element).IsNotDefault())
                {
                    if (badgeView.BottomBadge != null)
                    {
                        badgeView.BottomBadge.BackgroundColor = TabBadge.GetBadgeColor(element).ToAndroid().ToArgb();
                    }
                }
                return;
            }

            if (e.PropertyName == TabBadge.BadgeTextColorProperty.PropertyName)
            {
                badgeView.TextColor = TabBadge.GetBadgeTextColor(element).ToAndroid();
                if (TabBadge.GetBadgeTextColor(element).IsNotDefault())
                {
                    if (badgeView.BottomBadge != null)
                    {
                        badgeView.BottomBadge.BadgeTextColor = TabBadge.GetBadgeTextColor(element).ToAndroid().ToArgb();
                    }
                }
                return;
            }

            if (e.PropertyName == TabBadge.BadgeFontProperty.PropertyName)
            {
                badgeView.Typeface = TabBadge.GetBadgeFont(element).ToTypeface((element.Handler ?? Application.Current.Handler).MauiContext.Services.GetRequiredService<IFontManager>());
                return;
            }

            if (e.PropertyName == TabBadge.BadgePositionProperty.PropertyName)
            {
                badgeView.Postion = TabBadge.GetBadgePosition(element);
                return;
            }

            if (e.PropertyName == TabBadge.BadgeMarginProperty.PropertyName)
            {
                var margin = TabBadge.GetBadgeMargin(element);
                badgeView.SetMargins((float)margin.Left, (float)margin.Top, (float)margin.Right, (float)margin.Bottom);
                return;
            }
        }

        public static T FindChildOfType<T>(this ViewGroup parent) where T : View
        {
            if (parent == null)
                return null;

            if (parent.ChildCount == 0)
                return null;

            for (var i = 0; i < parent.ChildCount; i++)
            {
                var child = parent.GetChildAt(i);


                if (child is T typedChild)
                {
                    return typedChild;
                }

                if (!(child is ViewGroup))
                    continue;


                var result = FindChildOfType<T>(child as ViewGroup);
                if (result != null)
                    return result;
            }

            return null;
        }
    }
}