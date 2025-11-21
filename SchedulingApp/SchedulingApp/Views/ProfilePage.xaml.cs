namespace SchedulingApp.Views;
/*
 * Author: Alex Sorichetti
 * Date: NOV 21 2025
 * Desc: Backing connections for the profile and settings page
 */
public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
        LoadUserProfile();
    }

    //Loads user profile data
    private void LoadUserProfile()
    {
        //For functional builidng this would use an API or database, but for this purpose it uses prebuilt data
        UserFullNameLabel.Text = "Alex Sorichetti";
        UserEmailLabel.Text = "asori@email.com";
        FirstNameEntry.Text = "Alex";
        LastNameEntry.Text = "Sorichetti";
        EmailEntry.Text = "asori@email.com";
        PhoneEntry.Text = "(555) 123-4567"; //not a real phone number

        //Stats
        TotalShiftsLabel.Text = "15";
        TotalHoursLabel.Text = "42";
        OrganizationsLabel.Text = "5";

        //Preferences
        NotificationsSwitch.IsToggled = true;
        EmailNotificationsSwitch.IsToggled = true;
        CalendarSyncSwitch.IsToggled = false;
    }

    //Saves profile changes
    private async void OnSaveChangesClicked(object sender, EventArgs e)
    {
        //Validate inputs
        if (string.IsNullOrWhiteSpace(FirstNameEntry.Text) ||
            string.IsNullOrWhiteSpace(LastNameEntry.Text))
        {
            await DisplayAlert("Error", "First and Last name are required.", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(EmailEntry.Text) || !EmailEntry.Text.Contains("@"))
        {
            await DisplayAlert("Error", "Please enter a valid email address.", "OK");
            return;
        }

        // In a real app, save to database/API -- this is merely a sample
        UserFullNameLabel.Text = $"{FirstNameEntry.Text} {LastNameEntry.Text}";
        UserEmailLabel.Text = EmailEntry.Text;

        await DisplayAlert("Success", "Your profile has been updated!", "OK");
    }

    //Opens change password dialog
    private async void OnChangePasswordClicked(object sender, EventArgs e)
    {
        string currentPassword = await DisplayPromptAsync(
            "Change Password",
            "Enter your current password:",
            "Next",
            "Cancel",
            keyboard: Keyboard.Default,
            maxLength: 50);

        if (string.IsNullOrWhiteSpace(currentPassword))
            return;

        string newPassword = await DisplayPromptAsync(
            "Change Password",
            "Enter your new password:",
            "Next",
            "Cancel",
            keyboard: Keyboard.Default,
            maxLength: 50);

        if (string.IsNullOrWhiteSpace(newPassword))
            return;

        string confirmPassword = await DisplayPromptAsync(
            "Change Password",
            "Confirm your new password:",
            "Change Password",
            "Cancel",
            keyboard: Keyboard.Default,
            maxLength: 50);

        if (newPassword != confirmPassword)
        {
            await DisplayAlert("Error", "Passwords do not match!", "OK");
            return;
        }

        if (newPassword.Length < 6)
        {
            await DisplayAlert("Error", "Password must be at least 6 characters long.", "OK");
            return;
        }

        // In a real app, validate current password and save new one
        await DisplayAlert("Success", "Your password has been changed!", "OK");
    }

    //User log out
    private async void OnLogOutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert(
            "Log Out",
            "Are you sure you want to log out?",
            "Yes, Log Out",
            "Cancel");

        if (confirm)
        {
            // In a real app, clear authentication tokens and user data
            await DisplayAlert("Logged Out", "You have been successfully logged out.", "OK");
            //In a real application this would lead back to a login page.
        }

    }
}
