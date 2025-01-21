using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMOS_V4
{
    internal static class LoadBiODatabase
    {
        public static void LoadAll(string path)
        {
            string [] subpaths = Directory.GetDirectories(path);
            foreach (string subpath in subpaths)
            {
                string filename = new DirectoryInfo(subpath).Name;

            }
        }
        //public static void Load(string inPath,string outPath) 
        //{
        //    string filename = new DirectoryInfo(inPath).Name;
        //    string[] imagePaths = Directory.GetFiles(inPath);
        //    Lecture lecture = Lecture.Empty;
        //    Dictionary<Dictionary<string,string>, List<string>> Taxonomy = new Dictionary<Dictionary<string, string>, List<string>>();
        //    foreach (string imagePath in imagePaths)
        //    {
        //        if (Taxonomy.Keys.Contains(GetTaxons(imagePath)))
        //            Taxonomy[GetTaxons(imagePath)].Add(imagePath);
        //        else
        //            Taxonomy.Add(GetTaxons(imagePath), new List<string>() { imagePath });
        //    }
        //    foreach (var key in Taxonomy.Keys)
        //    {

        //        QuestionLine questionLine = new QuestionLine(string.Join(" a ", key.Keys), new List<string>(){ string.Join(" ", key.Values) },false, false);
        //        lecture.Questions.Add(new Question("", 5, new List<string>(), new List<QuestionLine>() { questionLine }));
        //        foreach (string imagepath in Taxonomy[key])
        //        {
        //            lecture.Questions[lecture.Questions.Count-1].ImageNames.Add( lecture.LoadImage(imagepath));
        //        }
                
        //    }
        //    AmosFile.CreateZippedData(outPath, lecture.Images, lecture.Questions);
        //}
        public static void Load(string inPath, string outPath)
        {
            string filename = new DirectoryInfo(inPath).Name;
            string[] imagePaths = Directory.GetFiles(inPath);
            Lecture lecture = Lecture.Empty;
            Dictionary<string, Taxonomy> Cards = new Dictionary<string, Taxonomy>();
            foreach (string imagePath in imagePaths)
            {
                string pathWithoutNumbers = imagePath.removeNumbers();
                if (!Cards.Keys.Contains(pathWithoutNumbers))
                    Cards[pathWithoutNumbers] = new Taxonomy(new List<string>(), new Dictionary<string, string>());
                Cards[pathWithoutNumbers].Taxons = GetTaxons(pathWithoutNumbers);
                Cards[pathWithoutNumbers].ImagePaths.Add(imagePath);
            }
            foreach (var value in Cards.Values)
            {

                QuestionLine questionLine = new QuestionLine(string.Join(" a ", value.Taxons.Keys), new List<string>() { string.Join(" ", value.Taxons.Values) }, false, false);
                lecture.Questions.Add(new Question("", 5, new List<string>(), new List<QuestionLine>() { questionLine }));
                foreach (string imagepath in value.ImagePaths)
                {
                    lecture.Questions[lecture.Questions.Count - 1].ImageNames.Add(lecture.LoadImage(imagepath));
                }

            }
            AmosFile.CreateZippedData(outPath, lecture.Images, lecture.Questions);
        }
        public static string LoadImage(this Lecture SelectedLecture, string path)
        {
            string fileName = Path.GetFileNameWithoutExtension(path);
            if (fileName != "")
            {
                if (SelectedLecture.Images.ContainsKey(fileName))
                {
                    int extension = 1;
                    while (SelectedLecture.Images.ContainsKey(fileName + extension)) extension++;
                    fileName += extension;
                }
                SelectedLecture.Images.Add(fileName + ".jpg", resize(new Bitmap(path), 720));
                //Question.ImageNames.Add(Path.GetFileName(fileName + ".jpg"));
                
            }
            return fileName + ".jpg";
        }
        private static Bitmap resize(Bitmap Image, int longestSide)
        {
            Bitmap Outp;
            if (Image.Width > Image.Height)
            {
                Outp = new Bitmap(Image, new Size(longestSide, (int)Math.Round((float)Image.Height * ((float)longestSide / Image.Width))));
            }
            else
            {
                Outp = new Bitmap(Image, new Size((int)Math.Round((float)Image.Width * ((float)longestSide / Image.Height)), longestSide));
            }
            return Outp;
        }
        private static string removeNumbers(this string str)
        {
            char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            foreach (char number in numbers)
            {
                str = str.Replace(number.ToString(),"");
            }
            return str;
        }
        private static Dictionary <string,string> GetTaxons(this string filename)
        {
            string Name = Path.GetFileNameWithoutExtension(filename);
            string[] taxons = Name.Split("_");
            Dictionary<string, string> outp = new Dictionary<string, string>();
            if (char.IsLower(taxons[0],0))
            {
                if (taxons[0] == "" || taxons[1] == "") MessageBox.Show("empty");
                else if (taxons[0].Substring(taxons[0].Length - 3) == "tí" || taxons[0].Substring(taxons[0].Length - 3) == "té") outp.Add("čeleď", taxons[0]);
                else if (taxons[0][taxons[0].Length - 1] == ('y' | 'i')) outp.Add("řád", taxons[0]);
                else if (char.IsUpper(taxons[0], 0)) outp.Add("latinsky", taxons[0]);//improvizované
                else if (char.IsLower(taxons[1], 0))
                {
                    outp.Add("rod", taxons[0]);
                    outp.Add("druh", taxons[1]);
                }
                else
                    outp.Add("rod", taxons[0]);
            }
            return outp;
            //for (int i = 0;  i < taxons.Length; i++)
            //{
            //    if(char.IsUpper(taxons[i], 0))
            //    {
                    
            //    }
            //}
        }
    }
    public class Taxonomy
    {
        public Taxonomy(List<string> imagePaths, Dictionary<string, string> taxons)
        {
            this.imagePaths = imagePaths;
            this.taxons = taxons;
        }
        List<string> imagePaths = new List<string>();
        Dictionary<string,string> taxons = new Dictionary<string,string>();

        public List<string> ImagePaths { get => imagePaths; set => imagePaths = value; }
        public Dictionary<string, string> Taxons { get => taxons; set => taxons = value; }
    }
}
