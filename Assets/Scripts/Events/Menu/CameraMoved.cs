using Labyrinth.Menu;

namespace Labyrinth.Events.Menu {

    public class CameraMoved : GameEvent {

        public MenuState NewState {
            get;
        }

        public CameraMoved(MenuState state) {
            NewState = state;
        }

    }

}