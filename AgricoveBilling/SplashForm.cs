//code by Magnus Johansson
using System.Windows.Forms;

namespace AgricoveBilling
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent ();
        }

        delegate void SetTextCallback( string text );

        public void SetText( string text )                                  //This updates the loading status
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if ( this.loadinglabel.InvokeRequired )
            {
                SetTextCallback d = new SetTextCallback ( SetText );
                this.Invoke ( d , new object [ ] { text } );
            }
            else
            {
                this.loadinglabel.Text = text;
            }
        }

        private void loadinglabel_TextChanged( object sender , System.EventArgs e )
        {
            if(loadinglabel.Text == "Done")
            {
                this.Close ();                      //this is how I close this thread from main
            }
        }
    }
}
