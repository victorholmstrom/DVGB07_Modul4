using DVGB07_Modul4.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVGB07_Modul4.Controls
{
    public partial class WarehouseControl : UserControl
    {
        private ArticleList orderList;
        private ArticleList inventoryList;
        private ArticleList storeList;

        private List<Label> labelBoxList;
        private List<TextBox> textBoxList;

        public WarehouseControl(ArticleList orderList, ArticleList inventoryList, ArticleList storeList)
        {
            InitializeComponent();
            this.orderList = orderList;
            this.inventoryList = inventoryList;
            this.storeList = storeList;
            
            labelBoxList = new List<Label>();
            textBoxList = new List<TextBox>();
            
            addPropGUI();
            updateOrderListBox();
        }

        public void updateOrderListBox()
        {
            orderListBox.Items.Clear();
            foreach (var article in orderList.articleList)
            {
                orderListBox.Items.Add($"{article.getAttributeValue("name")}");
            }
            orderList.writeFile();
        }

        public void updateInventoryGridView()
        {
            inventoryGridView.Rows.Clear();
            foreach (var product in inventoryList.articleList)
            {
                string[] row = new string[] {
                    product.getAttributeValue("id"),  
                    product.getAttributeValue("name"), 
                    product.getAttributeValue("price"), 
                    product.getAttributeValue("type"),
                    product.getAttributeValue("quantity"),
                    product.getAttributeValue("published")
                };
                inventoryGridView.Rows.Add(row);
            }
            inventoryList.writeFile();
        }

        //Method to display a selected article
        private void displayArticle(Article article)
        {
            int i = 0;

            foreach (var property in article.attribute)
            {
                if (i == textBoxList.Count || i == labelBoxList.Count)
                    break;
                labelBoxList[i].Text = property.Key.First().ToString().ToUpper() + property.Key.Substring(1) + ":";
                textBoxList[i].Text = property.Value;
                labelBoxList[i].Visible = true;
                textBoxList[i].Visible = true;
                i++;
            }
            for (int j = i; j < textBoxList.Count; j++)
            {
                labelBoxList[j].Visible = false;
                textBoxList[j].Visible = false;
            }
        }

        //Method to create a number of textboxes and labels for article display
        private void addPropGUI()
        {
            Point textPoint = new Point(68, 3);
            Point labelPoint = new Point(3, 6);
            Size textSize = new Size(137, 20);
            int SIZE = 12;


            for (int i = 0; i < SIZE; i++)
            {
                TextBox tb = new TextBox();
                splitContainer4.Panel1.Controls.Add(tb);
                tb.Location = new Point(textPoint.X, textPoint.Y + (i *25));
                tb.Size = textSize;
                tb.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                tb.Visible = false;
                textBoxList.Add(tb);

                Label label = new Label();
                splitContainer4.Panel1.Controls.Add(label);
                label.Location = new Point(labelPoint.X, labelPoint.Y + (i * 25));
                label.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                label.AutoSize = true;
                label.Visible = false;
                labelBoxList.Add(label);
            }
      
        }

        private void removeFromInventory(Article article)
        {
                if (int.Parse(article.getAttributeValue("quantity")) > 0)
                {
                    var result = MessageBox.Show("You still have stock of this item. Are you sure you want to remove it from the inventory?", "Warning!", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        if (storeList.articleExists(article.getAttributeValue("id")))
                            storeList.articleList.RemoveAt(storeList.findArticleIndex(article.getAttributeValue("id")));

                        inventoryList.articleList.Remove(article);
                        updateInventoryGridView();
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                        radioButton3.Checked = false;
                        quantityNumeric.Value = 0;
                        quantityNumeric.Text = "0";
                    }
                }
                else
                {
                    if (storeList.articleExists(article.getAttributeValue("id")))
                        storeList.articleList.RemoveAt(storeList.findArticleIndex(article.getAttributeValue("id")));

                    inventoryList.articleList.Remove(article);
                    updateInventoryGridView();
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    quantityNumeric.Value = 0;
                    quantityNumeric.Text = "0";
                }
        }

        //===================== Component functions =====================
        private void orderListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Article article = orderList.articleList[orderListBox.SelectedIndex];
                displayArticle(article);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void inventoryGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedIndex = inventoryGridView.CurrentRow.Index;
            displayArticle(inventoryList.articleList.ElementAt(selectedIndex));
        }

        private void AddToInventoryButton_Click(object sender, EventArgs e)
        {
            if (orderListBox.SelectedIndex >= 0)
            {
                var article = orderList.getItemByName((string)orderListBox.SelectedItem);

                if (!inventoryList.articleExists(article.getAttributeValue("id")))
                {
                    if (radioButton1.Checked)
                        article.setAttributeValue("quantity", "1");
                    else if (radioButton2.Checked)
                        article.setAttributeValue("quantity", "5");
                    else if (radioButton3.Checked)
                        article.setAttributeValue("quantity", "10");
                    else 
                        article.setAttributeValue("quantity", quantityNumeric.Value.ToString());
                    inventoryList.articleList.Add(article);
                    updateInventoryGridView();
                }
                radioButton1.Checked = true;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                quantityNumeric.Text = "0";
                quantityNumeric.Value = 0;
            }
        }

        private void RemoveFromInventoryButton_Click(object sender, EventArgs e)
        {
            if (inventoryList.articleList.Count > 0)
            {
                var index = inventoryGridView.CurrentRow.Index;
                var article = inventoryList.articleList[index];
                removeFromInventory(article);
            }
        }

        private void AddOrderArticleButton_Click(object sender, EventArgs e)
        {
            AddArticleForm addArticleForm = new AddArticleForm(orderList);
            
            addArticleForm.ShowDialog();
            updateOrderListBox();
        }

        private void RemoveOrderArticle_Click(object sender, EventArgs e)
        {
            if (orderList.articleList.Count > 0)
            {
                try
                {
                    orderList.articleList.RemoveAt(orderListBox.SelectedIndex);
                    updateOrderListBox();
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void publishButton_Click(object sender, EventArgs e)
        {
            if (inventoryList.articleList.Count > 0)
            {
                var selectedIndex = inventoryGridView.CurrentRow.Index;
                var article = inventoryList.articleList[selectedIndex];

                if (!storeList.articleExists(article.getAttributeValue("id")))
                {
                    article.setAttributeValue("published", "true");
                    storeList.articleList.Add(article);
                    updateInventoryGridView();
                }
                else
                {
                    MessageBox.Show("You have already published this article.");
                }
            }
        }

        private void retractButton_Click(object sender, EventArgs e)
        {
            if (inventoryList.articleList.Count > 0)
            {
                var selectedIndex = inventoryGridView.CurrentRow.Index;
                var article = inventoryList.articleList[selectedIndex];
            
                if (storeList.articleExists(article.getAttributeValue("id")))
                {
                    article.setAttributeValue("published", "false");
                    storeList.articleList.RemoveAt(selectedIndex);
                    updateInventoryGridView();
                }
                else
                {
                    MessageBox.Show("You can not retract an article that is not published.");
                }
            }
        }

        private void orderMoreButton_Click(object sender, EventArgs e)
        {
            if (orderMoreNumeric.Value > 0)
            {
                try
                {
                    var selectedIndex = inventoryGridView.CurrentRow.Index;
                    Article article = inventoryList.articleList.ElementAt(selectedIndex);

                    int quantity = int.Parse(inventoryList.articleList[selectedIndex].getAttributeValue("quantity")) + (int)orderMoreNumeric.Value;
                
                    if (quantity > 100)
                        quantity = 100;
                
                    inventoryList.articleList[selectedIndex].setAttributeValue("quantity", quantity.ToString());
                    updateInventoryGridView();
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            quantityNumeric.Value = 0;
            quantityNumeric.Text = "0";
        }

        private void quantityNumeric_ValueChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
        }







    }
}