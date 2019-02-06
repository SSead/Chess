using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Chess
{
    public partial class Form1 : Form
    {
        

        Image bishop_black;
        Image bishop_white;
        Image king_black;
        Image king_white;
        Image knight_black;
        Image knight_white;
        Image pawn_black;
        Image pawn_white;
        Image queen_black;
        Image queen_white;
        Image rook_black;
        Image rook_white;

        Image black_tile;
        Image white_tile;

        Image black_tile_select;
        Image white_tile_select;

        Image none;

        int mode;
        int selectedX;
        int selectedY;


        PictureBox[,] mat;
        public Form1()
        {
            InitializeComponent();

            mat = new PictureBox[8, 8] {
                {p00, p01, p02, p03, p04, p05, p06, p07 },
                {p10, p11, p12, p13, p14, p15, p16, p17 },
                {p20, p21, p22, p23, p24, p25, p26, p27 },
                {p30, p31, p32, p33, p34, p35, p36, p37 },
                {p40, p41, p42, p43, p44, p45, p46, p47 },
                {p50, p51, p52, p53, p54, p55, p56, p57 },
                {p60, p61, p62, p63, p64, p65, p66, p67 },
                {p70, p71, p72, p73, p74, p75, p76, p77 }
            };

            

            bishop_black = Chess.Properties.Resources.bishop_black;
            bishop_white = Chess.Properties.Resources.bishop_white;
            king_black = Chess.Properties.Resources.king_black;
            king_white = Chess.Properties.Resources.king_white;
            knight_black = Chess.Properties.Resources.knight_black;
            knight_white = Chess.Properties.Resources.knight_white;
            pawn_black = Chess.Properties.Resources.pawn_black;
            pawn_white = Chess.Properties.Resources.pawn_white;
            queen_black = Chess.Properties.Resources.queen_black;
            queen_white = Chess.Properties.Resources.queen_white;
            rook_black = Chess.Properties.Resources.rook_black;
            rook_white = Chess.Properties.Resources.rook_white;

            black_tile = Chess.Properties.Resources.black_tile;
            white_tile = Chess.Properties.Resources.white_tile;

            black_tile_select = Chess.Properties.Resources.black_tile_select;
            white_tile_select = Chess.Properties.Resources.white_tile_select;

            none = Chess.Properties.Resources.none;

            mode = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    mat[i, j].Image = none;
                    mat[i, j].BackgroundImage = ((i + (j % 2)) % 2 == 0 ? white_tile : black_tile);

                    mat[i, j].Click += pClick;
                }
            }

            mat[0, 0].Image = mat[0, 7].Image = rook_black;
            mat[0, 1].Image = mat[0, 6].Image = knight_black;
            mat[0, 2].Image = mat[0, 5].Image = bishop_black;
            mat[0, 3].Image = queen_black;
            mat[0, 4].Image = king_black;

            for (int i = 0; i < 8; i++)
            {
                mat[1, i].Image = pawn_black;
                mat[6, i].Image = pawn_white;
            }

            mat[7, 0].Image = mat[7, 7].Image = rook_white;
            mat[7, 1].Image = mat[7, 6].Image = knight_white;
            mat[7, 2].Image = mat[7, 5].Image = bishop_white;
            mat[7, 3].Image = queen_white;
            mat[7, 4].Image = king_white;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void pClick(object sender, EventArgs e)
        {
            PictureBox px = (PictureBox)sender;
            Console.WriteLine(px.Image);

            if (mode == 0)
            {
                if (px.Image != none)
                {
                    selectedX = px.Name[1] - 48;
                    selectedY = px.Name[2] - 48;

                    drawDots(px.Image);
                    mode = 1;
                }
            } else
            {
                if ((px.BackgroundImage == white_tile_select || px.BackgroundImage == black_tile_select) && !(px.Name[1] - 48 == selectedX && px.Name[2] - 48 == selectedY))
                {
                    px.Image = mat[selectedX, selectedY].Image;
                    mat[selectedX, selectedY].Image = none;

                    
                }

                clearDots();
                mode = 0;
            }
        }

        private void clearDots()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (mat[i, j].BackgroundImage == white_tile_select)
                    {
                        mat[i, j].BackgroundImage = white_tile;
                    }

                    if (mat[i, j].BackgroundImage == black_tile_select)
                    {
                        mat[i, j].BackgroundImage = black_tile;
                    }
                }
            }
        }

        private void drawDots(Image img)
        {
            if (img == rook_black || img == rook_white)
            {
                drawDotsRook();
            } else if (img == bishop_black || img == bishop_white)
            {
                drawDotsBishop();
            } else if (img == knight_black || img == knight_white)
            {
                drawDotsKnight();
            } else if (img == queen_black || img == queen_white)
            {
                drawDotsQueen();
            } else if (img == king_black || img == king_white)
            {
                drawDotsKing();
            } else if (img == pawn_black)
            {
                drawDotsPawnBlack();
            } else if (img == pawn_white)
            {
                drawDotsPawnWhite();
            }


            else
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        mat[i, j].BackgroundImage = (mat[i, j].BackgroundImage == black_tile ? black_tile_select : white_tile_select);
                   
                    }
                }
            }
        }

        private void drawDotsRook()
        {
            int i;
            for (i = selectedX + 1; i < 8 && mat[i, selectedY].Image == none; i++)
                mat[i, selectedY].BackgroundImage = (mat[i, selectedY].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

            if (i < 8 && !sameColor(mat[selectedX, selectedY].Image, mat[i, selectedY].Image))
                mat[i, selectedY].BackgroundImage = (mat[i, selectedY].BackgroundImage == white_tile ? white_tile_select : black_tile_select);


            for (i = selectedX - 1; i >= 0 && mat[i, selectedY].Image == none; i--)
                mat[i, selectedY].BackgroundImage = (mat[i, selectedY].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

            if (i >= 0 && !sameColor(mat[selectedX, selectedY].Image, mat[i, selectedY].Image))
                mat[i, selectedY].BackgroundImage = (mat[i, selectedY].BackgroundImage == white_tile ? white_tile_select : black_tile_select);


            for (i = selectedY + 1; i < 8 && mat[selectedX, i].Image == none; i++)
                mat[selectedX, i].BackgroundImage = (mat[selectedX, i].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

            if (i < 8 && !sameColor(mat[selectedX, selectedY].Image, mat[selectedX, i].Image))
                mat[selectedX, i].BackgroundImage = (mat[selectedX, i].BackgroundImage == white_tile ? white_tile_select : black_tile_select);


            for (i = selectedY - 1; i >= 0 && mat[selectedX, i].Image == none; i--)
                mat[selectedX, i].BackgroundImage = (mat[selectedX, i].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

            if (i >= 0 && !sameColor(mat[selectedX, selectedY].Image, mat[selectedX, i].Image))
                mat[selectedX, i].BackgroundImage = (mat[selectedX, i].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

        }

        private void drawDotsKnight()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (((Math.Abs(selectedX - i) == 2 && Math.Abs(selectedY - j) == 1)
                        || (Math.Abs(selectedX - i) == 1 && Math.Abs(selectedY - j) == 2))
                        && (mat[i, j].Image == none || !sameColor(mat[selectedX, selectedY].Image, mat[i, j].Image)))
                    {
                        mat[i, j].BackgroundImage = (mat[i, j].BackgroundImage == white_tile ? white_tile_select : black_tile_select);
                    }
                }
            }
        }
        private void drawDotsBishop()
        {
            int i, j;
            for (i = selectedX + 1, j = selectedY + 1; i < 8 && j < 8 && mat[i, j].Image == none; i++, j++)
                mat[i, j].BackgroundImage = (mat[i, j].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

            if (i < 8 && j < 8 && !sameColor(mat[selectedX, selectedY].Image, mat[i, j].Image))
                mat[i, j].BackgroundImage = (mat[i, j].BackgroundImage == white_tile ? white_tile_select : black_tile_select);


            for (i = selectedX + 1, j = selectedY - 1; i < 8 && j >= 0 && mat[i, j].Image == none; i++, j--)
                mat[i, j].BackgroundImage = (mat[i, j].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

            if (i < 8 && j >= 0 && !sameColor(mat[selectedX, selectedY].Image, mat[i, j].Image))
                mat[i, j].BackgroundImage = (mat[i, j].BackgroundImage == white_tile ? white_tile_select : black_tile_select);


            for (i = selectedX - 1, j = selectedY + 1; i >= 0 && j < 8 && mat[i, j].Image == none; i--, j++)
                mat[i, j].BackgroundImage = (mat[i, j].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

            if (i >= 0 && j < 8 && !sameColor(mat[selectedX, selectedY].Image, mat[i, j].Image))
                mat[i, j].BackgroundImage = (mat[i, j].BackgroundImage == white_tile ? white_tile_select : black_tile_select);


            for (i = selectedX - 1, j = selectedY - 1; i >= 0 && j >= 0 && mat[i, j].Image == none; i--, j--)
                mat[i, j].BackgroundImage = (mat[i, j].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

            if (i >= 0 && j >= 0 && !sameColor(mat[selectedX, selectedY].Image, mat[i, j].Image))
                mat[i, j].BackgroundImage = (mat[i, j].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

        }

        private void drawDotsQueen()
        {
            drawDotsBishop();
            drawDotsRook();
        }

        private void drawDotsKing()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Math.Abs(selectedX - i) <= 1 && Math.Abs(selectedY - j) <= 1 && !(i == selectedX && j == selectedY)
                        && (mat[i, j].Image == none || !sameColor(mat[selectedX, selectedY].Image, mat[i, j].Image)))

                    {
                        mat[i, j].BackgroundImage = (mat[i, j].BackgroundImage == white_tile ? white_tile_select : black_tile_select);
                    }
                }
            }
        }

        private void drawDotsPawnBlack()
        {
            if (selectedX == 1 && mat[3, selectedY].Image == none && mat[2, selectedY].Image == none)
                mat[3, selectedY].BackgroundImage = (mat[3, selectedY].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

            if (selectedX < 7 && mat[selectedX + 1, selectedY].Image == none)
                mat[selectedX + 1, selectedY].BackgroundImage = (mat[selectedX + 1, selectedY].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

            if (selectedX < 7 && selectedY < 7 && mat[selectedX + 1, selectedY + 1].Image != none && !sameColor(mat[selectedX, selectedY].Image, mat[selectedX + 1, selectedY + 1].Image))
                mat[selectedX + 1, selectedY + 1].BackgroundImage = (mat[selectedX + 1, selectedY + 1].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

            if (selectedX < 7 && selectedY > 0 && mat[selectedX + 1, selectedY - 1].Image != none && !sameColor(mat[selectedX, selectedY].Image, mat[selectedX + 1, selectedY - 1].Image))
                mat[selectedX + 1, selectedY - 1].BackgroundImage = (mat[selectedX + 1, selectedY - 1].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

        }

        private void drawDotsPawnWhite()
        {
            if (selectedX == 6 && mat[4, selectedY].Image == none && mat[5, selectedY].Image == none)
                mat[4, selectedY].BackgroundImage = (mat[4, selectedY].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

            if (selectedX > 0 && mat[selectedX - 1, selectedY].Image == none)
                mat[selectedX - 1, selectedY].BackgroundImage = (mat[selectedX - 1, selectedY].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

            if (selectedX > 0 && selectedY < 7 && mat[selectedX - 1, selectedY + 1].Image != none && !sameColor(mat[selectedX, selectedY].Image, mat[selectedX - 1, selectedY + 1].Image))
                mat[selectedX - 1, selectedY + 1].BackgroundImage = (mat[selectedX - 1, selectedY + 1].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

            if (selectedX > 0 && selectedY > 0 && mat[selectedX - 1, selectedY - 1].Image != none && !sameColor(mat[selectedX, selectedY].Image, mat[selectedX - 1, selectedY - 1].Image))
                mat[selectedX - 1, selectedY - 1].BackgroundImage = (mat[selectedX - 1, selectedY - 1].BackgroundImage == white_tile ? white_tile_select : black_tile_select);

        }
        private bool sameColor(Image a, Image b)
        {
            return ((a == rook_black || a == knight_black || a == bishop_black || a == queen_black || a == king_black || a == pawn_black)
                ^ (b == rook_white || b == knight_white || b == bishop_white || b == queen_white || b == king_white || b == pawn_white));
        }

        
    }  
}
