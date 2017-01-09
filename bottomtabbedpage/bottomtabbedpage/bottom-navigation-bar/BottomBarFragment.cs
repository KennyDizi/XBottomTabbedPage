using System;
using Android.Graphics.Drawables;

namespace BottomNavigationBar
{
    /// <summary>
    /// BottomBarFragment
    /// </summary>
    [Obsolete("Deprecated")]
    public class BottomBarFragment : BottomBarItemBase
    {
        /// <summary>
        /// Fragment
        /// </summary>
        public Android.App.Fragment Fragment
        {
            get;
            private set;
        }

        /// <summary>
        /// SupportFragment
        /// </summary>
        public Android.Support.V4.App.Fragment SupportFragment
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new Tab for the BottomBar.
        /// </summary>
        /// <param name="fragment">a Fragment to be shown when this Tab is selected.</param>
        /// <param name="iconResource"> a resource for the Tab icon.</param>
        /// <param name="title">title for the Tab.</param>
        public BottomBarFragment(Android.App.Fragment fragment, int iconResource, string title)
        {
            Fragment = fragment;
            _iconResource = iconResource;
            _title = title;
        }

        /// <summary>
        /// Creates a new Tab for the BottomBar.
        /// </summary>
        /// <param name="fragment"> a Fragment to be shown when this Tab is selected.</param>
        /// <param name="icon">an icon for the Tab.</param>
        /// <param name="title">title for the Tab.</param>
        public BottomBarFragment(Android.App.Fragment fragment, Drawable icon, string title)
        {
            Fragment = fragment;
            _icon = icon;
            _title = title;
        }

        /// <summary>
        /// Creates a new Tab for the BottomBar.
        /// </summary>
        /// <param name="fragment"> a Fragment to be shown when this Tab is selected.</param>
        /// <param name="icon"> an icon for the Tab.</param>
        /// <param name="titleResource">resource for the title.</param>
        public BottomBarFragment(Android.App.Fragment fragment, Drawable icon, int titleResource)
        {
            Fragment = fragment;
            _icon = icon;
            _titleResource = titleResource;
        }

        /// <summary>
        /// Creates a new Tab for the BottomBar.
        /// </summary>
        /// <param name="fragment"> a Fragment to be shown when this Tab is selected.</param>
        /// <param name="iconResource"> a resource for the Tab icon.</param>
        /// <param name="titleResource"> resource for the title.</param>
        public BottomBarFragment(Android.App.Fragment fragment, int iconResource, int titleResource)
        {
            Fragment = fragment;
            _iconResource = iconResource;
            _titleResource = titleResource;
        }

        /// <summary>
        /// Creates a new Tab for the BottomBar.
        /// </summary>
        /// <param name="fragment"> a Fragment to be shown when this Tab is selected.</param>
        /// <param name="iconResource">a resource for the Tab icon.</param>
        /// <param name="title"> title for the Tab.</param>
        public BottomBarFragment(Android.Support.V4.App.Fragment fragment, int iconResource, string title)
        {
            SupportFragment = fragment;
            _iconResource = iconResource;
            _title = title;
        }

        /// <summary>
        /// Creates a new Tab for the BottomBar.
        /// </summary>
        /// <param name="fragment"> a Fragment to be shown when this Tab is selected.</param>
        /// <param name="icon"> an icon for the Tab.</param>
        /// <param name="title"> title for the Tab.</param>
        public BottomBarFragment(Android.Support.V4.App.Fragment fragment, Drawable icon, string title)
        {
            SupportFragment = fragment;
            _icon = icon;
            _title = title;
        }

        /// <summary>
        /// Creates a new Tab for the BottomBar.
        /// </summary>
        /// <param name="fragment">a Fragment to be shown when this Tab is selected.</param>
        /// <param name="icon">an icon for the Tab.</param>
        /// <param name="titleResource">resource for the title.</param>
        public BottomBarFragment(Android.Support.V4.App.Fragment fragment, Drawable icon, int titleResource)
        {
            SupportFragment = fragment;
            _icon = icon;
            _titleResource = titleResource;
        }

        /// <summary>
        /// Creates a new Tab for the BottomBar.
        /// </summary>
        /// <param name="fragment">a Fragment to be shown when this Tab is selected.</param>
        /// <param name="iconResource">a resource for the Tab icon.</param>
        /// <param name="titleResource">resource for the title.</param>
        public BottomBarFragment(Android.Support.V4.App.Fragment fragment, int iconResource, int titleResource)
        {
            SupportFragment = fragment;
            _iconResource = iconResource;
            _titleResource = titleResource;
        }
    }
}

