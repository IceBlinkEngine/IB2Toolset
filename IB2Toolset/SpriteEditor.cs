using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IceBlinkCore;
using System.IO;
using System.Threading;

namespace IBBToolset
{
    public partial class SpriteEditor : Form
    {
        private Module se_mod = new Module();
        private Game se_game;
        private ParentForm se_prntForm;
        private Sprite sprite;
        private Bitmap propBitmap = new Bitmap(64, 64);
        private Bitmap actorBitmap = new Bitmap(64, 64);
        private Graphics p_device;
        public Bitmap p_surface;
        private Graphics a_device;
        public Bitmap a_surface;

        public SpriteEditor(Module mod, Game g, ParentForm prntForm)
        {
            InitializeComponent();
            se_mod = mod;
            se_game = g;
            se_prntForm = prntForm;
            sprite = new Sprite();
            sprite.passRefs(se_game);
            //create Props graphics device
            p_surface = new Bitmap(192, 192);
            pbProp.Image = p_surface;
            p_device = Graphics.FromImage(p_surface);
            //create Actors graphics device
            a_surface = new Bitmap(192, 192);
            pbActor.Image = a_surface;
            a_device = Graphics.FromImage(a_surface);
            refreshPanelInfo();
        }
        ~SpriteEditor()
        {
            try
            {
                a_device.Dispose();
                a_surface.Dispose();
                pbActor.Dispose();
                p_device.Dispose();
                p_surface.Dispose();
                pbProp.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to dispose");
                //this.errorLog("failed to dispose bitmap stuff: " + ex.ToString());
            }
        }

        #region File Stuff
        private void btnNew_Click(object sender, EventArgs e)
        {
            sprite = new Sprite();
            sprite.Image = new Bitmap(50, 50);
            refreshPanelInfo();
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = se_prntForm._mainDirectory + "\\modules\\" + se_mod.ModuleFolderName + "\\graphics\\sprites";
            //Empty the FileName text box of the dialog
            openFileDialog1.FileName = String.Empty;
            openFileDialog1.Filter = "Sprite files (*.spt)|*.spt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result != DialogResult.OK) return;
            if (openFileDialog1.FileName.Length == 0) return;
            if (result == DialogResult.OK) // Test result.
            {
                string filename = Path.GetFullPath(openFileDialog1.FileName);
                string directory = Path.GetDirectoryName(openFileDialog1.FileName);
                sprite = sprite.loadSpriteFile(filename);
                sprite.Image = new Bitmap(directory + "\\" + sprite.SpriteSheetFilename);            
            }
            refreshPanelInfo();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            saveAsFile();
        }
        private void saveFile(string filePath)
        {
            if (sprite.Filename.Length == 0)
            {
                saveAsFile();
                return;
            }
            //string directory = se_prntForm._mainDirectory + "\\modules\\" + se_mod.ModuleFolderName + "\\graphics\\sprites";
            string directory = filePath;
            try
            {
                if (!Directory.Exists(directory)) // if folder does not exist, create it and copy contents from previous folder
                {
                    MessageBox.Show("Didn't find the folder: " + directory);
                }
                else
                {
                    sprite.saveSpriteFile(directory + "\\" + sprite.Filename);
                    MessageBox.Show("Sprite saved");
                }                
            }
            catch (Exception e)
            {
                MessageBox.Show("failed to save sprite: " + e.ToString());
            }
        }
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            saveAsFile();
        }
        private void saveAsFile()
        {
            saveFileDialog1.RestoreDirectory = true; 
            saveFileDialog1.InitialDirectory = se_prntForm._mainDirectory + "\\modules\\" + se_mod.ModuleFolderName + "\\graphics\\sprites";
            saveFileDialog1.Filter = "Sprite files (*.spt)|*.spt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            //Empty the FileName text box of the dialog
            saveFileDialog1.FileName = String.Empty;            
            saveFileDialog1.DefaultExt = ".spt";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.OverwritePrompt = true;
            //saveFileDialog1.InitialDirectory = Environment.CurrentDirectory;

            //Open the dialog and determine which button was pressed
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result != DialogResult.OK) return;
            if (saveFileDialog1.FileName.Length == 0) return;
            //If the user presses the Save button
            if (result == DialogResult.OK)
            {
                sprite.Filename = Path.GetFileName(saveFileDialog1.FileName);
                string filePath2 = Path.GetFullPath(saveFileDialog1.FileName);
                string filePath = Path.GetDirectoryName(saveFileDialog1.FileName);
                MessageBox.Show("filename = " + sprite.Filename);
                saveFile(filePath);
            }
        }
        #endregion

        private void refreshPanelInfo()
        {
            try
            {
                numSize.Value = sprite.SpriteSize.Width;
                txtSpriteSheetFilename.Text = sprite.SpriteSheetFilename;
                txtTag.Text = sprite.Tag;
                rbtnActor.Checked = sprite.Actor;
                chkAnimated.Checked = sprite.Animated;
                chkShow.Checked = sprite.Show;
                chkLooping.Checked = sprite.Looping;
                numLayoutRows.Value = sprite.ImageLayout.Y;
                numLayoutColumns.Value = sprite.ImageLayout.X;
                numNumOfFrames.Value = sprite.NumberOfFrames;
                numFPS.Value = sprite.FPS;
                numIdleNoF.Value = sprite.IdleNumberOfFrames;
                numAttackingNoF.Value = sprite.AttackingNumberOfFrames;
                numWalkingNoF.Value = sprite.WalkingNumberOfFrames;
                numIdleFPS.Value = sprite.IdleFPS;
                numAttackingFPS.Value = sprite.AttackingFPS;
                numWalkingFPS.Value = sprite.WalkingFPS;
                numIdleDelay.Value = sprite.IdleDelay;
                if (sprite.Image != null)
                {
                    spritePropFirstFrameDraw();
                    spriteActorFirstFrameDraw();
                    //actorBitmap = new Bitmap(se_prntForm._mainDirectory + "\\modules\\" + se_mod.ModuleFolderName + "\\graphics\\sprites\\" + sprite.SpriteSheetFilename);
                    //propBitmap = new Bitmap(se_prntForm._mainDirectory + "\\modules\\" + se_mod.ModuleFolderName + "\\graphics\\sprites\\" + sprite.SpriteSheetFilename);
                    //pbActor.Image = (Image)actorBitmap;
                    //pbProp.Image = (Image)propBitmap;
                }
                if (sprite.SpriteSize.Width == 64)
                {
                    rbtn64.Checked = true;
                }
                else if (sprite.SpriteSize.Width == 128)
                {
                    rbtn128.Checked = true;
                }
                else if (sprite.SpriteSize.Width == 192)
                {
                    rbtn192.Checked = true;
                }
                else
                {
                    rbtn64.Checked = false;
                    rbtn128.Checked = false;
                    rbtn192.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to refresh panel: " + ex.ToString());
            }

        }
        private void numSize_ValueChanged(object sender, EventArgs e)
        {
            Size newSize = new Size((int)numSize.Value, (int)numSize.Value);
            sprite.SpriteSize = newSize;
        }
        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            openFileDialog2.InitialDirectory = se_prntForm._mainDirectory + "\\modules\\" + se_mod.ModuleFolderName + "\\graphics\\sprites";
            //Empty the FileName text box of the dialog
            openFileDialog2.FileName = String.Empty;
            openFileDialog2.Filter = "png files (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog2.FilterIndex = 1;

            DialogResult result = openFileDialog2.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string filename = openFileDialog2.SafeFileName;
                string directory = Path.GetDirectoryName(openFileDialog2.FileName);
                //MessageBox.Show("filename selected = " + filename);
                sprite.SpriteSheetFilename = filename;
                txtSpriteSheetFilename.Text = filename;
                sprite.Image = new Bitmap(directory + "\\" + sprite.SpriteSheetFilename);
                spriteActorFirstFrameDraw();
                spritePropFirstFrameDraw();
            }
        }
        private void txtTag_TextChanged(object sender, EventArgs e)
        {
            sprite.Tag = txtTag.Text;
        }
        private void rbtnProp_CheckedChanged(object sender, EventArgs e)
        {
            sprite.Actor = false;
            gbActor.Enabled = false;
            gbProp.Enabled = true;
        }
        private void rbtnActor_CheckedChanged(object sender, EventArgs e)
        {
            sprite.Actor = true;
            gbActor.Enabled = true;
            gbProp.Enabled = false;
        }
        private void rbtn64_CheckedChanged(object sender, EventArgs e)
        {
            Size newSize = new Size(64, 64);
            sprite.SpriteSize = newSize;
            numSize.Value = sprite.SpriteSize.Width;
            //refreshPanelInfo();
        }
        private void rbtn128_CheckedChanged(object sender, EventArgs e)
        {
            Size newSize = new Size(128, 128);
            sprite.SpriteSize = newSize;
            numSize.Value = sprite.SpriteSize.Width;
            //refreshPanelInfo();
        }
        private void rbtn192_CheckedChanged(object sender, EventArgs e)
        {
            Size newSize = new Size(192, 192);
            sprite.SpriteSize = newSize;
            numSize.Value = sprite.SpriteSize.Width;
            //refreshPanelInfo();
        }

        #region Prop Sprite
        private void chkAnimated_CheckedChanged(object sender, EventArgs e)
        {
            sprite.Animated = chkAnimated.Checked;
        }
        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            sprite.Show = chkShow.Checked;
        }
        private void chkLooping_CheckedChanged(object sender, EventArgs e)
        {
            sprite.Looping = chkLooping.Checked;
        }
        private void numLayoutRows_ValueChanged(object sender, EventArgs e)
        {
            //Point newlayout = sprite.ImageLayout;
            //newlayout.Y = (int)numLayoutRows.Value;
            sprite.ImageLayout.Y = (int)numLayoutRows.Value;
        }
        private void numLayoutColumns_ValueChanged(object sender, EventArgs e)
        {
            //Point newlayout = sprite.ImageLayout;
            //newlayout.X = (int)numLayoutColumns.Value;
            sprite.ImageLayout.X = (int)numLayoutColumns.Value;
        }
        private void numNumOfFrames_ValueChanged(object sender, EventArgs e)
        {
            sprite.NumberOfFrames = (int)numNumOfFrames.Value;
        }
        private void numFPS_ValueChanged(object sender, EventArgs e)
        {
            sprite.FPS = (int)numFPS.Value;
        }
        private void btnPropPlay_Click(object sender, EventArgs e)
        {
            int sleep = 300;
            if (sprite.FPS != 0)
                sleep = 1000/sprite.FPS;
            for (int y = 0; y < sprite.ImageLayout.Y; y++)
            {
                for (int x = 0; x < sprite.ImageLayout.X; x++)
                {
                    spritePropDraw(x, y, sleep);
                }
            }
        }
        public void spritePropDraw(int column, int row, int sleep)
        {
            p_device.Clear(Color.LightGray);
            //source image (x,y,width,height)
            Rectangle frame = new Rectangle((column * sprite.SpriteSize.Width), (sprite.SpriteSize.Height * row), sprite.SpriteSize.Width, sprite.SpriteSize.Height);
            //target location
            Rectangle target = new Rectangle(0, 0, sprite.SpriteSize.Width, sprite.SpriteSize.Height);
            //draw sprite
            p_device.DrawImage(sprite.Image, target, frame, GraphicsUnit.Pixel);
            pbProp.Image = p_surface;
            Application.DoEvents();
            Thread.Sleep(sleep);
        }
        public void spritePropFirstFrameDraw()
        {
            p_device.Clear(Color.LightGray);
            //source image (x,y,width,height)
            Rectangle frame = new Rectangle(0, 0, sprite.SpriteSize.Width, sprite.SpriteSize.Height);
            //target location
            Rectangle target = new Rectangle(0, 0, sprite.SpriteSize.Width, sprite.SpriteSize.Height);
            //draw sprite
            p_device.DrawImage(sprite.Image, target, frame, GraphicsUnit.Pixel);
            pbProp.Image = p_surface;
        }
        #endregion

        #region Actor Sprite
        private void numIdleNoF_ValueChanged(object sender, EventArgs e)
        {
            sprite.IdleNumberOfFrames = (int)numIdleNoF.Value;
        }        
        private void numAttackingNoF_ValueChanged(object sender, EventArgs e)
        {
            sprite.AttackingNumberOfFrames = (int)numAttackingNoF.Value;
        }
        private void numWalkingNoF_ValueChanged(object sender, EventArgs e)
        {
            sprite.WalkingNumberOfFrames = (int)numWalkingNoF.Value;
        }
        private void numIdleFPS_ValueChanged(object sender, EventArgs e)
        {
            sprite.IdleFPS = (int)numIdleFPS.Value;
        }
        private void numAttackingFPS_ValueChanged(object sender, EventArgs e)
        {
            sprite.AttackingFPS = (int)numAttackingFPS.Value;
        }
        private void numWalkingFPS_ValueChanged(object sender, EventArgs e)
        {
            sprite.WalkingFPS = (int)numWalkingFPS.Value;
        }
        private void numIdleDelay_ValueChanged(object sender, EventArgs e)
        {
            sprite.IdleDelay = (int)numIdleDelay.Value;
        }
        private void btnIdlePlay_Click(object sender, EventArgs e)
        {
            int row = 0; //idle animation is on the first row
            int sleep = 300;
            if (sprite.IdleFPS != 0)
            sleep = 1000 / sprite.IdleFPS;
            for (int x = 0; x < sprite.IdleNumberOfFrames; x++)
            {
                spriteActorDraw(x, row, sleep);
            }
            spriteActorFirstFrameDraw();
        }        
        private void btnAttackingPlay_Click(object sender, EventArgs e)
        {
            int row = 1; //attacking animation is on the first row
            int sleep = 300;
            if (sprite.AttackingFPS != 0)
                sleep = 1000 / sprite.AttackingFPS;
            for (int x = 0; x < sprite.AttackingNumberOfFrames; x++)
            {
                spriteActorDraw(x, row, sleep);
            }
            spriteActorFirstFrameDraw();
        }
        private void btnWalkingPlay_Click(object sender, EventArgs e)
        {
            int row = 2; //attacking animation is on the first row
            int sleep = 300;
            if (sprite.WalkingFPS != 0)
                sleep = 1000 / sprite.WalkingFPS;
            for (int x = 0; x < sprite.WalkingNumberOfFrames; x++)
            {
                spriteActorDraw(x, row, sleep);
            }
            spriteActorFirstFrameDraw();
        }
        private void btnIdleLoop_Click(object sender, EventArgs e)
        {
            int row = 0; //idle animation is on the first row
            int sleep = 300;
            if (sprite.IdleFPS != 0)
                sleep = 1000 / sprite.IdleFPS;
            for (int x = 0; x < sprite.IdleNumberOfFrames; x++)
            {
                spriteActorDraw(x, row, sleep);
            }
            spriteActorFirstFrameDraw();
            Application.DoEvents();
            Thread.Sleep(sprite.IdleDelay);
            for (int x = 0; x < sprite.IdleNumberOfFrames; x++)
            {
                spriteActorDraw(x, row, sleep);
            }
            spriteActorFirstFrameDraw();
        }
        public void spriteActorDraw(int column, int row, int sleep)
        {
            a_device.Clear(Color.LightGray);
            //source image (x,y,width,height)
            Rectangle frame = new Rectangle((column * sprite.SpriteSize.Width), (sprite.SpriteSize.Height * row), sprite.SpriteSize.Width, sprite.SpriteSize.Height);
            //target location
            Rectangle target = new Rectangle(0, 0, sprite.SpriteSize.Width, sprite.SpriteSize.Height);
            //draw sprite
            a_device.DrawImage(sprite.Image, target, frame, GraphicsUnit.Pixel);
            pbActor.Image = a_surface;
            Application.DoEvents();
            Thread.Sleep(sleep);
        }
        public void spriteActorFirstFrameDraw()
        {
            a_device.Clear(Color.LightGray);
            //source image (x,y,width,height)
            Rectangle frame = new Rectangle(0, 0, sprite.SpriteSize.Width, sprite.SpriteSize.Height);
            //target location
            Rectangle target = new Rectangle(0, 0, sprite.SpriteSize.Width, sprite.SpriteSize.Height);
            //draw sprite
            a_device.DrawImage(sprite.Image, target, frame, GraphicsUnit.Pixel);
            pbActor.Image = a_surface;
        }
        #endregion        
    }
}
