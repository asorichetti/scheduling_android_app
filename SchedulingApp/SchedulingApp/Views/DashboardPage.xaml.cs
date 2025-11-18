using SchedulingApp.Models;

namespace SchedulingApp.Views;

public partial class DashboardPage : ContentPage
{
	/*
	 * Author: Alex Sorichetti
	 * Date: Nov 18, 2025
	 * Desc: Back-end logic code for the Dashboard page
	 */
	public DashboardPage()
	{
		InitializeComponent();
		LoadDashboardData();
	}

	//Loads dashboard data including user info and upcoming shifts
	private void LoadDashboardData()
	{
		UserNameLabel.Text = "Alex Sorichetti";
		UpcomingCountLabel.Text = "3";
		HoursLabel.Text = "12";

		//Sample of upcoming shifts - Note, the data for these samples came from AI generation on Claude
		var upcomingShifts = new List<Shift>
		{
			new Shift
			{
				Id = 1,
				Title = "Food Bank - Sorting",
				StartTime = DateTime.Now.AddDays(2),
				EndTime = DateTime.Now.AddDays(2).AddHours(3),
				Location = "Community Center",
				OrganizationName = "City Food Bank"
			},
			new Shift {
				Id = 2,
				Title = "Park Cleanup",
				StartTime = DateTime.Now.AddDays(5),
				EndTime = DateTime.Now.AddDays(5).AddHours(2),
				Location = "Central Park",
				OrganizationName = "Green City Initiative"
			},
			new Shift
			{
				Id = 3,
				Title = "Tutoring Session",
				StartTime = DateTime.Now.AddDays(7),
				EndTime = DateTime.Now.AddDays(7).AddHours(1.5),
				Location = "Library - Room 3",
				OrganizationName = "Youth Education Program"
			}
		};
		UpcomingShiftsCollection.ItemsSource = upcomingShifts;
	}

	//Handles Browse Shift button click
	private async void OnBrowseShiftsClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//ShiftsPage");
	}

	//Handles the Details button click for individual shifts
	private async void OnShiftDetailsClicked(object sender, EventArgs e)
	{
		var button = sender as Button;
		var shift = button?.BindingContext as Shift;

		if (shift != null)
		{
			await DisplayAlert("Shift Details",
				$"Title: {shift.Title}\n\n" +
				$"Organization: {shift.OrganizationName}\n" +
				$"Date: {shift.StartTime:MMM dd, yyyy}\n" +
				$"Time: {shift.StartTime:hh:mm tt} - {shift.EndTime:hh:mm tt}\n" +
				$"Location: {shift.Location}\n\n" +
				$"Duration: {(shift.EndTime - shift.StartTime).TotalHours} hours",
				"Close");
		}
	}
}