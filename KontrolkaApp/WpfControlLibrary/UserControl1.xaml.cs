using System;
using System.Drawing;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Forms;

public class PlanetFieldControl : UserControl
{
    private Label nameLabel;
    private PictureBox imageBox;
    private Label priceLabel;

    public string PlanetName
    {
        get => nameLabel.Text;
        set => nameLabel.Text = value;
    }

    public Image PlanetImage
    {
        get => imageBox.Image;
        set => imageBox.Image = value;
    }

    public string PriceText
    {
        get => priceLabel.Text;
        set => priceLabel.Text = value;
    }

    public PlanetFieldControl()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        // Nazwa planety (u góry)
        nameLabel = new Label
        {
            Dock = DockStyle.Top,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            Height = 25,
            BackColor = Color.DarkBlue,
            ForeColor = Color.White
        };

        // Obrazek (w œrodku)
        imageBox = new PictureBox
        {
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.Zoom,
            BackColor = Color.Black
        };

        // Cena (na dole)
        priceLabel = new Label
        {
            Dock = DockStyle.Bottom,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 9, FontStyle.Italic),
            Height = 20,
            BackColor = Color.LightGray
        };

        // Dodaj elementy do kontrolki
        Controls.Add(imageBox);
        Controls.Add(priceLabel);
        Controls.Add(nameLabel);

        // Styl kontrolki
        this.BorderStyle = BorderStyle.FixedSingle;
        this.Size = new Size(120, 150);
    }
}
