using DVGB07_Modul4.Classes;
using DVGB07_Modul4.Controls;
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
    public partial class MainForm : Form
    {
        private ArticleList orderList;
        private ArticleList inventoryList;
        private ArticleList storeList;
        private ArticleList cartList;

        private WarehouseControl warehouse;
        private StoreControl store;

        public MainForm()
        {
            InitializeComponent();

            orderList = new ArticleList("order.csv");
            inventoryList = new ArticleList("inventory.csv");
            storeList = new ArticleList("store.csv");
            cartList = new ArticleList("cart.csv");

            //orderList.LoadXMLFile();

            warehouse = new WarehouseControl(orderList, inventoryList, storeList);
            store = new StoreControl(storeList, cartList, inventoryList);

            warehouse.updateOrderListBox();
            warehouse.updateInventoryGridView();
            store.updateStoreGridView();
            store.updateCartGridView();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            warehouse.Dock = DockStyle.Fill;
            WarehouseTab.Controls.Add(warehouse);

            store.Dock = DockStyle.Fill;
            StoreTab.Controls.Add(store);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            warehouse.updateInventoryGridView();
            store.updateStoreGridView();
            store.updateCartGridView();
        }
    }
}
