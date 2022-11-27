// +-------------------------------- DISCLAIMER ---------------------------------+
// |                                                                             |
// | This application program is provided to you free of charge as an example.   |
// | Despite the considerable efforts of Euresys personnel to create a usable    |
// | example, you should not assume that this program is error-free or suitable  |
// | for any purpose whatsoever.                                                 |
// |                                                                             |
// | EURESYS does not give any representation, warranty or undertaking that this |
// | program is free of any defect or error or suitable for any purpose. EURESYS |
// | shall not be liable, in contract, in torts or otherwise, for any damages,   |
// | loss, costs, expenses or other claims for compensation, including those     |
// | asserted by third parties, arising out of or in connection with the use of  |
// | this program.                                                               |
// |                                                                             |
// +-----------------------------------------------------------------------------+

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;

using Euresys.Open_eVision_2_17;

namespace QrcReading
{
  /// <summary>
  /// Summary description for Form1.
  /// </summary>
  public class MainForm : System.Windows.Forms.Form
  {
    // The source image
    private EImageBW8 m_Source = null;

    // The QR code reading tool
    private EQRCodeReader m_qrCodeReader = null;
    private EQRCode[] m_qrCode = null;
    private bool m_bFound;
    private EByteInterpretationMode m_byteInterpretationMode;
    private List<EQRCodeModel> m_models;

    // The string for the decoded text
    private string[] m_DecodedText = new string[256];


    private Font m_Font = new Font("Arial", 9);
    private Brush m_BlackBrush = new SolidBrush(Color.Black);
    private Brush m_WhiteBrush = new SolidBrush(Color.White);

    private System.Windows.Forms.MainMenu mainMenu;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.MenuItem fileMenu;
    private System.Windows.Forms.MenuItem fileLoadMenu;
    private System.Windows.Forms.MenuItem fileExitMenu;
    private System.Windows.Forms.MenuItem menuItem2;
    private System.Windows.Forms.MenuItem menuItem3;
    private System.Windows.Forms.MenuItem ApplicationIntroduction;
    private System.Windows.Forms.MenuItem parametersMenu;
    private System.Windows.Forms.MenuItem detectionTradeoffMenu;
    private System.Windows.Forms.MenuItem tradeOffFavorSpeedItem;
    private System.Windows.Forms.MenuItem tradeOffBalancedItem;
    private System.Windows.Forms.MenuItem tradeOffFavorReliabilityItem;
    private System.Windows.Forms.MenuItem byteInterpretationMenu;
    private System.Windows.Forms.MenuItem byteHexadecimalItem;
    private System.Windows.Forms.MenuItem byteUTF8Item;
    private System.Windows.Forms.MenuItem byteAutoItem;
    private System.Windows.Forms.MenuItem modelsMenu;
    private System.Windows.Forms.MenuItem modelsModel1Item;
    private System.Windows.Forms.MenuItem modelsModel2Item;
    private System.Windows.Forms.MenuItem modelsMicroQRItem;
    
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    public MainForm()
    {
      Easy.Initialize();

      m_Source = new EImageBW8();
      m_qrCodeReader = new EQRCodeReader();
      m_byteInterpretationMode = EByteInterpretationMode.Auto;
      m_models = new List<EQRCodeModel>();

      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();

      // Welcome message box
      ApplicationIntroduction_Click(null, null);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (components != null)
        {
          components.Dispose();
        }

        m_Source.Dispose();

        m_qrCodeReader.Dispose();
        if (m_qrCode != null)
        {
          for (int i = 0; i < m_qrCode.Length; i++)
          {
            m_qrCode[i].Dispose();
          }
        }

        Easy.Terminate();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
      this.fileMenu = new System.Windows.Forms.MenuItem();
      this.fileLoadMenu = new System.Windows.Forms.MenuItem();
      this.menuItem2 = new System.Windows.Forms.MenuItem();
      this.fileExitMenu = new System.Windows.Forms.MenuItem();
      this.parametersMenu = new System.Windows.Forms.MenuItem();
      this.detectionTradeoffMenu = new System.Windows.Forms.MenuItem();
      this.tradeOffFavorSpeedItem = new System.Windows.Forms.MenuItem();
      this.tradeOffBalancedItem = new System.Windows.Forms.MenuItem();
      this.tradeOffFavorReliabilityItem = new System.Windows.Forms.MenuItem();
      this.byteInterpretationMenu = new System.Windows.Forms.MenuItem();
      this.byteHexadecimalItem = new System.Windows.Forms.MenuItem();
      this.byteUTF8Item = new System.Windows.Forms.MenuItem();
      this.byteAutoItem = new System.Windows.Forms.MenuItem();
      this.modelsMenu = new System.Windows.Forms.MenuItem();
      this.modelsModel1Item = new System.Windows.Forms.MenuItem();
      this.modelsModel2Item = new System.Windows.Forms.MenuItem();
      this.modelsMicroQRItem = new System.Windows.Forms.MenuItem();
      this.menuItem3 = new System.Windows.Forms.MenuItem();
      this.ApplicationIntroduction = new System.Windows.Forms.MenuItem();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.SuspendLayout();
      //
      // mainMenu
      //
      this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenu,
            this.parametersMenu,
            this.menuItem3});
      //
      // fileMenu
      //
      this.fileMenu.Index = 0;
      this.fileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                            this.fileLoadMenu,
                                            this.menuItem2,
                                            this.fileExitMenu});
      this.fileMenu.Text = "File";
      //
      // fileLoadMenu
      //
      this.fileLoadMenu.Index = 0;
      this.fileLoadMenu.Text = "Load";
      this.fileLoadMenu.Click += new System.EventHandler(this.fileLoadMenu_Click);
      //
      // menuItem2
      //
      this.menuItem2.Index = 1;
      this.menuItem2.Text = "-";
      //
      // fileExitMenu
      //
      this.fileExitMenu.Index = 2;
      this.fileExitMenu.Text = "Exit";
      this.fileExitMenu.Click += new System.EventHandler(this.fileExitMenu_Click);
      //
      // parametersMenu
      // 
      this.parametersMenu.Index = 1;
      this.parametersMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                  this.detectionTradeoffMenu,
                                                  this.byteInterpretationMenu,
                                                  this.modelsMenu});
      this.parametersMenu.Text = "Parameters";
      // 
      // detectionTradeoffMenu
      // 
      this.detectionTradeoffMenu.Index = 0;
      this.detectionTradeoffMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                  this.tradeOffFavorSpeedItem,
                                                  this.tradeOffBalancedItem,
                                                  this.tradeOffFavorReliabilityItem});
      this.detectionTradeoffMenu.Text = "DetectionTradeOff";
      // 
      // tradeOffFavorSpeedItem
      // 
      this.tradeOffFavorSpeedItem.Index = 0;
      this.tradeOffFavorSpeedItem.Text = "FavorSpeed";
      this.tradeOffFavorSpeedItem.Click += new System.EventHandler(this.detectionTradeoffMenu_Click);
      // 
      // tradeOffBalancedItem
      // 
      this.tradeOffBalancedItem.Index = 1;
      this.tradeOffBalancedItem.Text = "Balanced";
      this.tradeOffBalancedItem.Checked = true;
      this.tradeOffBalancedItem.Click += new System.EventHandler(this.detectionTradeoffMenu_Click);
      // 
      // tradeOffFavorReliabilityItem
      // 
      this.tradeOffFavorReliabilityItem.Index = 2;
      this.tradeOffFavorReliabilityItem.Text = "FavorReliability";
      this.tradeOffFavorReliabilityItem.Click += new System.EventHandler(this.detectionTradeoffMenu_Click);

      // 
      // byteInterpretationMenu
      // 
      this.byteInterpretationMenu.Index = 1;
      this.byteInterpretationMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                  this.byteHexadecimalItem,
                                                  this.byteUTF8Item,
                                                  this.byteAutoItem});
      this.byteInterpretationMenu.Text = "Byte Interpretation";
      // 
      // byteHexadecimalItem
      // 
      this.byteHexadecimalItem.Index = 0;
      this.byteHexadecimalItem.Text = "Hexadecimal";
      this.byteHexadecimalItem.Click += new System.EventHandler(this.byteInterpretationMenu_Click);
      // 
      // byteUTF8Item
      // 
      this.byteUTF8Item.Index = 1;
      this.byteUTF8Item.Text = "UTF-8";
      this.byteUTF8Item.Click += new System.EventHandler(this.byteInterpretationMenu_Click);
      // 
      // byteAutoItem
      // 
      this.byteAutoItem.Index = 2;
      this.byteAutoItem.Text = "Auto";
      this.byteUTF8Item.Checked = true;
      this.byteAutoItem.Click += new System.EventHandler(this.byteInterpretationMenu_Click);
      
      // 
      // modelsMenu
      // 
      this.modelsMenu.Index = 2;
      this.modelsMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                    this.modelsModel1Item,
                                                    this.modelsModel2Item,
                                                    this.modelsMicroQRItem});
      this.modelsMenu.Text = "Searched Models";
      // 
      // modelsModel1Item
      // 
      this.modelsModel1Item.Index = 0;
      this.modelsModel1Item.Text = "QR Model 1";
      this.modelsModel1Item.Checked = true;
      this.modelsModel1Item.Click += new System.EventHandler(this.modelsMenu_Click);
      // 
      // modelsModel2Item
      // 
      this.modelsModel2Item.Index = 1;
      this.modelsModel2Item.Text = "QR Model 2";
      this.modelsModel2Item.Checked = true;
      this.modelsModel2Item.Click += new System.EventHandler(this.modelsMenu_Click);
      // 
      // modelsMicroQRItem
      // 
      this.modelsMicroQRItem.Index = 2;
      this.modelsMicroQRItem.Text = "Micro QR";
      this.modelsMicroQRItem.Click += new System.EventHandler(this.modelsMenu_Click);
      // 
      // menuItem3
      //
      this.menuItem3.Index = 2;
      this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                    this.ApplicationIntroduction});
      this.menuItem3.Text = "Help";
      //
      // ApplicationIntroduction
      //
      this.ApplicationIntroduction.Index = 0;
      this.ApplicationIntroduction.Text = "Application Introduction...";
      this.ApplicationIntroduction.Click += new System.EventHandler(this.ApplicationIntroduction_Click);
      //
      // MainForm
      //
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
      this.ClientSize = new System.Drawing.Size(768, 590);
      this.Menu = this.mainMenu;
      this.Name = "MainForm";
      this.Text = "QrcReading";
      this.Closed += new System.EventHandler(this.MainForm_Closed);
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
      this.ResumeLayout(false);

    }
    #endregion

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.Run(new MainForm());
    }


    private void MainForm_Load(object sender, System.EventArgs e)
    {
    }

private void fileLoadMenu_Click(object sender, System.EventArgs e)
    {
      // Show open file dialog
      openFileDialog.Filter = "Image Files (*.tif;*.jpg;*.bmp;*.png)|*.tif;*.jpg;*.bmp;*.png";
      if (openFileDialog.ShowDialog() == DialogResult.Cancel)
      {
        // No image selected, exit
        return;
      }

      try
      {
        // Load the image
        m_Source.Load(openFileDialog.FileName);

        Read();
      }
      catch (EException exc)
      {
        MessageBox.Show(exc.Message);
      }
      // Refresh form
      Refresh();
    }


    private void fileExitMenu_Click(object sender, System.EventArgs e)
    {
      // Close form and exit
      Close();
    }

    private void Read()
    {
      // Has an image been loaded yet ?
      if (m_Source.IsVoid)
        return;
      
      try
      {
        // Reset the flag
        m_bFound = false;

        // Locate and read the QR code
        m_qrCodeReader.SearchField = m_Source;

        m_qrCode = m_qrCodeReader.Read();
        m_bFound = true;

        for (int i = 0; i < m_qrCode.Length; i++)
        {
          m_DecodedText[i] = m_qrCode[i].GetDecodedString(m_byteInterpretationMode);
        }
      }
      catch (EException exc)
      {
        m_DecodedText[0] = "Error while decoding";
        MessageBox.Show(exc.Message);
      }
      // Refresh the form
      Refresh();

    }

    private void DrawBackedText(Graphics g, string text, int x, int y)
    {
      SizeF size = g.MeasureString(text, m_Font);

      g.FillRectangle(m_WhiteBrush, x, y, size.Width, size.Height);
      g.DrawString(text, m_Font, m_BlackBrush, x, y);
    }

    private void detectionTradeoffMenu_Click(object sender, System.EventArgs e)
    {
      this.tradeOffFavorSpeedItem.Checked = false;
      this.tradeOffBalancedItem.Checked = false;
      this.tradeOffFavorReliabilityItem.Checked = false;

      if (sender == this.tradeOffFavorSpeedItem)
      {
        this.tradeOffFavorSpeedItem.Checked = true;
        m_qrCodeReader.DetectionTradeOff = EQRDetectionTradeOff.FavorSpeed;
      }
      else if (sender == this.tradeOffBalancedItem)
      {
        this.tradeOffBalancedItem.Checked = true;
        m_qrCodeReader.DetectionTradeOff = EQRDetectionTradeOff.Balanced;
      }
      else
      {
        this.tradeOffFavorReliabilityItem.Checked = true;
        m_qrCodeReader.DetectionTradeOff = EQRDetectionTradeOff.FavorReliability;
      }
      Read();
    }

    private void byteInterpretationMenu_Click(object sender, System.EventArgs e)
    {
      this.byteAutoItem.Checked = false;
      this.byteHexadecimalItem.Checked = false;
      this.byteUTF8Item.Checked = false;

      if (sender == this.byteAutoItem)
      {
        this.byteAutoItem.Checked = true;
        m_byteInterpretationMode = EByteInterpretationMode.Auto;
      }
      else if (sender == this.byteUTF8Item)
      {
        this.byteUTF8Item.Checked = true;
        m_byteInterpretationMode = EByteInterpretationMode.UTF8;
      }
      else
      {
        this.byteHexadecimalItem.Checked = true;
        m_byteInterpretationMode = EByteInterpretationMode.Hexadecimal;
      }

      m_DecodedText.Initialize();
      try
      {
        for (int i = 0; i < m_qrCode.Length; i++)
        {
          m_DecodedText[i] = m_qrCode[i].GetDecodedString(m_byteInterpretationMode);
        }
      }
      catch (EException exc)
      {
        m_DecodedText[0] = "Error while decoding";
        MessageBox.Show(exc.Message);
      }
      // Refresh the form
      Refresh();
    }

    
    private void modelsMenu_Click(object sender, System.EventArgs e)
    {
      m_models.Clear();
     
      if (sender == this.modelsModel1Item)
      {
        this.modelsModel1Item.Checked = !this.modelsModel1Item.Checked;
      }
      else if (sender == this.modelsModel2Item)
      {
        this.modelsModel2Item.Checked = !this.modelsModel2Item.Checked;
      }
      else if (sender == this.modelsMicroQRItem)
      {
        this.modelsMicroQRItem.Checked = !this.modelsMicroQRItem.Checked;
      }


      if (this.modelsModel1Item.Checked)
      {
        m_models.Add(EQRCodeModel.Model1);
      }
      if (this.modelsModel2Item.Checked)
      {
        m_models.Add(EQRCodeModel.Model2);
      }
      if (this.modelsMicroQRItem.Checked)
      {
        m_models.Add(EQRCodeModel.MicroQR);
      }

      m_qrCodeReader.SearchedModels = m_models.ToArray();
      
      Read();
    }

    private void MainForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
    {
      try
      {
        // Has an image been loaded yet?
        if (m_Source.IsVoid)
          return;

        // Draw the image
        m_Source.Draw(e.Graphics);

        if (!m_bFound)
          return;

        int orgY = 0;
        for (int i = 0; i < m_qrCode.Length; i++)
        {
          orgY = orgY + 25;
          if (m_qrCode[i].UnusedErrorCorrection >= 0)
          {
            // Draw the QR code
            m_qrCode[i].Draw(e.Graphics, 1, 1, 0, 0);
            DrawBackedText(e.Graphics, m_DecodedText[i], 10, orgY);
          }
        }
      }
      catch (EException exc)
      {
        //m_DecodedText = "Error while drawing";
        MessageBox.Show(exc.Message);
      }

    }

    private void MainForm_Closed(object sender, System.EventArgs e)
    {
    }

    private void ApplicationIntroduction_Click(object sender, System.EventArgs e)
    {
      MessageBox.Show(
        @"Load any image from the 'Images\EasyQRCode' folder.
The QR code will be automatically located and decoded.

Use the Parameters tab to change the DetectionTradeOff, the byte interpretation mode and the models to search for.

Required license: EasyQRCode",
        "QrcReading",
        MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
  }
}
