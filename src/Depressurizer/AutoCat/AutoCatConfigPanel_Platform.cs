﻿#region LICENSE

//     This file (AutoCatConfigPanel_Platform.cs) is part of Depressurizer.
//     Copyright (C) 2011 Steve Labbe
//     Copyright (C) 2017 Theodoros Dimos
//     Copyright (C) 2017 Martijn Vegter
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

namespace Depressurizer
{
    public partial class AutoCatConfigPanel_Platform : AutoCatConfigPanel
    {
        #region Constructors and Destructors

        public AutoCatConfigPanel_Platform()
        {
            InitializeComponent();
            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);
        }

        #endregion

        #region Public Methods and Operators

        public override void LoadFromAutoCat(AutoCat autoCat)
        {
            AutoCatPlatform acPlatform = autoCat as AutoCatPlatform;
            if (acPlatform == null)
            {
                return;
            }

            txtPrefix.Text = acPlatform.Prefix == null ? string.Empty : acPlatform.Prefix;
            chkboxPlatforms.SetItemChecked(0, acPlatform.Windows);
            chkboxPlatforms.SetItemChecked(1, acPlatform.Mac);
            chkboxPlatforms.SetItemChecked(2, acPlatform.Linux);
            chkboxPlatforms.SetItemChecked(3, acPlatform.SteamOS);
        }

        public override void SaveToAutoCat(AutoCat autoCat)
        {
            AutoCatPlatform ac = autoCat as AutoCatPlatform;
            if (ac == null)
            {
                return;
            }

            ac.Prefix = txtPrefix.Text;
            ac.Windows = chkboxPlatforms.GetItemChecked(0);
            ac.Mac = chkboxPlatforms.GetItemChecked(1);
            ac.Linux = chkboxPlatforms.GetItemChecked(2);
            ac.SteamOS = chkboxPlatforms.GetItemChecked(3);
        }

        #endregion
    }
}
