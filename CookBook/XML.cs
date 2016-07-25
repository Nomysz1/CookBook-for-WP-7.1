using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO.IsolatedStorage;
using System.IO;

namespace CookBook
{
    public class XML
    {
        private const string path = @"recipes.xml";
        private static IEnumerable<XElement> GetParticulartypeNodes(XElement xmlDoc, Canvas cnvs)
        {
                string type = cnvs.Name;
                IEnumerable<XElement> query = (from el in xmlDoc.Elements("recipe")
                                               where el.Element("type").Value == type
                                               select el);
            return query;
        }
        private static void LoadXelementVariable(out XElement xmlDoc)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                    using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream(path, FileMode.Open, isf))
                    {
                        xmlDoc = XElement.Load(isfs);
                    }
            }
        }
        private static void SaveXelementVariable(ref XElement xmlDoc)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream(path, FileMode.Create, isf))
                {
                    xmlDoc.Save(isfs);
                }
            }
        }
        private static bool FindElement(ref string date, ref string title, IEnumerable<XElement> nodes, int index)
        {
            int counter = 0;
            foreach (var element in nodes)
            {
                if (counter++ == index)
                {
                    date = element.Element("date").Value;
                    title = element.Element("title").Value;
                    return true;
                }
            }
            return false;
        }
        //--------------------------------------------------------------------------------------------
        public static void CreateNewXMLFile()
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if(!isf.FileExists(path))
                    using (IsolatedStorageFileStream isfs = isf.CreateFile(path))
                    {
                    
                        XDocument xmlDoc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "no"),
                            new XElement("recipes",
                                new XElement("recipe",
                                new XElement("type", "pizza"),
                                new XElement("date", "2016/05/04"),
                                new XElement("title", "Supreno's pizza"),
                                new XElement("ingredients", "asfasnvvzm"),
                                new XElement("preparation", "alsldas"))));
                        xmlDoc.Save(isfs);
                    }
            }
        }
        public static void SaveSelectedNode(int index, Canvas cnvs, TextBox title_, TextBox ingredients, TextBox preparation)
        {
            XElement xmlDoc;
            LoadXelementVariable(out xmlDoc);
            var nodes = GetParticulartypeNodes(xmlDoc, cnvs);
            string date = "", title="";
            if (FindElement(ref date, ref title, nodes, index))
            {
                var node = (from el in xmlDoc.Elements("recipe")
                            where el.Element("date").Value == date
                            where el.Element("title").Value == title
                            select el).Single();
                node.Element("title").Value = title_.Text;
                node.Element("ingredients").Value = ingredients.Text;
                node.Element("preparation").Value = preparation.Text;
                SaveXelementVariable(ref xmlDoc);
            }
            else
                MessageBox.Show("I did not find the element");
        }
        public static void RemoveNode(int index, Canvas cnvs)
        {
            XElement xmlDoc;
            LoadXelementVariable(out xmlDoc);
            var nodes = GetParticulartypeNodes(xmlDoc, cnvs);
            string date = "", title = "";
            if (FindElement(ref date, ref title, nodes, index))
            {
                var recipe = (from el in xmlDoc.Elements("recipe")
                              where el.Element("date").Value == date
                              where el.Element("title").Value == title
                              select el).Single();
                recipe.Remove();
                SaveXelementVariable(ref xmlDoc);
            }
            else
                MessageBox.Show("I did not find the element");
        }
        public static void ShowDetails(int index, Canvas cnvs, out string title_,out string ingredients,out string preparation)
        {
            title_ = ingredients = preparation = "";
            XElement xmlDoc;
            LoadXelementVariable(out xmlDoc);
            var nodes = GetParticulartypeNodes(xmlDoc, cnvs);
            string date = "", title = "";
            if (FindElement(ref date, ref title, nodes, index))
            {
                var recipe = (from el in xmlDoc.Elements("recipe")
                              where (string)el.Element("date").Value == date
                              where (string)el.Element("title").Value == title
                              select el).Single();
                title_ = recipe.Element("title").Value;
                ingredients = recipe.Element("ingredients").Value;
                preparation = recipe.Element("preparation").Value;
            }
            else
                MessageBox.Show("I did not find the element");
        }
        public static void UpdateList(ListBox lb, Canvas cnvs)
        {
            XElement xmlDoc;
            LoadXelementVariable(out xmlDoc);
            if (lb.Items.Count != 0)
                lb.Items.Clear();
            string type = cnvs.Name;
            var elements = GetParticulartypeNodes(xmlDoc, cnvs);
            foreach (var element in elements)
            {
                string str = String.Format("{0} - {1}", element.Element("date").Value, element.Element("title").Value);
                lb.Items.Add(str);
            }
        }
        public static void AddNode(ref data dat)
        {
            XDocument xmlDoc;
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream(path, FileMode.Open, isf))
                {
                    xmlDoc = XDocument.Load(isfs);
                }
                xmlDoc.Element("recipes").Add(
                    new XElement("recipe",
                        new XElement("type", dat.type),
                        new XElement("date", dat.date),
                        new XElement("title", dat.title),
                        new XElement("ingredients", dat.ingredients),
                        new XElement("preparation", dat.preparation))
                );
                using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream(path, FileMode.Create, isf))
                {
                    xmlDoc.Save(isfs);
                }
            }
        }
    }
}
