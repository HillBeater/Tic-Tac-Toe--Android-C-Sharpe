using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.Content;
using System;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using AlertDialog = Android.App.AlertDialog;

namespace TicTacToe
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, View.IOnClickListener 
    {

        Button btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btnRstGme, btnNewGme;
        TextView headerText, tvO, tvX, tvScoreO, tvScoreX;

        int PlayerO = 0;
        int Player_X = 1;

        int activePlayer = 0 ;

        int[] filledPosition = { -1, -1, -1, -1, -1, -1, -1, -1, -1 };

        bool isGameActive = true;

        int OCounter = 0;
        int XCounter = 0;
        private Context context;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


            btn0 = FindViewById<Button>(Resource.Id.buttonB0);
            btn1 = FindViewById<Button>(Resource.Id.buttonB1);
            btn2 = FindViewById<Button>(Resource.Id.buttonB2);
            btn3 = FindViewById<Button>(Resource.Id.buttonB3);
            btn4 = FindViewById<Button>(Resource.Id.buttonB4);
            btn5 = FindViewById<Button>(Resource.Id.buttonB5);
            btn6 = FindViewById<Button>(Resource.Id.buttonB6);
            btn7 = FindViewById<Button>(Resource.Id.buttonB7);
            btn8 = FindViewById<Button>(Resource.Id.buttonB8);

            btnRstGme = FindViewById<Button>(Resource.Id.buttonRstGme);
            btnNewGme = FindViewById<Button>(Resource.Id.buttonNewGame);

            btn0.SetOnClickListener(this);
            btn1.SetOnClickListener(this);
            btn2.SetOnClickListener(this);
            btn3.SetOnClickListener(this);
            btn4.SetOnClickListener(this);
            btn5.SetOnClickListener(this);
            btn6.SetOnClickListener(this);
            btn7.SetOnClickListener(this);
            btn8.SetOnClickListener(this);

            headerText = FindViewById<TextView>(Resource.Id.header_text);
            tvO = FindViewById<TextView>(Resource.Id.tvO);
            tvX = FindViewById<TextView>(Resource.Id.tvX);
            tvScoreX = FindViewById<TextView>(Resource.Id.XScore);
            tvScoreO = FindViewById<TextView>(Resource.Id.OScore);

            headerText.Text = "Player O Turn";
            headerText.SetTextColor(Resources.GetColor(Resource.Color.teal_700));

            btnRstGme.Click += delegate
            {
                ContinueGame();
            };

            btnNewGme.Click += delegate
            {
                newGameDialog(v:"Are you sure?");
            };
        }

  
        public void OnClick(View view)
        {


            if (!isGameActive)
            {
                return;
            }

            Button clickedBtn = FindViewById<Button>(view.Id);
            int clickedTag = int.Parse(view.Tag.ToString());

            if (filledPosition[clickedTag] != -1)
            {
                return;
            }

            filledPosition[clickedTag] = activePlayer;

            if (activePlayer == 0)
            {
                clickedBtn.Text = "O";
                clickedBtn.SetTextColor(Resources.GetColor(Resource.Color.teal_700));

                activePlayer = Player_X;
                headerText.Text = "Player X Turn";
                headerText.SetTextColor(Resources.GetColor(Resource.Color.teal_200));

            }
            else
            {
                clickedBtn.Text = "X";
                clickedBtn.SetTextColor(Resources.GetColor(Resource.Color.teal_200));
                activePlayer = 0;
                headerText.Text = "Player O Turn";
                headerText.SetTextColor(Resources.GetColor(Resource.Color.teal_700));
            }


            checkWinner();
        }

        private void checkWinner()
        {
            int[,] winningPositions = { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };

            for (int i = 0; i < 8; i++)
            {
                int val0 = winningPositions[i,0];
                int val1 = winningPositions[i,1];
                int val2 = winningPositions[i,2];

                if (filledPosition[val0] == filledPosition[val1] && filledPosition[val1] == filledPosition[val2])
                {
                    if (filledPosition[val0] != -1)
                    {
                        isGameActive = false;

                        if (filledPosition[val0] == 0)
                        {
                            OCounter++;
                            tvScoreO.Text =OCounter.ToString();
                            tvO.Visibility = Android.Views.ViewStates.Visible;
                            showDialog(v:"Player O is winner.");
                        }
                        else
                        {
                            XCounter++;
                            tvScoreX.Text = XCounter.ToString();
                            tvX.Visibility = Android.Views.ViewStates.Visible;
                            showDialog(v:"Player X is winner.");

                        }
                    }

                }
            }
        }

        private void showDialog(string v)
        {
            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle(v);
            alert.SetButton("Continue", (c, ev) =>
            {
                ContinueGame();
            });
            alert.SetButton2("No", (c, ev) => {
                NewGame();
            });
            alert.Show();
        }

        private void newGameDialog(string v)
        {
            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle(v);
            alert.SetButton("OK", (c, ev) =>
            {
                NewGame();
            });
            alert.SetButton2("CANCEL", (c, ev) => { });
            alert.Show();
        }

        private void ContinueGame()
            {
                tvO.Visibility = Android.Views.ViewStates.Invisible;
                tvX.Visibility = Android.Views.ViewStates.Invisible;
                headerText.Text = "Player O Turn";
                headerText.SetTextColor(Resources.GetColor(Resource.Color.teal_700));
                activePlayer = 0;
                filledPosition = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 };

                btn0.Text = "";
                btn1.Text = "";
                btn2.Text = "";
                btn3.Text = "";
                btn4.Text = "";
                btn5.Text = "";
                btn6.Text = "";
                btn7.Text = "";
                btn8.Text = "";

                isGameActive = true;
            }

        private void NewGame()
        {
            XCounter = 0;
            OCounter = 0;
            tvScoreO.Text = OCounter.ToString();
            tvScoreX.Text = XCounter.ToString();
            tvO.Visibility = Android.Views.ViewStates.Invisible;
            tvX.Visibility = Android.Views.ViewStates.Invisible;
            headerText.Text = "Player O Turn";
            headerText.SetTextColor(Resources.GetColor(Resource.Color.teal_700));
            activePlayer = 0;
            filledPosition = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 };

            btn0.Text = "";
            btn1.Text = "";
            btn2.Text = "";
            btn3.Text = "";
            btn4.Text = "";
            btn5.Text = "";
            btn6.Text = "";
            btn7.Text = "";
            btn8.Text = "";

            isGameActive = true;
        }
    }
    }


    