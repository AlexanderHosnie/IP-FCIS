﻿using IP_FCIS.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IP_FCIS.Forms
{
    public partial class PictureForm : Form, IP_FCIS.Forms.MainForm.ImageBox
    {

        public TypicalImage opened_image;
        Point start, finish;
        public PictureForm()
        {
            InitializeComponent();
        }
        private void PictureForm_Load(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = opened_image.get_bitmap();

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void ChildForm_Activeted(object sender, EventArgs e)
        {
            try
            {
                Program.main_form.set_form_width_height_values(opened_image.get_width(), opened_image.get_height());
                if(Program.main_form.histogram_form != null)
                {
                    this.histogram();
                }

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void set_new_image()
        {
            try
            {
                this.pictureBox1.Image = this.opened_image.get_bitmap();
                Program.main_form.set_form_width_height_values(opened_image.get_width(), opened_image.get_height());
                this.Width = opened_image.get_width() + 50;
                this.Height = opened_image.get_height() + 50;
                if(Program.main_form.histogram_form != null)
                {
                    histogram();
                }

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void transformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.transformation();

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void transformation()
        {
            TransformationsForm trans_form = new TransformationsForm();
            trans_form.working_on = this.opened_image;
            trans_form.ShowDialog(this);
            this.opened_image = trans_form.working_on;
            set_new_image();
        }
        public void save()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = opened_image.get_file_name();
            save.Filter = "PPM Image (.ppm)|*.ppm|Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg; *.jpg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff; *.tif";
            Hashtable formats = new Hashtable();
            formats.Add("ppm", 1);
            formats.Add("bmp", 2);
            formats.Add("gif", 3);
            formats.Add("jpeg", 4);
            formats.Add("jpg", 4);
            formats.Add("png", 5);
            formats.Add("tiff", 6);
            formats.Add("tif", 6);
            save.FilterIndex = (int)formats[opened_image.get_extension()];
            save.ValidateNames = true;
            save.AddExtension = true;
            if (save.ShowDialog() == DialogResult.OK)
            {
                string ext = Path.GetExtension(save.FileName);
                if (ext == ".ppm")
                {
                    SaveForm saveform = new SaveForm();
                    saveform.saving_image = this.opened_image;
                    saveform.fileName = save.FileName;
                    saveform.ShowDialog(this);
                }
                else if(ext == ".bmp")
                {
                    opened_image.save_common(save.FileName, ImageFormat.Bmp);
                }
                else if(ext == ".gif")
                {
                    opened_image.save_common(save.FileName, ImageFormat.Gif);
                }
                else if (ext == ".jpeg")
                {
                    opened_image.save_common(save.FileName, ImageFormat.Jpeg);
                }
                else if (ext == ".png")
                {
                    opened_image.save_common(save.FileName, ImageFormat.Png);
                }
                else if (ext == ".tiff")
                {
                    opened_image.save_common(save.FileName, ImageFormat.Tiff);
                }

            }


        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.save();

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void gray_scale()
        {
            opened_image.gray_scale();
            set_new_image();
        }
        public void not()
        {
            opened_image.not();
            set_new_image();
        }
        public void histogram()
        {
            int[][] histogram_data = new int[4][];
            histogram_data[0] = new int[256];
            histogram_data[1] = new int[256];
            histogram_data[2] = new int[256];
            histogram_data[3] = new int[256];

            opened_image.histogram(ref histogram_data);

            if(Program.main_form.histogram_form == null)
            {
                Program.main_form.histogram_form = new HistogramForm();
                Program.main_form.histogram_form.MdiParent = this.ParentForm;
                
            }

            Program.main_form.histogram_form.Text = "Histogram " + this.Text;
            Program.main_form.histogram_form.current_histogram_data = histogram_data;
            Program.main_form.histogram_form.draw_histogram();

            if(!Program.main_form.histogram_form.Visible)
            {
                Program.main_form.histogram_form.Show();

            } 
            
        }
        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if(Program.main_form.histogram_form != null)
                {
                    Program.main_form.histogram_form.Activate();
                }
                this.histogram();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void brightnessContrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.brightness_contrast();

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void brightness_contrast()
        {
            BrightnessContrastForm brightness_contrast_form = new BrightnessContrastForm();
            brightness_contrast_form.current_image = this.opened_image;
            brightness_contrast_form.ShowDialog(this);
            this.opened_image = brightness_contrast_form.current_image;
            set_new_image();
        }
        private void gammaCorrectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.gamma_correction();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void gamma_correction()
        {
            GammaForm gamma_correction_form = new GammaForm();
            gamma_correction_form.current_image = this.opened_image;
            gamma_correction_form.ShowDialog(this);
            this.opened_image = gamma_correction_form.current_image;
            set_new_image();
        }
        public void bitplane()
        {
            BitPlaneForm bitPlaneForm = new BitPlaneForm();
            bitPlaneForm.source = this.opened_image;
            bitPlaneForm.ShowDialog(this);
            this.opened_image = bitPlaneForm.source;
            set_new_image();

        }
        public void quantization()
        {
            QuantizationForm quantiaztionForm = new QuantizationForm();
            quantiaztionForm.img = this.opened_image;
            quantiaztionForm.ShowDialog(this);
            set_new_image();
        }
        public void smooth()
        {
            SmoothForm smoothForm = new SmoothForm();
            smoothForm.img = this.opened_image;
            smoothForm.ShowDialog(this);
        }
        public void sharp()
        {
            SharpForm sharpForm = new SharpForm();
            sharpForm.img = this.opened_image;
            sharpForm.Show(this);
        }
        private void PictureForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.main_form.images_array.Remove(opened_image);
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //start.X = e.Location.X;
            //start.Y = e.Location.Y;
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //finish.X = e.Location.X;
            //finish.Y = e.Location.Y;
            //double angel = (Math.Atan2(finish.Y, finish.X) - Math.Atan2(start.Y, start.X)*180 /Math.PI);
            //MessageBox.Show(""+angel);
            //angel = Math.Abs(angel);
            // opened_image.rotate((float)angel);
            // pictureBox1.Image = opened_image.get_bitmap();
        }
        private void toolStripMenuIBitPlane_Click(object sender, EventArgs e)
        {
            try
            {
                this.bitplane();

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void quantizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.quantization();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
