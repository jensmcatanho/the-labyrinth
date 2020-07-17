using Labyrinth.Menu;

namespace Events.Menu {

    public class CameraMoved : GameEvent {

        public MenuState NewState {
            get;
        }

        public CameraMoved(MenuState state) {
            NewState = state;
        }

    }

}