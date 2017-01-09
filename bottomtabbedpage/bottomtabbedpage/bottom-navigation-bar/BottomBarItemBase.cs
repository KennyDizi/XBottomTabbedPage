/*
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

using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;

namespace BottomNavigationBar
{
    /// <summary>
    /// BottomBarItemBase
    /// </summary>
    public class BottomBarItemBase
    {
        protected int _iconResource;
        protected Drawable _icon;
        protected int _titleResource;
        protected string _title;
        protected int _color;
        protected bool _isEnabled = true;
        protected bool _isVisible = true;

        /// <summary>
        /// IsEnabled
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
            }
        }

        /// <summary>
        /// IsVisible
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
            }
        }

        /// <summary>
        /// GetIcon
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Drawable GetIcon(Context context)
        {
			return _iconResource != 0 ? AppCompatDrawableManager.Get ().GetDrawable(context, _iconResource) : _icon;
        }

        /// <summary>
        /// GetTitle
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetTitle(Context context)
        {
            return _titleResource != 0 ? context.GetString(_titleResource) : _title;
        }
    }
}