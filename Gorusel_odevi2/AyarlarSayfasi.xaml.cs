namespace Gorusel_odevi2;

public partial class AyarlarSayfasi : ContentPage
{
	public AyarlarSayfasi()
	{
		InitializeComponent();
	}

    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        if (e.Value)
            Application.Current.UserAppTheme = AppTheme.Light;
        else
            Application.Current.UserAppTheme = AppTheme.Dark;

    }
}