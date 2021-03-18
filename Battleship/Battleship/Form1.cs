using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Battleship
{
    public partial class PlayerMove_Lists : Form
    {

        List<Button> PlayerPos, EnemyPos;
        Random rand = new Random();
        int ships = 5, round = 15, score, enemyscore;
         
        public PlayerMove_Lists()
        {
            InitializeComponent();
            Restart();
        }

        private void Attack_Btn_Click(object sender, EventArgs e)
        {
            if(EnemyLocationList.Text != "")
            {
                var Target_Pos = EnemyLocationList.Text.ToLower();
                int Targer_index = EnemyPos.FindIndex(a => a.Name == Target_Pos);
                if(EnemyPos[Targer_index].Enabled && round > 0)
                {
                    round -= 1;
                    Rounds.Text = "Round:" + round;
                    if((string)EnemyPos[Targer_index].Tag == "enemyship")
                    {
                        EnemyPos[Targer_index].Enabled = false;
                        EnemyPos[Targer_index].BackColor = Color.Red;
                        score += 1;
                        Player_Txt.Text = score.ToString();
                        Enemy_Timer.Start();
                    }
                    else
                    {
                        EnemyPos[Targer_index].Enabled = false;
                        EnemyPos[Targer_index].BackColor = Color.Black;
                        Enemy_Timer.Start();
                    }
                }
            }
            else
            {
                MessageBox.Show("Choose Location from List","Hint");
            }
        }

        private void Enemy_Time_Event(object sender, EventArgs e)
        {
            if(PlayerPos.Count > 0 && round > 0)
            {
                round -= 1;
                Rounds.Text = "Round:" + round;
                int index = rand.Next(PlayerPos.Count);
                if ((string)PlayerPos[index].Tag == "playership")
                {
                    Enemy_Move.Text = PlayerPos[index].Text;
                    PlayerPos[index].Enabled = false;
                    PlayerPos[index].BackColor = Color.Black;
                    PlayerPos.RemoveAt(index);
                    enemyscore += 1;
                    Enemy_Txt.Text = enemyscore.ToString();
                    Enemy_Timer.Stop();
                }
                else
                {
                    Enemy_Move.Text = PlayerPos[index].Text;
                    PlayerPos[index].Enabled = false;
                    PlayerPos[index].BackColor = Color.Green;
                    PlayerPos.RemoveAt(index);
                    Enemy_Timer.Stop();
                }
            }
            if(round < 1 || enemyscore > 4 || score > 4)
            {
                if(score > enemyscore)
                {
                    MessageBox.Show("Match WON!", "Win");
                    Restart();
                }
                else if(enemyscore > score)
                {
                    MessageBox.Show("Match LOST!", "Loss");
                    Restart();
                }
                else if(enemyscore == score)
                {
                    MessageBox.Show("Match DRAW!", "Draw");
                    Restart();
                }
            }
           
        }

        private void PlayerPosEvent(object sender, EventArgs e)
        {
            if(ships > 0)
            {
                var button = (Button)sender;
                button.Enabled = false;
                button.Tag = "Player Ship";
                button.BackColor = Color.Orange;
                ships -= 1;

            }
            if(ships == 0)
            {
                Attack_btn.Enabled = true;
                Attack_btn.BackColor = Color.Red;
                Attack_btn.ForeColor = Color.White;
                Help_Txt.Text = "Pick Targets!";
            }
        }

        private void SetTarget()
        {
            for(int i = 0; i< 5; i++)
            {
                int index = rand.Next(EnemyPos.Count);
                if(EnemyPos[index].Enabled == true && (string)EnemyPos[index].Tag == null)
                {
                    EnemyPos[index].Tag = "Enemy Ship";
                    Debug.WriteLine("Enemy Pos:" + EnemyPos[index].Text);
                }
                else
                {
                    index = rand.Next(EnemyPos.Count);
                }
            }
        }

        private void Restart()
        {
            PlayerPos = new List<Button> { a1, a2, a3, a4, a5, b1, b2, b3, b4, b5, c1, c2, c3, c4, c5, d1, d2, d3, d4, d5, e1, e2, e3, e4, e5 };
            EnemyPos = new List<Button> { f1, f2, f3, f4, f5, g1, g2, g3, g4, g5, h1, h2, h3, h4, h5, i1, i2, i3, i4, i5, j1, j2, j3, j4, j5 };
            EnemyLocationList.Items.Clear();
            EnemyLocationList.Text = null;
            Help_Txt.Text = "Anchor 5 ships";
            for (int i = 0; i< EnemyPos.Count; i++)
            {
                EnemyPos[i].Enabled = true;
                EnemyPos[i].Tag = null;
                EnemyPos[i].BackColor = Color.YellowGreen;
                EnemyPos[i].BackgroundImage = null;
                EnemyLocationList.Items.Add(EnemyPos[i]);
            }
            for(int i = 0; i<PlayerPos.Count; i++)
            {
                PlayerPos[i].Enabled = true;
                PlayerPos[i].Tag = null;
                PlayerPos[i].BackColor = Color.White;
                PlayerPos[i].BackgroundImage = null;
            }
            score = 0;
            enemyscore = 0;
            round = 15;
            ships = 5;
            Player_Txt.Text = score.ToString();
            Enemy_Txt.Text = enemyscore.ToString();
            Enemy_Move.Text = "A1";
            Attack_btn.Enabled = false;
            SetTarget();
        }


    }
}
