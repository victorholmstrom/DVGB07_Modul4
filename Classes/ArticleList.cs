using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DVGB07_Modul4.Classes
{
    public class ArticleList
    {
        //List of articles
        public List<Article> articleList;
        //path to CSV-file, will be set in MainForm
        public string filePath { get; set; }

        public ArticleList(string filePath)
        {
            articleList = new List<Article>();
            this.filePath = filePath;
            LoadFile();
        }
        //Load information from CRV-file
        public void LoadFile()
        {

            StreamReader reader = new StreamReader(filePath);
            
            while (!reader.EndOfStream)
            {
                Article tempArticle = new Article();
                var line = reader.ReadLine();
                while (line != "")
                {
                    var value = line.Split(';');
                    //Console.WriteLine(value[0].ToString() + "" + value[1].ToString());
                    tempArticle.setAttributeValue(value[0].ToString(), value[1].ToString());
                    line = reader.ReadLine();
                    //Console.WriteLine("Readline = " + line.ToString());
                }
                addArticle(tempArticle);
            }
            reader.Close();
        }
        //Write information to CRV-file
        public void writeFile()
        {
            StringBuilder output = new StringBuilder();

            foreach (var article in articleList)
            {
                foreach (var property in article.attribute)
                {
                    output.AppendLine((property.Key.ToString() + ";" + property.Value.ToString()));
                }
                output.Append("\n");
            }
            try
            {
                File.WriteAllText(this.filePath, output.ToString());
                //Console.WriteLine(output.ToString());
            }
            catch (IOException ex)
            {
                Console.WriteLine(filePath.ToString());
                Console.WriteLine(ex.Message);
            }
        }

        public Article getItemByName(string name)
        {
            foreach (Article article in articleList)
            {
                if (article.getAttributeValue("name") == name)
                {
                    return article;
                }
            }
            return null;
        }

        public bool articleExists(string id)
        {
            foreach (Article article in articleList)
            {
                if (article.getAttributeValue("id") == id)
                    return true;
            }
            return false;
        }
        
        public void addArticle(Article article)
        {
            Article newArticle = new Article();

            foreach (var prop in article.attribute)
            {
                newArticle.setAttributeValue(prop.Key.ToString(), article.getAttributeValue(prop.Key.ToString()));
            }
            articleList.Add(newArticle);
        }

        public int findArticleIndex(string id)
        {
            for (int i = 0; i < articleList.Count; i++)
            {
                if (articleList[i].getAttributeValue("id") == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public void LoadXMLFile()
        {
            XmlDocument document = new XmlDocument();
            document.Load("products.xml");
            var root = document.FirstChild;

            foreach (XmlElement elem in root.ChildNodes)
            {
                var article = new Article();
                article.setAttributeValue("id", articleList.Count.ToString());
                article.setAttributeValue("name", string.Empty);
                article.setAttributeValue("price", string.Empty);
                article.setAttributeValue("type", string.Empty);

                foreach (XmlElement el in elem.ChildNodes)
                {
                    if (el.Name == "id")
                        continue;
                    article.setAttributeValue(el.Name, el.InnerText);
                }
                article.setAttributeValue("quantity", "0");
                article.setAttributeValue("published", "false");
                articleList.Add(article);
            }
        }
    }
}
