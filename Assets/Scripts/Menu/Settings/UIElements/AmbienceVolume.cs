namespace Labyrinth.Menu.Settings.UIElements {

	public class AmbienceVolume : VolumeSettings {

		protected override void UpdateSetting(float volume) {
			Core.AudioManager.Instance.AmbienceVolume = volume;
		}

		private void Awake() {
			_slider.value = Core.AudioManager.Instance.AmbienceVolume;
		}

	}

}