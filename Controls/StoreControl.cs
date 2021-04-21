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
    public partial class StoreControl : UserControl
    {
        private ArticleList storeList;
        private ArticleList cartList;
        private ArticleList inventoryList;

        private List<Label> labelBoxList;
        private List<TextBox> textBoxList;

        public StoreControl(ArticleList storeList, ArticleList cartList, ArticleList inventoryList)
        {
            InitializeComponent();
            this.storeList = storeList;
            this.cartList = cartList;
            this.inventoryList = inventoryList;

            //Lists of textboxes and labels to display the articles
            labelBoxList = new List<Label>();
            textBoxList = new List<TextBox>();

            addPropGUI();
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
                splitContainer7.Panel1.Controls.Add(tb);
                tb.Location = new Point(textPoint.X, textPoint.Y + (i * 25));
                tb.Size = textSize;
                tb.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                tb.Visible = false;
                textBoxList.Add(tb);

                Label label = new Label();
                splitContainer7.Panel1.Controls.Add(label);
                label.Location = new Point(labelPoint.X, labelPoint.Y + (i * 25));
                label.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                label.AutoSize = true;
                label.Visible = false;
                labelBoxList.Add(label);
            }

        }
        //Method to display a selected article
        public void displayArticle(Article article)
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

        public void updateStoreGridView()
        {
            int index = 0;
            try
            {
                index = storeGridView.CurrentRow.Index;
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            storeGridView.Rows.Clear();
            foreach (var product in storeList.articleList)
            {
                string[] row = new string[] {
                    product.getAttributeValue("id"),
                    product.getAttributeValue("name"),
                    product.getAttributeValue("price"),
                    product.getAttributeValue("type"),
                    product.getAttributeValue("quantity"),
                };
                storeGridView.Rows.Add(row);
            }
            try
            {
                storeGridView.CurrentCell = storeGridView.Rows[index].Cells[0];
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            storeList.writeFile();
        }

        public void updateCartGridView()
        {
            cartGridView.Rows.Clear();
            foreach (var product in cartList.articleList)
            {
                string[] row = new string[] {
                    product.getAttributeValue("id"),
                    product.getAttributeValue("name"),
                    product.getAttributeValue("price"),
                    product.getAttributeValue("type"),
                    product.getAttributeValue("quantity"),
                };
                cartGridView.Rows.Add(row);
            }
            cartList.writeFile();
        }
        
        private void purchaseArticles()
        {
            cartList.articleList.Clear();
            updateCartGridView();
            updateStoreGridView();
        }
        
        private void addToCart(Article article)
        {
            //Checks if the selected articles quantity is more than 0
            //if true then try to add the article to the shopping cart
            if (int.Parse(article.getAttributeValue("quantity")) > 0)
            {
                var inventoryQTY = int.Parse(article.getAttributeValue("quantity"));
                //Checks if the article already exists in the shopping cart,
                //if true then increment its quantity by 1
                if (cartList.articleExists(article.getAttributeValue("id")))
                {
                    var index = cartList.findArticleIndex(article.getAttributeValue("id"));
                    var qty = cartList.articleList[index].getAttributeValue("quantity");
                    cartList.articleList[index].setAttributeValue("quantity", (int.Parse(qty) + 1).ToString());
                }
                //else add the article to the shopping cart with a quantity of 1
                else
                {
                    cartList.addArticle(article);
                    cartList.articleList.Last().setAttributeValue("quantity", "1");
                }
                article.setAttributeValue("quantity", (inventoryQTY - 1).ToString());

                var inventoryIndex = inventoryList.findArticleIndex(article.getAttributeValue("id"));
                inventoryList.articleList[inventoryIndex].setAttributeValue("quantity", ( inventoryQTY - 1).ToString());

            }
            else
            {
                MessageBox.Show("There is no more stock of that item.");
            }
        }
        
        private void removeFromCart(Article article, int index)
        {
            //removes the article from the shopping cart and restore the quantity to the store
            try
            {
                Article storeArticle = storeList.getItemByName(article.getAttributeValue("name"));
                string tempQty = article.getAttributeValue("quantity");
                int Qty = int.Parse(storeArticle.getAttributeValue("quantity")) + int.Parse(tempQty);
                storeArticle.setAttributeValue("quantity", Qty.ToString());

                var inventoryIndex = inventoryList.findArticleIndex(article.getAttributeValue("id"));
                inventoryList.articleList[inventoryIndex].setAttributeValue("quantity", Qty.ToString());

                cartList.articleList.RemoveAt(index);
            }
            catch (NullReferenceException)
            {
                //catch if the article you try to remove from the shopping cart no longer exists in the store
                //try to restore the quantity to the inventory
                Console.WriteLine("The item youre trying to remove from the cart is no longer in the store.");
                try
                {
                    Article inventoryArticle = inventoryList.getItemByName(article.getAttributeValue("name"));
                    string tempQty = article.getAttributeValue("quantity");
                    int Qty = int.Parse(inventoryArticle.getAttributeValue("quantity")) + int.Parse(tempQty);
                    inventoryArticle.setAttributeValue("quantity", Qty.ToString());
                    cartList.articleList.RemoveAt(index);

                }
                //catch again if the same article does not exist in the inventory
                catch (NullReferenceException)
                {
                    Console.WriteLine("The item youre trying to remove from the cart is no longer in the store or inventory.");
                    cartList.articleList.RemoveAt(index);
                }
                
            }
            updateCartGridView();
            updateStoreGridView();
        }

        //===================== Component functions =====================
        private void storeGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedIndex = storeGridView.CurrentRow.Index;
            displayArticle(storeList.articleList.ElementAt(selectedIndex));
        }
        
        private void cartGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedIndex = cartGridView.CurrentRow.Index;
            displayArticle(cartList.articleList.ElementAt(selectedIndex));
        }
        
        private void purchaseButton_Click(object sender, EventArgs e)
        {
            if (cartList.articleList.Count > 0)
            {
                var result = MessageBox.Show("Do you want to buy these items?", "Confirm purchase", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    purchaseArticles();
            }
            else
            {
                MessageBox.Show("You have not added any items to your shopping cart.");
            }
        }

        private void addToCartButton_Click(object sender, EventArgs e)
        {
            if (storeList.articleList.Count > 0)
            {
                var selectedIndex = storeGridView.CurrentRow.Index;
                var tempArticle = storeList.articleList[selectedIndex];

                addToCart(tempArticle);
                updateCartGridView();
                updateStoreGridView();
            }
        }

        private void removeFromCartButton_Click(object sender, EventArgs e)
        {
            if (cartList.articleList.Count > 0)
            {
                var index = cartGridView.CurrentRow.Index;
                removeFromCart(cartList.articleList[index], index);
            }
        }

        



    }
}
