namespace SchedulingApp
{
    /*
     * Author: Alex Sorichetti
     * Date: Nov 18, 2025
     * Desc: The main application entry point
     */
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //Set the main page to AppShell for navigation purposes.
            MainPage = new AppShell();
        }

        //Handle when the app is put to sleep
        protected override void OnSleep()
        {
            //Save state or perform cleanup
            base.OnSleep();
        }

        //Handle when app is resumed
        protected override void OnResume()
        {
            //Refresh data or restore state
            base.OnResume();
        }
    }
}