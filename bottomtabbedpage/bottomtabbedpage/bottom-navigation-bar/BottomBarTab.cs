﻿/*
 * C# port BottomBar library for Android
 * Copyright (c) 2016 Iiro Krankka (http://github.com/roughike).
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Android.Graphics.Drawables;

namespace BottomNavigationBar
{
    /// <summary>
    /// BottomBarTab
    /// </summary>
    public class BottomBarTab : BottomBarItemBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id = -1;

        /// <summary>
        /// Creates a new Tab for the BottomBar
        /// </summary>
        /// <param name="iconResource">a resource for the Tab icon.</param>
        /// <param name="title">title for the Tab.</param>
        public BottomBarTab(int iconResource, string title)
        {
            _iconResource = iconResource;
            _title = title;
        }

        /// <summary>
        /// Creates a new Tab for the BottomBar.
        /// </summary>
        /// <param name="icon">an icon for the Tab.</param>
        /// <param name="title">title title for the Tab.</param>
        public BottomBarTab(Drawable icon, string title)
        {
            _icon = icon;
            _title = title;
        }

        /// <summary>
        /// Creates a new Tab for the BottomBar.
        /// </summary>
        /// <param name="icon">an icon for the Tab.</param>
        /// <param name="titleResource">resource for the title.</param>
        public BottomBarTab(Drawable icon, int titleResource)
        {
            _icon = icon;
            _titleResource = titleResource;
        }

        /// <summary>
        /// Creates a new Tab for the BottomBar.
        /// </summary>
        /// <param name="iconResource">a resource for the Tab icon.</param>
        /// <param name="titleResource">resource for the title.</param>
        public BottomBarTab(int iconResource, int titleResource)
        {
            _iconResource = iconResource;
            _titleResource = titleResource;
        }
    }
}