﻿#region LICENSE

//     This file (DlgGame.cs) is part of Depressurizer.
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

using System;
using System.IO;
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class DlgGame : Form
    {
        #region Fields

        public GameInfo Game;

        private readonly GameList Data;

        private readonly bool editMode;

        #endregion

        #region Constructors and Destructors

        public DlgGame(GameList data, GameInfo game = null) : this()
        {
            Data = data;
            Game = game;
            editMode = Game != null;
        }

        private DlgGame()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(txtExecutable.Text);
                    dialog.InitialDirectory = fileInfo.DirectoryName;
                    dialog.FileName = fileInfo.Name;
                }
                catch (ArgumentException) { }

                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtExecutable.Text = dialog.FileName;
                }
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (editMode)
            {
                Game.Name = txtName.Text;
                Game.Executable = txtExecutable.Text;
            }
            else
            {
                if (!int.TryParse(txtId.Text, out int id))
                {
                    MessageBox.Show(GlobalStrings.DlgGameDBEntry_IDMustBeInteger, GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Data.Games.ContainsKey(id))
                {
                    MessageBox.Show(GlobalStrings.DBEditDlg_GameIdAlreadyExists, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Game = new GameInfo(id, txtName.Text, Data, txtExecutable.Text);
                Game.ApplySource(GameListingSource.Manual);
                Data.Games.Add(id, Game);
            }

            Game.SetFavorite(chkFavorite.Checked);

            Game.Hidden = chkHidden.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void GameDlg_Load(object sender, EventArgs e)
        {
            if (editMode)
            {
                Text = GlobalStrings.DlgGame_EditGame;
                txtId.Text = Game.Id.ToString();
                txtName.Text = Game.Name;
                txtCategory.Text = Game.GetCatString();
                txtExecutable.Text = Game.Executable;
                chkFavorite.Checked = Game.IsFavorite();
                chkHidden.Checked = Game.Hidden;
                txtId.ReadOnly = true;
            }
            else
            {
                Text = GlobalStrings.DlgGame_CreateGame;
            }
        }

        #endregion
    }
}
