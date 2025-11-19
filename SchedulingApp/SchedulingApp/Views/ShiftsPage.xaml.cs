namespace SchedulingApp.Views;
using SchedulingApp.Models;

public partial class ShiftsPage : ContentPage
{
	/*
	 * Author: Alex Sorichetti
	 * Date: Nov 19, 2025
	 * Desc: Backend for the shifts page that allows to browse and sign up for available shifts
	 */
	private List<Shift> _allShifts;
	private List<Shift> _filteredShifts;
	public ShiftsPage()
	{
		InitializeComponent();
		LoadShifts();
	}

	//Function to load all available shifts
	private void LoadShifts()
	{
		//Sample data -- Generated via Claude AI
		_allShifts = new List<Shift>
		{
			new Shift
			{
				Id = 1,
				Title = "Food Bank - Sorting & Packing",
				Description = "Help sort and pack food donations for families in need",
				StartTime = DateTime.Now.AddDays(2),
				EndTime = DateTime.Now.AddDays(2).AddHours(3),
				Location = "Community Center - 123 Main St",
				SlotsAvailable = 5,
				SlotsTotal = 10,
				OrganizationName = "City Food Bank"
			},
			new Shift
			{
				Id = 2,
				Title = "Park Cleanup",
				Description = "Join us for a community park cleanup. Gloves and supplies provided.",
				StartTime = DateTime.Now.AddDays(5),
				EndTime = DateTime.Now.AddDays(5).AddHours(2),
				Location = "Central Park",
				SlotsAvailable = 12,
				SlotsTotal = 15,
				OrganizationName = "Green City Initiative"
			},
			new Shift
			{
				Id = 3,
				Title = "Tutoring Session - Math",
				Description = "Tutor middle school students in mathematics. Teaching experience preferred, but not required.",
				StartTime = DateTime.Now.AddDays(3),
				EndTime = DateTime.Now.AddDays(3).AddHours(1.5),
				Location = "Library -- Room 3",
				SlotsAvailable = 2,
				SlotsTotal = 5,
				OrganizationName = "Youth Education Program"
			},
			new Shift
			{
				Id = 4,
				Title = "Senior Center - Bingo Night",
				Description = "Help facilitate bingo night at the senior center. Bring your enthusiasm!",
				StartTime= DateTime.Now.AddDays(6).Date.AddHours(14),
				EndTime = DateTime.Now.AddDays(6).Date.AddHours(17),
				Location = "Golden Years Senior Center",
				SlotsAvailable = 1,
				SlotsTotal=6,
				OrganizationName = "Elder Care Services"
			},
             new Shift
            {
                Id = 5,
                Title = "Animal Shelter - Dog Walking",
                Description = "Walk and socialize dogs at our animal shelter. Dog handling experience helpful.",
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(1).AddHours(2),
                Location = "Happy Paws Animal Shelter",
                SlotsAvailable = 8,
                SlotsTotal = 10,
                OrganizationName = "Happy Paws"
            },
            new Shift
            {
                Id = 6,
                Title = "Beach Cleanup",
                Description = "Help keep our beaches clean! Bags and gloves provided.",
                StartTime = DateTime.Now.AddDays(7),
                EndTime = DateTime.Now.AddDays(7).AddHours(3),
                Location = "Sunset Beach",
                SlotsAvailable = 20,
                SlotsTotal = 25,
                OrganizationName = "Ocean Guardians"
            }
        };

		_filteredShifts = new List<Shift>(_allShifts);
		ShiftsCollection.ItemsSource = _filteredShifts;
	}
	//Updates Button styling to show active filter
	private void SetActiveFilter(Button activeButton)
	{
		//Reset all buttons
        AllFilterButton.BackgroundColor = Color.FromArgb("#E0E0E0");
        AllFilterButton.TextColor = Color.FromArgb("#4F4F4F");
        WeekFilterButton.BackgroundColor = Color.FromArgb("#E0E0E0");
        WeekFilterButton.TextColor = Color.FromArgb("#4F4F4F");
        WeekendFilterButton.BackgroundColor = Color.FromArgb("#E0E0E0");
        WeekendFilterButton.TextColor = Color.FromArgb("#4F4F4F");

        // Set active button
        activeButton.BackgroundColor = Color.FromArgb("#2196F3");
        activeButton.TextColor = Colors.White;
    }
	//Filter for Show All Shifts
	private void OnAllFilterClicked(object sender, EventArgs e)
	{
		SetActiveFilter(AllFilterButton);
		_filteredShifts = new List<Shift>(_allShifts);
		ShiftsCollection.ItemsSource = _filteredShifts;
	}

	//Filter: Show only shifts this week
	private void OnWeekFilterClicked(object sender, EventArgs e)
	{ 
		SetActiveFilter(WeekFilterButton);
		var endOfWeek = DateTime.Now.AddDays(7);
		_filteredShifts = _allShifts.Where(s => s.StartTime <= endOfWeek).ToList();
		ShiftsCollection.ItemsSource = _filteredShifts;
	}

	//Filter: Show only weekend shifts
	private void OnWeekendFilterClicked(object sender, EventArgs e)
	{
		SetActiveFilter(WeekendFilterButton);
		_filteredShifts = _allShifts.Where(s => s.StartTime.DayOfWeek == DayOfWeek.Sunday ||
				s.StartTime.DayOfWeek == DayOfWeek.Saturday).ToList();
		ShiftsCollection.ItemsSource = _filteredShifts;
	}

	//Handle search bar text changes
	private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
	{
		var searchText = e.NewTextValue?.ToLower() ?? "";
		if (string.IsNullOrEmpty(searchText)) 
		{
			ShiftsCollection.ItemsSource = _filteredShifts;
		} 
		else
		{
			var filtered = _filteredShifts.Where(s => s.Title.ToLower().Contains(searchText) ||
				s.OrganizationName.ToLower().Contains(searchText) ||
				s.Location.ToLower().Contains(searchText) ||
				s.Description.ToLower().Contains(searchText)).ToList();

			ShiftsCollection.ItemsSource = filtered;
		}
	}

	//Handles search button press
	private void OnSearchButtonPressed(object sender, EventArgs e)
	{
		ShiftSearchBar.Unfocus();
	}

	//Handles sign up button click

	private async void OnSignUpClicked(object sender, EventArgs e)
	{
		var button = sender as Button;
		var shift = button?.BindingContext as Shift;
		if (shift != null)
		{
			bool answer = await DisplayAlert(
				"Confirm Sign Up",
				$"Sign up for:\n\n{shift.Title}\n{shift.StartTime:MMM dd, yyy - hh:mm tt}\n{shift.Location}",
				"Yes, Sign me up!",
				"Cancel");

			if (answer)
			{
				//In making actually functional, would need to save information to databse or API,
				//but for example usage, this format works
				shift.SlotsAvailable--;

				await DisplayAlert(
					"Success!",
					$"You're signed up for {shift.Title}!\n\nCheck 'My Schedule' to see your upcoming shifts.",
					"OK");

				//Refresh list
				ShiftsCollection.ItemsSource = null;
				ShiftsCollection.ItemsSource = _filteredShifts;
			}	
		}
	}
}