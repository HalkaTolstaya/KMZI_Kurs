using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Курсовая_КМЗИ
{
    public partial class Form1 : Form
    {
        static BigInteger hash;
        public Form1()
        {
            InitializeComponent();
        }

        private void DES_Enter(object sender, EventArgs e)
        {
            tabControl.Size = new System.Drawing.Size(656, 618);
            ClientSize = new System.Drawing.Size(677, 637);
            tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
        }
        private void GOST_Enter(object sender, EventArgs e)
        {
            tabControl.Size = new System.Drawing.Size(656, 540);
            ClientSize = new System.Drawing.Size(677, 560);
            tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
        }
        private void RSA_Enter(object sender, EventArgs e)
        {
            tabControl.Size = new System.Drawing.Size(656, 392);
            ClientSize = new System.Drawing.Size(677, 415);
            tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
        }
        private void HASH_Enter(object sender, EventArgs e)
        {
            tabControl.Size = new System.Drawing.Size(404, 420);
            ClientSize = new System.Drawing.Size(424, 440);
            tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Normal;
        }
        private void digital_signature_Enter(object sender, EventArgs e)
        {
            tabControl.Size = new System.Drawing.Size(445, 443);
            ClientSize = new System.Drawing.Size(464, 462);
            tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Normal;
        }

        private void DES_Leave(object sender, EventArgs e)
        {
            clear_1();
            txt_source_1.Text = "";
            txt_first_subkey_1.Text = "";
        }
        private void GOST_Leave(object sender, EventArgs e)
        {
            clear_2();
            txt_source_2.Text = "";
            txt_first_subkey_2.Text = "";
        }
        private void RSA_Leave(object sender, EventArgs e)
        {
            clear_3();
            txt_p_3.Text = "";
            txt_q_3.Text = "";
        }
        private void HASH_Leave(object sender, EventArgs e)
        {
            clear_4();
            txt_p_4.Text = "";
            txt_q_4.Text = "";
            txt_H0_4.Text = "";
            txt_n_4.Text = "";
            txt_message_4.Text = "";
        }
        private void digital_signature_Leave(object sender, EventArgs e)
        {
            clear_5();
            txt_p_5.Text = "";
            txt_q_5.Text = "";
        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///////                 Глобальные переменные для всех задач                ///////
        ///////////////////////////////////////////////////////////////////////////////////

        ///////          Задача №1         ///////
        string str_1;
        private Dictionary<int, int[]> S_BOX1 = new Dictionary<int, int[]>(4) {
            {0, new int [] {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7} },
            {1, new int [] {0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8} },
            {2, new int [] {4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0} },
            {3, new int [] {15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13} },
        };
        private Dictionary<int, int[]> S_BOX2 = new Dictionary<int, int[]>(4) {
            {0, new int [] {15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10} },
            {1, new int [] {3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5} },
            {2, new int [] {0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15} },
            {3, new int [] {13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9} },
        };
        private Dictionary<int, int[]> S_BOX3 = new Dictionary<int, int[]>(4) {
            {0, new int [] {10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8} },
            {1, new int [] {13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1} },
            {2, new int [] {13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7} },
            {3, new int [] {1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12} },
        };
        private Dictionary<int, int[]> S_BOX4 = new Dictionary<int, int[]>(4) {
            {0, new int [] {7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15} },
            {1, new int [] {13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9} },
            {2, new int [] {10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4} },
            {3, new int [] {3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14} },
        };
        private Dictionary<int, int[]> S_BOX5 = new Dictionary<int, int[]>(4) {
            {0, new int [] {2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9} },
            {1, new int [] {14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6} },
            {2, new int [] {4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14} },
            {3, new int [] {11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3} },
        };
        private Dictionary<int, int[]> S_BOX6 = new Dictionary<int, int[]>(4) {
            {0, new int [] {12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11} },
            {1, new int [] {10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8} },
            {2, new int [] {9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6} },
            {3, new int [] {4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13} },
        };
        private Dictionary<int, int[]> S_BOX7 = new Dictionary<int, int[]>(4) {
            {0, new int [] {4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1} },
            {1, new int [] {13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6} },
            {2, new int [] {1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2} },
            {3, new int [] {6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12} },
        };
        private Dictionary<int, int[]> S_BOX8 = new Dictionary<int, int[]>(4) {
            {0, new int [] {13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7} },
            {1, new int [] {1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2} },
            {2, new int [] {7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8} },
            {3, new int [] {2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11} },
        };

        ///////          Задача №2         ///////
        string str_2 = "";
        private byte[,] table_2 = new byte[,] {
            /*         8   7   6   5   4   3   2   1  */
            /* 0 */  { 1,  13, 4,  6,  7,  5,  14, 4  }, 
            /* 1 */  { 15, 11, 11, 12, 13, 8,  11, 10 },
            /* 2 */  { 13, 4,  10, 7,  10, 1,  4,  9  },
            /* 3 */  { 0,  1,  0,  1,  1,  13, 12, 2  },
            /* 4 */  { 5,  3,  7,  5,  0,  10, 6,  13 },
            /* 5 */  { 7,  15, 2,  15, 8,  3,  13, 8  },
            /* 6 */  { 10, 5,  1,  13, 9,  4,  15, 0  },
            /* 7 */  { 4,  9,  13, 8,  15, 2,  10, 14 },
            /* 8 */  { 9,  0,  3,  4,  14, 14, 2,  6  },
            /* 9 */  { 2,  10, 6,  10, 4,  15, 3,  11 },
            /* 10 */ { 3,  14, 8,  9,  6,  12, 8,  1  },
            /* 11 */ { 14, 7,  5,  14, 12, 7,  1,  12 },
            /* 12 */ { 6,  6,  9,  0,  11, 6,  0,  7  },
            /* 13 */ { 11, 8,  12, 3,  2,  0,  7,  15 },
            /* 14 */ { 8,  2,  15, 11, 5,  9,  5,  5  },
            /* 15 */ { 12, 12, 14, 2,  3,  11, 9,  3  },
        };

        ///////          Задача №3         ///////
        int p_3, q_3, n_3, m_3, e_3, d_3; //ключи
        bool prime_3; //флаг простого числа

        ///////          Задача №4         ///////
        int p_4, q_4, n_4;
        bool prime_4; //флаг простого числа

        ///////          Задача №5         ///////
        int p_5, q_5, n_5, m_5, e_5, d_5;
        BigInteger r_5, S_5, R_5;
        bool prime_5; //флаг простого числа

        ///////////////////////////////////////////////////////////////////////////////////
        ///////                      Различные полезные функции                     ///////
        ///////////////////////////////////////////////////////////////////////////////////

        void Display_text_in_TextBox (byte[] B, TextBox txtBox, int n)
        {
            int i = 1; // Счётчик
            foreach (byte temp in B)
            {
                if (n != -1)
                { // -1 означает перевод в двоичную систему
                    txtBox.Text += Convert.ToString(temp); // Выводим бит
                    if (i % n == 0) // Если это 8-мой бит
                        txtBox.Text += " "; // Выводим пробел
                }
                else // Если n = -1, то перевести в двоичную систему
                    txtBox.Text += Convert.ToInt32(Convert.ToString(temp, 2)).ToString("00000000") + " ";
                i++; // Увеличиваем счётчик
            }
        }

        // Проверка p и q
        bool Check_Prime_Number(int p, int q, bool prost, TextBox txt_p, TextBox txt_q)
        {
            if (txt_p.Text == "")
                MessageBox.Show("Для продолжения необходимо ввести число p!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (txt_q.Text == "")
                MessageBox.Show("Для продолжения необходимо ввести число q!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (txt_p.Text == txt_q.Text)
            {
                MessageBox.Show("p и q не должны быть равны друг другу!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_q.Text = "";
            }
            else
            {
                p = Convert.ToInt32(txt_p.Text);
                q = Convert.ToInt32(txt_q.Text);
                prost = Prime(p); //  Проверка числа p
                if (prost == false)
                {
                    MessageBox.Show("Введенное число p не простое.\nВведите другое число p!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_p.Text = "";
                }
                else
                {
                    prost = Prime(q); // Проверка числа q
                    if (prost == false)
                    {
                        MessageBox.Show("Введенное число q не простое.\nВведите другое число q!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_q.Text = "";
                    }
                    else
                        return true;
                }
            }
            return false;
        }

        // Проверка является ли число простым
        bool Prime(int a)
        {
            if (a < 2)
                return false;
            if (a == 2)
                return true;
            for (int i = 2; i <= a / 2; i++)
            { // Нахождение делителей числа
                if (a % i == 0)
                {
                    return false; // число не является простым
                }
            }
            return true; // Иначе число простое
        }

        // Функция ищет НОД двух чисел
        int NOD(int a, int b)
        {
            return b == 0 ? a : NOD(b, a % b);
        }

        // Защита  для символов
        void symbol_protection (KeyPressEventArgs e)
        {
            if ((e.KeyChar < 1040 || e.KeyChar > 1103) && e.KeyChar != 8 && e.KeyChar != ' ') //можно вводить только русские буквы и backspace
                e.Handled = true;
        }

        // Защита  для чисел
        void number_protection (KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8) //можно вводить только цифры и backspace
                e.Handled = true;
        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///////                  Задача №1: Алгоритм шифрования DES                 ///////
        ///////////////////////////////////////////////////////////////////////////////////

        // Событие нажатиия кнопки Выполнить первый цикл алгоритма
        private void btn_run_1_Click(object sender, EventArgs e)
        {
            clear_1();
            if (txt_source_1.TextLength != 8) // Если исходный текст не введен или введен неверно
                MessageBox.Show("Поле с ФИО (исходным текстом) должно содержать 8 букв!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (txt_first_subkey_1.TextLength != 8) // Если первый подключ не введен или введен неверно
                MessageBox.Show("Необходимо ввести первые 8 букв отчества в поле подключа!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                // Перевод исходного текста в двоичную систему (по 8 бит)
                byte[] L0R0 = Encoding.GetEncoding(1251).GetBytes(txt_source_1.Text); // Перевод символов в числа
                Display_text_in_TextBox(L0R0, txt_source_binary_1, -1); // Вывод в текстбокс

                int i;
                // Получение исходного текста в двоичной форме (64 бита)
                str_1 = txt_source_binary_1.Text.Replace(" ", ""); // Считывание строки без пробелов
                L0R0 = new byte[str_1.Length]; // Задание длины (64 бита)
                //перевод в двоичку
                for (i = 0; i < str_1.Length; i++) // Пока строка не закончилась
                    L0R0[i] = Convert.ToByte(str_1[i].ToString()); // Заполнение массива битами

                byte[] L0 = new byte[32]; // Задание длины (32 бита)
                byte[] R0 = new byte[32]; // Задание длины (32 бита)

                // Для перестановки
                int[] Permutation = new int[] {
                    58, 50, 42, 34, 26, 18, 10, 2,
                    60, 52, 44, 36, 28, 20, 12, 4,
                    62, 54, 46, 38, 30, 22, 14, 6,
                    64, 56, 48, 40, 32, 24, 16, 8,
                    57, 49, 41, 33, 25, 17, 9, 1,
                    59, 51, 43, 35, 27, 19, 11, 3,
                    61, 53, 45, 37, 29, 21, 13, 5,
                    63, 55, 47, 39, 31, 23, 15, 7,
                };

                // Перестановка
                i = 0;
                txt_R0_32_1.Text = " ";
                foreach (byte temp in Permutation)
                {
                    if (i < 32)
                    {
                        L0[i] = L0R0[temp - 1];
                        if (i % 4 == 0 && i != 0)
                            txt_L0_1.Text += " ";
                        txt_L0_1.Text += L0[i];
                    }
                    else
                    {
                        // Разбиение R0 по 4 бита
                        R0[i - 32] = L0R0[temp - 1];
                        if (i % 4 == 0 && i != 32)
                            txt_R0_32_1.Text += "  ";
                        txt_R0_32_1.Text += R0[i - 32];
                    }
                    i++;
                }
                txt_R0_32_1.Text += " ";
                byte[] TrueR0 = new byte[32];
                R0.CopyTo(TrueR0, 0);
                // Получение R0 для расширения
                str_1 = txt_R0_32_1.Text; // Считывание строки
                R0 = new byte[48]; // Задание длины (48 бит)
                for (i = 0; i < 48; i++)
                { // Пока строка не закончилась
                    if (str_1[i] == ' ')
                        R0[i] = 9;
                    else
                        R0[i] = Convert.ToByte(str_1[i].ToString()); // Заполнение массива R0 битами
                }

                // Расширение R0 до 48 бит
                R0[0] = R0[46];
                for (i = 0; i < 47; i++)
                {
                    if (R0[i] == 9)
                    {
                        R0[i] = R0[i + 2]; // Добавление битов в конце
                        R0[i + 1] = R0[i - 1]; // Добавление битов в начале
                    }
                    txt_R0_48_1.Text += "" + R0[i];
                }
                R0[47] = R0[1];
                txt_R0_48_1.Text += "" + R0[47];

                // Перевод первого подключа в двоичную форму (по 8 бит)
                byte[] X0 = Encoding.GetEncoding(1251).GetBytes(txt_first_subkey_1.Text); // Перевод символов в числа
                Display_text_in_TextBox(X0, txt_X0_56_1, -1); // Вывод в текстбокс
                txt_X0_56_1.Text = txt_X0_56_1.Text.Remove(txt_X0_56_1.Text.Length - 1);




                // Уменьшение X0 до 48 бит
                str_1 = txt_X0_56_1.Text; // Считывание строки
                str_1 = str_1.Replace(" ", "");
                List<byte> Temp = new List<byte>(); 
                for (i = 0; i < str_1.Length - 1; i++)
                {
                    if (i % 8 != 7)
                        Temp.Add(Convert.ToByte(str_1[i].ToString()));

                }
                
                List<byte> Temp2 = new List<byte>();
                for (i = 0; i < Temp.Count - 2; i++)
                {
                    if (i % 8 != 7) 
                        Temp2.Add(Convert.ToByte(Temp[i].ToString()));

                }
                txt_X0_56_1.Text = "";
                Display_text_in_TextBox(Temp.ToArray(), txt_X0_56_1, 7); // Вывод в текстбокс
                Display_text_in_TextBox(Temp2.ToArray(), txt_X0_48_1, 6); // Вывод в текстбокс
                X0 = Temp2.ToArray();

                // Сложение (побитово)
                byte[] R0X0 = new byte[48]; // Задание длины (32 бита)
                for (i = 47; i > -1; i--)//XOR
                {
                    R0X0[i] = (byte)(R0[i] ^ X0[i]);
                    
                }
                // Разбитие по 6 бит
                Display_text_in_TextBox(R0X0, txt_sum_1, 6); // Вывод в текстбокс

                // Подстановка
                txt_podstanovka_1.Text += Convert.ToInt32(Convert.ToString(S_BOX1[R0X0[0] * 2 + R0X0[5]][R0X0[1] * 8 + R0X0[2] * 4 + R0X0[3] * 2 + R0X0[4]], 2)).ToString("0000") + " ";
                txt_podstanovka_1.Text += Convert.ToInt32(Convert.ToString(S_BOX2[R0X0[6] * 2 + R0X0[11]][R0X0[7] * 8 + R0X0[8] * 4 + R0X0[9] * 2 + R0X0[10]], 2)).ToString("0000") + " ";
                txt_podstanovka_1.Text += Convert.ToInt32(Convert.ToString(S_BOX3[R0X0[12] * 2 + R0X0[17]][R0X0[13] * 8 + R0X0[14] * 4 + R0X0[15] * 2 + R0X0[16]], 2)).ToString("0000") + " ";
                txt_podstanovka_1.Text += Convert.ToInt32(Convert.ToString(S_BOX4[R0X0[18] * 2 + R0X0[23]][R0X0[19] * 8 + R0X0[20] * 4 + R0X0[21] * 2 + R0X0[22]], 2)).ToString("0000") + " ";
                txt_podstanovka_1.Text += Convert.ToInt32(Convert.ToString(S_BOX5[R0X0[24] * 2 + R0X0[29]][R0X0[25] * 8 + R0X0[26] * 4 + R0X0[27] * 2 + R0X0[28]], 2)).ToString("0000") + " ";
                txt_podstanovka_1.Text += Convert.ToInt32(Convert.ToString(S_BOX6[R0X0[30] * 2 + R0X0[35]][R0X0[31] * 8 + R0X0[32] * 4 + R0X0[33] * 2 + R0X0[34]], 2)).ToString("0000") + " ";
                txt_podstanovka_1.Text += Convert.ToInt32(Convert.ToString(S_BOX7[R0X0[36] * 2 + R0X0[41]][R0X0[37] * 8 + R0X0[38] * 4 + R0X0[39] * 2 + R0X0[40]], 2)).ToString("0000") + " ";
                txt_podstanovka_1.Text += Convert.ToInt32(Convert.ToString(S_BOX8[R0X0[42] * 2 + R0X0[47]][R0X0[43] * 8 + R0X0[44] * 4 + R0X0[45] * 2 + R0X0[46]], 2)).ToString("0000") + "";

                // Записать результат подстановки
                str_1 = txt_podstanovka_1.Text.Replace(" ", ""); // Считывание строки без пробелов
                L0R0 = new byte[str_1.Length]; // Задание длины
                for (i = 0; i < str_1.Length; i++) // Пока строка не закончилась
                    L0R0[i] = Convert.ToByte(str_1[i].ToString()); // Заполнение массива битами

                // Для перестановки
                Permutation = new int[] {
                    16, 7, 20, 21,
                    29, 12, 28, 17,
                    1, 15, 23, 26,
                    5, 18, 31, 10,
                    2, 8, 24, 14,
                    32, 27, 3, 9,
                    19, 13, 30, 6,
                    22, 11, 4, 25,
                };

                // Перестановка
                i = 0;
                R0X0 = new byte[32];
                foreach (byte temp in Permutation)
                {
                    R0X0[i] = L0R0[temp - 1];
                    if (i % 4 == 0 && i != 0)
                        txt_perestanovka_1_1.Text += " ";
                    txt_perestanovka_1_1.Text += R0X0[i];
                    i++;
                }
                byte[] TrueR02 = new byte[32];
                R0X0.CopyTo(TrueR02, 0);
                // Объединение строк
                // Сложение (побитово)
                byte[] R1 = new byte[32]; // Задание длины (32 бита)
                for (i = 31; i > -1; i--)//XOR
                {
                    R1[i] = (byte)(TrueR02[i] ^ L0[i]);

                }
                Display_text_in_TextBox(TrueR0, txt_obyedinenie_1, 8); // Вывод в текстбокс
                Display_text_in_TextBox(R1, txt_obyedinenie_1, 8); // Вывод в текстбокс

                // Запись объединенной строки
                str_1 = txt_obyedinenie_1.Text.Replace(" ", ""); // Считывание строки без пробелов
                L0R0 = new byte[str_1.Length]; // Задание длины
                for (i = 0; i < str_1.Length; i++) // Пока строка не закончилась
                    L0R0[i] = Convert.ToByte(str_1[i].ToString()); // Заполнение массива битами

                // Для перестановки
                Permutation = new int[] {
                    40, 8, 48, 16, 56, 24, 64, 32,
                    39, 7, 47, 15, 55, 23, 63, 31,
                    38, 6, 46, 14, 54, 22, 62, 30,
                    37, 5, 45, 13, 53, 21, 61, 29,
                    36, 4, 44, 12, 52, 20, 60, 28,
                    35, 3, 43, 11, 51, 19, 59, 27,
                    34, 2, 42, 10, 50, 18, 58, 26,
                    33, 1, 41, 9, 49, 17, 57, 25,
                };

                // Перестановка
                i = 0;
                R0X0 = new byte[64];
                foreach (byte temp in Permutation)
                {
                    R0X0[i] = L0R0[temp - 1];
                    if (i % 8 == 0 && i != 0)
                        txt_perestanovka_2_1.Text += " ";
                    txt_perestanovka_2_1.Text += R0X0[i];
                    i++;
                }
            }
        }

        void clear_1 ()
        {
            txt_source_binary_1.Text = "";
            txt_L0_1.Text = "";
            txt_R0_32_1.Text = "";
            txt_R0_48_1.Text = "";
            txt_X0_56_1.Text = "";
            txt_X0_48_1.Text = "";
            txt_sum_1.Text = "";
            txt_podstanovka_1.Text = "";
            txt_perestanovka_1_1.Text = "";
            txt_obyedinenie_1.Text = "";
            txt_perestanovka_2_1.Text = "";
        }

        // Защита 
        private void txt_source_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            symbol_protection(e);
        }

        private void txt_first_subkey_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            symbol_protection(e);
        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///////             Задача №2: Алгоритм шифрования ГОСТ 28147-89            ///////
        ///////////////////////////////////////////////////////////////////////////////////

        // Событие нажатия кнопки "Выполнить первый цикл алгоритма"
        private void btn_run_2_Click(object sender, EventArgs e)
        {
            clear_2();

            // Проверка входных данных
            if (txt_source_2.Text == "" || txt_source_2.TextLength != 8)
                MessageBox.Show(this, "Поле с ФИО (исходным текстом) должно содержать 8 букв!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (txt_first_subkey_2.Text == "" || txt_first_subkey_2.TextLength != 4)
                MessageBox.Show(this, "Необходимо ввести первые 4 буквы отчества в поле подключа!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                // Преобразование входный данных в двоичный вид и вывести в TextBox'ы
                // Для первых 4 символов
                str_2 = "";
                for (int i = 0; i < 4; i++)
                    str_2 += Convert.ToString(txt_source_2.Text[i]);
                byte[] L0 = Encoding.GetEncoding(1251).GetBytes(str_2);
                foreach (byte temp in L0)
                    txt_L0_2.Text += Convert.ToInt32(Convert.ToString(temp, 2)).ToString("00000000") + " ";

                // Для вторых 4 символов
                str_2 = "";
                for (int i = 4; i < 8; i++)
                    str_2 += Convert.ToString(txt_source_2.Text[i]);
                byte[] R0 = Encoding.GetEncoding(1251).GetBytes(str_2);                
                foreach (byte temp in R0)
                    txt_R0_2.Text += Convert.ToInt32(Convert.ToString(temp, 2)).ToString("00000000") + " ";

                // Вывести в TextBox подключ
                byte [] X0 = Encoding.GetEncoding(1251).GetBytes(txt_first_subkey_2.Text);
                foreach (byte temp in X0)
                    txt_X0_2.Text += Convert.ToString(temp, 2) + " ";

                // Удалить пробел в конце строки у L0, R0 и X0
                txt_L0_2.Text = Regex.Replace(txt_L0_2.Text, @"\s$", "");
                txt_R0_2.Text = Regex.Replace(txt_R0_2.Text, @"\s$", "");
                txt_X0_2.Text = Regex.Replace(txt_X0_2.Text, @"\s$", "");

                // Убрать пробелы из TextBox L0 и преобразовать в массив
                str_2 = txt_L0_2.Text.Replace(" ", "");
                L0 = new byte[str_2.Length];
                for (int i = 0; i < str_2.Length; i++)
                    L0[i] = Convert.ToByte(str_2[i].ToString());

                // Убрать пробелы из TextBox R0 и преобразовать в массив
                str_2 = txt_R0_2.Text.Replace(" ", "");
                R0 = new byte[str_2.Length];
                for (int i = 0; i < str_2.Length; i++)
                    R0[i] = Convert.ToByte(str_2[i].ToString());

                // Убрать пробелы из TextBox X0 и преобразовать в массив
                str_2 = txt_X0_2.Text.Replace(" ", "");
                X0 = new byte[str_2.Length];
                for (int i = 0; i < str_2.Length; i++)
                    X0[i] = Convert.ToByte(str_2[i].ToString());

                // Сложение R0 и X0 по модулю 2^32
                byte transfer = 0;
                byte[] R0_X0 = new byte[32];
                for (int i = 31; i >= 0; i--)
                {
                    if (R0[i] + X0[i] + transfer == 3)
                    {
                        R0_X0[i] = 1;
                        transfer = 1;
                    }
                    else if (R0[i] + X0[i] + transfer == 2)
                    {
                        R0_X0[i] = 0;
                        transfer = 1;
                    }
                    else if (R0[i] + X0[i] + transfer == 1)
                    {
                        R0_X0[i] = 1;
                        transfer = 0;
                    }
                    else if (R0[i] + X0[i] + transfer == 0)
                    {
                        R0_X0[i] = 0;
                        transfer = 0;
                    }

                }

                // Вывод R0 + X0 в TextBox
                for (int i = 0; i < R0_X0.Length; i++)
                {
                    txt_sum_2.Text += Convert.ToString(R0_X0[i]);
                    if ((i + 1) % 8 == 0)
                        txt_sum_2.Text += " ";
                }

                int DEC; // Строка в Блоке
                int count = 0; // Номер столбца в блоке
                byte[] L0_R0 = new byte[32];
                for (int i = 0; i < 29; i += 4, count++)
                {
                    DEC = R0_X0[i] * 8 + R0_X0[i + 1] * 4 + R0_X0[i + 2] * 2 + R0_X0[i + 3]; // Перевод в 10-ную систему
                    L0_R0[count] = table_2[DEC, count]; // Подстановка значений из Блока
                    if (i != 0 && i % 4 == 0 && count % 2 != 0) // Если 2 числа заменено на значение из Блока
                        // Перевод в 2-ную систему (вывод 4 + 4 бита)
                        txt_permutation_2.Text += Convert.ToInt32(Convert.ToString(L0_R0[count - 1], 2)).ToString("0000") + Convert.ToInt32(Convert.ToString(L0_R0[count], 2)).ToString("0000") + " ";
                }

                str_2 = txt_permutation_2.Text.Replace(" ", "");
                R0_X0 = new byte[str_2.Length];
                for (int i = 0; i < str_2.Length; i++)
                    R0_X0[i] = Convert.ToByte(str_2[i].ToString());

                byte[] func_R0_X0 = new byte[32]; // Задание длины (32 бита)
                for (int i = 0; i < 21; i++) // Первые 21 бит
                    func_R0_X0[i] = R0_X0[i + 11]; // Сдвиг влево на 11 бит
                for (int i = 21; i < 32; i++) // Оставшиеся 11 бит
                    func_R0_X0[i] = R0_X0[i - 21]; // Сдвиг влево на 11 бит
                for (int i = 0; i < func_R0_X0.Length; i++)
                {
                    txt_func_2.Text += Convert.ToString(func_R0_X0[i]);
                    if ((i + 1) % 8 == 0)
                        txt_func_2.Text += " ";
                }

                byte[] R1 = new byte[32]; // Задание длины (32 бита)
                for (int i = 31; i > -1; i--)
                { // Пока сложение по mod 2 не закончилось
                    if (L0[i] + func_R0_X0[i] == 2) // Если 1 + 1
                        R1[i] = 0; // Сумма
                    else if (L0[i] + func_R0_X0[i] == 1) // Если 0 + 0 или 1 + 0
                        R1[i] = 1; // Сумма
                    else if (L0[i] + func_R0_X0[i] == 0)
                        R1[i] = 0;
                }
                for (int i = 0; i < R1.Length; i++)
                {
                    txt_R1_2.Text += Convert.ToString(R1[i]);
                    if ((i + 1) % 8 == 0)
                        txt_R1_2.Text += " ";
                }
                txt_R0_R1_2.Text = txt_R0_2.Text + " " + txt_R1_2.Text;
            }
        }

        void clear_2()
        {
            txt_source_binary_1.Text = "";
            txt_L0_2.Text = "";
            txt_R0_2.Text = "";
            txt_X0_2.Text = "";
            txt_sum_2.Text = "";
            txt_permutation_2.Text = "";
            txt_func_2.Text = "";
            txt_R1_2.Text = "";
        }

        // Защита 
        private void txt_source_2_KeyPress(object sender, KeyPressEventArgs e)
        {
            symbol_protection(e);
        }

        private void txt_first_subkey_2_KeyPress(object sender, KeyPressEventArgs e)
        {
            symbol_protection(e);
        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///////                  Задача №3: Алгоритм шифрования RSA                 ///////
        ///////////////////////////////////////////////////////////////////////////////////
        //


       static char[] alphabet = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
        static string[] numbers = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33" };
        //фио в цифры
       static  BigInteger textincode(string a)
        {
            string RES="";
            for (int i = 0; i < a.Length; i++)
            {
                for (int ii = 0; ii < 33; ii++)
                {
                    if (a[i] == alphabet[ii]) RES += numbers[ii];

                }
            }
            return BigInteger.Parse(RES);
        }
        // цифры в фио 22 01 15
       static string codeintext(int dec)
        {

            string a = dec.ToString();
            string res = "";

            for (int i = 0; i < a.Length; i += 2)
            {
                for (int ii = 0; ii < 33; ii++)
                {
                    int a_1_int = int.Parse(a[i].ToString());
                    int a_2_int = int.Parse(a[i + 1].ToString());
                    int first_num = int.Parse(numbers[ii]) / 10;
                    int second_num = int.Parse(numbers[ii]) % 10;
                    if (a_1_int == first_num && a_2_int == second_num)
                    {
                        res += alphabet[ii];
                    }
                }
            }
            return res;
        }
            //////////////////////////////////

            // Вычисление ключей
            private void btn_gen_keys_3_Click(object sender, EventArgs e)
        {
            // Очистка ключей
            clear_3();

            if (Check_Prime_Number(p_3, q_3, prime_3, txt_p_3, txt_q_3) == true)
            { // Проверка p и q
                p_3 = Convert.ToInt32(txt_p_3.Text); // Ввод числа p
                q_3 = Convert.ToInt32(txt_q_3.Text); // Ввод числа q
                n_3 = p_3 * q_3; // Вычисление n
                m_3 = (p_3 - 1) * (q_3 - 1); // Вычисление m
                // Вычисление d
                d_3 = -1;
                for (int i = 2; d_3 < 0; i++)
                { // Пока минимальное подходящее число d не найдено
                    if (NOD(i, m_3) == 1 && i < m_3 && Prime(i) == true) // Если число взаимно простое с m, меньше m и простое
                        d_3 = i; //число d найдено
                    else if (i > m_3)
                    { // Если число больше m
                        MessageBox.Show("Число m маленькое, невозможно вычислить число d!\r\nВведите другие числа p и q!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_p_3.Text = "";
                        txt_q_3.Text = "";
                        goto Finish; // Закончить вычисление ключей
                    }
                }
                // Вычисление e
                e_3 = 1;
                while (true)
                { // Пока число не будет найдено
                    if ((d_3 * e_3) % m_3 == 1) // Если число удовлетворяет условию
                        break; // Закончить поиск
                    else
                        e_3++; // Следующее число
                }
                txt_n_3.Text = "" + n_3; // Вывод n
                txt_m_3.Text = "" + m_3; // Вывод m
                txt_d_3.Text = "" + d_3; // Вывод d
                txt_e_3.Text = "" + e_3; // Вывод e
                txt_open_key_3.Text = "(" + e_3 + "; " + n_3 + ")"; // Вывод открытого ключа
                txt_secret_key_3.Text = "(" + d_3 + "; " + n_3 + ")"; // Вывод закрытого ключа
            Finish:;
            }
        }

        // Зашифровать сообщение
        private void btn_encrypt_3_Click(object sender, EventArgs e)
        {
            // Clear_Message(); // Очистка
            if (n_3 < 0)
                MessageBox.Show("Число n должно быть больше 255 для корректной работы алгоритма!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (txt_full_name_3.Text == "") // Если сообщение не введено
                MessageBox.Show("Сообщение пустое. Введите сообщение", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                BigInteger a;
                string txt = txt_full_name_3.Text;
                a = textincode(txt);
                //txt_encrypt_3.Text = a.ToString(); // Вывод зашифрованного значения 22 01 15
                //a = Math.Abs(a);
                int length = a.ToString().Length;
                byte[] codeText = new byte[length/2];
                for (int i = length/2-1; i >= 0; i--)
                {
                    codeText[i] = (byte)(a % 100);
                    a /= 100;
                }

               // Перевод символов в числа
                foreach (byte temp in codeText)
                { // Пока есть числа
                    BigInteger M = new BigInteger(temp); // Создание переменной для больших значений вычислений
                    M = BigInteger.Pow(M, e_3); //находим M^e
                    M %= n_3; //находим M^e mod n
                    txt_encrypt_3.Text += M + ","; // Вывод зашифрованного значения
                }
                txt_encrypt_3.Text = txt_encrypt_3.Text.Substring(0, txt_encrypt_3.Text.Length - 1); // Удаление лишнего пробела
            }
        }

            // Расшифровать сообщение
            private void btn_decrypt_3_Click(object sender, EventArgs e)
        {
            txt_decrypt_3.Text = ""; // Очистка текстбокса декодированного сообщения
            string[] Message = txt_encrypt_3.Text.Replace(" ", "").Split(','); // Считывание строки без пробелов
            byte[] res = new byte[Message.Length]; // Задание длины
            for (int i = 0; i < Message.Length; i++)
            { // Пока строка не закончилась
                BigInteger C = new BigInteger(Convert.ToInt32(Message[i])); // Создание переменной для больших значений вычислений
                C = BigInteger.Pow(C, d_3); //находим (M^e)^d
                C %= n_3; //находим (M^e)^d mod n
                res[i] = Convert.ToByte(C.ToString()); // Запись расшифрованного значения
            }
            for (int i = 0; i < res.Length; i++)
            {
                txt_decrypt_3.Text += (res[i].ToString())+",";

            }
            txt_decrypt_3.Text = txt_decrypt_3.Text.Substring(0, txt_decrypt_3.Text.Length - 1); // Удаление лишнего пробела
                                                                                                 // Перевод чисел в символы и их вывод
        }

        void clear_3()
        {
            txt_n_3.Text = "";
            txt_m_3.Text = "";
            txt_d_3.Text = "";
            txt_e_3.Text = "";
            txt_full_name_3.Text = "";
            txt_encrypt_3.Text = "";
            txt_decrypt_3.Text = "";
            txt_open_key_3.Text = "";
            txt_secret_key_3.Text = "";
        }

        // Защита 
        private void txt_p_3_KeyPress(object sender, KeyPressEventArgs e)
        {
            number_protection(e);
        }

        private void txt_q_3_KeyPress(object sender, KeyPressEventArgs e)
        {
            number_protection(e);
        }

        private void txt_full_name_3_KeyPress(object sender, KeyPressEventArgs e)
        {
            symbol_protection(e);
        }

        private void txt_first_subkey_1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_full_name_3_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_L0_1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gp_output_1_Enter(object sender, EventArgs e)
        {

        }

        private void lbl_output_9_1_Click(object sender, EventArgs e)
        {

        }

        private void lbl_output_10_1_Click(object sender, EventArgs e)
        {

        }

        private void txt_hash_5_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_ecp_s_5_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_message_5_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_H0_5_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_H0_4_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_X0_56_1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_X0_48_1_TextChanged(object sender, EventArgs e)
        {

        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///////                    Задача №4: Функция хеширования                   ///////
        ///////////////////////////////////////////////////////////////////////////////////
        private void btn_run_4_Click(object sender, EventArgs e)
        {
            clear_4();
            if (Check_Prime_Number(p_4, q_4, prime_4, txt_p_4, txt_q_4) == true)
            { // Проверка p и q
                p_4 = Convert.ToInt32(txt_p_4.Text); // Ввод числа p
                q_4 = Convert.ToInt32(txt_q_4.Text); // Ввод числа q
                n_4 = p_4 * q_4; // Вычисление n
                txt_n_4.Text = "" + n_4; // Вывод n
            }

            if (txt_H0_4.Text == "") // Если H0 не введено
                MessageBox.Show("Вектор инициализации H0 не введен.\r\nВведите вектор инициализации H0!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (txt_message_4.Text == "") // Если сообщение не введено
                MessageBox.Show("Сообщение не введено. Введите сообщение!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                BigInteger a;
                string txt = txt_message_4.Text;
                a = textincode(txt);
                //txt_encrypt_3.Text = a.ToString(); // Вывод зашифрованного значения 22 01 15
                //a = Math.Abs(a);
                int length = a.ToString().Length;
                byte[] M = new byte[length / 2];
                for (int i1 = length / 2 - 1; i1 >= 0; i1--)
                {
                    M[i1] = (byte)(a % 100);
                    a /= 100;
                };
                //byte[] M = Encoding.GetEncoding(1251).GetBytes(txt_message_4.Text); // Перевод символов в числа
                BigInteger[] H = new BigInteger[txt_message_4.Text.Length + 1]; // Определение кол-ва букв
                H[0] = Convert.ToInt32(txt_H0_4.Text); // Ввод H0
                int i;
                for (i = 1; i < txt_message_4.Text.Length + 1; i++)
                {
                    H[i] = BigInteger.Pow(H[i - 1] + M[i - 1], 2) % n_4; // Вычисление Hi = (Hi-1 + Mi)^2 mod n
                    txt_result_4.Text += String.Format("H{0} = (H{1} + M{0})^2 mod n = ({2} + {3})^2 mod {4} = {5}\r\n", i, i - 1, H[i - 1], M[i - 1], n_4, H[i]);
                }
                txt_hash_4.Text = "" + H[i - 1]; // Вывод хэш-образа
                hash = H[i - 1];
                txt_message_5.Text = txt_message_4.Text;
                txt_H0_5.Text = txt_H0_4.Text;
            }

        }

        void clear_4()
        {
            txt_hash_4.Text = "";
            txt_result_4.Text = "";
        }

        // Защита 
        private void txt_p_4_KeyPress(object sender, KeyPressEventArgs e)
        {
            number_protection(e);
        }

        private void txt_q_4_KeyPress(object sender, KeyPressEventArgs e)
        {
            number_protection(e);
        }

        private void txt_H0_4_KeyPress(object sender, KeyPressEventArgs e)
        {
            number_protection(e);
        }

        private void txt_message_4_KeyPress(object sender, KeyPressEventArgs e)
        {
            symbol_protection(e);
        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///////                Задача №5: Электронная цифровая подпись              ///////
        ///////////////////////////////////////////////////////////////////////////////////
        private void btn_gen_keys_5_Click(object sender, EventArgs e)
        {
            clear_5();
            if (Check_Prime_Number(p_5, q_5, prime_5, txt_p_5, txt_q_5) == true)
            { // Проверка p и q
                p_5 = Convert.ToInt32(txt_p_5.Text); // Ввод числа p
                q_5 = Convert.ToInt32(txt_q_5.Text); // Ввод числа q
                n_5 = p_5 * q_5; // Вычисление n
                m_5 = (p_5 - 1) * (q_5 - 1); // Вычисление m
                // Вычисление d
                d_5 = -1;
                for (int i = 2; d_5 < 0; i++)
                { // Пока минимальное подходящее число d не найдено
                    if (NOD(i, m_5) == 1 && i < m_5 && Prime(i) == true) // Если число взаимно простое с m, меньше m и простое
                        d_5 = i; //число d найдено
                    else if (i > m_5)
                    { // Если число больше m
                        MessageBox.Show("Число m маленькое, невозможно вычислить число d!\r\nВведите другие числа p и q!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_p_5.Text = "";
                        txt_q_5.Text = "";
                        goto Finish; // Закончить вычисление ключей
                    }
                }
                // Вычисление e
                e_5 = 1;
                while (true)
                { // Пока число не будет найдено
                    if ((d_5 * e_5) % m_5 == 1) // Если число удовлетворяет условию
                        break; // Закончить поиск
                    else
                        e_5++; // Следующее число
                }
                txt_open_key_5.Text = "(" + e_5 + "; " + n_5 + ")"; // Вывод открытого ключа
                txt_secret_key_5.Text = "(" + d_5 + "; " + n_5 + ")"; // Вывод закрытого ключа
            Finish:;
            }
        }

        private void btn_run_5_Click(object sender, EventArgs e)
        {
            if (txt_H0_5.Text == "") // Если H0 не введено
                MessageBox.Show("Вектор инициализации H0 не введен.\r\nВведите вектор инициализации H0!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (txt_message_5.Text == "") // Если сообщение не введено
                MessageBox.Show("Сообщение не введено. Введите сообщение", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
 
                r_5 = hash; // Значение хэш-образа
                txt_hash_5.Text = "" + r_5; // Вывод хэш-образа
                S_5 = BigInteger.Pow(r_5, d_5) % n_5; // Вычисление s = r^d mod n
                txt_ecp_s_5.Text = "" + S_5; // Вывод ЭЦП
            }
        }

        private void btn_check_ecp_5_Click(object sender, EventArgs e)
        {
            txt_ecp_r_5.Text = "";
            R_5 = BigInteger.Pow(S_5, e_5) % n_5; // Вычисление r' = s^e mod n
            txt_ecp_r_5.Text = "" + R_5; // Вывод r'
            if (txt_ecp_r_5.Text == txt_hash_5.Text)
                btn_check_ecp_5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)192))));
            else
                btn_check_ecp_5.BackColor = System.Drawing.Color.FromArgb((int)(((byte)(255))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
        }

        void clear_5()
        {
            txt_open_key_5.Text = "";
            txt_secret_key_5.Text = "";
            txt_H0_5.Text = "";
            txt_message_5.Text = "";
            txt_hash_5.Text = "";
            txt_ecp_s_5.Text = "";
            txt_ecp_r_5.Text = "";
        }

        // Защита 
        private void txt_p_5_KeyPress(object sender, KeyPressEventArgs e)
        {
            number_protection(e);
        }

        private void txt_q_5_KeyPress(object sender, KeyPressEventArgs e)
        {
            number_protection(e);
        }

        private void txt_H0_5_KeyPress(object sender, KeyPressEventArgs e)
        {
            number_protection(e);
        }

        private void txt_message_5_KeyPress(object sender, KeyPressEventArgs e)
        {
            symbol_protection(e);
        }
    }
}