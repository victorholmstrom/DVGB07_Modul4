﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVGB07_Modul4.Classes
{
    public class ArticleList : ArticleList
    {
        public ArticleList()
        {
            articleList = new List<Article>();
            filePath = "cart.csv";
            LoadFile();
        }
    }
}
