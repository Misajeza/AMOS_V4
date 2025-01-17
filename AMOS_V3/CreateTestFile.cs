using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMOS_V4
{
    internal static class CreateTestFile
    {
        public static void Create()
        {
            var images = new Dictionary<string, Bitmap>();
            images.Add("pozadi_plankton.jpg", resize(new Bitmap("C:\\Users\\42073\\Pictures\\pozadí\\pozadi_plankton.png"),1080));

            List <string> image = new List<string> { "pozadi_plankton.jpg" };
            List <string> ans = new List<string> { "ha!" };
            List<QuestionLine> ques = new List<QuestionLine> { new QuestionLine("kdo?", ans, false, false) };
            List<Question> questions = new List<Question>();
            questions.Add(new Question("Test1", 5f, image, ques));

            images.Add("ježkonas.png", resize(new Bitmap("C:\\Users\\42073\\Pictures\\MEMES\\ježkonas.png"), 1080));
            images.Add("buchanka-podyji-pozadi2.jpg", resize(new Bitmap("C:\\Users\\42073\\Pictures\\pozadí\\buchanka-podyji-pozadi2.jpg"),1080));
            images.Add("WIN_20221225_22_16_38_Pro.jpg", resize(new Bitmap("C:\\Users\\42073\\Pictures\\Z fotoaparátu\\WIN_20221225_22_16_38_Pro.jpg"),1080));
            

            List<string> image2 = new List<string> { "ježkonas.png", "buchanka-podyji-pozadi2.jpg" };
            List<string> ans2 = new List<string> { "dabadu","hut" };
            List<QuestionLine> ques2 = new List<QuestionLine> { new QuestionLine("Jabba", ans2, false, false), new QuestionLine("jak?", ans2, false, false) };
            questions.Add(new Question("Test2", 4f, image2, ques2));

            AmosFile.CreateZippedData("TestAmosFile3.amos", images, questions);
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
                Outp = new Bitmap(Image, new Size(Image.Width * (longestSide / Image.Width), longestSide));
            }
            return Outp;
        }
    }
}
