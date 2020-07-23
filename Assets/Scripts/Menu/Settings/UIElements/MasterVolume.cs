namespace Labyrinth.Menu.Settings.UIElements {

	public class MasterVolume : VolumeSettings {

		protected override void UpdateSetting(float volume) {
			Core.AudioManager.Instance.MasterVolume = volume;
		}

		private void Awake() {
			_slider.value = Core.AudioManager.Instance.MasterVolume;
		}

	}

}