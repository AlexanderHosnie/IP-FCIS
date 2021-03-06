﻿using IP_FCIS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IP_FCIS.Forms
{
    public partial class BrightnessContrastForm : Form
    {
        public TypicalImage current_image;
        public TypicalImage small_current, edited_current;
        public BrightnessContrastForm()
        {
            InitializeComponent();
        }
        private void trackBrightness_ValueChanged(object sender, EventArgs e)
        {
            this.numericBrightness.Value = this.trackBrightness.Value;
            edited_current = small_current.change_brightness(Convert.ToInt32(this.numericBrightness.Value));
            edited_current = edited_current.change_contrast(Convert.ToInt32(this.numericContrast.Value));
            this.Adjusted.Image = edited_current.get_bitmap();

        }
        private void numericBrightness_ValueChanged(object sender, EventArgs e)
        {
            this.trackBrightness.Value = Convert.ToInt32(this.numericBrightness.Value);
            
        }
        private void trackContrast_ValueChanged(object sender, EventArgs e)
        {
            this.numericContrast.Value = this.trackContrast.Value;
            edited_current = small_current.change_brightness(Convert.ToInt32(this.numericBrightness.Value));
            edited_current = edited_current.change_contrast(Convert.ToInt32(this.numericContrast.Value));
            this.Adjusted.Image = edited_current.get_bitmap();
        }
        private void numericContrast_ValueChanged(object sender, EventArgs e)
        {
            this.trackContrast.Value = Convert.ToInt32(this.numericContrast.Value);
        }
        private void BrightnessContrastForm_Load(object sender, EventArgs e)
        {
            try
            {
                int width, height;
                small_current = new TypicalImage(current_image);
                if (small_current.get_width() > 200 || small_current.get_height() > 200)
                {
                    if(small_current.get_width() > small_current.get_height())
                    {
                        width = 200;
                        height = (width * small_current.get_height()) / small_current.get_width();

                    } else
                    {
                        height = 200;
                        width = (height * small_current.get_width()) / small_current.get_height();
                    }

                    small_current.resize(width, height);
                }
                this.Original.Image = this.small_current.get_bitmap();
                this.Adjusted.Image = this.small_current.get_bitmap();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                current_image = current_image.change_brightness(Convert.ToInt32(this.numericBrightness.Value));
                current_image = current_image.change_contrast(Convert.ToInt32(this.numericContrast.Value));
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

    }
}
