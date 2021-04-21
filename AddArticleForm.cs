using DVGB07_Modul4.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVGB07_Modul4
{
    public partial class AddArticleForm : Form
    {
        private Point textPoint = new Point(100, 6);
        private Point labelPoint = new Point(9, 9);
        private Size textSize = new Size(143, 20);

        private List<TextBox> valueBoxList;
        private List<TextBox> attributeBoxList;
        private List<Label> labelBoxList;

        private string[] labelText = {"ID", "Name", "Price", "Type"};
        private int row;

        private ArticleList orderList;
        private Article newArticle { get; set; }

        public AddArticleForm(ArticleList orderList)
        {
            InitializeComponent();
            this.orderList = orderList;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            valueBoxList = new List<TextBox>();
            attributeBoxList = new List<TextBox>();
            labelBoxList = new List<Label>();


            for (row = 0; row < 4; row++)
            {
                AddTextBox(new Point(textPoint.X, textPoint.Y + (row * 25)));
                AddLabel(new Point(labelPoint.X, labelPoint.Y + (row * 25)));
                labelBoxList.Last().Text = labelText[row];
            }

            AddLabel(new Point(labelPoint.X, labelPoint.Y + (row * 25)));
            labelBoxList.Last().Text = "Attribute";
            labelBoxList.Remove(labelBoxList.Last());

            AddLabel(new Point(textPoint.X - 3, labelPoint.Y + (row * 25)));
            labelBoxList.Last().Text = "Value";
            labelBoxList.Remove(labelBoxList.Last());

            row++;
        }

        private void newFieldButtonClick(object sender, EventArgs e)
        {
            AddTextBox(new Point(labelPoint.X + 3, textPoint.Y + (row * 25)));
            valueBoxList.Last().Size = new Size(82, textSize.Height);
            attributeBoxList.Add(valueBoxList.Last());
            valueBoxList.Remove(valueBoxList.Last());

            AddTextBox(new Point(textPoint.X, textPoint.Y + (row * 25)));

            newFieldButton.Location = new Point(newFieldButton.Location.X, newFieldButton.Location.Y + 25);
            deleteFieldButton.Location = new Point(deleteFieldButton.Location.X, deleteFieldButton.Location.Y + 25);

            deleteFieldButton.Enabled = true;
            row++;
            if (valueBoxList.Count >= 10) 
            {
                newFieldButton.Enabled = false;
            }
        }

        private void deleteFieldButtonClick(object sender, EventArgs e)
        {
            newFieldButton.Location = new Point(newFieldButton.Location.X, newFieldButton.Location.Y - 25);
            deleteFieldButton.Location = new Point(deleteFieldButton.Location.X, deleteFieldButton.Location.Y - 25);
            newFieldButton.Enabled = true;
            
            if (valueBoxList.Count >= 5)
            {
                var attBox = attributeBoxList.Last();
                var valBox = valueBoxList.Last();
                Controls.Remove(attBox);
                Controls.Remove(valBox);
                attributeBoxList.Remove(attBox);
                valueBoxList.Remove(valBox);
                row--;
            }
            if (valueBoxList.Count < 5 )
            {
                deleteFieldButton.Enabled = false;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            newArticle = new Article();

            newArticle.setAttributeValue("id", valueBoxList[0].Text);
            newArticle.setAttributeValue("name", valueBoxList[1].Text);
            newArticle.setAttributeValue("price", valueBoxList[2].Text);
            newArticle.setAttributeValue("type", valueBoxList[3].Text);

            for (int i = 4; i < valueBoxList.Count; i++)
            {
                newArticle.setAttributeValue(attributeBoxList[i - 4].Text, valueBoxList[i].Text);
            }

            newArticle.setAttributeValue("quantity", "0");
            newArticle.setAttributeValue("published", "false");

            if (checkValues())
            {
                orderList.articleList.Add(newArticle);
                Close();
            }
        }

        private bool checkValues()
        {
            for (int i = 0; i < 4; i++)
            {
                if (valueBoxList[i].Text == "")
                {
                    MessageBox.Show($"{labelBoxList[i].Text}-field is empty.");
                    return false;
                }
            }

            int id;
            if (int.TryParse(newArticle.getAttributeValue("id"), out id))
            {
                if (orderList.articleExists(id.ToString()))
                {
                    MessageBox.Show("There already is a product with that ID.");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Unaccepted value for ID. Only accept numbers.");
                return false;
            }

            int price;
            if (int.TryParse(newArticle.getAttributeValue("price"), out price))
            {
                if (price <= 0)
                {
                    MessageBox.Show("Price cant be negative or 0.");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Price can only be numbers.");
                return false;
            }
            return true;
        }

        private void AddTextBox(Point location)
        {
            TextBox tb = new TextBox();
            Controls.Add(tb);
            tb.Location = location;
            tb.Size = textSize;
            tb.Visible = true;
            valueBoxList.Add(tb);
        }

        private void AddLabel(Point location)
        {
            Label label = new Label();
            Controls.Add(label);
            label.Location = location;
            label.AutoSize = true;
            label.Visible = true;
            labelBoxList.Add(label);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    
    }
}
