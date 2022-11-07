//==========================================
// Варинт задания №5
//
// Оценки по программированию
// 9-ти ( девяти ) студентов группы.
//==========================================

// Задаем программе используемые в данном коде библиотеки
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Lab_5_Comp.gr.Var._5_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            // Инициализация компонентов программы
            InitializeComponent();

            this.Show();

            // Создаем графический контекст формы
            Graphics g = this.CreateGraphics();

            // Выводим подсказку
            g.DrawString(
                "Кликните мышкой по элементу PictureBox",
                new Font("Arial", 10, FontStyle.Regular),
                Brushes.Red,
                0, 0
            );
     
            // Очистка объекта g
            g.Dispose();

        }
     
        // Обработчик события Клика по pictureBox1
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            
            // Очищаем форму
            this.Refresh();
            
            // Создаем графический контест pictureBox1
            Graphics g = pictureBox1.CreateGraphics();
            
            // Задаем координаты точек многоугольника
            Point[] point = new Point[11] {
                new Point(120, 20),
                new Point(180, 30), new Point(240, 20),
                new Point(420, 30), new Point(420, 80),
                new Point(300, 30), new Point(360, 20),
                new Point(360, 90), new Point(300, 80),
                new Point(240, 90), new Point(180, 80),
            };
            
            // Рисуем многоугольник
            g.DrawPolygon(new Pen(Color.Blue, 2), point);
            
            // Задаем фонт и выранивание по центру
            Font fn = new Font("Arial", 10, FontStyle.Bold);

            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Задаем и выводим поясняющую надпись
            string s = "Оценки по программированию 9-ти ( девяти ) студентов";
            g.DrawString(s, fn, Brushes.Red, new Rectangle(125, 20, 300, 70), sf);
            
            // Рисуем рамку по периметру pictureBox1
            g.DrawRectangle(
                new Pen(Color.Blue, 1), 
                0, 0,
                pictureBox1.Width - 1, pictureBox1.Height - 1
            );
            
            // Задаем координаты начала и конца осей x и y
            int startX = 30; int endX = pictureBox1.Width - 10;
            int startY = 120; int endY = pictureBox1.Height - 20;
            
            // Рисуем ось x
            g.DrawLine(new Pen(Color.Black, 1), startX, endY,
            endX, endY);
            
            // Рисуем ось y
            g.DrawLine(new Pen(Color.Black, 1), startX, startY,
            startX, endY);
            
            // Задаем значения учащихся
            string[] year = new string[9] { 
                "1 уч.",
                "2 уч.",
                "3 уч.",
                "4 уч.",
                "5 уч.",
                "6 уч.",
                "7 уч.",
                "8 уч.",
                "9 уч."
            };
            
            // Задаем значения элементов массива баллов
            int[] score = new int[9] { 
                4, 3, 3, 
                5, 2, 1, 
                5, 4, 4 
            };
            
            // Находим максимум в массиве баллов
            int max = -1;
            for (int i = 0; i < score.Length; i++) 
                if (score[i] > max) 
                    max = score[i];
            
            // Задаем масштабный делитель
            double mash = 5.0;
            
            // Определяем количество точек на единицу баллов
            double dy = (endY - startY) / (max / mash);
            
            // Задаем ширину прямоугольников диаграммы
            int widthRect = ((endX - startX) / score.Length) / 2;
            
            // Определяем сплошную закраску
            SolidBrush sb = new SolidBrush(Color.BlueViolet);
            
            // Определяем закраску штриховкой
            HatchBrush hb = new HatchBrush(HatchStyle.BackwardDiagonal,
            Color.CornflowerBlue, Color.Bisque);

            // Определяем закраску изображением
            Image img = Image.FromFile("img.bmp");
            TextureBrush tb = new TextureBrush(img);

            // Задаем начальную координату x
            int x = startX + widthRect;
            
            // Организуем цикл по элементам массива баллов
            for (int i = 0; i < score.Length; i++)
            {
                
                // Задаем прямоугольную область элемента диаграммы
                Rectangle rect = new Rectangle(
                    x, endY - (int)( dy * ( score[i] / mash ) ), 
                    widthRect, (int)( dy * (score[i] / mash ) )
                );
                
                // Выводим закрашенные разными стилями и цветом прямоугольники
                if ( i < 3 )                g.FillRectangle(sb, rect);
                if ( (i >= 3) && (i < 6) )  g.FillRectangle(hb, rect);
                if ( (i >= 6) && (i < 9) )  g.FillRectangle(tb, rect);
               
                // Выводим рамку вокруг прямоугольника
                g.DrawRectangle(new Pen(Color.Black, 1), rect);
                
                // Переходим к отображению следующего элемента
                x += 2 * widthRect;
            
            }

            // Разметка по оси Y
            Pen p = new Pen(Color.Blue, 2)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
            };

            fn = new Font("Arial", 8, FontStyle.Bold);
            
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            
            for (int i = 0; i < score.Length; i++)
            {
                
                // Рисуем штрихпунктирные линии
                g.DrawLine(
                    p, startX - 5, endY - (int)( dy * ( score[i] /mash ) ),
                    endX, endY - (int)( dy * ( score[i] / mash ) )
                );

                // Выводим размеры баллов
                g.DrawString(
                    score[i].ToString(), 
                    fn, Brushes.Black,
                    
                    new Rectangle(1, endY - (int)( dy * ( score[i] / mash ) ) - (int)fn.Size,
                    30, 20),
                    
                    sf
                );

            }

            // Разметка по оси X
            sf.Alignment = StringAlignment.Center;
            x = startX + widthRect + widthRect / 2;
            
            for (int i = 0; i < year.Length; i++)
            {
            
                // Рисуем черточки по оси X
                g.DrawLine(
                    new Pen(Color.Black, 1),
                    x, endY - 5,
                    x, endY + 5);
                
                // Выводим баллы
                g.DrawString(
                    year[i],
                    fn, Brushes.Black,
                    
                    new Rectangle(
                        x - 25, endY,
                        50, 22),
                    
                    sf);
                
                x += 2 * widthRect;
            
            }
        
        } // Конец обработки события клика по PictureBox1
    }
}
